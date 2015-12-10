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

namespace Allors.Adapters.Object.SqlClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Allors.Meta;

    public sealed class Session : ISession
    {
        private static readonly IObject[] EmptyObjects = { };

        private readonly Database database;

        public Connection Connection { get; }

        public SessionCommands Commands { get; }

        private ChangeSet changeSet;

        internal Dictionary<Reference, Roles> modifiedRolesByReference;
        internal Dictionary<ObjectId, Reference> referenceByObjectId;
        internal Dictionary<IAssociationType, Dictionary<Reference, Reference>> associationByRoleByAssociationType;
        internal Dictionary<IAssociationType, Dictionary<Reference, ObjectId[]>> associationsByRoleByAssociationType;

        private Dictionary<Reference, Roles> unflushedRolesByReference;
        private Dictionary<IAssociationType, HashSet<ObjectId>> triggersFlushRolesByAssociationType;

        private HashSet<ObjectId> existingObjectIdsWithoutReference;
        private HashSet<Reference> referencesWithoutCacheId;

        private bool busyCommittingOrRollingBack;

        private Dictionary<string, object> properties;

        internal Session(Database database, Connection connection)
        {
            this.database = database;
            this.Connection = connection;

            this.Prefetcher = new Prefetcher(this);
            this.Commands = new SessionCommands(this, connection);

            this.referenceByObjectId = new Dictionary<ObjectId, Reference>();

            this.existingObjectIdsWithoutReference = new HashSet<ObjectId>();
            this.referencesWithoutCacheId = new HashSet<Reference>();

            this.associationByRoleByAssociationType = new Dictionary<IAssociationType, Dictionary<Reference, Reference>>();
            this.associationsByRoleByAssociationType = new Dictionary<IAssociationType, Dictionary<Reference, ObjectId[]>>();

            this.changeSet = new ChangeSet();
        }

        internal Prefetcher Prefetcher { get; }

        IDatabase ISession.Database => this.database;

        public Database Database => this.database;

        internal ChangeSet ChangeSet => this.changeSet;

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

            var reference = this.Commands.CreateObject(objectType);
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

            var references = this.Commands.CreateObjects(objectType, count);

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

        public IObject Insert(IClass @class, string objectIdString)
        {
            var objectId = this.Database.ObjectIds.Parse(objectIdString);
            var insertedObject = this.Insert(@class, objectId);

            this.changeSet.OnCreated(objectId);

            return insertedObject;
        }

        public IObject Insert(IClass @class, ObjectId objectId)
        {
            if (this.referenceByObjectId.ContainsKey(objectId))
            {
                var oldStrategy = this.referenceByObjectId[objectId].Strategy;
                if (!oldStrategy.IsDeleted)
                {
                    throw new Exception("Duplicate object id " + objectId);
                }
            }

            var reference = this.Commands.InsertObject(@class, objectId);
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
            return strategy?.GetObject();
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
                reference = this.Commands.InstantiateObject(objectId);
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
            if (objectIdStrings == null || objectIdStrings.Length == 0)
            {
                return EmptyObjects;
            }

            var objectIds = new ObjectId[objectIdStrings.Length];
            for (var i = 0; i < objectIdStrings.Length; i++)
            {
                objectIds[i] = this.Database.ObjectIds.Parse(objectIdStrings[i]);
            }

            return this.Instantiate(objectIds);
        }

        public IObject[] Instantiate(IObject[] objects)
        {
            if (objects == null || objects.Length == 0)
            {
                return EmptyObjects;
            }

            var objectIds = new ObjectId[objects.Length];
            for (var i = 0; i < objects.Length; i++)
            {
                objectIds[i] = objects[i].Strategy.ObjectId;
            }

            return this.Instantiate(objectIds);
        }

        public IObject[] Instantiate(ObjectId[] objectIds)
        {
            if (objectIds == null || objectIds.Length == 0)
            {
                return EmptyObjects;
            }

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
                var nonCachedReferences = this.Commands.InstantiateObjects(nonCachedObjectIds);
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
            var references = this.Prefetcher.GetReferencesForPrefetching(objectIds);

            if (references.Count != 0)
            {
                this.Flush();

                var prefetcher = new Prefetching(this.Prefetcher, prefetchPolicy, references);
                prefetcher.Execute();
            }
        }

        public void Prefetch(PrefetchPolicy prefetchPolicy, params IStrategy[] strategies)
        {
            var objectIds = strategies.Select(x => x.ObjectId);
            var references = this.Prefetcher.GetReferencesForPrefetching(objectIds);

            if (references.Count != 0)
            {
                this.Flush();

                var prefetcher = new Prefetching(this.Prefetcher, prefetchPolicy, references);
                prefetcher.Execute();
            }
        }

        public void Prefetch(PrefetchPolicy prefetchPolicy, params string[] objectIdStrings)
        {
            var objectIds = objectIdStrings.Select(v => this.Database.ObjectIds.Parse(v));
            var references = this.Prefetcher.GetReferencesForPrefetching(objectIds);

            if (references.Count != 0)
            {
                this.Flush();

                var prefetcher = new Prefetching(this.Prefetcher, prefetchPolicy, references);
                prefetcher.Execute();
            }
        }

        public void Prefetch(PrefetchPolicy prefetchPolicy, params ObjectId[] objectIds)
        {
            var references = this.Prefetcher.GetReferencesForPrefetching(objectIds);

            if (references.Count != 0)
            {
                this.Flush();

                var prefetcher = new Prefetching(this.Prefetcher, prefetchPolicy, references);
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
                        this.Commands.UpdateCacheIds();

                        changed = this.modifiedRolesByReference.Select(dictionaryEntry => dictionaryEntry.Key.ObjectId).ToArray();
                    }

                    this.Connection.Commit();

                    this.modifiedRolesByReference = null;

                    var referencesWithStrategy = new HashSet<Reference>();
                    foreach (var reference in this.referenceByObjectId.Values)
                    {
                        reference.Commit(referencesWithStrategy);
                    }

                    this.existingObjectIdsWithoutReference = new HashSet<ObjectId>();
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

                    this.Prefetcher.ResetCommands();
                    this.Commands.ResetCommands();
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

                    this.Connection.Rollback();

                    var referencesWithStrategy = new HashSet<Reference>();
                    foreach (var reference in this.referenceByObjectId.Values)
                    {
                        reference.Rollback(referencesWithStrategy);
                    }

                    this.existingObjectIdsWithoutReference = new HashSet<ObjectId>();
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

                    this.Prefetcher.ResetCommands();
                    this.Commands.ResetCommands();
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
            return "Session[id=" + this.GetHashCode() + "] " + this.Database;
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
                    this.existingObjectIdsWithoutReference.Add(objectId);
                    this.Instantiate(this.existingObjectIdsWithoutReference.ToArray());
                    this.existingObjectIdsWithoutReference = new HashSet<ObjectId>();
                    this.referenceByObjectId.TryGetValue(objectId, out reference);
                }
                else
                {
                    reference = new Reference(this, objectType, objectId, false);
                    this.referenceByObjectId[objectId] = reference;
                    this.referencesWithoutCacheId.Add(reference);
                }
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
                association = this.Commands.GetCompositeAssociation(roleStrategy.Reference, associationType);
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
                associations = this.Commands.GetCompositesAssociation(roleStrategy, associationType);
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
                associationsByRole[role] = associations.Add(association.ObjectId);
            }
        }

        internal void RemoveAssociation(Reference association, Reference role, IAssociationType associationType)
        {
            var associationsByRole = this.GetAssociationsByRole(associationType);

            ObjectId[] associations;
            if (associationsByRole.TryGetValue(role, out associations))
            {
                associationsByRole[role] = associations.Remove(association.ObjectId);
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
                var cacheIdByObjectId = this.Commands.GetCacheIds(this.referencesWithoutCacheId);
                foreach (var association in this.referencesWithoutCacheId)
                {
                    long cacheId;
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

        internal Reference GetOrCreateReferenceForExistingObject(IClass objectType, ObjectId objectId)
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

        internal Reference GetOrCreateReferenceForExistingObject(IClass objectType, ObjectId objectId, long cacheId)
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

        internal Reference CreateReferenceForNewObject(IClass objectType, ObjectId objectId)
        {
            var strategyReference = new Reference(this, objectType, objectId, true);
            this.referenceByObjectId[objectId] = strategyReference;
            return strategyReference;
        }

        internal Dictionary<Reference, Reference> GetAssociationByRole(IAssociationType associationType)
        {
            Dictionary<Reference, Reference> associationByRole;
            if (!this.associationByRoleByAssociationType.TryGetValue(associationType, out associationByRole))
            {
                associationByRole = new Dictionary<Reference, Reference>();
                this.associationByRoleByAssociationType[associationType] = associationByRole;
            }

            return associationByRole;
        }

        internal Dictionary<Reference, ObjectId[]> GetAssociationsByRole(IAssociationType associationType)
        {
            Dictionary<Reference, ObjectId[]> associationsByRole;
            if (!this.associationsByRoleByAssociationType.TryGetValue(associationType, out associationsByRole))
            {
                associationsByRole = new Dictionary<Reference, ObjectId[]>();
                this.associationsByRoleByAssociationType[associationType] = associationsByRole;
            }

            return associationsByRole;
        }
        
        internal void FlushConditionally(ObjectId roleId, IAssociationType associationType)
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
    
    }
}