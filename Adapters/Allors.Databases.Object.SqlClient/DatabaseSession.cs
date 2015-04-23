//------------------------------------------------------------------------------------------------- 
// <copyright file="DatabaseSession.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
// 
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// 
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// <summary>Defines the Session type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Databases.Object.SqlClient
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;

    using Allors.Databases.Object.SqlClient.Caching;
    using Allors.Meta;

    internal sealed class DatabaseSession : IDatabaseSession
    {
        private static readonly ObjectId[] EmptyObjectIds = { };

        private readonly Database database;

        private SqlConnection connection;
        private SqlTransaction transaction;

        private ChangeSet changeSet;

        private Dictionary<Reference, Roles> modifiedRolesByReference;
        private Dictionary<Reference, Roles> unflushedRolesByReference;
        private Dictionary<IAssociationType, HashSet<ObjectId>> triggersFlushRolesByAssociationType;

        private Dictionary<ObjectId, Reference> referenceByObjectId;
        private HashSet<Reference> referencesWithoutCacheId;

        private Dictionary<IAssociationType, Dictionary<Reference, Reference>> associationByRoleByAssociationType;
        private Dictionary<IAssociationType, Dictionary<Reference, ObjectId[]>> associationsByRoleByAssociationType;

        private bool busyCommittingOrRollingBack;

        private Dictionary<string, object> properties;

        #region Commands
        private Dictionary<IClass, SqlCommand> getUnitRolesByClass;
        private Dictionary<IClass, Dictionary<IRoleType, SqlCommand>> setUnitRoleByRoleTypeByClass;
        private Dictionary<IClass, Dictionary<IList<IRoleType>, SqlCommand>> setUnitRolesByRoleTypeByClass;
        private Dictionary<IClass, SqlCommand> prefetchUnitRolesByClass;
        private Dictionary<IRoleType, SqlCommand> getCompositeRoleByRoleType;
        private Dictionary<IRoleType, SqlCommand> prefetchCompositeRoleByRoleType;
        private Dictionary<IRoleType, SqlCommand> setCompositeRoleByRoleType;
        private Dictionary<IRoleType, SqlCommand> getCompositesRoleByRoleType;
        private Dictionary<IRoleType, SqlCommand> prefetchCompositesRoleByRoleType;
        private Dictionary<IRoleType, SqlCommand> addCompositeRoleByRoleType;
        private Dictionary<IRoleType, SqlCommand> removeCompositeRoleByRoleType;
        private Dictionary<IRoleType, SqlCommand> clearCompositeAndCompositesRoleByRoleType;
        private Dictionary<IAssociationType, SqlCommand> getCompositeAssociationByAssociationType;
        private Dictionary<IAssociationType, SqlCommand> prefetchCompositeAssociationByAssociationType;
        private Dictionary<IAssociationType, SqlCommand> getCompositesAssociationByAssociationType;

        private SqlCommand instantiateObject;
        private SqlCommand instantiateObjects;
        private Dictionary<IClass, SqlCommand> createObjectByClass;
        private Dictionary<IClass, SqlCommand> createObjectsByClass;
        private Dictionary<IClass, SqlCommand> deleteObjectByClass;
        private Dictionary<IClass, SqlCommand> insertObjectByClass;

        private SqlCommand getCacheIds;
        private SqlCommand updateCacheIds;
        private SqlCommand getObjectType;
        #endregion

        internal DatabaseSession(Database database)
        {
            this.database = database;

            this.referenceByObjectId = new Dictionary<ObjectId, Reference>();
            this.referencesWithoutCacheId = new HashSet<Reference>();

            this.associationByRoleByAssociationType = new Dictionary<IAssociationType, Dictionary<Reference, Reference>>();
            this.associationsByRoleByAssociationType = new Dictionary<IAssociationType, Dictionary<Reference, ObjectId[]>>();

            this.changeSet = new ChangeSet();
        }

        IDatabase IDatabaseSession.Database
        {
            get { return this.database; }
        }

        public Database Database
        {
            get { return this.database; }
        }

        public IPopulation Population
        {
            get
            {
                return this.Database;
            }
        }

        internal ChangeSet ChangeSet
        {
            get
            {
                return this.changeSet;
            }
        }

        #region Commands Properties

        private Dictionary<IClass, SqlCommand> GetUnitRolesByClass
        {
            get
            {
                return this.getUnitRolesByClass ?? (this.getUnitRolesByClass = new Dictionary<IClass, SqlCommand>());
            }
        }

        private Dictionary<IClass, SqlCommand> PrefetchUnitRolesByClass
        {
            get
            {
                return this.prefetchUnitRolesByClass ?? (this.prefetchUnitRolesByClass = new Dictionary<IClass, SqlCommand>());
            }
        }
        
        private Dictionary<IClass, Dictionary<IRoleType, SqlCommand>> SetUnitRoleByRoleTypeByClass
        {
            get
            {
                return this.setUnitRoleByRoleTypeByClass ?? (this.setUnitRoleByRoleTypeByClass = new Dictionary<IClass, Dictionary<IRoleType, SqlCommand>>());
            }
        }

        private Dictionary<IClass, Dictionary<IList<IRoleType>, SqlCommand>> SetUnitRolesByRoleTypeByClass
        {
            get
            {
                return this.setUnitRolesByRoleTypeByClass ?? (this.setUnitRolesByRoleTypeByClass = new Dictionary<IClass, Dictionary<IList<IRoleType>, SqlCommand>>());
            }
        }

        private Dictionary<IRoleType, SqlCommand> GetCompositeRoleByRoleType
        {
            get
            {
                return this.getCompositeRoleByRoleType ?? (this.getCompositeRoleByRoleType = new Dictionary<IRoleType, SqlCommand>());
            }
        }

        private Dictionary<IRoleType, SqlCommand> PrefetchCompositeRoleByRoleType
        {
            get
            {
                return this.prefetchCompositeRoleByRoleType ?? (this.prefetchCompositeRoleByRoleType = new Dictionary<IRoleType, SqlCommand>());
            }
        }
        
        private Dictionary<IRoleType, SqlCommand> SetCompositeRoleByRoleType
        {
            get
            {
                return this.setCompositeRoleByRoleType ?? (this.setCompositeRoleByRoleType = new Dictionary<IRoleType, SqlCommand>());
            }
        }
        
        private Dictionary<IRoleType, SqlCommand> GetCompositesRoleByRoleType
        {
            get
            {
                return this.getCompositesRoleByRoleType ?? (this.getCompositesRoleByRoleType = new Dictionary<IRoleType, SqlCommand>());
            }
        }
        
        private Dictionary<IRoleType, SqlCommand> PrefetchCompositesRoleByRoleType
        {
            get
            {
                return this.prefetchCompositesRoleByRoleType ?? (this.prefetchCompositesRoleByRoleType = new Dictionary<IRoleType, SqlCommand>());
            }
        }
        
        private Dictionary<IRoleType, SqlCommand> AddCompositeRoleByRoleType
        {
            get
            {
                return this.addCompositeRoleByRoleType ?? (this.addCompositeRoleByRoleType = new Dictionary<IRoleType, SqlCommand>());
            }
        }
        
        private Dictionary<IRoleType, SqlCommand> RemoveCompositeRoleByRoleType
        {
            get
            {
                return this.removeCompositeRoleByRoleType ?? (this.removeCompositeRoleByRoleType = new Dictionary<IRoleType, SqlCommand>());
            }
        }
        
        private Dictionary<IRoleType, SqlCommand> ClearCompositeAndCompositesRoleByRoleType
        {
            get
            {
                return this.clearCompositeAndCompositesRoleByRoleType ?? (this.clearCompositeAndCompositesRoleByRoleType = new Dictionary<IRoleType, SqlCommand>());
            }
        }
        
        private Dictionary<IAssociationType, SqlCommand> GetCompositeAssociationByAssociationType
        {
            get
            {
                return this.getCompositeAssociationByAssociationType ?? (this.getCompositeAssociationByAssociationType = new Dictionary<IAssociationType, SqlCommand>());
            }
        }

        private Dictionary<IAssociationType, SqlCommand> PrefetchCompositeAssociationByAssociationType
        {
            get
            {
                return this.prefetchCompositeAssociationByAssociationType ?? (this.prefetchCompositeAssociationByAssociationType = new Dictionary<IAssociationType, SqlCommand>());
            }
        }

        private Dictionary<IAssociationType, SqlCommand> GetCompositesAssociationByAssociationType
        {
            get
            {
                return this.getCompositesAssociationByAssociationType ?? (this.getCompositesAssociationByAssociationType = new Dictionary<IAssociationType, SqlCommand>());
            }
        }

        private Dictionary<IClass, SqlCommand> CreateObjectByClass 
        {
            get
            {
                return this.createObjectByClass ?? (this.createObjectByClass = new Dictionary<IClass, SqlCommand>());
            }
        }

        private Dictionary<IClass, SqlCommand> CreateObjectsByClass
        {
            get
            {
                return this.createObjectsByClass ?? (this.createObjectsByClass = new Dictionary<IClass, SqlCommand>());
            }
        }

        private Dictionary<IClass, SqlCommand> DeleteObjectByClass
        {
            get
            {
                return this.deleteObjectByClass ?? (this.deleteObjectByClass = new Dictionary<IClass, SqlCommand>());
            }
        }

        private Dictionary<IClass, SqlCommand> InsertObjectByClass
        {
            get
            {
                return this.insertObjectByClass ?? (this.insertObjectByClass = new Dictionary<IClass, SqlCommand>());
            }
        }

        #endregion

        public object this[string name]
        {
            get
            {
                if (this.properties == null)
                {
                    return null;
                }

                object value;
                this.properties.TryGetValue(name, out value);
                return value;
            }

            set
            {
                if (this.properties == null)
                {
                    this.properties = new Dictionary<string, object>();
                }

                if (value == null)
                {
                    this.properties.Remove(name);
                }
                else
                {
                    this.properties[name] = value;
                }
            }
        }

        public IObject Create(IClass objectType)
        {
            if (!objectType.IsClass)
            {
                throw new ArgumentException("Can not create non concrete composite type " + objectType);
            }

            var reference = this.CreateObject(objectType);
            this.referenceByObjectId[reference.ObjectId] = reference;

            this.Database.Cache.SetObjectType(reference.ObjectId, objectType);

            this.changeSet.OnCreated(reference.ObjectId);

            return reference.Strategy.GetObject();
        }

        public IObject[] Create(IClass objectType, int count)
        {
            if (!objectType.IsClass)
            {
                throw new ArgumentException("Can not create non concrete composite type " + objectType);
            }

            var references = this.CreateObjects(objectType, count);

            var arrayType = this.Database.ObjectFactory.GetTypeForObjectType(objectType);
            var domainObjects = (IObject[])Array.CreateInstance(arrayType, count);

            for (var i = 0; i < references.Count; i++)
            {
                var reference = references[i];
                this.referenceByObjectId[reference.ObjectId] = reference;

                domainObjects[i] = reference.Strategy.GetObject();

                this.changeSet.OnCreated(reference.ObjectId);
            }

            return domainObjects;
        }

        public IObject Insert(IClass domainType, string objectIdString)
        {
            var objectId = this.Database.ObjectIds.Parse(objectIdString);
            var insertedObject = this.Insert(domainType, objectId);

            this.changeSet.OnCreated(objectId);

            return insertedObject;
        }

        public IObject Insert(IClass domainType, ObjectId objectId)
        {
            if (this.referenceByObjectId.ContainsKey(objectId))
            {
                var oldStrategy = this.referenceByObjectId[objectId].Strategy;
                if (!oldStrategy.IsDeleted)
                {
                    throw new Exception("Duplicate object id " + objectId);
                }
            }

            var reference = this.InsertObject(domainType, objectId);
            this.referenceByObjectId[objectId] = reference;
            var insertedObject = reference.Strategy.GetObject();

            this.changeSet.OnCreated(objectId);

            return insertedObject;
        }

        public IObject Instantiate(IObject obj)
        {
            return this.Instantiate(obj.Strategy.ObjectId);
        }

        public IObject Instantiate(string objectId)
        {
            var id = this.Database.ObjectIds.Parse(objectId);
            return this.Instantiate(id);
        }

        public IObject Instantiate(ObjectId objectId)
        {
            var strategy = this.InstantiateStrategy(objectId);
            if (strategy == null)
            {
                return null;
            }

            return strategy.GetObject();
        }

        public IStrategy InstantiateStrategy(ObjectId objectId)
        {
            if (objectId == null)
            {
                return null;
            }

            Reference reference;
            if (!this.referenceByObjectId.TryGetValue(objectId, out reference))
            {
                reference = this.InstantiateObject(objectId);
                if (reference != null)
                {
                    this.referenceByObjectId[objectId] = reference;
                }
            }

            if (reference == null || !reference.Exists)
            {
                return null;
            }

            return reference.Strategy;
        }

        public IObject[] Instantiate(string[] objectIdStrings)
        {
            var objectIds = new ObjectId[objectIdStrings.Length];
            for (var i = 0; i < objectIdStrings.Length; i++)
            {
                objectIds[i] = this.Database.ObjectIds.Parse(objectIdStrings[i]);
            }

            return this.Instantiate(objectIds);
        }

        public IObject[] Instantiate(IObject[] objects)
        {
            var objectIds = new ObjectId[objects.Length];
            for (var i = 0; i < objects.Length; i++)
            {
                objectIds[i] = objects[i].Strategy.ObjectId;
            }

            return this.Instantiate(objectIds);
        }

        public IObject[] Instantiate(ObjectId[] objectIds)
        {
            var references = new List<Reference>(objectIds.Length);

            var nonCachedObjectIds = new List<ObjectId>();
            foreach (var objectId in objectIds)
            {
                Reference reference;
                if (!this.referenceByObjectId.TryGetValue(objectId, out reference))
                {
                    nonCachedObjectIds.Add(objectId);
                }
                else
                {
                    if (!reference.Strategy.IsDeleted)
                    {
                        references.Add(reference);
                    }
                }
            }

            if (nonCachedObjectIds.Count > 0)
            {
                var nonCachedReferences = this.InstantiateObjects(nonCachedObjectIds);
                references.AddRange(nonCachedReferences);

                var objectByObjectId = references.ToDictionary(strategyReference => strategyReference.ObjectId, strategyReference => strategyReference.Strategy.GetObject());

                var allorsObjects = new List<IObject>();
                foreach (var objectId in objectIds)
                {
                    IObject allorsObject;
                    if (objectByObjectId.TryGetValue(objectId, out allorsObject))
                    {
                        allorsObjects.Add(allorsObject);
                    }
                }

                return allorsObjects.ToArray();
            }
            else
            {
                var allorsObjects = new IObject[references.Count];
                for (var i = 0; i < allorsObjects.Length; i++)
                {
                    allorsObjects[i] = references[i].Strategy.GetObject();
                }

                return allorsObjects;
            }
        }

        public void Prefetch(PrefetchPolicy prefetchPolicy, params IObject[] objects)
        {
            var objectIds = objects.Select(x => x.Strategy.ObjectId);
            var references = this.GetReferences(objectIds);

            if (references.Count != 0)
            {
                this.Flush();

                var prefetcher = new Prefetcher(this, prefetchPolicy, references);
                prefetcher.Execute();
            }
        }

        public void Prefetch(PrefetchPolicy prefetchPolicy, params IStrategy[] strategies)
        {
            var objectIds = strategies.Select(x => x.ObjectId);
            var references = this.GetReferences(objectIds);

            if (references.Count != 0)
            {
                this.Flush();

                var prefetcher = new Prefetcher(this, prefetchPolicy, references);
                prefetcher.Execute();
            }
        }

        public void Prefetch(PrefetchPolicy prefetchPolicy, params string[] objectIds)
        {
        }

        public void Prefetch(PrefetchPolicy prefetchPolicy, ObjectId[] objectIds)
        {
            var references = this.GetReferences(objectIds);

            if (references.Count != 0)
            {
                this.Flush();

                var prefetcher = new Prefetcher(this, prefetchPolicy, references);
                prefetcher.Execute();
            }
        }

        public IChangeSet Checkpoint()
        {
            try
            {
                return this.changeSet;
            }
            finally
            {
                this.changeSet = new ChangeSet();
            }
        }

        public Extent<T> Extent<T>() where T : IObject
        {
            return this.Extent((IComposite)this.Database.ObjectFactory.GetObjectTypeForType(typeof(T)));
        }

        public Allors.Extent Extent(IComposite type)
        {
            return new ExtentFiltered(this, type);
        }

        public Allors.Extent Union(Allors.Extent firstOperand, Allors.Extent secondOperand)
        {
            return new ExtentOperation(((Extent)firstOperand).ContainedInExtent, ((Extent)secondOperand).ContainedInExtent, ExtentOperations.Union);
        }

        public Allors.Extent Intersect(Allors.Extent firstOperand, Allors.Extent secondOperand)
        {
            return new ExtentOperation(((Extent)firstOperand).ContainedInExtent, ((Extent)secondOperand).ContainedInExtent, ExtentOperations.Intersect);
        }

        public Allors.Extent Except(Allors.Extent firstOperand, Allors.Extent secondOperand)
        {
            return new ExtentOperation(((Extent)firstOperand).ContainedInExtent, ((Extent)secondOperand).ContainedInExtent, ExtentOperations.Except);
        }

        public void Commit()
        {
            if (!this.busyCommittingOrRollingBack)
            {
                try
                {
                    this.busyCommittingOrRollingBack = true;

                    var accessed = new List<ObjectId>(this.referenceByObjectId.Keys);
                    var changed = ObjectId.EmptyObjectIds;

                    this.Flush();

                    if (this.modifiedRolesByReference != null)
                    {
                        this.UpdateCacheIds();

                        changed = this.modifiedRolesByReference.Select(dictionaryEntry => dictionaryEntry.Key.ObjectId).ToArray();
                    }

                    this.SqlCommit();

                    this.modifiedRolesByReference = null;

                    var referencesWithStrategy = new HashSet<Reference>();
                    foreach (var reference in this.referenceByObjectId.Values)
                    {
                        reference.Commit(referencesWithStrategy);
                    }

                    this.referencesWithoutCacheId = referencesWithStrategy;

                    this.referenceByObjectId = new Dictionary<ObjectId, Reference>();
                    foreach (var reference in referencesWithStrategy)
                    {
                        this.referenceByObjectId[reference.ObjectId] = reference;
                    }

                    this.associationByRoleByAssociationType = new Dictionary<IAssociationType, Dictionary<Reference, Reference>>();
                    this.associationsByRoleByAssociationType = new Dictionary<IAssociationType, Dictionary<Reference, ObjectId[]>>();

                    this.changeSet = new ChangeSet();

                    this.busyCommittingOrRollingBack = false;

                    this.Database.Cache.OnCommit(accessed, changed);

                    this.ResetSqlCommands();
                }
                finally
                {
                    this.busyCommittingOrRollingBack = false;
                }
            }
        }

        public void Rollback()
        {
            if (!this.busyCommittingOrRollingBack)
            {
                try
                {
                    this.busyCommittingOrRollingBack = true;

                    var accessed = new List<ObjectId>(this.referenceByObjectId.Keys);

                    this.SqlRollback();

                    var referencesWithStrategy = new HashSet<Reference>();
                    foreach (var reference in this.referenceByObjectId.Values)
                    {
                        reference.Rollback(referencesWithStrategy);
                    }

                    this.referencesWithoutCacheId = referencesWithStrategy;

                    this.referenceByObjectId = new Dictionary<ObjectId, Reference>();
                    foreach (var reference in referencesWithStrategy)
                    {
                        this.referenceByObjectId[reference.ObjectId] = reference;
                    }

                    this.unflushedRolesByReference = null;
                    this.modifiedRolesByReference = null;
                    this.triggersFlushRolesByAssociationType = null;

                    this.associationByRoleByAssociationType = new Dictionary<IAssociationType, Dictionary<Reference, Reference>>();
                    this.associationsByRoleByAssociationType = new Dictionary<IAssociationType, Dictionary<Reference, ObjectId[]>>();

                    this.changeSet = new ChangeSet();

                    this.Database.Cache.OnRollback(accessed);

                    this.ResetSqlCommands();
                }
                finally
                {
                    this.busyCommittingOrRollingBack = false;
                }
            }
        }

        public void Dispose()
        {
            this.Rollback();
        }

        public T Create<T>() where T : IObject
        {
            var objectType = (IClass)this.Database.ObjectFactory.GetObjectTypeForType(typeof(T));
            return (T)this.Create(objectType);
        }

        public override string ToString()
        {
            return "Session[id=" + this.GetHashCode() + "] " + this.Population;
        }

        internal Roles GetOrCreateRoles(Reference reference)
        {
            if (this.modifiedRolesByReference != null)
            {
                Roles roles;
                if (this.modifiedRolesByReference.TryGetValue(reference, out roles))
                {
                    return roles;
                }
            }

            return new Roles(reference);
        }

        internal Reference GetOrCreateReferenceForExistingObject(ObjectId objectId)
        {
            Reference reference;
            if (!this.referenceByObjectId.TryGetValue(objectId, out reference))
            {
                var objectType = this.Database.Cache.GetObjectType(objectId);
                if (objectType == null)
                {
                    objectType = this.GetClass(objectId);
                    this.Database.Cache.SetObjectType(objectId, objectType);
                }

                reference = new Reference(this, objectType, objectId, false);
                this.referenceByObjectId[objectId] = reference;
                this.referencesWithoutCacheId.Add(reference);
            }
            else
            {
                reference.Exists = true;
            }

            return reference;
        }

        internal Reference GetAssociation(Strategy roleStrategy, IAssociationType associationType)
        {
            var associationByRole = this.GetAssociationByRole(associationType);

            Reference association;
            if (!associationByRole.TryGetValue(roleStrategy.Reference, out association))
            {
                this.FlushConditionally(roleStrategy.ObjectId, associationType);
                association = this.GetCompositeAssociation(roleStrategy.Reference, associationType);
                associationByRole[roleStrategy.Reference] = association;
            }

            return association;
        }

        internal void SetAssociation(Reference previousAssociation, Strategy roleStrategy, IAssociationType associationType)
        {
            var associationByRole = this.GetAssociationByRole(associationType);
            associationByRole[roleStrategy.Reference] = previousAssociation;
        }

        internal Reference[] GetOrCreateReferencesForExistingObjects(IEnumerable<ObjectId> objectIds)
        {
            return objectIds.Select(this.GetOrCreateReferenceForExistingObject).ToArray();
        }

        internal ObjectId[] GetAssociations(Strategy roleStrategy, IAssociationType associationType)
        {
            var associationsByRole = this.GetAssociationsByRole(associationType);

            ObjectId[] associations;
            if (!associationsByRole.TryGetValue(roleStrategy.Reference, out associations))
            {
                this.FlushConditionally(roleStrategy.ObjectId, associationType);
                associations = this.GetCompositesAssociation(roleStrategy, associationType);
                associationsByRole[roleStrategy.Reference] = associations;
            }

            return associations;
        }

        internal void AddAssociation(Reference association, Reference role, IAssociationType associationType)
        {
            var associationsByRole = this.GetAssociationsByRole(associationType);

            ObjectId[] associations;
            if (associationsByRole.TryGetValue(role, out associations))
            {
                var newAssociations = new ObjectId[associations.Length + 1];
                associations.CopyTo(newAssociations, 0);
                newAssociations[newAssociations.Length - 1] = association.ObjectId;
                associationsByRole[role] = newAssociations;
            }
        }

        internal void RemoveAssociation(Reference association, Reference role, IAssociationType associationType)
        {
            var associationsByRole = this.GetAssociationsByRole(associationType);

            ObjectId[] associations;
            if (associationsByRole.TryGetValue(role, out associations))
            {
                var associationList = new List<ObjectId>(associations);
                associationList.Remove(association.ObjectId);
                associations = associationList.ToArray();
                associationsByRole[role] = associations;
            }
        }

        internal void Flush()
        {
            if (this.unflushedRolesByReference != null)
            {
                var flush = new Flush(this, this.unflushedRolesByReference);
                flush.Execute();

                this.unflushedRolesByReference = null;
                this.triggersFlushRolesByAssociationType = null;
            }
        }

        internal void AddReferenceWithoutCacheIdOrExistsKnown(Reference reference)
        {
            this.referencesWithoutCacheId.Add(reference);
        }

        internal void GetCacheIdsAndExists()
        {
            if (this.referencesWithoutCacheId.Count > 0)
            {
                var cacheIdByObjectId = this.GetCacheIds(this.referencesWithoutCacheId);
                foreach (var association in this.referencesWithoutCacheId)
                {
                    int cacheId;
                    if (cacheIdByObjectId.TryGetValue(association.ObjectId, out cacheId))
                    {
                        association.CacheId = cacheId;
                        association.Exists = true;
                    }
                    else
                    {
                        association.Exists = false;
                    }
                }

                this.referencesWithoutCacheId = new HashSet<Reference>();
            }
        }

        internal void RequireFlush(Reference association, Roles roles)
        {
            if (this.unflushedRolesByReference == null)
            {
                this.unflushedRolesByReference = new Dictionary<Reference, Roles>();
            }

            this.unflushedRolesByReference[association] = roles;

            if (this.modifiedRolesByReference == null)
            {
                this.modifiedRolesByReference = new Dictionary<Reference, Roles>();
            }

            this.modifiedRolesByReference[association] = roles;
        }

        internal void TriggerFlush(ObjectId role, IAssociationType associationType)
        {
            if (this.triggersFlushRolesByAssociationType == null)
            {
                this.triggersFlushRolesByAssociationType = new Dictionary<IAssociationType, HashSet<ObjectId>>();
            }

            HashSet<ObjectId> associations;
            if (!this.triggersFlushRolesByAssociationType.TryGetValue(associationType, out associations))
            {
                associations = new HashSet<ObjectId>();
                this.triggersFlushRolesByAssociationType[associationType] = associations;
            }

            associations.Add(role);
        }

        internal List<Reference> GetReferences(IEnumerable<ObjectId> objectIds)
        {
            var references = new List<Reference>();

            List<ObjectId> referencesToInstantiate = null;
            foreach (var objectId in objectIds)
            {
                Reference reference;
                this.referenceByObjectId.TryGetValue(objectId, out reference);
                if (reference != null && reference.ExistsKnown && !reference.IsUnknownCacheId)
                {
                    if (reference.Exists && !reference.IsNew)
                    {
                        references.Add(reference);
                    }
                }
                else
                {
                    if (referencesToInstantiate == null)
                    {
                        referencesToInstantiate = new List<ObjectId>();
                    }

                    referencesToInstantiate.Add(objectId);
                }
            }

            if (referencesToInstantiate != null)
            {
                var existsUnknownReferences = this.InstantiateObjects(referencesToInstantiate);
                references.AddRange(existsUnknownReferences);
            }

            return references;
        }

        internal Command CreateCommand(string commandText)
        {
            var command = this.CreateSqlCommand(commandText);
            return new Command(command);
        }

        #region Commands
        internal void DeleteObject(Strategy strategy)
        {
            this.deleteObjectByClass = this.deleteObjectByClass ?? new Dictionary<IClass, SqlCommand>();

            var @class = strategy.Class;

            SqlCommand command;
            if (!this.DeleteObjectByClass.TryGetValue(@class, out command))
            {
                var sql = "BEGIN\n";

                sql += "DELETE FROM " + this.Database.Mapping.TableNameForObjects + "\n";
                sql += "WHERE " + Mapping.ColumnNameForObject + "=" + Mapping.ParamNameForObject + ";\n";

                sql += "DELETE FROM " + this.Database.Mapping.TableNameForObjectByClass[@class.ExclusiveClass] + "\n";
                sql += "WHERE " + Mapping.ColumnNameForObject + "=" + Mapping.ParamNameForObject + ";\n";

                sql += "END;";

                command = this.CreateSqlCommand(sql);
                this.AddObjectParameter(command, strategy.ObjectId);
                this.deleteObjectByClass[@class] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForObject].Value = strategy.ObjectId.Value;
            }

            command.ExecuteNonQuery();
        }

        internal void GetUnitRoles(Roles roles)
        {
            var reference = roles.Reference;
            var @class = reference.Class;

            SqlCommand command;
            if (!this.GetUnitRolesByClass.TryGetValue(@class, out command))
            {
                var sql = this.Database.Mapping.ProcedureNameForGetUnitRolesByClass[@class];
                command = this.CreateSqlProcedureCommand(sql);
                this.AddObjectParameter(command, reference.ObjectId);
                this.getUnitRolesByClass[@class] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForObject].Value = reference.ObjectId.Value;
            }

            using (DbDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var sortedUnitRoles = this.Database.GetSortedUnitRolesByObjectType(reference.Class);

                    for (var i = 0; i < sortedUnitRoles.Length; i++)
                    {
                        var roleType = sortedUnitRoles[i];

                        object unit = null;
                        if (!reader.IsDBNull(i))
                        {
                            var unitTypeTag = ((IUnit)roleType.ObjectType).UnitTag;
                            switch (unitTypeTag)
                            {
                                case UnitTags.AllorsString:
                                    unit = reader.GetString(i);
                                    break;
                                case UnitTags.AllorsInteger:
                                    unit = reader.GetInt32(i);
                                    break;
                                case UnitTags.AllorsFloat:
                                    unit = reader.GetDouble(i);
                                    break;
                                case UnitTags.AllorsDecimal:
                                    unit = reader.GetDecimal(i);
                                    break;
                                case UnitTags.AllorsDateTime:
                                    var dateTime = reader.GetDateTime(i);
                                    if (dateTime == DateTime.MaxValue || dateTime == DateTime.MinValue)
                                    {
                                        unit = dateTime;
                                    }
                                    else
                                    {
                                        unit = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond, DateTimeKind.Utc);
                                    }

                                    break;
                                case UnitTags.AllorsBoolean:
                                    unit = reader.GetBoolean(i);
                                    break;
                                case UnitTags.AllorsUnique:
                                    unit = reader.GetGuid(i);
                                    break;
                                case UnitTags.AllorsBinary:
                                    var byteArray = (byte[])reader.GetValue(i);
                                    unit = byteArray;
                                    break;
                                default:
                                    throw new ArgumentException("Unknown Unit ObjectType: " + roleType.ObjectType.Name);
                            }
                        }

                        roles.CachedObject.SetValue(roleType, unit);
                    }
                }
            }
        }

        internal void PrefetchUnitRoles(IClass @class, List<Reference> associations, IRoleType anyRoleType)
        {
            var references = this.FilterForPrefetchRoles(associations, anyRoleType);
            if (references.Count == 0)
            {
                return;
            }

            SqlCommand command;
            if (!this.PrefetchUnitRolesByClass.TryGetValue(@class, out command))
            {
                var sql = this.Database.Mapping.ProcedureNameForPrefetchUnitRolesByClass[@class];
                command = this.CreateSqlProcedureCommand(sql);
                this.AddObjectTableParameter(command, references);
                this.prefetchUnitRolesByClass[@class] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(references);
            }

            using (DbDataReader reader = command.ExecuteReader())
            {
                var sortedUnitRoles = this.Database.GetSortedUnitRolesByObjectType(@class);
                var cache = this.Database.Cache;

                while (reader.Read())
                {
                    var objectId = this.Database.ObjectIds.Parse(reader[0].ToString());
                    var reference = this.referenceByObjectId[objectId];

                    Roles modifiedRoles = null;
                    if (this.modifiedRolesByReference != null)
                    {
                        this.modifiedRolesByReference.TryGetValue(reference, out modifiedRoles);
                    }

                    var cachedObject = cache.GetOrCreateCachedObject(@class, objectId, reference.CacheId);

                    for (var i = 0; i < sortedUnitRoles.Length; i++)
                    {
                        var roleType = sortedUnitRoles[i];

                        var index = i + 1;
                        object unit = null;
                        if (!reader.IsDBNull(index))
                        {
                            var unitTypeTag = ((IUnit)roleType.ObjectType).UnitTag;
                            switch (unitTypeTag)
                            {
                                case UnitTags.AllorsString:
                                    unit = reader.GetString(index);
                                    break;
                                case UnitTags.AllorsInteger:
                                    unit = reader.GetInt32(index);
                                    break;
                                case UnitTags.AllorsFloat:
                                    unit = reader.GetDouble(index);
                                    break;
                                case UnitTags.AllorsDecimal:
                                    unit = reader.GetDecimal(index);
                                    break;
                                case UnitTags.AllorsDateTime:
                                    var dateTime = reader.GetDateTime(index);
                                    if (dateTime == DateTime.MaxValue || dateTime == DateTime.MinValue)
                                    {
                                        unit = dateTime;
                                    }
                                    else
                                    {
                                        unit = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond, DateTimeKind.Utc);
                                    }

                                    break;
                                case UnitTags.AllorsBoolean:
                                    unit = reader.GetBoolean(index);
                                    break;
                                case UnitTags.AllorsUnique:
                                    unit = reader.GetGuid(index);
                                    break;
                                case UnitTags.AllorsBinary:
                                    var byteArray = (byte[])reader.GetValue(index);
                                    unit = byteArray;
                                    break;
                                default:
                                    throw new ArgumentException("Unknown Unit ObjectType: " + roleType.ObjectType.Name);
                            }
                        }

                        if (modifiedRoles == null || !modifiedRoles.ModifiedRoleByRoleType.ContainsKey(roleType))
                        {
                            cachedObject.SetValue(roleType, unit);
                        }
                    }
                }
            }
        }

        internal void SetUnitRole(List<UnitRelation> relations, IClass exclusiveRootClass, IRoleType roleType)
        {
            var schema = this.Database.Mapping;

            Dictionary<IRoleType, SqlCommand> commandByRoleType;
            if (!this.SetUnitRoleByRoleTypeByClass.TryGetValue(exclusiveRootClass, out commandByRoleType))
            {
                commandByRoleType = new Dictionary<IRoleType, SqlCommand>();
                this.setUnitRoleByRoleTypeByClass.Add(exclusiveRootClass, commandByRoleType);
            }

            string tableTypeName;

            var unitTypeTag = ((IUnit)roleType.ObjectType).UnitTag;
            switch (unitTypeTag)
            {
                case UnitTags.AllorsString:
                    tableTypeName = schema.TableTypeNameForStringRelation;
                    break;

                case UnitTags.AllorsInteger:
                    tableTypeName = schema.TableTypeNameForIntegerRelation;
                    break;

                case UnitTags.AllorsFloat:
                    tableTypeName = schema.TableTypeNameForFloatRelation;
                    break;

                case UnitTags.AllorsBoolean:
                    tableTypeName = schema.TableTypeNameForBooleanRelation;
                    break;

                case UnitTags.AllorsDateTime:
                    tableTypeName = schema.TableTypeNameForDateTimeRelation;
                    break;

                case UnitTags.AllorsUnique:
                    tableTypeName = schema.TableTypeNameForUniqueRelation;
                    break;

                case UnitTags.AllorsBinary:
                    tableTypeName = schema.TableTypeNameForBinaryRelation;
                    break;

                case UnitTags.AllorsDecimal:
                    tableTypeName = schema.TableTypeNameForDecimalRelationByScaleByPrecision[roleType.Precision.Value][roleType.Scale.Value];
                    break;

                default:
                    throw new ArgumentException("Unknown Unit ObjectType: " + unitTypeTag);
            }

            SqlCommand command;
            if (!commandByRoleType.TryGetValue(roleType, out command))
            {
                var sql = this.Database.Mapping.ProcedureNameForSetUnitRoleByRelationTypeByClass[exclusiveRootClass][roleType.RelationType];
                command = this.CreateSqlProcedureCommand(sql);

                var sqlParameter = command.CreateParameter();
                sqlParameter.SqlDbType = SqlDbType.Structured;
                sqlParameter.TypeName = tableTypeName;
                sqlParameter.ParameterName = Mapping.ParamNameForTableType;
                sqlParameter.Value = this.Database.CreateRelationTable(roleType, relations);

                command.Parameters.Add(sqlParameter);
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateRelationTable(roleType, relations);
            }

            command.ExecuteNonQuery();
        }

        internal void SetUnitRoles(Roles roles, List<IRoleType> sortedRoleTypes)
        {
            var exclusiveRootClass = roles.Reference.Class.ExclusiveClass;

            Dictionary<IList<IRoleType>, SqlCommand> setUnitRoleByRoleType;
            if (!this.SetUnitRolesByRoleTypeByClass.TryGetValue(exclusiveRootClass, out setUnitRoleByRoleType))
            {
                setUnitRoleByRoleType = new Dictionary<IList<IRoleType>, SqlCommand>(new SortedRoleTypeComparer());
                this.setUnitRolesByRoleTypeByClass.Add(exclusiveRootClass, setUnitRoleByRoleType);
            }

            SqlCommand command;
            if (!setUnitRoleByRoleType.TryGetValue(sortedRoleTypes, out command))
            {
                command = this.CreateSqlCommand();
                this.AddObjectParameter(command, roles.Reference.ObjectId);

                var sql = new StringBuilder();
                sql.Append("UPDATE " + this.Database.Mapping.TableNameForObjectByClass[exclusiveRootClass] + " SET\n");

                var count = 0;
                foreach (var roleType in sortedRoleTypes)
                {
                    if (count > 0)
                    {
                        sql.Append(" , ");
                    }

                    ++count;

                    var column = this.Database.Mapping.ColumnNameByRelationType[roleType.RelationType];
                    sql.Append(column + "=" + this.Database.Mapping.ParamNameByRoleType[roleType]);

                    var unit = roles.ModifiedRoleByRoleType[roleType];
                    var sqlParameter1 = command.CreateParameter();
                    sqlParameter1.ParameterName = this.Database.Mapping.ParamNameByRoleType[roleType];
                    sqlParameter1.SqlDbType = this.Database.Mapping.GetSqlDbType(roleType);
                    sqlParameter1.Value = unit ?? DBNull.Value;

                    command.Parameters.Add(sqlParameter1);
                }

                sql.Append("\nWHERE " + Mapping.ColumnNameForObject + "=" + Mapping.ParamNameForObject + "\n");

                command.CommandText = sql.ToString();
                command.ExecuteNonQuery();

                setUnitRoleByRoleType.Add(sortedRoleTypes, command);
            }
            else
            {
                command.Parameters[Mapping.ParamNameForObject].Value = roles.Reference.ObjectId.Value ?? DBNull.Value;

                foreach (var roleType in sortedRoleTypes)
                {
                    var unit = roles.ModifiedRoleByRoleType[roleType];
                    command.Parameters[this.Database.Mapping.ParamNameByRoleType[roleType]].Value = unit ?? DBNull.Value;
                }

                command.ExecuteNonQuery();
            }
        }

        internal void GetCompositeRole(Roles roles, IRoleType roleType)
        {
            var reference = roles.Reference;

            SqlCommand command;
            if (!this.GetCompositeRoleByRoleType.TryGetValue(roleType, out command))
            {
                var sql = this.Database.Mapping.ProcedureNameForGetRoleByRelationType[roleType.RelationType];
                command = this.CreateSqlProcedureCommand(sql);
                this.AddAssociationTableParameter(command, reference.ObjectId); 
                this.getCompositeRoleByRoleType[roleType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForAssociation].Value = reference.ObjectId.Value;
            }

            var result = command.ExecuteScalar();
            if (result == null || result == DBNull.Value)
            {
                roles.CachedObject.SetValue(roleType, null);
            }
            else
            {
                var objectId = this.Database.ObjectIds.Parse(result.ToString());
                // TODO: Should add to objectsToLoad
                roles.CachedObject.SetValue(roleType, objectId);
            }
        }

        internal void PrefetchCompositeRoleObjectTable(List<Reference> associations, IRoleType roleType, List<ObjectId> nestedObjectIds)
        {
            var references = nestedObjectIds == null ? this.FilterForPrefetchRoles(associations, roleType) : this.FilterForPrefetchCompositeRoles(associations, roleType, nestedObjectIds);
            if (references.Count == 0)
            {
                return;
            }

            SqlCommand command;
            if (!this.PrefetchCompositeRoleByRoleType.TryGetValue(roleType, out command))
            {
                command = this.CreateSqlProcedureCommand(this.Database.Mapping.ProcedureNameForPrefetchRoleByRelationType[roleType.RelationType]);
                this.AddObjectTableParameter(command, references);
                this.prefetchCompositeRoleByRoleType[roleType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(references);
            }

            using (DbDataReader reader = command.ExecuteReader())
            {
                var cache = this.Database.Cache;

                while (reader.Read())
                {
                    var associationId = this.Database.ObjectIds.Parse(reader[0].ToString());
                    var associationReference = this.referenceByObjectId[associationId];

                    var cachedObject = cache.GetOrCreateCachedObject(associationReference.Class, associationId, associationReference.CacheId);

                    var roleIdValue = reader[1];

                    if (roleIdValue == null || roleIdValue == DBNull.Value)
                    {
                        cachedObject.SetValue(roleType, null);
                    }
                    else
                    {
                        var roleId = this.Database.ObjectIds.Parse(roleIdValue.ToString());
                        cachedObject.SetValue(roleType, roleId);

                        if (nestedObjectIds != null)
                        {
                            nestedObjectIds.Add(roleId);
                        }
                    }
                }
            }
        }

        internal void PrefetchCompositeRoleRelationTable(List<Reference> associations, IRoleType roleType, List<ObjectId> nestedObjectIds)
        {
            var references = nestedObjectIds == null ? this.FilterForPrefetchRoles(associations, roleType) : this.FilterForPrefetchCompositeRoles(associations, roleType, nestedObjectIds);
            if (references.Count == 0)
            {
                return;
            }

            SqlCommand command;
            if (!this.PrefetchCompositeRoleByRoleType.TryGetValue(roleType, out command))
            {
                var sql = this.Database.Mapping.ProcedureNameForPrefetchRoleByRelationType[roleType.RelationType];
                command = this.CreateSqlProcedureCommand(sql);
                this.AddObjectTableParameter(command, references);
                this.prefetchCompositeRoleByRoleType[roleType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(references);
            }

            var roleByAssociation = new Dictionary<Reference, ObjectId>();
            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var associationId = this.Database.ObjectIds.Parse(reader[0].ToString());
                    var associationReference = this.referenceByObjectId[associationId];
                    var roleId = this.Database.ObjectIds.Parse(reader[1].ToString());
                    roleByAssociation.Add(associationReference, roleId);
                }
            }

            var cache = this.Database.Cache;
            foreach (var reference in references)
            {
                var cachedObject = cache.GetOrCreateCachedObject(reference.Class, reference.ObjectId, reference.CacheId);

                ObjectId roleId;
                if (roleByAssociation.TryGetValue(reference, out roleId))
                {
                    cachedObject.SetValue(roleType, roleId);
                
                    if (nestedObjectIds != null)
                    {
                        nestedObjectIds.Add(roleId);
                    }
                }
                else
                {
                    cachedObject.SetValue(roleType, null);
                }
            }
        }

        internal void SetCompositeRole(List<CompositeRelation> relations, IRoleType roleType)
        {
            SqlCommand command;
            if (!this.SetCompositeRoleByRoleType.TryGetValue(roleType, out command))
            {
                string sql;
                if (!roleType.RelationType.ExistExclusiveClasses)
                {
                    sql = this.Database.Mapping.ProcedureNameForSetRoleByRelationType[roleType.RelationType];
                }
                else
                {
                    sql = this.Database.Mapping.ProcedureNameForSetRoleByRelationType[roleType.RelationType];
                }

                command = this.CreateSqlProcedureCommand(sql);
                this.AddCompositeRoleTableParameter(command, relations);
                this.setCompositeRoleByRoleType[roleType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateRelationTable(relations);
            }

            command.ExecuteNonQuery();
        }

        internal void GetCompositesRole(Roles roles, IRoleType roleType)
        {
            var reference = roles.Reference;

            SqlCommand command;
            if (!this.GetCompositesRoleByRoleType.TryGetValue(roleType, out command))
            {
                var associationType = roleType.AssociationType;

                string sql;
                if (associationType.IsMany || !roleType.RelationType.ExistExclusiveClasses)
                {
                    sql = this.Database.Mapping.ProcedureNameForGetRoleByRelationType[roleType.RelationType];
                }
                else
                {
                    sql = this.Database.Mapping.ProcedureNameForGetRoleByRelationType[roleType.RelationType];
                }

                command = this.CreateSqlProcedureCommand(sql);
                this.AddAssociationParameter(command, reference.ObjectId);
                this.getCompositesRoleByRoleType[roleType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForAssociation].Value = reference.ObjectId.Value;
            }

            var objectIds = new List<ObjectId>();
            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = this.Database.ObjectIds.Parse(reader[0].ToString());
                    objectIds.Add(id);
                }
            }

            roles.CachedObject.SetValue(roleType, objectIds.ToArray());
        }

        internal void PrefetchCompositesRoleObjectTable(List<Reference> associations, IRoleType roleType, List<ObjectId> nestedObjectIds)
        {
            var references = nestedObjectIds == null ? this.FilterForPrefetchRoles(associations, roleType) : this.FilterForPrefetchCompositesRoles(associations, roleType, nestedObjectIds);
            if (references.Count == 0)
            {
                return;
            }

            SqlCommand command;
            if (!this.PrefetchCompositesRoleByRoleType.TryGetValue(roleType, out command))
            {
                var sql = this.Database.Mapping.ProcedureNameForPrefetchRoleByRelationType[roleType.RelationType];
                command = this.CreateSqlProcedureCommand(sql);
                this.AddObjectTableParameter(command, references);
                this.prefetchCompositesRoleByRoleType[roleType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(references);
            }

            var rolesByAssociation = new Dictionary<Reference, List<ObjectId>>();
            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var associationId = this.Database.ObjectIds.Parse(reader[0].ToString());
                    var associationReference = this.referenceByObjectId[associationId];
                    
                    var roleIdValue = reader[1];
                    if (roleIdValue == null || roleIdValue == DBNull.Value)
                    {
                        rolesByAssociation[associationReference] = null;
                    }
                    else
                    {
                        var objectId = this.Database.ObjectIds.Parse(roleIdValue.ToString());
                        List<ObjectId> roles;
                        if (!rolesByAssociation.TryGetValue(associationReference, out roles))
                        {
                            roles = new List<ObjectId>();
                            rolesByAssociation[associationReference] = roles;
                        }

                        roles.Add(objectId);
                    }
                }
            }

            var cache = this.Database.Cache;
            foreach (var dictionaryEntry in rolesByAssociation)
            {
                var association = dictionaryEntry.Key;
                var roles = dictionaryEntry.Value;

                var cachedObject = cache.GetOrCreateCachedObject(association.Class, association.ObjectId, association.CacheId);
                cachedObject.SetValue(roleType, roles == null ? EmptyObjectIds : roles.ToArray());

                if (nestedObjectIds != null)
                {
                    nestedObjectIds.AddRange(roles);
                }
            }
        }

        internal void PrefetchCompositesRoleRelationTable(List<Reference> associations, IRoleType roleType, List<ObjectId> nestedObjectIds)
        {
            var references = nestedObjectIds == null ? this.FilterForPrefetchRoles(associations, roleType) : this.FilterForPrefetchCompositesRoles(associations, roleType, nestedObjectIds);
            if (references.Count == 0)
            {
                return;
            }

            SqlCommand command;
            if (!this.PrefetchCompositesRoleByRoleType.TryGetValue(roleType, out command))
            {
                var sql = this.Database.Mapping.ProcedureNameForPrefetchRoleByRelationType[roleType.RelationType];
                command = this.CreateSqlProcedureCommand(sql);
                this.AddObjectTableParameter(command, references);
                this.prefetchCompositesRoleByRoleType[roleType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(references);
            }
            
            var rolesByAssociation = new Dictionary<Reference, List<ObjectId>>();
            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var associationId = this.Database.ObjectIds.Parse(reader[0].ToString());
                    var associationReference = this.referenceByObjectId[associationId];

                    var role = this.Database.ObjectIds.Parse(reader[1].ToString());
                    List<ObjectId> roles;
                    if (!rolesByAssociation.TryGetValue(associationReference, out roles))
                    {
                        roles = new List<ObjectId>();
                        rolesByAssociation[associationReference] = roles;
                    }

                    roles.Add(role);
                }
            }

            var cache = this.Database.Cache;
            foreach (var reference in references)
            {
                Roles modifiedRoles = null;
                if (this.modifiedRolesByReference != null)
                {
                    this.modifiedRolesByReference.TryGetValue(reference, out modifiedRoles);
                }
                
                if (modifiedRoles == null || !modifiedRoles.ModifiedRoleByRoleType.ContainsKey(roleType))
                {
                    var cachedObject = cache.GetOrCreateCachedObject(reference.Class, reference.ObjectId, reference.CacheId);

                    List<ObjectId> roles;
                    if (rolesByAssociation.TryGetValue(reference, out roles))
                    {
                        cachedObject.SetValue(roleType, roles.ToArray());

                        if (nestedObjectIds != null)
                        {
                            nestedObjectIds.AddRange(roles);
                        }
                    }
                    else
                    {
                        cachedObject.SetValue(roleType, EmptyObjectIds);
                    }
                }
            }
        }

        internal void AddCompositeRole(List<CompositeRelation> relations, IRoleType roleType)
        {
            SqlCommand command;
            if (!this.AddCompositeRoleByRoleType.TryGetValue(roleType, out command))
            {
                var sql = this.database.Mapping.ProcedureNameForAddRoleByRelationType[roleType.RelationType];
                command = this.CreateSqlProcedureCommand(sql);
                this.AddCompositeRoleTableParameter(command, relations);
                this.addCompositeRoleByRoleType[roleType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.database.CreateRelationTable(relations);
            }

            command.ExecuteNonQuery();
        }

        internal void RemoveCompositeRole(List<CompositeRelation> relations, IRoleType roleType)
        {
            SqlCommand command;
            if (!this.RemoveCompositeRoleByRoleType.TryGetValue(roleType, out command))
            {
                var sql = this.Database.Mapping.ProcedureNameForRemoveRoleByRelationType[roleType.RelationType];
                command = this.CreateSqlProcedureCommand(sql);
                this.AddCompositeRoleTableParameter(command, relations);
                this.removeCompositeRoleByRoleType[roleType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateRelationTable(relations);
            }

            command.ExecuteNonQuery();
        }

        internal void ClearCompositeAndCompositesRole(IList<ObjectId> associations, IRoleType roleType)
        {
            var sql = this.database.Mapping.ProcedureNameForClearRoleByRelationType[roleType.RelationType];
            
            SqlCommand command;
            if (!this.ClearCompositeAndCompositesRoleByRoleType.TryGetValue(roleType, out command))
            {
                command = this.CreateSqlProcedureCommand(sql);
                this.AddObjectTableParameter(command, associations);
                this.clearCompositeAndCompositesRoleByRoleType[roleType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.database.CreateObjectTable(associations);
            }

            command.ExecuteNonQuery();
        }

        internal void PrefetchCompositeAssociationObjectTable(List<Reference> roles, IAssociationType associationType, List<ObjectId> nestedObjectIds)
        {
            var references = nestedObjectIds == null ? this.FilterForPrefetchAssociations(roles, associationType) : this.FilterForPrefetchCompositeAssociations(roles, associationType, nestedObjectIds);
            if (references.Count == 0)
            {
                return;
            }

            SqlCommand command;
            if (!this.PrefetchCompositeAssociationByAssociationType.TryGetValue(associationType, out command))
            {
                var roleType = associationType.RoleType;
                var sql = this.database.Mapping.ProcedureNameForPrefetchAssociationByRelationType[roleType.RelationType];
                command = this.CreateSqlProcedureCommand(sql);
                this.AddObjectTableParameter(command, references);
                this.prefetchCompositeAssociationByAssociationType[associationType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(references);
            }

            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var roleId = this.Database.ObjectIds.Parse(reader[1].ToString());
                    var role = this.referenceByObjectId[roleId];

                    var associationByRole = this.GetAssociationByRole(associationType);
                    if (!associationByRole.ContainsKey(role))
                    {
                        var associationIdValue = reader[0];
                        Reference association = null;
                        if (associationIdValue != null && associationIdValue != DBNull.Value)
                        {
                            var associationId = this.Database.ObjectIds.Parse(associationIdValue.ToString());
                            if (associationType.ObjectType.ExistExclusiveClass)
                            {
                                association = this.GetOrCreateReferenceForExistingObject(associationType.ObjectType.ExclusiveClass, associationId);
                            }
                            else
                            {
                                association = this.GetOrCreateReferenceForExistingObject(associationId);
                            }

                            if (nestedObjectIds != null)
                            {
                                nestedObjectIds.Add(association.ObjectId);
                            }
                        }

                        associationByRole[role] = association;

                        this.FlushConditionally(roleId, associationType);
                    }
                }
            }
        }

        internal void PrefetchCompositeAssociationRelationTable(List<Reference> roles, IAssociationType associationType, List<ObjectId> nestedObjectIds)
        {
            var references = nestedObjectIds == null ? this.FilterForPrefetchAssociations(roles, associationType) : this.FilterForPrefetchCompositesAssociations(roles, associationType, nestedObjectIds);
            if (references.Count == 0)
            {
                return;
            }

            SqlCommand command;
            if (!this.PrefetchCompositeAssociationByAssociationType.TryGetValue(associationType, out command))
            {
                var roleType = associationType.RoleType;
                var sql = this.database.Mapping.ProcedureNameForPrefetchAssociationByRelationType[roleType.RelationType];
                command = this.CreateSqlProcedureCommand(sql);
                this.AddObjectTableParameter(command, roles);
                this.prefetchCompositeAssociationByAssociationType[associationType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(roles);
            }

            var prefetchedAssociationByRole = new Dictionary<Reference, ObjectId>();
            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var roleId = this.Database.ObjectIds.Parse(reader[1].ToString());
                    var roleReference = this.referenceByObjectId[roleId];
                    var associationId = this.Database.ObjectIds.Parse(reader[0].ToString());
                    prefetchedAssociationByRole.Add(roleReference, associationId);
                }
            }

            var associationByRole = this.GetAssociationByRole(associationType);
            foreach (var role in roles)
            {
                if (!associationByRole.ContainsKey(role))
                {
                    Reference association = null;

                    ObjectId associationId;
                    if (prefetchedAssociationByRole.TryGetValue(role, out associationId))
                    {
                        if (associationType.ObjectType.ExistExclusiveClass)
                        {
                            association = this.GetOrCreateReferenceForExistingObject(associationType.ObjectType.ExclusiveClass, associationId);
                        }
                        else
                        {
                            association = this.GetOrCreateReferenceForExistingObject(associationId);
                        }

                        if (nestedObjectIds != null)
                        {
                            nestedObjectIds.Add(associationId);
                        }
                    }
                    
                    associationByRole[role] = association;

                    this.FlushConditionally(role.ObjectId, associationType);
                }
            }
        }

        internal void PrefetchCompositesAssociationObjectTable(List<Reference> roles, IAssociationType associationType, List<ObjectId> nestedObjectIds)
        {
            var references = nestedObjectIds == null ? this.FilterForPrefetchAssociations(roles, associationType) : this.FilterForPrefetchCompositeAssociations(roles, associationType, nestedObjectIds);
            if (references.Count == 0)
            {
                return;
            }

            SqlCommand command;
            if (!this.PrefetchCompositeAssociationByAssociationType.TryGetValue(associationType, out command))
            {
                var roleType = associationType.RoleType;
                var sql = this.database.Mapping.ProcedureNameForPrefetchAssociationByRelationType[roleType.RelationType];
                command = this.CreateSqlProcedureCommand(sql);
                this.AddObjectTableParameter(command, roles);
                this.prefetchCompositeAssociationByAssociationType[associationType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(roles);
            }

            var prefetchedAssociationByRole = new Dictionary<Reference, List<ObjectId>>();
            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var roleId = this.Database.ObjectIds.Parse(reader[1].ToString());
                    var roleReference = this.referenceByObjectId[roleId];

                    var associationIdValue = reader[0];
                    if (associationIdValue != null && associationIdValue != DBNull.Value)
                    {
                        List<ObjectId> associations;
                        if (!prefetchedAssociationByRole.TryGetValue(roleReference, out associations))
                        {
                            associations = new List<ObjectId>();
                            prefetchedAssociationByRole.Add(roleReference, associations);
                        }

                        var associationId = this.Database.ObjectIds.Parse(associationIdValue.ToString());
                        associations.Add(associationId);

                        if (associationType.ObjectType.ExistExclusiveClass)
                        {
                            this.GetOrCreateReferenceForExistingObject(associationType.ObjectType.ExclusiveClass, associationId);
                        }
                        else
                        {
                            this.GetOrCreateReferenceForExistingObject(associationId);
                        }
                    }
                }
            }

            var associationsByRole = this.GetAssociationsByRole(associationType);
            foreach (var role in roles)
            {
                if (!associationsByRole.ContainsKey(role))
                {
                    List<ObjectId> associations;
                    if (!prefetchedAssociationByRole.TryGetValue(role, out associations))
                    {
                        associationsByRole[role] = EmptyObjectIds;
                    }
                    else
                    {
                        associationsByRole[role] = associations.ToArray();

                        if (nestedObjectIds != null)
                        {
                            nestedObjectIds.AddRange(associations);
                        }
                    }

                    this.FlushConditionally(role.ObjectId, associationType);
                }
            }
        }

        internal void PrefetchCompositesAssociationRelationTable(List<Reference> roles, IAssociationType associationType, List<ObjectId> nestedObjectIds)
        {
            var references = nestedObjectIds == null ? this.FilterForPrefetchAssociations(roles, associationType) : this.FilterForPrefetchCompositeAssociations(roles, associationType, nestedObjectIds);
            if (references.Count == 0)
            {
                return;
            }
            
            SqlCommand command;
            if (!this.PrefetchCompositeAssociationByAssociationType.TryGetValue(associationType, out command))
            {
                var roleType = associationType.RoleType;
                var sql = this.database.Mapping.ProcedureNameForPrefetchAssociationByRelationType[roleType.RelationType];
                command = this.CreateSqlProcedureCommand(sql);
                this.AddObjectTableParameter(command, roles);
                this.prefetchCompositeAssociationByAssociationType[associationType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(roles);
            }

            var prefetchedAssociationByRole = new Dictionary<Reference, List<ObjectId>>();
            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var roleId = this.Database.ObjectIds.Parse(reader[1].ToString());
                    var roleReference = this.referenceByObjectId[roleId];

                    List<ObjectId> associations;
                    if (!prefetchedAssociationByRole.TryGetValue(roleReference, out associations))
                    {
                        associations = new List<ObjectId>();
                        prefetchedAssociationByRole.Add(roleReference, associations);
                    }

                    var associationId = this.Database.ObjectIds.Parse(reader[0].ToString());
                    associations.Add(associationId);

                    if (associationType.ObjectType.ExistExclusiveClass)
                    {
                        this.GetOrCreateReferenceForExistingObject(associationType.ObjectType.ExclusiveClass, associationId);
                    }
                    else
                    {
                        this.GetOrCreateReferenceForExistingObject(associationId);
                    }
                }
            }

            var associationsByRole = this.GetAssociationsByRole(associationType);
            foreach (var role in roles)
            {
                if (!associationsByRole.ContainsKey(role))
                {
                    List<ObjectId> associations;
                    if (!prefetchedAssociationByRole.TryGetValue(role, out associations))
                    {
                        associationsByRole[role] = EmptyObjectIds;
                    }
                    else
                    {
                        associationsByRole[role] = associations.ToArray();

                        if (nestedObjectIds != null)
                        {
                            nestedObjectIds.AddRange(associations);
                        }
                    }

                    this.FlushConditionally(role.ObjectId, associationType);
                }
            }
        }

        private Reference GetCompositeAssociation(Reference role, IAssociationType associationType)
        {
            Reference associationObject = null;

            SqlCommand command;
            if (!this.GetCompositeAssociationByAssociationType.TryGetValue(associationType, out command))
            {
                var roleType = associationType.RoleType;
                var sql = this.database.Mapping.ProcedureNameForGetAssociationByRelationType[roleType.RelationType];
                command = this.CreateSqlProcedureCommand(sql);
                this.AddCompositeRoleParameter(command, role.ObjectId);
                this.getCompositeAssociationByAssociationType[associationType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForCompositeRole].Value = role.ObjectId.Value ?? DBNull.Value;
            }

            object result = command.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                var id = this.database.ObjectIds.Parse(result.ToString());

                if (associationType.ObjectType.ExistExclusiveClass)
                {
                    associationObject = this.GetOrCreateReferenceForExistingObject(associationType.ObjectType.ExclusiveClass, id);
                }
                else
                {
                    associationObject = this.GetOrCreateReferenceForExistingObject(id);
                }
            }

            return associationObject;
        }

        private ObjectId[] GetCompositesAssociation(Strategy role, IAssociationType associationType)
        {
            SqlCommand command;
            if (!this.GetCompositesAssociationByAssociationType.TryGetValue(associationType, out command))
            {
                var roleType = associationType.RoleType;
                var sql = this.Database.Mapping.ProcedureNameForGetAssociationByRelationType[roleType.RelationType];
                command = this.CreateSqlProcedureCommand(sql);
                this.AddCompositeRoleParameter(command, role.ObjectId);
                this.getCompositesAssociationByAssociationType[associationType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForCompositeRole].Value = role.ObjectId.Value;
            }

            var objectIds = new List<ObjectId>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = this.Database.ObjectIds.Parse(reader[0].ToString());
                    objectIds.Add(id);
                }
            }

            return objectIds.ToArray();
        }

        private Reference CreateObject(IClass @class)
        {
            SqlCommand command;
            if (!this.CreateObjectByClass.TryGetValue(@class, out command))
            {
                var sql = this.database.Mapping.ProcedureNameForCreateObjectByClass[@class];
                command = this.CreateSqlProcedureCommand(sql);
                this.AddTypeParameter(command, @class);
                this.createObjectByClass[@class] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForType].Value = (object)@class.Id ?? DBNull.Value;
            }

            var result = command.ExecuteScalar();
            var objectId = this.database.ObjectIds.Parse(result.ToString());
            return this.CreateAssociationForNewObject(@class, objectId);
        }

        private IList<Reference> CreateObjects(IClass @class, int count)
        {
            SqlCommand command;
            if (!this.CreateObjectsByClass.TryGetValue(@class, out command))
            {
                var sql = this.database.Mapping.ProcedureNameForCreateObjectsByClass[@class];
                command = this.CreateSqlProcedureCommand(sql);
                command.CommandType = CommandType.StoredProcedure;
                this.AddTypeParameter(command, @class);
                this.AddCountParameter(command, count);
                this.createObjectsByClass[@class] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForType].Value = (object)@class.Id ?? DBNull.Value;
                command.Parameters[Mapping.ParamNameForCount].Value = (object)count ?? DBNull.Value;
            }

            var objectIds = new List<object>();
            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    object id = this.database.ObjectIds.Parse(reader[0].ToString());
                    objectIds.Add(id);
                }
            }

            var strategies = new List<Reference>();

            foreach (var id in objectIds)
            {
                var objectId = this.database.ObjectIds.Parse(id.ToString());
                var strategySql = this.CreateAssociationForNewObject(@class, objectId);
                strategies.Add(strategySql);
            }

            return strategies;
        }

        private Reference InsertObject(IClass @class, ObjectId objectId)
        {
            SqlCommand command;
            if (!this.InsertObjectByClass.TryGetValue(@class, out command))
            {
                var schema = this.Database.Mapping;

                // TODO: Make this a single pass Query.
                var sql = "IF EXISTS (\n";
                sql += "    SELECT " + Mapping.ColumnNameForObject + "\n";
                sql += "    FROM " + schema.TableNameForObjectByClass[@class.ExclusiveClass] + "\n";
                sql += "    WHERE " + Mapping.ColumnNameForObject + "=" + Mapping.ParamNameForObject + "\n";
                sql += ")\n";
                sql += "    SELECT 1\n";
                sql += "ELSE\n";
                sql += "    BEGIN\n";

                sql += "    SET IDENTITY_INSERT " + schema.TableNameForObjects + " ON\n";

                sql += "    INSERT INTO " + schema.TableNameForObjects + " (" + Mapping.ColumnNameForObject + "," + Mapping.ColumnNameForType + "," + Mapping.ColumnNameForCache + ")\n";
                sql += "    VALUES (" + Mapping.ParamNameForObject + "," + Mapping.ParamNameForType + ", " + Reference.InitialCacheId + ");\n";

                sql += "    SET IDENTITY_INSERT " + schema.TableNameForObjects + " OFF;\n";

                sql += "    INSERT INTO " + schema.TableNameForObjectByClass[@class.ExclusiveClass] + " (" + Mapping.ColumnNameForObject + "," + Mapping.ColumnNameForType + ")\n";
                sql += "    VALUES (" + Mapping.ParamNameForObject + "," + Mapping.ParamNameForType + ");\n";

                sql += "    SELECT 0;\n";
                sql += "    END";

                command = this.CreateSqlCommand(sql);
                this.AddObjectParameter(command, objectId);
                this.AddTypeParameter(command, @class);

                this.insertObjectByClass[@class] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForObject].Value = objectId.Value ?? DBNull.Value;
                command.Parameters[Mapping.ParamNameForType].Value = (object)@class.Id ?? DBNull.Value;
            }

            var result = command.ExecuteScalar();
            if (result == null)
            {
                throw new Exception("Reader returned no rows");
            }

            if (long.Parse(result.ToString()) > 0)
            {
                throw new Exception("Duplicate id error");
            }

            return this.CreateAssociationForNewObject(@class, objectId);
        }

        private Reference InstantiateObject(ObjectId objectId)
        {
            var command = this.instantiateObject;
            if (command == null)
            {
                var sql = "SELECT " + Mapping.ColumnNameForType + ", " + Mapping.ColumnNameForCache + "\n";
                sql += "FROM " + this.database.Mapping.TableNameForObjects + "\n";
                sql += "WHERE " + Mapping.ColumnNameForObject + "=" + Mapping.ParamNameForObject + "\n";

                command = this.CreateSqlCommand(sql);
                this.AddObjectParameter(command, objectId);
                this.instantiateObject = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForObject].Value = objectId.Value ?? DBNull.Value;
            }

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var classId = reader.GetGuid(0);
                    var cacheId = reader.GetInt32(1);

                    var type = (IClass)this.Database.MetaPopulation.Find(classId);
                    return this.GetOrCreateReferenceForExistingObject(type, objectId, cacheId);
                }

                return null;
            }
        }

        private IEnumerable<Reference> InstantiateObjects(List<ObjectId> objectIds)
        {
            var references = new List<Reference>();

            var command = this.instantiateObjects;
            if (command == null)
            {
                var sql = "SELECT " + Mapping.ColumnNameForObject + "," + Mapping.ColumnNameForType + "," + Mapping.ColumnNameForCache + "\n";
                sql += "FROM " + this.database.Mapping.TableNameForObjects + "\n";
                sql += "WHERE " + Mapping.ColumnNameForObject + " IN\n";
                sql += "( SELECT " + this.Database.Mapping.TableTypeColumnNameForObject + " FROM " + Mapping.ParamNameForTableType + " )\n";

                command = this.CreateSqlCommand(sql);
                this.AddObjectTableParameter(command, objectIds);

                this.instantiateObjects = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(objectIds);
            }

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var objectIdString = reader.GetValue(0).ToString();
                    var classId = reader.GetGuid(1);
                    var cacheId = reader.GetInt32(2);

                    var objectId = this.Database.ObjectIds.Parse(objectIdString);
                    var type = (IClass)this.Database.ObjectFactory.GetObjectTypeForType(classId);
                    var reference = this.GetOrCreateReferenceForExistingObject(type, objectId, cacheId);
                    references.Add(reference);
                }
            }

            return references;
        }

        private IClass GetClass(ObjectId objectId)
        {
            var command = this.getObjectType;
            if (command == null)
            {
                var sql = "SELECT " + Mapping.ColumnNameForType + "\n";
                sql += "FROM " + this.database.Mapping.TableNameForObjects + "\n";
                sql += "WHERE " + Mapping.ColumnNameForObject + "=" + Mapping.ParamNameForObject + "\n";

                command = this.CreateSqlCommand(sql);
                this.AddObjectParameter(command, objectId);

                this.getObjectType = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForObject].Value = objectId.Value ?? DBNull.Value;
            }

            var result = command.ExecuteScalar();
            if (result == null)
            {
                return null;
            }

            return (IClass)this.Database.ObjectFactory.GetObjectTypeForType((Guid)result);
        }

        private Dictionary<ObjectId, int> GetCacheIds(ISet<Reference> references)
        {
            var command = this.getCacheIds;

            if (command == null)
            {
                var sql = this.Database.Mapping.ProcedureNameForGetCache;
                command = this.CreateSqlProcedureCommand(sql);
                this.AddObjectTableParameter(command, references);
                this.getCacheIds = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(references);
            }

            var cacheIdByObjectId = new Dictionary<ObjectId, int>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var objectId = this.Database.ObjectIds.Parse(reader[0].ToString());
                    var cacheId = reader.GetInt32(1);

                    cacheIdByObjectId.Add(objectId, cacheId);
                }
            }

            return cacheIdByObjectId;
        }

        private void UpdateCacheIds()
        {
            var command = this.updateCacheIds;
            if (command == null)
            {
                var sql = this.Database.Mapping.ProcedureNameForUpdateCache;
                command = this.CreateSqlProcedureCommand(sql);
                this.AddObjectTableParameter(command, this.modifiedRolesByReference.Keys);
                this.updateCacheIds = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(this.modifiedRolesByReference.Keys);
            }

            command.ExecuteNonQuery();
        }

        #endregion

        private void ResetSqlCommands()
        {
            this.getUnitRolesByClass = null;
            this.prefetchUnitRolesByClass = null;
            this.setUnitRoleByRoleTypeByClass = null;

            this.getCompositeRoleByRoleType = null;
            this.setCompositeRoleByRoleType = null;
            this.prefetchCompositeRoleByRoleType = null;
            this.getCompositesRoleByRoleType = null;
            this.prefetchCompositesRoleByRoleType = null;
            this.addCompositeRoleByRoleType = null;
            this.removeCompositeRoleByRoleType = null;
            this.clearCompositeAndCompositesRoleByRoleType = null;
            this.getCompositeAssociationByAssociationType = null;
            this.getCompositesAssociationByAssociationType = null;
            this.prefetchCompositeAssociationByAssociationType = null;

            this.instantiateObject = null;
            this.instantiateObjects = null;
            this.setUnitRolesByRoleTypeByClass = null;
            this.createObjectByClass = null;
            this.createObjectsByClass = null;
            this.insertObjectByClass = null;
            this.deleteObjectByClass = null;

            this.getCacheIds = null;
            this.updateCacheIds = null;
            this.getObjectType = null;
        }

        private Reference GetOrCreateReferenceForExistingObject(IClass objectType, ObjectId objectId)
        {
            Reference reference;
            if (!this.referenceByObjectId.TryGetValue(objectId, out reference))
            {
                reference = new Reference(this, objectType, objectId, false);
                this.referenceByObjectId[objectId] = reference;
                this.referencesWithoutCacheId.Add(reference);
            }
            else
            {
                reference.Exists = true;
            }

            return reference;
        }

        private Reference GetOrCreateReferenceForExistingObject(IClass objectType, ObjectId objectId, int cacheId)
        {
            Reference reference;
            if (!this.referenceByObjectId.TryGetValue(objectId, out reference))
            {
                reference = new Reference(this, objectType, objectId, cacheId);
                this.referenceByObjectId[objectId] = reference;
            }
            else
            {
                reference.CacheId = cacheId;
                reference.Exists = true;
            }

            return reference;
        }

        private Reference CreateAssociationForNewObject(IClass objectType, ObjectId objectId)
        {
            var strategyReference = new Reference(this, objectType, objectId, true);
            this.referenceByObjectId[objectId] = strategyReference;
            return strategyReference;
        }

        private Dictionary<Reference, Reference> GetAssociationByRole(IAssociationType associationType)
        {
            Dictionary<Reference, Reference> associationByRole;
            if (!this.associationByRoleByAssociationType.TryGetValue(associationType, out associationByRole))
            {
                associationByRole = new Dictionary<Reference, Reference>();
                this.associationByRoleByAssociationType[associationType] = associationByRole;
            }

            return associationByRole;
        }

        private Dictionary<Reference, ObjectId[]> GetAssociationsByRole(IAssociationType associationType)
        {
            Dictionary<Reference, ObjectId[]> associationsByRole;
            if (!this.associationsByRoleByAssociationType.TryGetValue(associationType, out associationsByRole))
            {
                associationsByRole = new Dictionary<Reference, ObjectId[]>();
                this.associationsByRoleByAssociationType[associationType] = associationsByRole;
            }

            return associationsByRole;
        }

        private List<Reference> FilterForPrefetchRoles(List<Reference> associations, IRoleType roleType)
        {
            var references = new List<Reference>();

            var cache = this.Database.Cache;
            
            foreach (var association in associations)
            {
                object role;

                Roles roles;
                if (this.modifiedRolesByReference != null && this.modifiedRolesByReference.TryGetValue(association, out roles))
                {
                    if (roles.TryGetUnitRole(roleType, out role))
                    {
                        continue;
                    }
                }
                else
                {
                    if (!association.IsUnknownCacheId)
                    {
                        var cacheObject = cache.GetOrCreateCachedObject(association.Class, association.ObjectId, association.CacheId);
                        if (cacheObject.TryGetValue(roleType, out role))
                        {
                            continue;
                        }
                    }
                }

                references.Add(association);
            }

            return references;
        }

        private List<Reference> FilterForPrefetchCompositeRoles(List<Reference> associations, IRoleType roleType, List<ObjectId> nestedObjects)
        {
            var references = new List<Reference>();

            var cache = this.Database.Cache;

            foreach (var association in associations)
            {
                Roles roles;
                if (this.modifiedRolesByReference != null && this.modifiedRolesByReference.TryGetValue(association, out roles))
                {
                    ObjectId role;
                    if (roles.TryGetCompositeRole(roleType, out role))
                    {
                        if (role != null)
                        {
                            nestedObjects.Add(role);
                        }

                        continue;
                    }
                }
                else
                {
                    if (!association.IsUnknownCacheId)
                    {
                        var cacheObject = cache.GetOrCreateCachedObject(association.Class, association.ObjectId, association.CacheId);

                        object role;
                        if (cacheObject.TryGetValue(roleType, out role))
                        {
                            if (role != null)
                            {
                                nestedObjects.Add((ObjectId)role);
                            }

                            continue;
                        }
                    }
                }

                references.Add(association);
            }

            return references;
        }

        private List<Reference> FilterForPrefetchCompositesRoles(List<Reference> associations, IRoleType roleType, List<ObjectId> nestedObjects)
        {
            var references = new List<Reference>();

            var cache = this.Database.Cache;

            foreach (var association in associations)
            {
                Roles roles;
                if (this.modifiedRolesByReference != null && this.modifiedRolesByReference.TryGetValue(association, out roles))
                {
                    IEnumerable<ObjectId> role;
                    if (roles.TryGetCompositesRole(roleType, out role))
                    {
                        nestedObjects.AddRange(role);
                        continue;
                    }
                }
                else
                {
                    if (!association.IsUnknownCacheId)
                    {
                        var cacheObject = cache.GetOrCreateCachedObject(association.Class, association.ObjectId, association.CacheId);

                        object role;
                        if (cacheObject.TryGetValue(roleType, out role))
                        {
                            nestedObjects.AddRange((ObjectId[])role);
                            continue;
                        }
                    }
                }

                references.Add(association);
            }

            return references;
        }

        private List<Reference> FilterForPrefetchAssociations(List<Reference> roles, IAssociationType associationType)
        {
            Dictionary<Reference, Reference> associationByRole;
            if (!this.associationByRoleByAssociationType.TryGetValue(associationType, out associationByRole))
            {
                return roles;
            }

            return roles.Where(role => !associationByRole.ContainsKey(role)).ToList();
        }

        private List<Reference> FilterForPrefetchCompositeAssociations(List<Reference> roles, IAssociationType associationType, List<ObjectId> nestedObjectIds)
        {
            Dictionary<Reference, Reference> associationByRole;
            if (!this.associationByRoleByAssociationType.TryGetValue(associationType, out associationByRole))
            {
                return roles;
            }

            var references = new List<Reference>();
            foreach (var role in roles)
            {
                Reference association;
                if (associationByRole.TryGetValue(role, out association))
                {
                    nestedObjectIds.Add(association.ObjectId);
                    continue;
                }

                references.Add(role);
            }

            return references;
        }

        private List<Reference> FilterForPrefetchCompositesAssociations(List<Reference> roles, IAssociationType associationType, List<ObjectId> nestedObjectIds)
        {
            Dictionary<Reference, ObjectId[]> associationByRole;
            if (!this.associationsByRoleByAssociationType.TryGetValue(associationType, out associationByRole))
            {
                return roles;
            }

            var references = new List<Reference>();
            foreach (var role in roles)
            {
                ObjectId[] association;
                if (associationByRole.TryGetValue(role, out association))
                {
                    nestedObjectIds.AddRange(association);
                    continue;
                }

                references.Add(role);
            }

            return references;
        }
        
        private void FlushConditionally(ObjectId roleId, IAssociationType associationType)
        {
            if (this.triggersFlushRolesByAssociationType != null)
            {
                HashSet<ObjectId> roles;
                if (this.triggersFlushRolesByAssociationType.TryGetValue(associationType, out roles))
                {
                    if (roles.Contains(roleId))
                    {
                        this.Flush();
                    }
                }
            }
        }

        private SqlCommand CreateSqlCommand()
        {
            if (this.connection == null)
            {
                this.connection = new SqlConnection(this.Database.ConnectionString);
                this.connection.Open();
                this.transaction = this.connection.BeginTransaction(this.Database.IsolationLevel);
            }

            var command = this.connection.CreateCommand();
            command.Transaction = this.transaction;
            command.CommandTimeout = this.Database.CommandTimeout;
            return command;
        }

        private SqlCommand CreateSqlCommand(string commandText)
        {
            var command = this.CreateSqlCommand();
            command.CommandText = commandText;
            return command;
        }

        private SqlCommand CreateSqlProcedureCommand(string sql)
        {
            var command = this.CreateSqlCommand(sql);
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        private void AddObjectParameter(SqlCommand command, ObjectId objectId)
        {
            var sqlParameter = command.CreateParameter();
            sqlParameter.ParameterName = Mapping.ParamNameForObject;
            sqlParameter.SqlDbType = this.Database.Mapping.SqlDbTypeForObject;
            sqlParameter.Value = objectId.Value;

            command.Parameters.Add(sqlParameter);
        }

        private void AddTypeParameter(SqlCommand command, IClass @class)
        {
            var sqlParameter = command.CreateParameter();
            sqlParameter.ParameterName = Mapping.ParamNameForType;
            sqlParameter.SqlDbType = Mapping.SqlDbTypeForType;
            sqlParameter.Value = @class.Id;

            command.Parameters.Add(sqlParameter);
        }

        private void AddCountParameter(SqlCommand command, int count)
        {
            var sqlParameter = command.CreateParameter();
            sqlParameter.ParameterName = Mapping.ParamNameForCount;
            sqlParameter.SqlDbType = Mapping.SqlDbTypeForCount;
            sqlParameter.Value = count;

            command.Parameters.Add(sqlParameter);
        }

        private void AddCompositeRoleParameter(SqlCommand command, ObjectId objectId)
        {
            var sqlParameter = command.CreateParameter();
            sqlParameter.ParameterName = Mapping.ParamNameForCompositeRole;
            sqlParameter.SqlDbType = this.database.Mapping.SqlDbTypeForObject;
            sqlParameter.Value = objectId.Value;

            command.Parameters.Add(sqlParameter);
        }

        private void AddAssociationParameter(SqlCommand command, ObjectId objectId)
        {
            var sqlParameter = command.CreateParameter();
            sqlParameter.ParameterName = Mapping.ParamNameForAssociation;
            sqlParameter.SqlDbType = this.database.Mapping.SqlDbTypeForObject;
            sqlParameter.Value = objectId.Value;

            command.Parameters.Add(sqlParameter);
        }

        private void AddObjectTableParameter(SqlCommand command, IEnumerable<Reference> references)
        {
            var sqlParameter = command.CreateParameter();
            sqlParameter.SqlDbType = SqlDbType.Structured;
            sqlParameter.TypeName = this.Database.Mapping.TableTypeNameForObject;
            sqlParameter.ParameterName = Mapping.ParamNameForTableType;
            sqlParameter.Value = this.Database.CreateObjectTable(references);

            command.Parameters.Add(sqlParameter);
        }

        private void AddObjectTableParameter(SqlCommand command, IEnumerable<ObjectId> objectIds)
        {
            var sqlParameter = command.CreateParameter();
            sqlParameter.SqlDbType = SqlDbType.Structured;
            sqlParameter.TypeName = this.Database.Mapping.TableTypeNameForObject;
            sqlParameter.ParameterName = Mapping.ParamNameForTableType;
            sqlParameter.Value = this.Database.CreateObjectTable(objectIds);

            command.Parameters.Add(sqlParameter);
        }

        private void AddCompositeRoleTableParameter(SqlCommand command, IEnumerable<CompositeRelation> relations)
        {
            var sqlParameter = command.CreateParameter();
            sqlParameter.SqlDbType = SqlDbType.Structured;
            sqlParameter.TypeName = this.Database.Mapping.TableTypeNameForCompositeRelation;
            sqlParameter.ParameterName = Mapping.ParamNameForTableType;
            sqlParameter.Value = this.Database.CreateRelationTable(relations);

            command.Parameters.Add(sqlParameter);
        }

        private void AddAssociationTableParameter(SqlCommand command, ObjectId objectId)
        {
            var sqlParameter = command.CreateParameter();
            sqlParameter.ParameterName = Mapping.ParamNameForAssociation;
            sqlParameter.SqlDbType = this.Database.Mapping.SqlDbTypeForObject;
            sqlParameter.Value = objectId.Value;

            command.Parameters.Add(sqlParameter);
        }

        private void SqlCommit()
        {
            try
            {
                if (this.transaction != null)
                {
                    this.transaction.Commit();
                }
            }
            finally
            {
                this.transaction = null;
                if (this.connection != null)
                {
                    this.connection.Close();
                }

                this.connection = null;
            }
        }

        private void SqlRollback()
        {
            try
            {
                if (this.transaction != null)
                {
                    this.transaction.Rollback();
                }
            }
            finally
            {
                this.transaction = null;
                if (this.connection != null)
                {
                    this.connection.Close();
                }

                this.connection = null;
            }
        }

        private class SortedRoleTypeComparer : IEqualityComparer<IList<IRoleType>>
        {
            public bool Equals(IList<IRoleType> x, IList<IRoleType> y)
            {
                if (x.Count == y.Count)
                {
                    for (var i = 0; i < x.Count; i++)
                    {
                        if (!x[i].Equals(y[i]))
                        {
                            return false;
                        }
                    }

                    return true;
                }

                return false;
            }

            public int GetHashCode(IList<IRoleType> roleTypes)
            {
                var hashCode = 0;
                foreach (var roleType in roleTypes)
                {
                    hashCode = hashCode ^ roleType.GetHashCode();
                }

                return hashCode;
            }
        }
    }
}