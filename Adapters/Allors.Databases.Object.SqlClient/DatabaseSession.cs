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
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;

    using Allors.Meta;

    internal class DatabaseSession : IDatabaseSession, ICommandFactory
    {
        private readonly Database database;

        private SqlConnection connection;
        private SqlTransaction transaction;

        private ChangeSet changeSet;

        private Dictionary<Reference, Roles> modifiedRolesByReference;
        private Dictionary<Reference, Roles> unflushedRolesByReference;
        private Dictionary<IAssociationType, HashSet<ObjectId>> triggersFlushRolesByIAssociationType;

        private Dictionary<ObjectId, Reference> referenceByObjectId;
        private HashSet<Reference> referencesWithoutCacheId;

        private Dictionary<IAssociationType, Dictionary<Reference, Reference>> associationByRoleByIAssociationType;
        private Dictionary<IAssociationType, Dictionary<Reference, ObjectId[]>> associationsByRoleByIAssociationType;

        private bool busyCommittingOrRollingBack;

        private Dictionary<string, object> properties;

        private Dictionary<IRoleType, SqlCommand> addCompositeRoleByRoleType;
        private Dictionary<IRoleType, SqlCommand> clearCompositeAndCompositesRoleByRoleType;
        private Dictionary<IClass, SqlCommand> createObjectByClass;
        private Dictionary<IClass, SqlCommand> createObjectsByClass;
        private SqlCommand getCacheIds;
        private SqlCommand updateCacheIds;
        private Dictionary<IAssociationType, SqlCommand> getCompositeAssociationByAssociationType;
        private Dictionary<IAssociationType, SqlCommand> getCompositeAssociationsByAssociationType;
        private Dictionary<IRoleType, SqlCommand> getCompositeRoleByRoleType;
        private Dictionary<IRoleType, SqlCommand> getCompositeRolesByRoleType;
        private Dictionary<IClass, SqlCommand> getUnitRolesByClass;
        private Dictionary<IRoleType, SqlCommand> removeCompositeRoleByRoleType;
        private Dictionary<IRoleType, SqlCommand> setCompositeRoleByRoleType;
        private Dictionary<IClass, Dictionary<IRoleType, SqlCommand>> setUnitRoleByIRoleTypeByIObjectType;
        private Dictionary<IClass, SqlCommand> deleteObjectByClass;
        private SqlCommand getObjectType;
        private Dictionary<IClass, SqlCommand> insertObjectByClass;
        private SqlCommand instantiateObject;
        private SqlCommand instantiateObjects;
        private Dictionary<IClass, Dictionary<IList<IRoleType>, SqlCommand>> commandByKeyBySortedRoleTypesObjectType;

        internal DatabaseSession(Database database)
        {
            this.database = database;

            this.referenceByObjectId = new Dictionary<ObjectId, Reference>();
            this.referencesWithoutCacheId = new HashSet<Reference>();

            this.associationByRoleByIAssociationType = new Dictionary<IAssociationType, Dictionary<Reference, Reference>>();
            this.associationsByRoleByIAssociationType = new Dictionary<IAssociationType, Dictionary<Reference, ObjectId[]>>();

            this.changeSet = new ChangeSet();
        }

        internal event SessionCommittingEventHandler Committing;

        internal event SessionCommittedEventHandler Committed;
        
        internal event SessionRollingBackEventHandler RollingBack;
        
        internal event SessionRolledBackEventHandler RolledBack;

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

        public virtual IObject Create(IClass objectType)
        {
            if (!objectType.IsClass)
            {
                throw new ArgumentException("Can not create non concrete composite type " + objectType);
            }

            var strategyReference = this.CreateObject(objectType);
            this.referenceByObjectId[strategyReference.ObjectId] = strategyReference;

            this.Database.Cache.SetObjectType(strategyReference.ObjectId, objectType);

            this.changeSet.OnCreated(strategyReference.ObjectId);

            return strategyReference.Strategy.GetObject();
        }

        public virtual IObject[] Create(IClass objectType, int count)
        {
            if (!objectType.IsClass)
            {
                throw new ArgumentException("Can not create non concrete composite type " + objectType);
            }

            var strategyReferences = this.CreateObjects(objectType, count);

            var arrayType = this.Database.ObjectFactory.GetTypeForObjectType(objectType);
            var domainObjects = (IObject[])Array.CreateInstance(arrayType, count);

            for (var i = 0; i < strategyReferences.Count; i++)
            {
                var strategyReference = strategyReferences[i];
                this.referenceByObjectId[strategyReference.ObjectId] = strategyReference;

                domainObjects[i] = strategyReference.Strategy.GetObject();

                this.changeSet.OnCreated(strategyReference.ObjectId);
            }

            return domainObjects;
        }

        public virtual IObject Insert(IClass domainType, string objectIdString)
        {
            var objectId = this.Database.ObjectIds.Parse(objectIdString);
            var insertedObject = this.Insert(domainType, objectId);

            this.changeSet.OnCreated(objectId);

            return insertedObject;
        }

        public virtual IObject Insert(IClass domainType, ObjectId objectId)
        {
            if (this.referenceByObjectId.ContainsKey(objectId))
            {
                var oldStrategy = this.referenceByObjectId[objectId].Strategy;
                if (!oldStrategy.IsDeleted)
                {
                    throw new Exception("Duplicate object id " + objectId);
                }
            }

            var strategyReference = this.InsertObject(domainType, objectId);
            this.referenceByObjectId[objectId] = strategyReference;
            var insertedObject = strategyReference.Strategy.GetObject();

            this.changeSet.OnCreated(objectId);

            return insertedObject;
        }

        public virtual IObject Instantiate(IObject obj)
        {
            return this.Instantiate(obj.Strategy.ObjectId);
        }

        public virtual IObject Instantiate(string objectId)
        {
            var id = this.Database.ObjectIds.Parse(objectId);
            return this.Instantiate(id);
        }

        public virtual IObject Instantiate(ObjectId objectId)
        {
            var strategyReference = this.InstantiateSqlStrategy(objectId);
            if (strategyReference == null)
            {
                return null;
            }

            return strategyReference.Strategy.GetObject();
        }

        public virtual IStrategy InstantiateStrategy(ObjectId objectId)
        {
            var strategyReference = this.InstantiateSqlStrategy(objectId);
            if (strategyReference == null)
            {
                return null;
            }

            return strategyReference.Strategy;
        }

        public virtual IObject[] Instantiate(string[] objectIdStrings)
        {
            var objectIds = new ObjectId[objectIdStrings.Length];
            for (var i = 0; i < objectIdStrings.Length; i++)
            {
                objectIds[i] = this.Database.ObjectIds.Parse(objectIdStrings[i]);
            }

            return this.Instantiate(objectIds);
        }

        public virtual IObject[] Instantiate(IObject[] objects)
        {
            var objectIds = new ObjectId[objects.Length];
            for (var i = 0; i < objects.Length; i++)
            {
                objectIds[i] = objects[i].Strategy.ObjectId;
            }

            return this.Instantiate(objectIds);
        }

        public virtual IObject[] Instantiate(ObjectId[] objectIds)
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

        public void Prefetch(ObjectId[] objectIds, IPropertyType[] propertyTypes)
        {
            var references = new List<Reference>();
            List<ObjectId> existsUnknown = null;

            foreach (var objectId in objectIds)
            {
                Reference reference;
                this.referenceByObjectId.TryGetValue(objectId, out reference);
                if (reference != null && reference.ExistsKnown)
                {
                    if (reference.Exists)
                    {
                        references.Add(reference);
                    }
                }
                else
                {
                    if (existsUnknown == null)
                    {
                        existsUnknown = new List<ObjectId>();
                    }

                    existsUnknown.Add(objectId);
                }
            }

            if (existsUnknown != null)
            {
                var existsUnknownReferences = this.InstantiateObjects(existsUnknown);
                references.AddRange(existsUnknownReferences);
            }

            var prefetcher = new Prefetcher(this, references, propertyTypes);
            prefetcher.Execute();
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

        public virtual Extent<T> Extent<T>() where T : IObject
        {
            return this.Extent((IComposite)this.Database.ObjectFactory.GetObjectTypeForType(typeof(T)));
        }

        public virtual Allors.Extent Extent(IComposite type)
        {
            return new ExtentFiltered(this, type);
        }

        public virtual Allors.Extent Union(Allors.Extent firstOperand, Allors.Extent secondOperand)
        {
            return new ExtentOperation(((Extent)firstOperand).ContainedInExtent, ((Extent)secondOperand).ContainedInExtent, ExtentOperations.Union);
        }

        public virtual Allors.Extent Intersect(Allors.Extent firstOperand, Allors.Extent secondOperand)
        {
            return new ExtentOperation(((Extent)firstOperand).ContainedInExtent, ((Extent)secondOperand).ContainedInExtent, ExtentOperations.Intersect);
        }

        public virtual Allors.Extent Except(Allors.Extent firstOperand, Allors.Extent secondOperand)
        {
            return new ExtentOperation(((Extent)firstOperand).ContainedInExtent, ((Extent)secondOperand).ContainedInExtent, ExtentOperations.Except);
        }

        public virtual void Commit()
        {
            if (!this.busyCommittingOrRollingBack)
            {
                try
                {
                    this.busyCommittingOrRollingBack = true;
                    if (this.Committing != null)
                    {
                        // Errors thrown in Committing event handlers 
                        // should have no effect on the current state of the Session.
                        this.Committing(this, new SessionCommittingEventArgs(this));
                    }

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

                    this.associationByRoleByIAssociationType = new Dictionary<IAssociationType, Dictionary<Reference, Reference>>();
                    this.associationsByRoleByIAssociationType = new Dictionary<IAssociationType, Dictionary<Reference, ObjectId[]>>();

                    this.changeSet = new ChangeSet();

                    this.busyCommittingOrRollingBack = false;

                    this.Database.Cache.OnCommit(accessed, changed);

                    this.ResetSqlCommands();
                }
                finally
                {
                    this.busyCommittingOrRollingBack = false;
                }

                if (this.Committed != null)
                {
                    this.Committed(this, new SessionCommittedEventArgs(this));
                }
            }
        }

        public virtual void Rollback()
        {
            if (!this.busyCommittingOrRollingBack)
            {
                try
                {
                    this.busyCommittingOrRollingBack = true;
                    if (this.RollingBack != null)
                    {
                        this.RollingBack(this, new SessionRollingBackEventArgs(this));
                    }

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
                    this.triggersFlushRolesByIAssociationType = null;

                    this.associationByRoleByIAssociationType = new Dictionary<IAssociationType, Dictionary<Reference, Reference>>();
                    this.associationsByRoleByIAssociationType = new Dictionary<IAssociationType, Dictionary<Reference, ObjectId[]>>();

                    this.changeSet = new ChangeSet();

                    this.Database.Cache.OnRollback(accessed);

                    this.ResetSqlCommands();
                }
                finally
                {
                    this.busyCommittingOrRollingBack = false;
                }

                if (this.RolledBack != null)
                {
                    this.RolledBack(this, new SessionRolledBackEventArgs(this));
                }
            }
        }

        public virtual void Dispose()
        {
            this.Rollback();
        }

        public virtual T Create<T>() where T : IObject
        {
            var objectType = (IClass)this.Database.ObjectFactory.GetObjectTypeForType(typeof(T));
            return (T)this.Create(objectType);
        }

        public virtual SqlCommand CreateSqlCommand(string commandText)
        {
            var command = this.CreateSqlCommand();
            command.CommandText = commandText;
            return command;
        }

        public override string ToString()
        {
            return "Session[id=" + this.GetHashCode() + "] " + this.Population;
        }

        internal virtual Reference GetAssociation(Strategy roleStrategy, IAssociationType associationType)
        {
            var associationByRole = this.GetAssociationByRole(associationType);

            Reference association;
            if (!associationByRole.TryGetValue(roleStrategy.Reference, out association))
            {
                this.FlushConditionally(roleStrategy, associationType);
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

        internal virtual ObjectId[] GetAssociations(Strategy roleStrategy, IAssociationType associationType)
        {
            var associationsByRole = this.GetAssociationsByRole(associationType);

            ObjectId[] associations;
            if (!associationsByRole.TryGetValue(roleStrategy.Reference, out associations))
            {
                this.FlushConditionally(roleStrategy, associationType);
                associations = this.GetCompositeAssociations(roleStrategy, associationType);
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

        internal virtual Reference[] GetOrCreateAssociationsForExistingObjects(IEnumerable<ObjectId> objectIds)
        {
            return objectIds.Select(this.GetOrCreateAssociationForExistingObject).ToArray();
        }

        internal virtual Reference GetOrCreateAssociationForExistingObject(ObjectId objectId)
        {
            Reference association;
            if (!this.referenceByObjectId.TryGetValue(objectId, out association))
            {
                var objectType = this.Database.Cache.GetObjectType(objectId);
                if (objectType == null)
                {
                    objectType = this.GetObjectType(objectId);
                    this.Database.Cache.SetObjectType(objectId, objectType);
                }

                association = this.CreateReference(objectType, objectId, false);
                this.referenceByObjectId[objectId] = association;
                this.referencesWithoutCacheId.Add(association);
            }

            return association;
        }

        internal virtual Reference GetOrCreateAssociationForExistingObject(IClass objectType, ObjectId objectId)
        {
            Reference association;
            if (!this.referenceByObjectId.TryGetValue(objectId, out association))
            {
                association = this.CreateReference(objectType, objectId, false);
                this.referenceByObjectId[objectId] = association;
                this.referencesWithoutCacheId.Add(association);
            }

            return association;
        }

        internal virtual Reference GetOrCreateAssociationForExistingObject(IClass objectType, ObjectId objectId, int cacheId)
        {
            Reference association;
            if (!this.referenceByObjectId.TryGetValue(objectId, out association))
            {
                association = this.CreateReference(objectType, objectId, cacheId);
                this.referenceByObjectId[objectId] = association;
            }

            return association;
        }

        internal virtual Reference CreateAssociationForNewObject(IClass objectType, ObjectId objectId)
        {
            var strategyReference = this.CreateReference(objectType, objectId, true);
            this.referenceByObjectId[objectId] = strategyReference;
            return strategyReference;
        }

        internal virtual Roles GetOrCreateRoles(Reference reference)
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

        internal virtual void Flush()
        {
            if (this.unflushedRolesByReference != null)
            {
                var flush = this.CreateFlush(this.unflushedRolesByReference);
                flush.Execute();
            }

            this.unflushedRolesByReference = null;
            this.triggersFlushRolesByIAssociationType = null;
        }

        internal virtual void FlushConditionally(Strategy strategy, IAssociationType associationType)
        {
            if (this.triggersFlushRolesByIAssociationType != null)
            {
                HashSet<ObjectId> roles;
                if (this.triggersFlushRolesByIAssociationType.TryGetValue(associationType, out roles))
                {
                    if (roles.Contains(strategy.ObjectId))
                    {
                        this.Flush();
                    }
                }
            }
        }

        internal void AddReferenceWithoutCacheId(Reference reference)
        {
            this.referencesWithoutCacheId.Add(reference);
        }

        protected internal virtual void GetCacheIdsAndExists()
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

        protected internal virtual void RequireFlush(Reference association, Roles roles)
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

        protected internal virtual void TriggerFlush(ObjectId role, IAssociationType associationType)
        {
            if (this.triggersFlushRolesByIAssociationType == null)
            {
                this.triggersFlushRolesByIAssociationType = new Dictionary<IAssociationType, HashSet<ObjectId>>();
            }

            HashSet<ObjectId> associations;
            if (!this.triggersFlushRolesByIAssociationType.TryGetValue(associationType, out associations))
            {
                associations = new HashSet<ObjectId>();
                this.triggersFlushRolesByIAssociationType[associationType] = associations;
            }

            associations.Add(role);
        }

        protected virtual void UpdateCacheIds()
        {
            this.UpdateCacheIds(this.modifiedRolesByReference);
        }

        protected virtual Reference CreateReference(IClass objectType, ObjectId objectId, bool isNew)
        {
            return new Reference(this, objectType, objectId, isNew);
        }

        protected virtual Reference CreateReference(IClass objectType, ObjectId objectId, int cacheId)
        {
            return new Reference(this, objectType, objectId, cacheId);
        }

        private Reference InstantiateSqlStrategy(ObjectId objectId)
        {
            if (objectId == null)
            {
                return null;
            }

            Reference strategyReference;
            if (!this.referenceByObjectId.TryGetValue(objectId, out strategyReference))
            {
                strategyReference = this.InstantiateObject(objectId);
                if (strategyReference != null)
                {
                    this.referenceByObjectId[objectId] = strategyReference;
                }
            }

            if (strategyReference == null || !strategyReference.Exists)
            {
                return null;
            }

            return strategyReference;
        }

        private Dictionary<Reference, Reference> GetAssociationByRole(IAssociationType associationType)
        {
            Dictionary<Reference, Reference> associationByRole;
            if (!this.associationByRoleByIAssociationType.TryGetValue(associationType, out associationByRole))
            {
                associationByRole = new Dictionary<Reference, Reference>();
                this.associationByRoleByIAssociationType[associationType] = associationByRole;
            }

            return associationByRole;
        }

        private Dictionary<Reference, ObjectId[]> GetAssociationsByRole(IAssociationType associationType)
        {
            Dictionary<Reference, ObjectId[]> associationsByRole;
            if (!this.associationsByRoleByIAssociationType.TryGetValue(associationType, out associationsByRole))
            {
                associationsByRole = new Dictionary<Reference, ObjectId[]>();
                this.associationsByRoleByIAssociationType[associationType] = associationsByRole;
            }

            return associationsByRole;
        }

        internal virtual SqlCommand CreateSqlCommand()
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
        
        internal Command CreateCommand(string commandText)
        {
            return new Command(this, commandText);
        }

        internal void AddCompositeRole(List<CompositeRelation> relations, IRoleType roleType)
        {
            this.addCompositeRoleByRoleType = this.addCompositeRoleByRoleType ?? new Dictionary<IRoleType, SqlCommand>();

            SqlCommand command;
            if (!this.addCompositeRoleByRoleType.TryGetValue(roleType, out command))
            {
                string sql;
                if (roleType.AssociationType.IsMany || !roleType.RelationType.ExistExclusiveClasses)
                {
                    sql = this.database.Mapping.ProcedureNameForAddRoleByRelationType[roleType.RelationType];
                }
                else
                {
                    sql = this.database.Mapping.ProcedureNameForAddRoleByRelationTypeByClass[((IComposite)roleType.ObjectType).ExclusiveClass][roleType.RelationType];
                }

                command = this.CreateSqlCommand(sql);
                command.CommandType = CommandType.StoredProcedure;
                var sqlParameter = command.CreateParameter();
                sqlParameter.SqlDbType = SqlDbType.Structured;
                sqlParameter.TypeName = this.database.Mapping.TableTypeNameForCompositeRelation;
                sqlParameter.ParameterName = Mapping.ParamNameForTableType;
                sqlParameter.Value = this.database.CreateRelationTable(relations);

                command.Parameters.Add(sqlParameter);

                this.addCompositeRoleByRoleType[roleType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.database.CreateRelationTable(relations);
            }

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        internal void ClearCompositeAndCompositesRole(IList<ObjectId> associations, IRoleType roleType)
        {
            this.clearCompositeAndCompositesRoleByRoleType = this.clearCompositeAndCompositesRoleByRoleType ?? new Dictionary<IRoleType, SqlCommand>();

            string sql;
            if ((roleType.IsMany && roleType.AssociationType.IsMany) || !roleType.RelationType.ExistExclusiveClasses)
            {
                sql = this.database.Mapping.ProcedureNameForClearRoleByRelationType[roleType.RelationType];
            }
            else
            {
                if (roleType.IsOne)
                {
                    sql = this.database.Mapping.ProcedureNameForClearRoleByRelationTypeByClass[roleType.AssociationType.ObjectType.ExclusiveClass][roleType.RelationType];
                }
                else
                {
                    sql = this.database.Mapping.ProcedureNameForClearRoleByRelationTypeByClass[((IComposite)roleType.ObjectType).ExclusiveClass][roleType.RelationType];
                }
            }

            SqlCommand command;
            if (!this.clearCompositeAndCompositesRoleByRoleType.TryGetValue(roleType, out command))
            {
                command = this.CreateSqlCommand(sql);
                command.CommandType = CommandType.StoredProcedure;
                var sqlParameter = command.CreateParameter();
                sqlParameter.SqlDbType = SqlDbType.Structured;
                sqlParameter.TypeName = this.database.Mapping.TableTypeNameForObject;
                sqlParameter.ParameterName = Mapping.ParamNameForTableType;
                sqlParameter.Value = this.database.CreateObjectTable(associations);

                command.Parameters.Add(sqlParameter);

                this.clearCompositeAndCompositesRoleByRoleType[roleType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.database.CreateObjectTable(associations);
            }

            command.ExecuteNonQuery();
        }

        internal void GetCompositeRole(Roles roles, IRoleType roleType)
        {
            this.getCompositeRoleByRoleType = this.getCompositeRoleByRoleType ?? new Dictionary<IRoleType, SqlCommand>();

            var reference = roles.Reference;

            SqlCommand command;
            if (!this.getCompositeRoleByRoleType.TryGetValue(roleType, out command))
            {
                IAssociationType associationType = roleType.AssociationType;

                string sql;
                if (!roleType.RelationType.ExistExclusiveClasses)
                {
                    sql = this.Database.Mapping.ProcedureNameForGetRoleByRelationType[roleType.RelationType];
                }
                else
                {
                    sql = this.Database.Mapping.ProcedureNameForGetRoleByRelationTypeByClass[associationType.ObjectType.ExclusiveClass][roleType.RelationType];
                }

                command = this.CreateSqlCommand(sql);
                command.CommandType = CommandType.StoredProcedure;
                var sqlParameter = command.CreateParameter();
                sqlParameter.ParameterName = Mapping.ParamNameForAssociation;
                sqlParameter.SqlDbType = this.Database.Mapping.SqlDbTypeForObject;
                sqlParameter.Value = reference.ObjectId.Value ?? DBNull.Value;

                command.Parameters.Add(sqlParameter);

                this.getCompositeRoleByRoleType[roleType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForAssociation].Value = reference.ObjectId.Value ?? DBNull.Value;
            }

            object result = command.ExecuteScalar();

            if (result == null || result == DBNull.Value)
            {
                roles.CachedObject.SetValue(roleType, null);
            }
            else
            {
                var objectId = this.Database.ObjectIds.Parse(result.ToString());
                roles.CachedObject.SetValue(roleType, objectId);
            }
        }

        internal void GetCompositeRoles(Roles roles, IRoleType roleType)
        {
            this.getCompositeRolesByRoleType = this.getCompositeRolesByRoleType ?? new Dictionary<IRoleType, SqlCommand>();

            var reference = roles.Reference;

            SqlCommand command;
            if (!this.getCompositeRolesByRoleType.TryGetValue(roleType, out command))
            {
                var associationType = roleType.AssociationType;

                string sql;
                if (associationType.IsMany || !roleType.RelationType.ExistExclusiveClasses)
                {
                    sql = this.Database.Mapping.ProcedureNameForGetRoleByRelationType[roleType.RelationType];
                }
                else
                {
                    sql = this.Database.Mapping.ProcedureNameForGetRoleByRelationTypeByClass[((IComposite)roleType.ObjectType).ExclusiveClass][roleType.RelationType];
                }
 
                command = this.CreateSqlCommand(sql);
                command.CommandType = CommandType.StoredProcedure;
                var sqlParameter = command.CreateParameter();
                sqlParameter.ParameterName = Mapping.ParamNameForAssociation;
                sqlParameter.SqlDbType = this.Database.Mapping.SqlDbTypeForObject;
                sqlParameter.Value = reference.ObjectId.Value ?? DBNull.Value;

                command.Parameters.Add(sqlParameter);

                this.getCompositeRolesByRoleType[roleType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForAssociation].Value = reference.ObjectId.Value ?? DBNull.Value;
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

        internal void GetUnitRoles(Roles roles)
        {
            this.getUnitRolesByClass = this.getUnitRolesByClass ?? new Dictionary<IClass, SqlCommand>();

            var reference = roles.Reference;
            var @class = reference.Class;

            SqlCommand command;
            if (!this.getUnitRolesByClass.TryGetValue(@class, out command))
            {
                var sql = this.Database.Mapping.ProcedureNameForGetUnitsByClass[@class];

                command = this.CreateSqlCommand(sql);
                command.CommandType = CommandType.StoredProcedure;
                var sqlParameter = command.CreateParameter();
                sqlParameter.ParameterName = Mapping.ParamNameForObject;
                sqlParameter.SqlDbType = this.Database.Mapping.SqlDbTypeForObject;
                sqlParameter.Value = reference.ObjectId.Value ?? DBNull.Value;

                command.Parameters.Add(sqlParameter);

                this.getUnitRolesByClass[@class] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForObject].Value = reference.ObjectId.Value ?? DBNull.Value;
            }

            using (DbDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var sortedUnitRoles = this.Database.GetSortedUnitRolesByIObjectType(reference.Class);

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

        internal void RemoveCompositeRole(List<CompositeRelation> relations, IRoleType roleType)
        {
            this.removeCompositeRoleByRoleType = this.removeCompositeRoleByRoleType ?? new Dictionary<IRoleType, SqlCommand>();

            SqlCommand command;
            if (!this.removeCompositeRoleByRoleType.TryGetValue(roleType, out command))
            {
                string sql;
                var associationType = roleType.AssociationType;

                if (associationType.IsMany || !roleType.RelationType.ExistExclusiveClasses)
                {
                    sql = this.Database.Mapping.ProcedureNameForRemoveRoleByRelationType[roleType.RelationType];
                }
                else
                {
                    sql = this.Database.Mapping.ProcedureNameForRemoveRoleByRelationTypeByClass[((IComposite)roleType.ObjectType).ExclusiveClass][roleType.RelationType];
                }

                command = this.CreateSqlCommand(sql);
                command.CommandType = CommandType.StoredProcedure;
                var sqlParameter = command.CreateParameter();
                sqlParameter.SqlDbType = SqlDbType.Structured;
                sqlParameter.TypeName = this.Database.Mapping.TableTypeNameForCompositeRelation;
                sqlParameter.ParameterName = Mapping.ParamNameForTableType;
                sqlParameter.Value = this.Database.CreateRelationTable(relations);

                command.Parameters.Add(sqlParameter);

                this.removeCompositeRoleByRoleType[roleType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateRelationTable(relations);
            }

            command.ExecuteNonQuery();
        }

        internal void SetCompositeRole(List<CompositeRelation> relations, IRoleType roleType)
        {
            this.setCompositeRoleByRoleType = this.setCompositeRoleByRoleType ?? new Dictionary<IRoleType, SqlCommand>();

            SqlCommand command;
            if (!this.setCompositeRoleByRoleType.TryGetValue(roleType, out command))
            {
                var associationType = roleType.AssociationType;

                string sql;
                if (!roleType.RelationType.ExistExclusiveClasses)
                {
                    sql = this.Database.Mapping.ProcedureNameForSetRoleByRelationType[roleType.RelationType];
                }
                else
                {
                    sql = this.Database.Mapping.ProcedureNameForSetRoleByRelationTypeByClass[associationType.ObjectType.ExclusiveClass][roleType.RelationType];
                }

                command = this.CreateSqlCommand(sql);
                command.CommandType = CommandType.StoredProcedure;
                var sqlParameter = command.CreateParameter();
                sqlParameter.SqlDbType = SqlDbType.Structured;
                sqlParameter.TypeName = this.Database.Mapping.TableTypeNameForCompositeRelation;
                sqlParameter.ParameterName = Mapping.ParamNameForTableType;
                sqlParameter.Value = this.Database.CreateRelationTable(relations);

                command.Parameters.Add(sqlParameter);
                this.setCompositeRoleByRoleType[roleType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateRelationTable(relations);
            }

            command.ExecuteNonQuery();
        }

        internal void SetUnitRole(List<UnitRelation> relations, IClass exclusiveRootClass, IRoleType roleType)
        {
            this.setUnitRoleByIRoleTypeByIObjectType = this.setUnitRoleByIRoleTypeByIObjectType ?? new Dictionary<IClass, Dictionary<IRoleType, SqlCommand>>();

            var schema = this.Database.Mapping;

            Dictionary<IRoleType, SqlCommand> commandByIRoleType;
            if (!this.setUnitRoleByIRoleTypeByIObjectType.TryGetValue(exclusiveRootClass, out commandByIRoleType))
            {
                commandByIRoleType = new Dictionary<IRoleType, SqlCommand>();
                this.setUnitRoleByIRoleTypeByIObjectType.Add(exclusiveRootClass, commandByIRoleType);
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
            if (!commandByIRoleType.TryGetValue(roleType, out command))
            {
                var sql = this.Database.Mapping.ProcedureNameForSetRoleByRelationTypeByClass[exclusiveRootClass][roleType.RelationType];

                command = this.CreateSqlCommand(sql);
                command.CommandType = CommandType.StoredProcedure;
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
            this.commandByKeyBySortedRoleTypesObjectType = this.commandByKeyBySortedRoleTypesObjectType ?? new Dictionary<IClass, Dictionary<IList<IRoleType>, SqlCommand>>();

            var exclusiveRootClass = roles.Reference.Class.ExclusiveClass;

            Dictionary<IList<IRoleType>, SqlCommand> commandByKey;
            if (!this.commandByKeyBySortedRoleTypesObjectType.TryGetValue(exclusiveRootClass, out commandByKey))
            {
                commandByKey = new Dictionary<IList<IRoleType>, SqlCommand>(new SortedIRoleTypesComparer());
                this.commandByKeyBySortedRoleTypesObjectType.Add(exclusiveRootClass, commandByKey);
            }

            SqlCommand command;
            if (!commandByKey.TryGetValue(sortedRoleTypes, out command))
            {
                command = this.CreateSqlCommand();

                var sqlParameter = command.CreateParameter();
                sqlParameter.ParameterName = Mapping.ParamNameForObject;
                sqlParameter.SqlDbType = this.Database.Mapping.SqlDbTypeForObject;
                sqlParameter.Value = roles.Reference.ObjectId.Value ?? DBNull.Value;

                command.Parameters.Add(sqlParameter);

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

                    var unit = roles.ModifiedRoleByIRoleType[roleType];
                    var sqlParameter1 = command.CreateParameter();
                    sqlParameter1.ParameterName = this.Database.Mapping.ParamNameByRoleType[roleType];
                    sqlParameter1.SqlDbType = this.Database.Mapping.GetSqlDbType(roleType);
                    sqlParameter1.Value = unit ?? DBNull.Value;

                    command.Parameters.Add(sqlParameter1);
                }

                sql.Append("\nWHERE " + Mapping.ColumnNameForObject + "=" + Mapping.ParamNameForObject + "\n");

                command.CommandText = sql.ToString();
                command.ExecuteNonQuery();

                commandByKey.Add(sortedRoleTypes, command);
            }
            else
            {
                command.Parameters[Mapping.ParamNameForObject].Value = roles.Reference.ObjectId.Value ?? DBNull.Value;

                foreach (var roleType in sortedRoleTypes)
                {
                    var column = this.Database.Mapping.ColumnNameByRelationType[roleType.RelationType];

                    var unit = roles.ModifiedRoleByIRoleType[roleType];
                    command.Parameters[this.Database.Mapping.ParamNameByRoleType[roleType]].Value = unit ?? DBNull.Value;
                }

                command.ExecuteNonQuery();
            }

        }

        internal void DeleteObject(Strategy strategy)
        {
            this.deleteObjectByClass = this.deleteObjectByClass ?? new Dictionary<IClass, SqlCommand>();
            
            var @class = strategy.Class;

            SqlCommand command;
            if (!this.deleteObjectByClass.TryGetValue(@class, out command))
            {
                var sql = String.Empty;

                sql += "BEGIN\n";

                sql += "DELETE FROM " + this.Database.Mapping.TableNameForObjects + "\n";
                sql += "WHERE " + Mapping.ColumnNameForObject + "=" + Mapping.ParamNameForObject + ";\n";

                sql += "DELETE FROM " + this.Database.Mapping.TableNameForObjectByClass[(@class).ExclusiveClass] + "\n";
                sql += "WHERE " + Mapping.ColumnNameForObject + "=" + Mapping.ParamNameForObject + ";\n";

                sql += "END;";
                
                command = this.CreateSqlCommand(sql);
                var sqlParameter = command.CreateParameter();
                sqlParameter.ParameterName = Mapping.ParamNameForObject;
                sqlParameter.SqlDbType = this.Database.Mapping.SqlDbTypeForObject;
                sqlParameter.Value = strategy.ObjectId.Value ?? DBNull.Value;

                command.Parameters.Add(sqlParameter);

                this.deleteObjectByClass[@class] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForObject].Value = strategy.ObjectId.Value ?? DBNull.Value;
            }

            command.ExecuteNonQuery();
        }

        private Reference CreateObject(IClass @class)
        {
            this.createObjectByClass = this.createObjectByClass ?? new Dictionary<IClass, SqlCommand>();

            SqlCommand command;
            if (!this.createObjectByClass.TryGetValue(@class, out command))
            {
                var sql = this.database.Mapping.ProcedureNameForCreateObjectByClass[@class];
                command = this.CreateSqlCommand(sql);
                command.CommandType = CommandType.StoredProcedure;
                var sqlParameter = command.CreateParameter();
                sqlParameter.ParameterName = Mapping.ParamNameForType;
                sqlParameter.SqlDbType = Mapping.SqlDbTypeForType;
                sqlParameter.Value = (object)@class.Id ?? DBNull.Value;

                command.Parameters.Add(sqlParameter);

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
            this.createObjectsByClass = this.createObjectsByClass ?? new Dictionary<IClass, SqlCommand>();

            SqlCommand command;
            if (!this.createObjectsByClass.TryGetValue(@class, out command))
            {
                var sql = this.database.Mapping.ProcedureNameForCreateObjectsByClass[@class];
                command = this.CreateSqlCommand(sql);
                command.CommandType = CommandType.StoredProcedure;
                var sqlParameter = command.CreateParameter();
                sqlParameter.ParameterName = Mapping.ParamNameForType;
                sqlParameter.SqlDbType = Mapping.SqlDbTypeForType;
                sqlParameter.Value = (object)@class.Id ?? DBNull.Value;

                command.Parameters.Add(sqlParameter);
                var sqlParameter1 = command.CreateParameter();
                sqlParameter1.ParameterName = Mapping.ParamNameForCount;
                sqlParameter1.SqlDbType = Mapping.SqlDbTypeForCount;
                sqlParameter1.Value = (object)count ?? DBNull.Value;

                command.Parameters.Add(sqlParameter1);

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

            foreach (object id in objectIds)
            {
                var objectId = this.database.ObjectIds.Parse(id.ToString());
                var strategySql = this.CreateAssociationForNewObject(@class, objectId);
                strategies.Add(strategySql);
            }

            return strategies;
        }

        private Dictionary<ObjectId, int> GetCacheIds(ISet<Reference> strategyReferences)
        {
            var command = this.getCacheIds;

            if (command == null)
            {
                var sql = this.Database.Mapping.ProcedureNameForGetCache;
                command = this.CreateSqlCommand(sql);
                command.CommandType = CommandType.StoredProcedure;
                var sqlParameter = command.CreateParameter();
                sqlParameter.SqlDbType = SqlDbType.Structured;
                sqlParameter.TypeName = this.Database.Mapping.TableTypeNameForObject;
                sqlParameter.ParameterName = Mapping.ParamNameForTableType;
                sqlParameter.Value = this.Database.CreateObjectTable(strategyReferences);

                command.Parameters.Add(sqlParameter);

                this.getCacheIds = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(strategyReferences);
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

        private Reference GetCompositeAssociation(Reference role, IAssociationType associationType)
        {
            this.getCompositeAssociationByAssociationType = this.getCompositeAssociationByAssociationType ?? new Dictionary<IAssociationType, SqlCommand>();

            Reference associationObject = null;

            SqlCommand command;
            if (!this.getCompositeAssociationByAssociationType.TryGetValue(associationType, out command))
            {
                var roleType = associationType.RoleType;

                string sql;
                if (associationType.RelationType.ExistExclusiveClasses)
                {
                    if (roleType.IsOne)
                    {
                        sql = this.database.Mapping.ProcedureNameForGetAssociationByRelationTypeByClass[associationType.ObjectType.ExclusiveClass][roleType.RelationType];
                    }
                    else
                    {
                        sql = this.database.Mapping.ProcedureNameForGetAssociationByRelationTypeByClass[((IComposite)roleType.ObjectType).ExclusiveClass][roleType.RelationType];
                    }
                }
                else
                {
                    sql = this.database.Mapping.ProcedureNameForGetAssociationByRelationType[roleType.RelationType];
                }

                command = this.CreateSqlCommand(sql);
                command.CommandType = CommandType.StoredProcedure;
                var sqlParameter = command.CreateParameter();
                sqlParameter.ParameterName = Mapping.ParamNameForRole;
                sqlParameter.SqlDbType = this.database.Mapping.SqlDbTypeForObject;
                sqlParameter.Value = role.ObjectId.Value ?? DBNull.Value;

                command.Parameters.Add(sqlParameter);

                this.getCompositeAssociationByAssociationType[associationType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForRole].Value = role.ObjectId.Value ?? DBNull.Value;
            }

            object result = command.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                var id = this.database.ObjectIds.Parse(result.ToString());

                if (associationType.ObjectType.ExistExclusiveClass)
                {
                    associationObject = this.GetOrCreateAssociationForExistingObject(associationType.ObjectType.ExclusiveClass, id);
                }
                else
                {
                    associationObject = this.GetOrCreateAssociationForExistingObject(id);
                }
            }

            return associationObject;
        }

        private ObjectId[] GetCompositeAssociations(Strategy role, IAssociationType associationType)
        {
            this.getCompositeAssociationsByAssociationType = this.getCompositeAssociationsByAssociationType ?? new Dictionary<IAssociationType, SqlCommand>();

            var roleType = associationType.RoleType;

            string sql;
            if (roleType.IsMany || !associationType.RelationType.ExistExclusiveClasses)
            {
                sql = this.Database.Mapping.ProcedureNameForGetAssociationByRelationType[roleType.RelationType];
            }
            else
            {
                sql = this.Database.Mapping.ProcedureNameForGetAssociationByRelationTypeByClass[associationType.ObjectType.ExclusiveClass][roleType.RelationType];
            }

            SqlCommand command;
            if (!this.getCompositeAssociationsByAssociationType.TryGetValue(associationType, out command))
            {
                command = this.CreateSqlCommand(sql);
                command.CommandType = CommandType.StoredProcedure;
                var sqlParameter = command.CreateParameter();
                sqlParameter.ParameterName = Mapping.ParamNameForRole;
                sqlParameter.SqlDbType = this.Database.Mapping.SqlDbTypeForObject;
                sqlParameter.Value = role.ObjectId.Value ?? DBNull.Value;

                command.Parameters.Add(sqlParameter);

                this.getCompositeAssociationsByAssociationType[associationType] = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForRole].Value = role.ObjectId.Value ?? DBNull.Value;
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

        private IClass GetObjectType(ObjectId objectId)
        {
            var command = this.getObjectType;
            if (command == null)
            {
                var sql = "SELECT " + Mapping.ColumnNameForType + "\n";
                sql += "FROM " + this.database.Mapping.TableNameForObjects + "\n";
                sql += "WHERE " + Mapping.ColumnNameForObject + "=" + Mapping.ParamNameForObject + "\n";

                command = this.CreateSqlCommand(sql);
                var sqlParameter = command.CreateParameter();
                sqlParameter.ParameterName = Mapping.ParamNameForObject;
                sqlParameter.SqlDbType = this.Database.Mapping.SqlDbTypeForObject;
                sqlParameter.Value = objectId.Value ?? DBNull.Value;

                command.Parameters.Add(sqlParameter);

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

        private Reference InsertObject(IClass @class, ObjectId objectId)
        {
            this.insertObjectByClass = this.insertObjectByClass ?? new Dictionary<IClass, SqlCommand>();

            SqlCommand command;
            if (!this.insertObjectByClass.TryGetValue(@class, out command))
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

                var sqlParameter = command.CreateParameter();
                sqlParameter.ParameterName = Mapping.ParamNameForObject;
                sqlParameter.SqlDbType = this.Database.Mapping.SqlDbTypeForObject;
                sqlParameter.Value = objectId.Value ?? DBNull.Value;

                command.Parameters.Add(sqlParameter);
                var sqlParameter1 = command.CreateParameter();
                sqlParameter1.ParameterName = Mapping.ParamNameForType;
                sqlParameter1.SqlDbType = Mapping.SqlDbTypeForType;
                sqlParameter1.Value = (object)@class.Id ?? DBNull.Value;

                command.Parameters.Add(sqlParameter1);

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

            if (Int64.Parse(result.ToString()) > 0)
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
                var sqlParameter = command.CreateParameter();
                sqlParameter.ParameterName = Mapping.ParamNameForObject;
                sqlParameter.SqlDbType = this.Database.Mapping.SqlDbTypeForObject;
                sqlParameter.Value = objectId.Value ?? DBNull.Value;

                command.Parameters.Add(sqlParameter);

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
                    return this.GetOrCreateAssociationForExistingObject(type, objectId, cacheId);
                }

                return null;
            }
        }

        private IEnumerable<Reference> InstantiateObjects(List<ObjectId> objectids)
        {
            var strategies = new List<Reference>();

            var command = this.instantiateObjects;
            if (command == null)
            {
                var sql = "SELECT " + Mapping.ColumnNameForObject + "," + Mapping.ColumnNameForType + "," + Mapping.ColumnNameForCache + "\n";
                sql += "FROM " + this.database.Mapping.TableNameForObjects + "\n";
                sql += "WHERE " + Mapping.ColumnNameForObject + " IN\n";
                sql += "( SELECT " + this.Database.Mapping.TableTypeColumnNameForObject + " FROM " + Mapping.ParamNameForTableType + " )\n";

                command = this.CreateSqlCommand(sql);
                var sqlParameter = command.CreateParameter();
                sqlParameter.SqlDbType = SqlDbType.Structured;
                sqlParameter.TypeName = this.Database.Mapping.TableTypeNameForObject;
                sqlParameter.ParameterName = Mapping.ParamNameForTableType;
                sqlParameter.Value = this.Database.CreateObjectTable(objectids);

                command.Parameters.Add(sqlParameter);

                this.instantiateObjects = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(objectids);
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
                    strategies.Add(this.GetOrCreateAssociationForExistingObject(type, objectId, cacheId));
                }
            }

            return strategies;
        }

        private void UpdateCacheIds(Dictionary<Reference, Roles> dictionary)
        {
            var command = this.updateCacheIds;
            if (command == null)
            {
                var sql = this.Database.Mapping.ProcedureNameForUpdateCache;
                command = this.CreateSqlCommand(sql);
                command.CommandType = CommandType.StoredProcedure;
                var sqlParameter = command.CreateParameter();
                sqlParameter.SqlDbType = SqlDbType.Structured;
                sqlParameter.TypeName = this.database.Mapping.TableTypeNameForObject;
                sqlParameter.ParameterName = Mapping.ParamNameForTableType;
                sqlParameter.Value = this.Database.CreateObjectTable(this.modifiedRolesByReference.Keys);

                command.Parameters.Add(sqlParameter);

                this.updateCacheIds = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.Database.CreateObjectTable(this.modifiedRolesByReference.Keys);
            }

            command.ExecuteNonQuery();
        }

        private Flush CreateFlush(Dictionary<Reference, Roles> unsyncedRolesByReference)
        {
            return new Flush(this, unsyncedRolesByReference);
        }

        private void ResetSqlCommands()
        {
            this.addCompositeRoleByRoleType = null;
            this.clearCompositeAndCompositesRoleByRoleType = null;
            this.createObjectByClass = null;
            this.createObjectsByClass = null;
            this.getCacheIds = null;
            this.getCompositeAssociationByAssociationType = null;
            this.getCompositeAssociationsByAssociationType = null;
            this.getCompositeRoleByRoleType = null;
            this.getCompositeRolesByRoleType = null;
            this.getUnitRolesByClass = null;
            this.removeCompositeRoleByRoleType = null;
            this.setCompositeRoleByRoleType = null;
            this.setUnitRoleByIRoleTypeByIObjectType = null;
            this.updateCacheIds = null;
            this.deleteObjectByClass = null;
            this.getObjectType = null;
            this.insertObjectByClass = null;
            this.instantiateObject = null;
            this.instantiateObjects = null;
            this.commandByKeyBySortedRoleTypesObjectType = null;
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

        private class SortedIRoleTypesComparer : IEqualityComparer<IList<IRoleType>>
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