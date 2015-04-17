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

    using Allors.Meta;

    internal class DatabaseSession : IDatabaseSession, ICommandFactory
    {
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

        internal event SessionCommittingEventHandler Committing;

        internal event SessionCommittedEventHandler Committed;

        internal event SessionRollingBackEventHandler RollingBack;

        internal event SessionRolledBackEventHandler RolledBack;

        public IPopulation Population
        {
            get
            {
                return this.Database;
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

        internal ChangeSet ChangeSet
        {
            get
            {
                return this.changeSet;
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

            this.SqlDatabase.Cache.SetObjectType(strategyReference.ObjectId, objectType);

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

            var arrayType = this.SqlDatabase.ObjectFactory.GetTypeForObjectType(objectType);
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
            var objectId = this.SqlDatabase.ObjectIds.Parse(objectIdString);
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

            var strategyReference = this.SessionCommands.InsertObjectCommand.Execute(domainType, objectId);
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
            var id = this.SqlDatabase.ObjectIds.Parse(objectId);
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
                objectIds[i] = this.SqlDatabase.ObjectIds.Parse(objectIdStrings[i]);
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
                var nonCachedReferences = this.SessionCommands.InstantiateObjectsCommand.Execute(nonCachedObjectIds);
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
            return this.Extent((IComposite)this.SqlDatabase.ObjectFactory.GetObjectTypeForType(typeof(T)));
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

                    this.SqlDatabase.Cache.OnCommit(accessed, changed);

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

                    this.SqlDatabase.Cache.OnRollback(accessed);

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
            var objectType = (IClass)this.SqlDatabase.ObjectFactory.GetObjectTypeForType(typeof(T));
            return (T)this.Create(objectType);
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
                associations = this.SessionCommands.GetCompositeAssociationsCommand.Execute(roleStrategy, associationType);
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
                var objectType = this.SqlDatabase.Cache.GetObjectType(objectId);
                if (objectType == null)
                {
                    objectType = this.SessionCommands.GetObjectType.Execute(objectId);
                    this.SqlDatabase.Cache.SetObjectType(objectId, objectType);
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

        public override string ToString()
        {
            return "Session[id=" + this.GetHashCode() + "] " + this.Population;
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
            this.SessionCommands.UpdateCacheIdsCommand.Execute(this.modifiedRolesByReference);
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
                strategyReference = this.SessionCommands.InstantiateObjectCommand.Execute(objectId);
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


















        private readonly Database database;

        private SqlConnection connection;
        private SqlTransaction transaction;

        private SessionCommands sessionCommands;

        internal DatabaseSession(Database database)
        {
            this.database = database;
        
            this.referenceByObjectId = new Dictionary<ObjectId, Reference>();
            this.referencesWithoutCacheId = new HashSet<Reference>();

            this.associationByRoleByIAssociationType = new Dictionary<IAssociationType, Dictionary<Reference, Reference>>();
            this.associationsByRoleByIAssociationType = new Dictionary<IAssociationType, Dictionary<Reference, ObjectId[]>>();

            this.changeSet = new ChangeSet();
        }

        public Allors.IDatabase Database
        {
            get { return this.database; }
        }

        internal Database SqlDatabase
        {
            get { return this.database; }
        }

        internal Database SqlClientDatabase
        {
            get { return this.database; }
        }

        internal SessionCommands SessionCommands
        {
            get
            {
                return this.sessionCommands ?? (this.sessionCommands = new SessionCommands(this));
            }
        }

        public virtual SqlCommand CreateSqlCommand(string commandText)
        {
            var command = this.CreateSqlCommand();
            command.CommandText = commandText;
            return command;
        }

        internal virtual SqlCommand CreateSqlCommand()
        {
            if (this.connection == null)
            {
                this.connection = new SqlConnection(this.SqlDatabase.ConnectionString);
                this.connection.Open();
                this.transaction = this.connection.BeginTransaction(this.SqlDatabase.IsolationLevel);
            }

            var command = this.connection.CreateCommand();
            command.Transaction = this.transaction;
            command.CommandTimeout = this.SqlDatabase.CommandTimeout;
            return command;
        }
        
        internal Command CreateCommand(string commandText)
        {
            return new Command(this, commandText);
        }

        protected Flush CreateFlush(Dictionary<Reference, Roles> unsyncedRolesByReference)
        {
            return new Flush(this, unsyncedRolesByReference);
        }

        protected void SqlCommit()
        {
            try
            {
                this.sessionCommands = null;
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

        protected void SqlRollback()
        {
            try
            {
                this.sessionCommands = null;
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




        private void ResetSqlCommands()
        {
            this.addCompositeRoleByRoleType = null;
            this.clearCompositeAndCompositesRoleByRoleType = null;
            this.createObjectByClass = null;
            this.createObjectsByClass = null;
            this.getCacheIds = null;
            this.getCompositeAssociationByAssociationType = null;
        }

        private Dictionary<IRoleType, SqlCommand> addCompositeRoleByRoleType;

        internal void AddCompositeRole(List<CompositeRelation> relations, IRoleType roleType)
        {
            this.addCompositeRoleByRoleType = this.addCompositeRoleByRoleType ?? new Dictionary<IRoleType, SqlCommand>();

            SqlCommand command;
            if (!this.addCompositeRoleByRoleType.TryGetValue(roleType, out command))
            {
                string sql;
                if (roleType.AssociationType.IsMany || !roleType.RelationType.ExistExclusiveLeafClasses)
                {
                    sql = this.database.Mapping.ProcedureNameForAddRoleByRelationType[roleType.RelationType];
                }
                else
                {
                    sql = this.database.Mapping.ProcedureNameForAddRoleByRelationTypeByClass[((IComposite)roleType.ObjectType).ExclusiveLeafClass][roleType.RelationType];
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

        private Dictionary<IRoleType, SqlCommand> clearCompositeAndCompositesRoleByRoleType;

        internal void ClearCompositeAndCompositesRole(IList<ObjectId> associations, IRoleType roleType)
        {
            this.clearCompositeAndCompositesRoleByRoleType = this.clearCompositeAndCompositesRoleByRoleType ?? new Dictionary<IRoleType, SqlCommand>();

            string sql;
            if ((roleType.IsMany && roleType.AssociationType.IsMany) || !roleType.RelationType.ExistExclusiveLeafClasses)
            {
                sql = this.database.Mapping.ProcedureNameForClearRoleByRelationType[roleType.RelationType];
            }
            else
            {
                if (roleType.IsOne)
                {
                    sql = this.database.Mapping.ProcedureNameForClearRoleByRelationTypeByClass[roleType.AssociationType.ObjectType.ExclusiveLeafClass][roleType.RelationType];
                }
                else
                {
                    sql = this.database.Mapping.ProcedureNameForClearRoleByRelationTypeByClass[((IComposite)roleType.ObjectType).ExclusiveLeafClass][roleType.RelationType];
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

        private Dictionary<IClass, SqlCommand> createObjectByClass;

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

        private Dictionary<IClass, SqlCommand> createObjectsByClass;

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

        private SqlCommand getCacheIds;

        Dictionary<ObjectId, int> GetCacheIds(ISet<Reference> strategyReferences)
        {
            var command = this.getCacheIds;
            if (command == null)
            {
                var sql = this.database.Mapping.ProcedureNameForGetCache;
                command = this.CreateSqlCommand(sql);
                command.CommandType = CommandType.StoredProcedure;

                var sqlParameter = command.CreateParameter();
                sqlParameter.SqlDbType = SqlDbType.Structured;
                sqlParameter.TypeName = this.database.Mapping.TableTypeNameForObject;
                sqlParameter.ParameterName = Mapping.ParamNameForTableType;
                sqlParameter.Value = this.database.CreateObjectTable(strategyReferences);

                this.getCacheIds = command;
            }
            else
            {
                command.Parameters[Mapping.ParamNameForTableType].Value = this.database.CreateObjectTable(strategyReferences);
            }

            var cacheIdByObjectId = new Dictionary<ObjectId, int>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var objectId = this.database.ObjectIds.Parse(reader[0].ToString());
                    var cacheId = reader.GetInt32(1);

                    cacheIdByObjectId.Add(objectId, cacheId);
                }
            }

            return cacheIdByObjectId;
        }

        private Dictionary<IAssociationType, SqlCommand> getCompositeAssociationByAssociationType;

        private Reference GetCompositeAssociation(Reference role, IAssociationType associationType)
        {
            this.getCompositeAssociationByAssociationType = this.getCompositeAssociationByAssociationType ?? new Dictionary<IAssociationType, SqlCommand>();

            Reference associationObject = null;

            SqlCommand command;
            if (!this.getCompositeAssociationByAssociationType.TryGetValue(associationType, out command))
            {
                var roleType = associationType.RoleType;

                string sql;
                if (associationType.RelationType.ExistExclusiveLeafClasses)
                {
                    if (roleType.IsOne)
                    {
                        sql = this.database.Mapping.ProcedureNameForGetAssociationByRelationTypeByClass[associationType.ObjectType.ExclusiveLeafClass][roleType.RelationType];
                    }
                    else
                    {
                        sql = this.database.Mapping.ProcedureNameForGetAssociationByRelationTypeByClass[((IComposite)roleType.ObjectType).ExclusiveLeafClass][roleType.RelationType];
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

                if (associationType.ObjectType.ExistExclusiveLeafClass)
                {
                    associationObject = this.GetOrCreateAssociationForExistingObject(associationType.ObjectType.ExclusiveLeafClass, id);
                }
                else
                {
                    associationObject = this.GetOrCreateAssociationForExistingObject(id);
                }
            }

            return associationObject;
        }

    }
}