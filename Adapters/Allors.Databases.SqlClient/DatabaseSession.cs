// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseSession.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Adapters.Database.SqlClient
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Allors.Adapters.Database.SqlClient.IntegerId;
    using Allors.Databases;
    using Allors.Meta;
    using Allors.Populations;

    public class DatabaseSession : IDatabaseSession
    {
        private static readonly ObjectId[] EmptyObjectIds = { };
        private static readonly IObject[] EmptyObjects = { };

        private readonly Database database;
        private readonly IRoleCache roleCache;
        private readonly IClassCache classCache;

        private SqlConnection connection;
        private SqlTransaction transaction;

        private Dictionary<string, object> properties;

        private Dictionary<ObjectId, int> cacheIdByObjectId;
        private Dictionary<ObjectId, IClass> classByObjectId;

        private HashSet<ObjectId> newObjects;
        private HashSet<ObjectId> deletedObjects;
        private HashSet<ObjectId> flushDeletedObjects;
        private HashSet<ObjectId> changedObjects;

        private ChangeSet changeSet;

        private Dictionary<IRoleType, Dictionary<ObjectId, object>> unitRoleByAssociationByRoleType;
        private Dictionary<IRoleType, HashSet<ObjectId>> unitFlushAssociationsByRoleType;

        private Dictionary<IRoleType, Dictionary<ObjectId, ObjectId>> oneToOneRoleByAssociationByRoleType;
        private Dictionary<IRoleType, HashSet<ObjectId>> oneToOneFlushAssociationsByRoleType;
        private Dictionary<IAssociationType, Dictionary<ObjectId, ObjectId>> oneToOneAssociationByRoleByAssociationType;

        private Dictionary<IRoleType, Dictionary<ObjectId, ObjectId>> manyToOneRoleByAssociationByRoleType;
        private Dictionary<IRoleType, HashSet<ObjectId>> manyToOneFlushAssociationsByRoleType;
        private Dictionary<IAssociationType, Dictionary<ObjectId, ObjectId[]>> manyToOneAssociationByRoleByAssociationType;
        private Dictionary<IAssociationType, HashSet<ObjectId>> manyToOneTriggerFlushRolesByAssociationType;

        private Dictionary<IRoleType, Dictionary<ObjectId, ObjectId[]>> oneToManyCurrentRoleByAssociationByRoleType;
        private Dictionary<IRoleType, Dictionary<ObjectId, ObjectId[]>> oneToManyOriginalRoleByAssociationByRoleType;
        private Dictionary<IAssociationType, Dictionary<ObjectId, ObjectId>> oneToManyAssociationByRoleByAssociationType;

        private Dictionary<IRoleType, Dictionary<ObjectId, ObjectId[]>> manyToManyCurrentRoleByAssociationByRoleType;
        private Dictionary<IRoleType, Dictionary<ObjectId, ObjectId[]>> manyToManyOriginalRoleByAssociationByRoleType;
        private Dictionary<IAssociationType, Dictionary<ObjectId, ObjectId[]>> manyToManyAssociationByRoleByAssociationType;
        private Dictionary<IAssociationType, HashSet<ObjectId>> manyToManyTriggerFlushRolesByAssociationType;

        private HashSet<ObjectId> objectsToFetch;

        public DatabaseSession(Database database)
        {
            this.database = database;
            this.roleCache = database.RoleCache;
            this.classCache = database.ClassCache;
        }

        IPopulation ISession.Population 
        {
            get
            {
                return this.database;
            }
        }

        IDatabase IDatabaseSession.Database 
        {
            get
            {
                return this.database;
            }
        }

        public Database Database
        {
            get
            {
                return this.database;
            }
        }

        internal ChangeSet ChangeSet
        {
            get
            {
                return this.changeSet ?? (this.changeSet = new ChangeSet());
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
                    if (this.properties.Count == 0)
                    {
                        this.properties = null;
                    }
                }
                else
                {
                    this.properties[name] = value;
                }
            }
        }

        public IChangeSet Checkpoint()
        {
            try
            {
                return this.ChangeSet;
            }
            finally
            {
                this.changeSet = null;
            }
        }

        public Extent<T> Extent<T>() where T : IObject
        {
            return this.Extent((IComposite)this.Database.ObjectFactory.GetObjectTypeForType(typeof(T)));
        }

        public Extent Extent(IComposite objectType)
        {
            return new AllorsExtentFilteredSql(this, objectType);
        }

        public Extent Except(Extent firstOperand, Extent secondOperand)
        {
            return new AllorsExtentOperationSql((AllorsExtentSql)firstOperand, (AllorsExtentSql)secondOperand, AllorsExtentOperationTypeSqlBundled.EXCEPT);
        }

        public Extent Intersect(Extent firstOperand, Extent secondOperand)
        {
            return new AllorsExtentOperationSql((AllorsExtentSql)firstOperand, (AllorsExtentSql)secondOperand, AllorsExtentOperationTypeSqlBundled.INTERSECT);
        }

        public Extent Union(Extent firstOperand, Extent secondOperand)
        {
            return new AllorsExtentOperationSql((AllorsExtentSql)firstOperand, (AllorsExtentSql)secondOperand, AllorsExtentOperationTypeSqlBundled.UNION);
        }

        public void Commit()
        {
            try
            {
                this.Flush();
                this.UpdateCacheIds();

                // update class cache
                if (this.deletedObjects != null && this.deletedObjects.Count > 0)
                {
                    var deletedObjectsArray = new List<ObjectId>(this.deletedObjects).ToArray();
                    this.roleCache.Invalidate(deletedObjectsArray);
                    this.classCache.Invalidate(deletedObjectsArray);
                }

                // update role cache
                if (this.changedObjects != null && this.changedObjects.Count > 0)
                {
                    var changedObjectsArray = new List<ObjectId>(this.changedObjects).ToArray();
                    this.roleCache.Invalidate(changedObjectsArray);
                }

                this.Reset();

                if (this.transaction != null)
                {
                    this.transaction.Commit();
                    this.transaction = null;
                }
            }
            finally
            {
                this.LazyDisconnect();
            }
        }

        public void Rollback()
        {
            try
            {
                this.Reset();
            }
            finally
            {
                this.LazyDisconnect();
            }
        }

        public T Create<T>() where T : IObject
        {
            var objectType = this.Database.ObjectFactory.GetObjectTypeForType(typeof(T));
            var @class = objectType as IClass;
            if (@class == null)
            {
                throw new Exception("IObjectType is not a Class");
            }

            return (T)this.Create(@class);
        }

        public IObject Create(IClass objectType)
        {
            using (var command = this.CreateCommand(this.Database.SchemaName + "." + Mapping.ProcedureNameForCreateObject))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(Mapping.ParameterNameForType, Mapping.SqlDbTypeForType).Value = objectType.Id;
                command.Parameters.Add(Mapping.ParameterNameForCache, Mapping.SqlDbTypeForCache).Value = Database.CacheDefaultValue;

                var result = command.ExecuteScalar().ToString();
                
                var objectId = this.database.ObjectIds.Parse(result);

                if (this.cacheIdByObjectId == null)
                {
                    this.cacheIdByObjectId = new Dictionary<ObjectId, int>();
                }

                if (this.classByObjectId == null)
                {
                    this.classByObjectId = new Dictionary<ObjectId, IClass>();
                }

                this.cacheIdByObjectId[objectId] = Database.CacheDefaultValue;
                this.classByObjectId[objectId] = objectType;

                this.ChangeSet.OnCreated(objectId);
                var strategy = new Strategy(this, objectId);
                var domainObject = strategy.GetObject();

                return domainObject;
            }
        }
        
        public IObject[] Create(IClass objectType, int count)
        {
            var arrayType = this.Database.ObjectFactory.GetTypeForObjectType(objectType);
            var domainObjects = (IObject[])Array.CreateInstance(arrayType, count);

            using (var command = this.CreateCommand(this.Database.SchemaName + "." + Mapping.ProcedureNameForCreateObjects))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(Mapping.ParameterNameForCount, Mapping.SqlTypeForCount).Value = count;
                command.Parameters.Add(Mapping.ParameterNameForType, Mapping.SqlDbTypeForType).Value = objectType.Id;
                command.Parameters.Add(Mapping.ParameterNameForCache, Mapping.SqlDbTypeForCache).Value = Database.CacheDefaultValue;

                if (this.cacheIdByObjectId == null)
                {
                    this.cacheIdByObjectId = new Dictionary<ObjectId, int>();
                }

                if (this.classByObjectId == null)
                {
                    this.classByObjectId = new Dictionary<ObjectId, IClass>();
                }

                using (var reader = command.ExecuteReader())
                {
                    var i = 0;
                    while (reader.Read())
                    {
                        var objectId = this.database.ObjectIds.Parse(reader[0].ToString());

                        this.classByObjectId[objectId] = objectType;
                        this.cacheIdByObjectId[objectId] = Database.CacheDefaultValue;

                        this.ChangeSet.OnCreated(objectId);
                        var strategy = new Strategy(this, objectId);
                        var domainObject = strategy.GetObject();
                        domainObjects[i++] = domainObject;
                    }
                }
            }

            return domainObjects;
        }

        public IObject Instantiate(IObject obj)
        {
            return this.Instantiate(obj.Id);
        }

        public IObject Instantiate(string objectId)
        {
            return this.Instantiate(this.Database.ObjectIds.Parse(objectId));
        }

        public IObject Instantiate(ObjectId objectId)
        {
            var strategy = this.InstantiateStrategy(objectId);
            return strategy != null ? strategy.GetObject() : null;
        }

        public IObject[] Instantiate(IObject[] objects)
        {
            var objectIds = new List<ObjectId>(objects.Length);
            foreach (var obj in objects)
            {
                if (obj != null)
                {
                    objectIds.Add(obj.Id);
                }
            }

            return this.Instantiate(objectIds.ToArray());
        }

        public IObject[] Instantiate(string[] objectIdStrings)
        {
            var objectIds = new List<ObjectId>(objectIdStrings.Length);
            foreach (var objectIdString in objectIdStrings)
            {
                if (objectIdString != null)
                {
                    objectIds.Add(this.Database.ObjectIds.Parse(objectIdString));
                }
            }

            return this.Instantiate(objectIds.ToArray());
        }

        public IObject[] Instantiate(ObjectId[] objectIds)
        {
            var strategies = this.InstantiateStrategies(objectIds);
            if (strategies == null || strategies.Length == 0)
            {
                return EmptyObjects;
            }

            var objects = new IObject[strategies.Length];
            for (var i = 0; i < objects.Length; i++)
            {
                objects[i] = strategies[i].GetObject();
            }

            return objects;
        }

        public IObject Insert(IClass objectType, string objectIdString)
        {
             var objectId = this.Database.ObjectIds.Parse(objectIdString);
             var insertedObject = this.Insert(objectType, objectId);

            return insertedObject;
        }

        public IObject Insert(IClass objectType, ObjectId objectId)
        {
            if (this.flushDeletedObjects != null)
            {
                this.Flush();
            }

            var mapping = this.Database.Mapping;
            var cmdText = @"
BEGIN
SET IDENTITY_INSERT " + this.Database.SchemaName + "." + Mapping.TableNameForObjects + @" ON;
INSERT INTO " + this.Database.SchemaName + "." + Mapping.TableNameForObjects + " (" + Mapping.ColumnNameForObject + ", " + Mapping.ColumnNameForType + ", " + Mapping.ColumnNameForCache + @")
VALUES (" + Mapping.ParameterNameForObject + ", " + Mapping.ParameterNameForType + @", " + Mapping.ParameterNameForCache + @");
SET IDENTITY_INSERT " + this.Database.SchemaName + "." + Mapping.TableNameForObjects + @" OFF;
END
";
            using (var command = this.CreateCommand(this.Database.SchemaName + "." + Mapping.ProcedureNameForInsertObject))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(Mapping.ParameterNameForObject, mapping.SqlDbTypeForObject).Value = objectId.Value;
                command.Parameters.Add(Mapping.ParameterNameForType, Mapping.SqlDbTypeForType).Value = objectType.Id;
                command.Parameters.Add(Mapping.ParameterNameForCache, Mapping.SqlDbTypeForCache).Value = Database.CacheDefaultValue;
                command.ExecuteNonQuery();

                if (this.cacheIdByObjectId == null)
                {
                    this.cacheIdByObjectId = new Dictionary<ObjectId, int>();
                }

                if (this.classByObjectId == null)
                {
                    this.classByObjectId = new Dictionary<ObjectId, IClass>();
                }

                this.cacheIdByObjectId[objectId] = Database.CacheDefaultValue;
                this.classByObjectId[objectId] = objectType;

                this.ChangeSet.OnCreated(objectId);
                var strategy = new Strategy(this, objectId);
                var domainObject = strategy.GetObject();

                return domainObject;
            }
        }

        IStrategy IDatabaseSession.InstantiateStrategy(ObjectId objectId)
        {
            return this.InstantiateStrategy(objectId);
        }

        public void Dispose()
        {
            this.Rollback();
        }

        internal IClass GetObjectType(ObjectId objectId)
        {
            return this.classByObjectId[objectId];
        }

        internal bool IsNew(ObjectId objectId)
        {
            return this.newObjects != null && this.newObjects.Contains(objectId);
        }

        internal bool IsDeleted(ObjectId objectId)
        {
            if (this.deletedObjects != null && this.deletedObjects.Contains(objectId))
            {
                return true;
            }

            if (this.cacheIdByObjectId != null && this.cacheIdByObjectId.ContainsKey(objectId))
            {
                return false;
            }

            this.AddObjectToFetch(objectId);            
            this.FetchObjects();

            if (this.deletedObjects != null)
            {
                if (this.deletedObjects.Contains(objectId))
                {
                    return true;
                }
            }

            return false;
        }

        internal void Delete(ObjectId objectId)
        {
            if (this.deletedObjects == null)
            {
                this.deletedObjects = new HashSet<ObjectId>();
            }

            if (this.flushDeletedObjects == null)
            {
                this.flushDeletedObjects = new HashSet<ObjectId>();
            }

            this.deletedObjects.Add(objectId);
            this.flushDeletedObjects.Add(objectId);

            this.ChangeSet.OnDeleted(objectId);
        }
        
        #region Unit
        internal bool ExistUnitRole(ObjectId association, IRoleType roleType)
        {
            return this.GetUnitRole(association, roleType) != null;
        }

        internal virtual object GetUnitRole(ObjectId association, IRoleType roleType)
        {
            var roleByAssociation = this.GetUnitRoleByAssociation(roleType);

            object role;
            if (!roleByAssociation.TryGetValue(association, out role))
            {
                var cacheId = this.GetCacheId(association);
                if (!this.roleCache.TryGetUnit(association, cacheId, roleType, out role))
                {
                    role = this.FetchUnitRole(association, roleType);
                }
                
                roleByAssociation[association] = role;
            }

            return role;
        }

        internal virtual void SetUnitRole(ObjectId association, IRoleType roleType, object role)
        {
            roleType.Normalize(role);

            var existingRole = this.GetUnitRole(association, roleType);
            if (!Equals(role, existingRole))
            {
                var roleByAssociation = this.GetUnitRoleByAssociation(roleType);
                var flushAssociations = this.GetUnitFlushAssociations(roleType);

                roleByAssociation[association] = role;
                flushAssociations.Add(association);

                this.ChangeSet.OnChangingUnitRole(association, roleType);
                this.OnObjectChanged(association);
            }
        }

        internal void RemoveUnitRole(ObjectId association, IRoleType roleType)
        {
            var existingRole = this.GetUnitRole(association, roleType);
            if (existingRole != null)
            {
                var roleByAssociation = this.GetUnitRoleByAssociation(roleType);
                var flushAssociations = this.GetUnitFlushAssociations(roleType);

                roleByAssociation[association] = null;
                flushAssociations.Add(association);

                this.ChangeSet.OnChangingUnitRole(association, roleType);
                this.OnObjectChanged(association);
            }
        }

        #endregion

        #region One <-> One
        internal bool ExistCompositeRoleOneToOne(ObjectId association, IRoleType roleType)
        {
            return this.GetCompositeRoleOneToOne(association, roleType) != null;
        }

        internal ObjectId GetCompositeRoleOneToOne(ObjectId association, IRoleType roleType)
        {
            var roleByAssociation = this.GetOneToOneRoleByAssociation(roleType);

            ObjectId role;
            if (!roleByAssociation.TryGetValue(association, out role))
            {
                var cacheId = this.GetCacheId(association);
                if (!this.roleCache.TryGetComposite(association, cacheId, roleType, out role))
                {
                    role = this.FetchCompositeRole(association, roleType);
                }
                
                roleByAssociation[association] = role;
            }

            return role;
        }

        internal void SetCompositeRoleOneToOne(ObjectId association, IRoleType roleType, ObjectId role)
        {
            var existingRole = this.GetCompositeRoleOneToOne(association, roleType);
            if (!Equals(role, existingRole))
            {
                var roleByAssociation = this.GetOneToOneRoleByAssociation(roleType);
                var flushAssociations = this.GetOneToOneFlushAssociations(roleType);

                var associationByRole = this.GetOneToOneAssociationByRole(roleType.AssociationType);

                // Existing Association -> Role
                ObjectId existingAssociation;
                if (!associationByRole.TryGetValue(role, out existingAssociation))
                {
                    var existingAssociationFound = false;
                    foreach (var kvp in roleByAssociation)
                    {
                        if (role.Equals(kvp.Value))
                        {
                            existingAssociation = kvp.Key;
                            existingAssociationFound = true;
                            break;
                        }
                    }

                    if (!existingAssociationFound)
                    {
                        existingAssociation = this.GetCompositeAssociationOneToOne(role, roleType.AssociationType);
                    }
                }

                if (existingAssociation != null)
                {
                    roleByAssociation[existingAssociation] = null;
                    this.OnObjectChanged(existingAssociation);
                }
                
                // Association <- Existing Role
                if (existingRole != null)
                {
                    associationByRole[existingRole] = null;
                }
                
                // Association <- Role
                associationByRole[role] = association;
                
                // Association -> Role
                roleByAssociation[association] = role;
                flushAssociations.Add(association);

                this.ChangeSet.OnChangingCompositeRole(association, roleType, existingRole, role);
                this.OnObjectChanged(association);
            }
        }

        internal void RemoveCompositeRoleOneToOne(ObjectId association, IRoleType roleType)
        {
            var existingRole = this.GetCompositeRoleOneToOne(association, roleType);
            if (existingRole != null)
            {
                var roleByAssociation = this.GetOneToOneRoleByAssociation(roleType);
                var associationByRole = this.GetOneToOneAssociationByRole(roleType.AssociationType);

                // Association <- Role
                associationByRole[existingRole] = null;

                // Association -> Role
                roleByAssociation[association] = null;

                var flushAssociations = this.GetOneToOneFlushAssociations(roleType);
                flushAssociations.Add(association);

                this.ChangeSet.OnChangingCompositeRole(association, roleType, existingRole, null);
                this.OnObjectChanged(association);
            }
        }

        internal bool ExistCompositeAssociationOneToOne(ObjectId role, IAssociationType associationType)
        {
            return this.GetCompositeAssociationOneToOne(role, associationType) != null;
        }

        internal ObjectId GetCompositeAssociationOneToOne(ObjectId role, IAssociationType associationType)
        {
            var associationByRole = this.GetOneToOneAssociationByRole(associationType);
            ObjectId association;
            if (!associationByRole.TryGetValue(role, out association))
            {
                association = this.FetchCompositeAssociation(role, associationType);
                associationByRole[role] = association;
            }

            return association;
        }
        #endregion

        #region Many <-> One
        internal bool ExistCompositeRoleManyToOne(ObjectId association, IRoleType roleType)
        {
            return this.GetCompositeRoleManyToOne(association, roleType) != null;
        }

        internal ObjectId GetCompositeRoleManyToOne(ObjectId association, IRoleType roleType)
        {
            var roleByAssociation = this.GetManyToOneRoleByAssociation(roleType);

            ObjectId role;
            if (!roleByAssociation.TryGetValue(association, out role))
            {
                var cacheId = this.GetCacheId(association);
                if (!this.roleCache.TryGetComposite(association, cacheId, roleType, out role))
                {
                    role = this.FetchCompositeRole(association, roleType);
                }

                roleByAssociation[association] = role;
            }

            return role;
        }

        internal void SetCompositeRoleManyToOne(ObjectId association, IRoleType roleType, ObjectId role)
        {
            var existingRole = this.GetCompositeRoleManyToOne(association, roleType);
            if (!Equals(role, existingRole))
            {
                var roleByAssociation = this.GetManyToOneRoleByAssociation(roleType);
                var flushAssociations = this.GetManyToOneFlushAssociations(roleType);

                var associationByRole = this.GetManyToOneAssociationByRole(roleType.AssociationType);
                var triggerFlushRoles = this.GetManyToOneTriggerFlushRoles(roleType.AssociationType);

                // Association <- Existing Role
                if (existingRole != null)
                {
                    associationByRole.Remove(existingRole);
                    triggerFlushRoles.Add(existingRole);
                }

                // Association <- Role
                associationByRole.Remove(role);
                triggerFlushRoles.Add(role);

                // Association -> Role
                roleByAssociation[association] = role;
                flushAssociations.Add(association);

                this.ChangeSet.OnChangingCompositeRole(association, roleType, existingRole, role);
                this.OnObjectChanged(association);
            }
        }

        internal void RemoveCompositeRoleManyToOne(ObjectId association, IRoleType roleType)
        {
            var existingRole = this.GetCompositeRoleManyToOne(association, roleType);
            if (existingRole != null)
            {
                var roleByAssociation = this.GetManyToOneRoleByAssociation(roleType);
                var flushAssociations = this.GetManyToOneFlushAssociations(roleType);

                var associationByRole = this.GetManyToOneAssociationByRole(roleType.AssociationType);
                var triggerFlushRoles = this.GetManyToOneTriggerFlushRoles(roleType.AssociationType);

                // Association <- Existing Role
                ObjectId[] existingAssociations;
                if (associationByRole.TryGetValue(existingRole, out existingAssociations))
                {
                    var associations = new List<ObjectId>(existingAssociations);
                    associations.Remove(association);
                    associationByRole[existingRole] = associations.Count != 0 ? associations.ToArray() : null;
                }
                else
                {
                    triggerFlushRoles.Add(existingRole);
                }

                // Association -> Role
                roleByAssociation[association] = null;
                flushAssociations.Add(association);

                this.ChangeSet.OnChangingCompositeRole(association, roleType, existingRole, null);
                this.OnObjectChanged(association);
            }
        }

        internal bool ExistCompositeAssociationsManyToOne(ObjectId role, IAssociationType associationType)
        {
            return this.GetCompositeAssociationsManyToOne(role, associationType).Length > 0;
        }

        internal ObjectId[] GetCompositeAssociationsManyToOne(ObjectId role, IAssociationType associationType)
        {
            if (this.manyToOneTriggerFlushRolesByAssociationType != null)
            {
                HashSet<ObjectId> triggerFlushRoles;
                if (this.manyToOneTriggerFlushRolesByAssociationType.TryGetValue(associationType, out triggerFlushRoles))
                {
                    if (triggerFlushRoles.Contains(role))
                    {
                        this.FlushManyToOne(associationType.RoleType);
                        this.manyToOneTriggerFlushRolesByAssociationType.Remove(associationType);
                    }
                }
            }

            var associationByRole = this.GetManyToOneAssociationByRole(associationType);
            ObjectId[] associations;
            if (!associationByRole.TryGetValue(role, out associations))
            {
                associations = this.FetchCompositeAssociations(role, associationType);
                associationByRole[role] = associations;
            }

            return associations ?? EmptyObjectIds;
        }
        #endregion

        #region One <-> Many
        internal bool ExistCompositeRoleOneToMany(ObjectId association, IRoleType roleType)
        {
            return this.GetCompositeRolesOneToMany(association, roleType).Length > 0;
        }

        internal ObjectId[] GetCompositeRolesOneToMany(ObjectId association, IRoleType roleType)
        {
            var roleByAssociation = this.GetOneToManyCurrentRoleByAssociation(roleType);

            ObjectId[] role;
            if (!roleByAssociation.TryGetValue(association, out role))
            {
                var cacheId = this.GetCacheId(association);
                if (!this.roleCache.TryGetComposites(association, cacheId, roleType, out role))
                {
                    role = this.FetchCompositeRoles(association, roleType);
                }

                roleByAssociation[association] = role;
            }

            return role;
        }

        internal void AddCompositeRoleOneToMany(ObjectId association, IRoleType roleType, ObjectId role)
        {
            var existingRoleArray = this.GetCompositeRolesOneToMany(association, roleType);
            var existingRoles = new List<ObjectId>(existingRoleArray);
            if (!existingRoles.Contains(role))
            {
                var currentRoleByAssociation = this.GetOneToManyCurrentRoleByAssociation(roleType);
                var originalRoleByAssociation = this.GetOneToManyOriginalRoleByAssociation(roleType);

                var associationByRole = this.GetOneToManyAssociationByRole(roleType.AssociationType);

                // Existing Association -> Role
                ObjectId existingAssociation;
                if (!associationByRole.TryGetValue(role, out existingAssociation))
                {
                    var existingAssociationFound = false;
                    foreach (var kvp in currentRoleByAssociation)
                    {
                        if (role.Equals(kvp.Value))
                        {
                            existingAssociation = kvp.Key;
                            existingAssociationFound = true;
                            break;
                        }
                    }

                    if (!existingAssociationFound)
                    {
                        existingAssociation = this.GetCompositeAssociationOneToOne(role, roleType.AssociationType);
                    }
                }

                if (existingAssociation != null)
                {
                    ObjectId[] rolesOfExistingAssociation;
                    if (currentRoleByAssociation.TryGetValue(existingAssociation, out rolesOfExistingAssociation))
                    {
                        var newRolesOfExistingAssociation = new List<ObjectId>(rolesOfExistingAssociation);
                        newRolesOfExistingAssociation.Remove(role);
                        currentRoleByAssociation[existingAssociation] = newRolesOfExistingAssociation.ToArray();
                    }

                    this.OnObjectChanged(existingAssociation);
                }

                // Association <- Role
                associationByRole[role] = association;

                // Association -> Roles
                if (!originalRoleByAssociation.ContainsKey(association))
                {
                    originalRoleByAssociation[association] = existingRoleArray;
                }
                
                existingRoles.Add(role);
                currentRoleByAssociation[association] = existingRoles.ToArray();

                this.ChangeSet.OnChangingCompositesRole(association, roleType, role);
                this.OnObjectChanged(association);
            }
        }

        internal void RemoveCompositeRoleOneToMany(ObjectId association, IRoleType roleType, ObjectId role)
        {
            var existingRoleArray = this.GetCompositeRolesOneToMany(association, roleType);
            var existingRoles = new List<ObjectId>(existingRoleArray);
            if (existingRoles.Contains(role))
            {
                var currentRoleByAssociation = this.GetOneToManyCurrentRoleByAssociation(roleType);
                var originalRoleByAssociation = this.GetOneToManyOriginalRoleByAssociation(roleType);

                var associationByRole = this.GetOneToManyAssociationByRole(roleType.AssociationType);

                // Association <- Role
                associationByRole[role] = null;

                // Association -> Roles
                if (!originalRoleByAssociation.ContainsKey(association))
                {
                    originalRoleByAssociation[association] = existingRoleArray;
                }

                existingRoles.Remove(role);
                currentRoleByAssociation[association] = existingRoles.ToArray();

                this.ChangeSet.OnChangingCompositesRole(association, roleType, role);
                this.OnObjectChanged(association);
            }
        }

        internal void SetCompositeRoleOneToMany(ObjectId association, IRoleType roleType, ObjectId[] roles)
        {
            // TODO: optimize
            var existingRoles = new HashSet<ObjectId>(this.GetCompositeRolesOneToMany(association, roleType));

            var addRoles = new HashSet<ObjectId>(roles);
            addRoles.ExceptWith(existingRoles);
            
            existingRoles.ExceptWith(roles);
            var removeRoles = existingRoles;

            foreach (var role in addRoles)
            {
                this.AddCompositeRoleOneToMany(association, roleType, role);
            }

            foreach (var role in removeRoles)
            {
                this.RemoveCompositeRoleOneToMany(association, roleType, role);
            }
        }

        internal void RemoveCompositeRolesOneToMany(ObjectId association, IRoleType roleType)
        {
            // TODO: Optimize
            foreach (var role in this.GetCompositeRolesOneToMany(association, roleType))
            {
                this.RemoveCompositeRoleOneToMany(association, roleType, role);
            }
        }

        internal bool ExistCompositeAssociationOneToMany(ObjectId role, IAssociationType associationType)
        {
            return this.GetCompositeAssociationOneToMany(role, associationType) != null;
        }

        // TODO: Merge with GetCompositeAssociationOneToOne
        internal ObjectId GetCompositeAssociationOneToMany(ObjectId role, IAssociationType associationType)
        {
            var associationByRole = this.GetOneToManyAssociationByRole(associationType);
            ObjectId association;
            if (!associationByRole.TryGetValue(role, out association))
            {
                association = this.FetchCompositeAssociation(role, associationType);
                associationByRole[role] = association;
            }

            return association;
        }
        #endregion

        #region Many <-> Many
        internal bool ExistCompositeRoleManyToMany(ObjectId association, IRoleType roleType)
        {
            return this.GetCompositeRolesManyToMany(association, roleType).Length > 0;
        }

        internal ObjectId[] GetCompositeRolesManyToMany(ObjectId association, IRoleType roleType)
        {
            var roleByAssociation = this.GetManyToManyCurrentRoleByAssociation(roleType);

            ObjectId[] role;
            if (!roleByAssociation.TryGetValue(association, out role))
            {
                role = this.FetchCompositeRoles(association, roleType);
                roleByAssociation[association] = role;
            }

            return role;
        }

        internal void AddCompositeRoleManyToMany(ObjectId association, IRoleType roleType, ObjectId role)
        {
            var existingRoleArray = this.GetCompositeRolesManyToMany(association, roleType);
            var existingRoles = new List<ObjectId>(existingRoleArray);
            if (!existingRoles.Contains(role))
            {
                var originalRoleByAssociation = this.GetManyToManyOriginalRoleByAssociation(roleType);
                var currentRoleByAssociation = this.GetManyToManyCurrentRoleByAssociation(roleType);

                var triggerFlushRoles = this.GetManyToManyTriggerFlushRoles(roleType.AssociationType);
                var associationByRole = this.GetManyToManyAssociationByRole(roleType.AssociationType);

                // Association <- Role
                triggerFlushRoles.Add(role);
                associationByRole.Remove(role);

                // Association -> Roles
                if (!originalRoleByAssociation.ContainsKey(association))
                {
                    originalRoleByAssociation[association] = existingRoleArray;
                }

                existingRoles.Add(role);
                currentRoleByAssociation[association] = existingRoles.ToArray();

                this.ChangeSet.OnChangingCompositesRole(association, roleType, role);
                this.OnObjectChanged(association);
            }
        }

        internal void RemoveCompositeRoleManyToMany(ObjectId association, IRoleType roleType, ObjectId role)
        {
            var existingRoleArray = this.GetCompositeRolesManyToMany(association, roleType);
            var existingRoles = new List<ObjectId>(existingRoleArray);
            if (existingRoles.Contains(role))
            {
                var originalRoleByAssociation = this.GetManyToManyOriginalRoleByAssociation(roleType);
                var currentRoleByAssociation = this.GetManyToManyCurrentRoleByAssociation(roleType);

                var triggerFlushRoles = this.GetManyToManyTriggerFlushRoles(roleType.AssociationType);
                var associationByRole = this.GetManyToManyAssociationByRole(roleType.AssociationType);

                // Association <- Role
                triggerFlushRoles.Add(role);
                associationByRole.Remove(role);

                // Association -> Roles
                if (!originalRoleByAssociation.ContainsKey(association))
                {
                    originalRoleByAssociation[association] = existingRoleArray;
                }

                existingRoles.Remove(role);
                currentRoleByAssociation[association] = existingRoles.ToArray();

                this.ChangeSet.OnChangingCompositesRole(association, roleType, role);
                this.OnObjectChanged(association);
            }
        }

        internal void SetCompositeRoleManyToMany(ObjectId association, IRoleType roleType, ObjectId[] roles)
        {
            // TODO: optimize
            var existingRoles = new HashSet<ObjectId>(this.GetCompositeRolesManyToMany(association, roleType));

            var addRoles = new HashSet<ObjectId>(roles);
            addRoles.ExceptWith(existingRoles);

            existingRoles.ExceptWith(roles);
            var removeRoles = existingRoles;

            foreach (var role in addRoles)
            {
                this.AddCompositeRoleManyToMany(association, roleType, role);
            }

            foreach (var role in removeRoles)
            {
                this.RemoveCompositeRoleManyToMany(association, roleType, role);
            }
        }

        internal void RemoveCompositeRolesManyToMany(ObjectId association, IRoleType roleType)
        {
            // TODO: Optimize
            foreach (var role in this.GetCompositeRolesManyToMany(association, roleType))
            {
                this.RemoveCompositeRoleManyToMany(association, roleType, role);
            }
        }

        internal bool ExistCompositeAssociationsManyToMany(ObjectId role, IAssociationType associationType)
        {
            return this.GetCompositeAssociationsManyToMany(role, associationType).Length > 0;
        }

        internal ObjectId[] GetCompositeAssociationsManyToMany(ObjectId role, IAssociationType associationType)
        {
            if (this.manyToManyTriggerFlushRolesByAssociationType != null)
            {
                HashSet<ObjectId> triggerFlushRoles;
                if (this.manyToManyTriggerFlushRolesByAssociationType.TryGetValue(associationType, out triggerFlushRoles))
                {
                    if (triggerFlushRoles.Contains(role))
                    {
                        this.FlushManyToMany(associationType.RoleType);
                        this.manyToManyTriggerFlushRolesByAssociationType.Remove(associationType);
                    }
                }
            }

            var associationByRole = this.GetManyToManyAssociationByRole(associationType);
            ObjectId[] associations;
            if (!associationByRole.TryGetValue(role, out associations))
            {
                associations = this.FetchCompositeAssociations(role, associationType);
                associationByRole[role] = associations;
            }

            return associations ?? EmptyObjectIds;
        }
        #endregion

        internal SqlCommand CreateCommand(string cmdText)
        {
            if (this.connection == null)
            {
                this.connection = new SqlConnection(this.Database.ConnectionString);
                this.connection.Open();
                this.transaction = this.connection.BeginTransaction(this.Database.IsolationLevel);
            }

            var command = new SqlCommand(cmdText, this.connection, this.transaction)
            {
                CommandTimeout = this.Database.CommandTimeout
            };
            return command;
        }

        internal void Flush()
        {
            if (this.unitFlushAssociationsByRoleType != null)
            {
                foreach (var roleType in new List<IRoleType>(this.unitFlushAssociationsByRoleType.Keys))
                {
                    this.FlushUnit(roleType);
                }
            }

            if (this.oneToOneFlushAssociationsByRoleType != null)
            {
                foreach (var roleType in new List<IRoleType>(this.oneToOneFlushAssociationsByRoleType.Keys))
                {
                    this.FlushOneToOne(roleType);
                }
            }

            if (this.manyToOneFlushAssociationsByRoleType != null)
            {
                foreach (var roleType in new List<IRoleType>(this.manyToOneFlushAssociationsByRoleType.Keys))
                {
                    this.FlushManyToOne(roleType);
                }
            }

            if (this.oneToManyOriginalRoleByAssociationByRoleType != null)
            {
                foreach (var roleType in new List<IRoleType>(this.oneToManyOriginalRoleByAssociationByRoleType.Keys))
                {
                    this.FlushOneToMany(roleType);
                }
            }

            if (this.manyToManyOriginalRoleByAssociationByRoleType != null)
            {
                foreach (var roleType in new List<IRoleType>(this.manyToManyOriginalRoleByAssociationByRoleType.Keys))
                {
                    this.FlushManyToMany(roleType);
                }
            }

            this.FlushDeletedObjects();
        }

        private static void SplitFlushAssociations(IEnumerable<ObjectId> flushAssociations, IReadOnlyDictionary<ObjectId, object> roleByAssociation, out IList<ObjectId> flushDeleted, out IList<ObjectId> flushChanged)
        {
            IList<ObjectId> deleted = null;
            IList<ObjectId> changed = null;
            foreach (var flushAssociation in flushAssociations)
            {
                var unit = roleByAssociation[flushAssociation];

                if (unit == null)
                {
                    if (deleted == null)
                    {
                        deleted = new List<ObjectId>();
                    }

                    deleted.Add(flushAssociation);
                }
                else
                {
                    if (changed == null)
                    {
                        changed = new List<ObjectId>();
                    }

                    changed.Add(flushAssociation);
                }
            }

            flushDeleted = deleted;
            flushChanged = changed;
        }

        private static void SplitFlushAssociations(IEnumerable<ObjectId> flushAssociations, IReadOnlyDictionary<ObjectId, ObjectId> roleByAssociation, out IList<ObjectId> flushDeleted, out IList<ObjectId> flushChanged)
        {
            IList<ObjectId> deleted = null;
            IList<ObjectId> changed = null;
            foreach (var flushAssociation in flushAssociations)
            {
                var unit = roleByAssociation[flushAssociation];

                if (unit == null)
                {
                    if (deleted == null)
                    {
                        deleted = new List<ObjectId>();
                    }

                    deleted.Add(flushAssociation);
                }
                else
                {
                    if (changed == null)
                    {
                        changed = new List<ObjectId>();
                    }

                    changed.Add(flushAssociation);
                }
            }

            flushDeleted = deleted;
            flushChanged = changed;
        }

        private int GetCacheId(ObjectId objectId)
        {
            int cacheId;
            if (this.cacheIdByObjectId == null || !this.cacheIdByObjectId.TryGetValue(objectId, out cacheId))
            {
                this.AddObjectToFetch(objectId);
                this.FetchObjects();
                cacheId = this.cacheIdByObjectId[objectId];
            }

            return cacheId;
        }

        private IStrategy InstantiateStrategy(ObjectId objectId)
        {
            if (this.deletedObjects != null && this.deletedObjects.Contains(objectId))
            {
                return null;
            }

            if (this.cacheIdByObjectId == null || !this.cacheIdByObjectId.ContainsKey(objectId))
            {
                this.AddObjectToFetch(objectId);
                this.FetchObjects();
            }

            if (this.deletedObjects != null && this.deletedObjects.Contains(objectId))
            {
                return null;
            }

            return new Strategy(this, objectId);
        }
        
        private IStrategy[] InstantiateStrategies(ObjectId[] objectIds)
        {
            var fetchRequired = false;
            foreach (var objectId in objectIds)
            {
                if (this.deletedObjects != null && this.deletedObjects.Contains(objectId))
                {
                    continue;
                }

                if (this.cacheIdByObjectId == null || !this.cacheIdByObjectId.ContainsKey(objectId))
                {
                    this.AddObjectToFetch(objectId);
                    fetchRequired = true;
                }
            }

            if (fetchRequired)
            {
                this.FetchObjects();
            }

            var strategies = new List<IStrategy>();
            foreach (var objectId in objectIds)
            {
                if (this.deletedObjects == null || !this.deletedObjects.Contains(objectId))
                {
                    var strategy = new Strategy(this, objectId);
                    strategies.Add(strategy);
                }
            }

            return strategies.ToArray();
        }
        
        private void FetchObjects()
        {
            var mapping = this.database.Mapping;
            using (var command = this.CreateCommand(this.Database.SchemaName + "." + Mapping.ProcedureNameForFetchObjects))
            {
                command.CommandType = CommandType.StoredProcedure;
                var objectDataRecords = new ObjectDataRecords(mapping, this.objectsToFetch);
                var parameter = command.Parameters.Add(Mapping.ParameterNameForObjectTable, SqlDbType.Structured);
                parameter.TypeName = this.Database.SchemaName + "." + Mapping.TableTypeNameForObjects;
                parameter.Value = objectDataRecords;
                
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var objectId = this.database.ObjectIds.Parse(reader.GetValue(0).ToString());
                        var typeId = reader.GetGuid(1);
                        var cacheId = reader.GetInt32(2);
                        var type = (IClass)this.database.ObjectFactory.MetaPopulation.Find(typeId);

                        if (this.classByObjectId == null)
                        {
                            this.classByObjectId = new Dictionary<ObjectId, IClass>();
                        }

                        if (this.cacheIdByObjectId == null)
                        {
                            this.cacheIdByObjectId = new Dictionary<ObjectId, int>();
                        }
                        
                        this.classByObjectId[objectId] = type;
                        this.cacheIdByObjectId[objectId] = cacheId;

                        this.classCache.Set(objectId, type);

                        this.objectsToFetch.Remove(objectId);
                    }

                    foreach (var objectId in this.objectsToFetch)
                    {
                        if (this.deletedObjects == null)
                        {
                            this.deletedObjects = new HashSet<ObjectId>();
                        }

                        this.deletedObjects.Add(objectId);
                    }
                }

                this.objectsToFetch = null;
            }
        }

        private object FetchUnitRole(ObjectId association, IRoleType roleType)
        {
            var mapping = this.database.Mapping;
            using (var command = this.CreateCommand(this.Database.SchemaName + "." + mapping.GetProcedureNameForFetchRole(roleType.RelationType)))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(Mapping.ParameterNameForAssociation, mapping.SqlDbTypeForObject).Value = association.Value;
                var role = command.ExecuteScalar();

                var cacheId = this.GetCacheId(association);
                this.roleCache.SetUnit(association, cacheId, roleType, role);
                
                return role;
            }
        }

        private ObjectId FetchCompositeRole(ObjectId association, IRoleType roleType)
        {
            var mapping = this.database.Mapping;

            using (var command = this.CreateCommand(this.Database.SchemaName + "." + mapping.GetProcedureNameForFetchRole(roleType.RelationType)))
            {
                ObjectId role = null;
                
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(Mapping.ParameterNameForAssociation, mapping.SqlDbTypeForObject).Value = association.Value;
                var result = command.ExecuteScalar();
                if (result != null)
                {
                    role = this.database.ObjectIds.Parse(result.ToString());
                    this.AddObjectToFetchIfNotCached(role);
                }

                var cacheId = this.GetCacheId(association);
                this.roleCache.SetComposite(association, cacheId, roleType, role);

                return role;
            }
        }

        private ObjectId[] FetchCompositeRoles(ObjectId association, IRoleType roleType)
        {
            var mapping = this.database.Mapping;

            List<ObjectId> roles = null;
            using (var command = this.CreateCommand(this.Database.SchemaName + "." + mapping.GetProcedureNameForFetchRole(roleType.RelationType)))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(Mapping.ParameterNameForAssociation, mapping.SqlDbTypeForObject).Value = association.Value;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (roles == null)
                        {
                            roles = new List<ObjectId>();
                        }

                        var value = reader.GetValue(0).ToString();
                        var role = this.database.ObjectIds.Parse(value);
                        roles.Add(role);

                        this.AddObjectToFetchIfNotCached(role);
                    }
                }
            }

            var roleArray = roles != null ? roles.ToArray() : EmptyObjectIds;

            var cacheId = this.GetCacheId(association);
            this.roleCache.SetComposites(association, cacheId, roleType, roleArray);

            return roleArray;
        }

        private ObjectId FetchCompositeAssociation(ObjectId role, IAssociationType associationType)
        {
            var schema = this.database.Mapping;

            var cmdText = @"
SELECT " + Mapping.ColumnNameForAssociation + @"
FROM " + this.Database.SchemaName + "." + schema.GetTableName(associationType.RelationType) + @"
WHERE " + Mapping.ColumnNameForRole + @"=" + Mapping.ParameterNameForRole + @";
";
            using (var command = this.CreateCommand(cmdText))
            {
                command.Parameters.Add(Mapping.ParameterNameForRole, schema.SqlDbTypeForObject).Value = role.Value;
                var result = command.ExecuteScalar();
                if (result != null)
                {
                    var association = this.database.ObjectIds.Parse(result.ToString());

                    this.AddObjectToFetchIfNotCached(association);

                    return association;
                }
            }

            return null;
        }

        private ObjectId[] FetchCompositeAssociations(ObjectId role, IAssociationType associationType)
        {
            var schema = this.database.Mapping;

            var cmdText = @"
SELECT " + Mapping.ColumnNameForAssociation + @"
FROM " + this.Database.SchemaName + "." + schema.GetTableName(associationType.RelationType) + @"
WHERE " + Mapping.ColumnNameForRole + @"=" + Mapping.ParameterNameForRole + @";
";
            List<ObjectId> associations = null;
            using (var command = this.CreateCommand(cmdText))
            {
                command.Parameters.Add(Mapping.ParameterNameForRole, schema.SqlDbTypeForObject).Value = role.Value;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (associations == null)
                        {
                            associations = new List<ObjectId>();
                        }

                        var value = reader.GetValue(0).ToString();
                        var association = this.database.ObjectIds.Parse(value);
                        associations.Add(association);

                        this.AddObjectToFetchIfNotCached(association);
                    }
                }
            }

            return associations != null ? associations.ToArray() : EmptyObjectIds;
        }

        private void FlushDeletedObjects()
        {
            if (this.flushDeletedObjects != null)
            {
                var mapping = this.database.Mapping;

                var cmdText = @"
DELETE FROM " + this.Database.SchemaName + "." + Mapping.TableNameForObjects + @"
WHERE " + Mapping.ColumnNameForObject + @" IN (SELECT " + Mapping.TableTypeColumnNameForObject + " FROM " + Mapping.ParameterNameForObjectTable + @");
";
                using (var command = this.CreateCommand(cmdText))
                {
                    var objectDataRecords = new ObjectDataRecords(mapping, this.flushDeletedObjects);
                    var parameter = command.Parameters.Add(Mapping.ParameterNameForObjectTable, SqlDbType.Structured);
                    parameter.TypeName = this.Database.SchemaName + "." + Mapping.TableTypeNameForObjects;
                    parameter.Value = objectDataRecords;

                    command.ExecuteNonQuery();
                }
            }
        }

        private void FlushUnit(IRoleType roleType)
        {
            if (this.unitFlushAssociationsByRoleType != null)
            {
                HashSet<ObjectId> flushAssociations;
                if(this.unitFlushAssociationsByRoleType.TryGetValue(roleType, out flushAssociations))
                {
                    var roleByAssociation = this.unitRoleByAssociationByRoleType[roleType];

                    IList<ObjectId> flushDeleted;
                    IList<ObjectId> flushChanged;
                    SplitFlushAssociations(flushAssociations, roleByAssociation, out flushDeleted, out flushChanged);

                    this.FlushRelationDelete(roleType, flushDeleted);

                    if (flushChanged != null)
                    {
                        var relationType = roleType.RelationType;
                        var mapping = this.database.Mapping;
                        var cmdText = @"
MERGE " + this.Database.SchemaName + "." + mapping.GetTableName(roleType.RelationType) + @" AS _X
USING (SELECT * FROM " + Mapping.ParameterNameForRelationTable + @") AS _Y
    ON _X." + Mapping.ColumnNameForAssociation + @" = _Y." + Mapping.TableTypeColumnNameForAssociation + @"
WHEN MATCHED THEN
UPDATE
    SET " + Mapping.ColumnNameForRole + @" = _Y." + Mapping.TableTypeColumnNameForRole + @"
WHEN NOT MATCHED THEN
    INSERT (" + Mapping.ColumnNameForAssociation + @", " + Mapping.ColumnNameForRole + @")
    VALUES(_Y." + Mapping.TableTypeColumnNameForAssociation + @", _Y." + Mapping.TableTypeColumnNameForRole + @");
";
                        using (var command = this.CreateCommand(cmdText))
                        {
                            var unitRoleDataRecords = new UnitRoleDataRecords(mapping, roleType, flushChanged, roleByAssociation);
                            var parameter = command.Parameters.Add(Mapping.ParameterNameForRelationTable, SqlDbType.Structured);
                            parameter.TypeName = this.Database.SchemaName + "." + mapping.GetTableTypeName(relationType);
                            parameter.Value = unitRoleDataRecords;
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void FlushOneToOne(IRoleType roleType)
        {
            if (this.oneToOneFlushAssociationsByRoleType != null)
            {
                HashSet<ObjectId> flushAssociations;
                if(this.oneToOneFlushAssociationsByRoleType.TryGetValue(roleType, out flushAssociations))
                {
                    var roleByAssociation = this.oneToOneRoleByAssociationByRoleType[roleType];

                    IList<ObjectId> flushDeleted;
                    IList<ObjectId> flushChanged;
                    SplitFlushAssociations(flushAssociations, roleByAssociation, out flushDeleted, out flushChanged);

                    this.FlushRelationDelete(roleType, flushDeleted);

                    if (flushChanged != null)
                    {
                        var relationType = roleType.RelationType;
                        var mapping = this.database.Mapping;
                        var cmdText = @"
DELETE FROM " + this.Database.SchemaName + "." + mapping.GetTableName(roleType.RelationType) + @"
WHERE " + Mapping.ColumnNameForRole + @" IN (SELECT " + Mapping.TableTypeColumnNameForRole + " FROM " + Mapping.ParameterNameForRelationTable + @");

MERGE " + this.Database.SchemaName + "." + mapping.GetTableName(roleType.RelationType) + @" AS _X
USING (SELECT * FROM " + Mapping.ParameterNameForRelationTable + @") AS _Y
    ON _X." + Mapping.ColumnNameForAssociation + @" = _Y." + Mapping.TableTypeColumnNameForAssociation + @"
WHEN MATCHED THEN
UPDATE
    SET " + Mapping.ColumnNameForRole + @" = _Y." + Mapping.TableTypeColumnNameForRole + @"
WHEN NOT MATCHED THEN
    INSERT (" + Mapping.ColumnNameForAssociation + @", " + Mapping.ColumnNameForRole + @")
    VALUES(_Y." + Mapping.TableTypeColumnNameForAssociation + @", _Y." + Mapping.TableTypeColumnNameForRole + @");
";
                        using (var command = this.CreateCommand(cmdText))
                        {
                            var compositeRoleDataRecords = new CompositeRoleDataRecords(mapping, roleType, flushChanged, roleByAssociation);
                            var parameter = command.Parameters.Add(Mapping.ParameterNameForRelationTable, SqlDbType.Structured);
                            parameter.TypeName = this.Database.SchemaName + "." + mapping.GetTableTypeName(relationType);
                            parameter.Value = compositeRoleDataRecords;
                            command.ExecuteNonQuery();
                        }
                    }
                }

                this.oneToOneFlushAssociationsByRoleType.Remove(roleType);
            }
        }

        private void FlushManyToOne(IRoleType roleType)
        {
            if (this.manyToOneFlushAssociationsByRoleType != null)
            {
                HashSet<ObjectId> flushAssociations;
                if (this.manyToOneFlushAssociationsByRoleType.TryGetValue(roleType, out flushAssociations))
                {
                    var roleByAssociation = this.manyToOneRoleByAssociationByRoleType[roleType];

                    IList<ObjectId> flushDeleted;
                    IList<ObjectId> flushChanged;
                    SplitFlushAssociations(flushAssociations, roleByAssociation, out flushDeleted, out flushChanged);

                    this.FlushRelationDelete(roleType, flushDeleted);

                    if (flushChanged != null)
                    {
                        var relationType = roleType.RelationType;
                        var mapping = this.database.Mapping;
                        var cmdText = @"
MERGE " + this.Database.SchemaName + "." + mapping.GetTableName(roleType.RelationType) + @" AS _X
USING (SELECT * FROM " + Mapping.ParameterNameForRelationTable + @") AS _Y
    ON _X." + Mapping.ColumnNameForAssociation + @" = _Y." + Mapping.TableTypeColumnNameForAssociation + @"
WHEN MATCHED THEN
UPDATE
        SET " + Mapping.ColumnNameForRole + @" = _Y." + Mapping.TableTypeColumnNameForRole + @"
WHEN NOT MATCHED THEN
INSERT (" + Mapping.ColumnNameForAssociation + @", " + Mapping.ColumnNameForRole + @")
VALUES(_Y." + Mapping.TableTypeColumnNameForAssociation + @", _Y." + Mapping.TableTypeColumnNameForRole + @");
";
                        using (var command = new SqlCommand(cmdText, this.connection, this.transaction))
                        {
                            var compositeRoleDataRecords = new CompositeRoleDataRecords(mapping, roleType, flushChanged, roleByAssociation);
                            var parameter = command.Parameters.Add(Mapping.ParameterNameForRelationTable, SqlDbType.Structured);
                            parameter.TypeName = this.Database.SchemaName + "." + mapping.GetTableTypeName(relationType);
                            parameter.Value = compositeRoleDataRecords;
                            command.ExecuteNonQuery();
                        }
                    }
                }

                this.manyToOneFlushAssociationsByRoleType.Remove(roleType);
            }
        }

        private void FlushOneToMany(IRoleType roleType)
        {
            if (this.oneToManyOriginalRoleByAssociationByRoleType != null)
            {
                Dictionary<ObjectId, ObjectId[]> originalRoleByAssociation;
                if (this.oneToManyOriginalRoleByAssociationByRoleType.TryGetValue(roleType, out originalRoleByAssociation))
                {
                    var roleByAssociation = this.oneToManyCurrentRoleByAssociationByRoleType[roleType];

                    List<ObjectId> flushDeleted = null;
                    Dictionary<ObjectId, ObjectId[]> flushRemovedRoleByAssociation = null;
                    Dictionary<ObjectId, ObjectId[]> flushAddedRoleByAssociation = null;

                    foreach (var kvp in originalRoleByAssociation)
                    {
                        var association = kvp.Key;
                        var originalRole = kvp.Value;
                        var currentRole = roleByAssociation[association];

                        if (currentRole.Length == 0)
                        {
                            if (flushDeleted == null)
                            {
                                flushDeleted = new List<ObjectId>();
                            }

                            flushDeleted.Add(association);
                        }
                        else
                        {
                            var remove = new HashSet<ObjectId>(originalRole);
                            remove.ExceptWith(currentRole);
                            if (remove.Count > 0)
                            {
                                if (flushRemovedRoleByAssociation == null)
                                {
                                    flushRemovedRoleByAssociation = new Dictionary<ObjectId, ObjectId[]>();
                                }

                                flushRemovedRoleByAssociation[association] = new List<ObjectId>(remove).ToArray();
                            }

                            var add = new HashSet<ObjectId>(currentRole);
                            add.ExceptWith(originalRole);
                            if (add.Count > 0)
                            {
                                if (flushAddedRoleByAssociation == null)
                                {
                                    flushAddedRoleByAssociation = new Dictionary<ObjectId, ObjectId[]>();
                                }

                                flushAddedRoleByAssociation[association] = new List<ObjectId>(add).ToArray();
                            }
                        }
                    }

                    this.FlushRelationDelete(roleType, flushDeleted);

                    if (flushAddedRoleByAssociation != null)
                    {
                        var relationType = roleType.RelationType;
                        var mapping = this.database.Mapping;
                        var cmdText = @"
DELETE FROM " + this.Database.SchemaName + "." + mapping.GetTableName(roleType.RelationType) + @"
WHERE " + Mapping.ColumnNameForRole + @" IN (SELECT " + Mapping.TableTypeColumnNameForRole + " FROM " + Mapping.ParameterNameForRelationTable + @");
                    
INSERT INTO " + this.Database.SchemaName + "." + mapping.GetTableName(roleType.RelationType) + @" (" + Mapping.ColumnNameForAssociation + @", " + Mapping.ColumnNameForRole + @")
SELECT " + Mapping.TableTypeColumnNameForAssociation + ", " + Mapping.TableTypeColumnNameForRole + " FROM " + Mapping.ParameterNameForRelationTable + @";
";
                        using (var command = this.CreateCommand(cmdText))
                        {
                            var compositesRoleDataRecords = new CompositesRoleDataRecords(mapping, roleType, flushAddedRoleByAssociation);
                            var parameter = command.Parameters.Add(Mapping.ParameterNameForRelationTable, SqlDbType.Structured);
                            parameter.TypeName = this.Database.SchemaName + "." + mapping.GetTableTypeName(relationType);
                            parameter.Value = compositesRoleDataRecords;
                            command.ExecuteNonQuery();
                        }
                    }

                    if (flushRemovedRoleByAssociation != null)
                    {
                        var relationType = roleType.RelationType;
                        var mapping = this.database.Mapping;
                        var cmdText = @"
DELETE _x
FROM " + (this.Database.SchemaName + "." + mapping.GetTableName(roleType.RelationType)) + @" AS _x
INNER JOIN " + Mapping.ParameterNameForRelationTable + @" AS _y
ON _x." + Mapping.ColumnNameForAssociation + @"=_y." + Mapping.TableTypeColumnNameForAssociation + @"
WHERE _x." + Mapping.ColumnNameForRole + @" = _y." + Mapping.TableTypeColumnNameForRole + @";
";
                        using (var command = this.CreateCommand(cmdText))
                        {
                            var compositesRoleDataRecords = new CompositesRoleDataRecords(mapping, roleType, flushRemovedRoleByAssociation);
                            var parameter = command.Parameters.Add(Mapping.ParameterNameForRelationTable, SqlDbType.Structured);
                            parameter.TypeName = this.Database.SchemaName + "." + mapping.GetTableTypeName(relationType);
                            parameter.Value = compositesRoleDataRecords;
                            command.ExecuteNonQuery();
                        }
                    }
                }

                this.oneToManyOriginalRoleByAssociationByRoleType.Remove(roleType);
            }
        }

        private void FlushManyToMany(IRoleType roleType)
        {
            if (this.manyToManyOriginalRoleByAssociationByRoleType != null)
            {
                Dictionary<ObjectId, ObjectId[]> originalRoleByAssociation;
                if (this.manyToManyOriginalRoleByAssociationByRoleType.TryGetValue(roleType, out originalRoleByAssociation))
                {
                    var roleByAssociation = this.manyToManyCurrentRoleByAssociationByRoleType[roleType];

                    List<ObjectId> flushDeleted = null;
                    Dictionary<ObjectId, ObjectId[]> flushRemovedRoleByAssociation = null;
                    Dictionary<ObjectId, ObjectId[]> flushAddedRoleByAssociation = null;

                    foreach (var kvp in originalRoleByAssociation)
                    {
                        var association = kvp.Key;
                        var originalRole = kvp.Value;
                        var currentRole = roleByAssociation[association];
                        if (currentRole.Length == 0)
                        {
                            if (flushDeleted == null)
                            {
                                flushDeleted = new List<ObjectId>();
                            }

                            flushDeleted.Add(association);
                        }
                        else
                        {
                            var remove = new HashSet<ObjectId>(originalRole);
                            remove.ExceptWith(currentRole);
                            if (remove.Count > 0)
                            {
                                if (flushRemovedRoleByAssociation == null)
                                {
                                    flushRemovedRoleByAssociation = new Dictionary<ObjectId, ObjectId[]>();
                                }

                                flushRemovedRoleByAssociation[association] = new List<ObjectId>(remove).ToArray();
                            }

                            var add = new HashSet<ObjectId>(currentRole);
                            add.ExceptWith(originalRole);
                            if (add.Count > 0)
                            {
                                if (flushAddedRoleByAssociation == null)
                                {
                                    flushAddedRoleByAssociation = new Dictionary<ObjectId, ObjectId[]>();
                                }

                                flushAddedRoleByAssociation[association] = new List<ObjectId>(add).ToArray();
                            }
                        }
                    }

                    this.FlushRelationDelete(roleType, flushDeleted);

                    if (flushAddedRoleByAssociation != null)
                    {
                        var relationType = roleType.RelationType;
                        var mapping = this.database.Mapping;
                        var cmdText = @"
INSERT INTO " + this.Database.SchemaName + "." + mapping.GetTableName(roleType.RelationType) + @" (" + Mapping.ColumnNameForAssociation + @", " + Mapping.ColumnNameForRole + @")
SELECT " + Mapping.TableTypeColumnNameForAssociation + ", " + Mapping.TableTypeColumnNameForRole + " FROM " + Mapping.ParameterNameForRelationTable + @";
";
                        using (var command = this.CreateCommand(cmdText))
                        {
                            var compositesRoleDataRecords = new CompositesRoleDataRecords(mapping, roleType, flushAddedRoleByAssociation);
                            var parameter = command.Parameters.Add(Mapping.ParameterNameForRelationTable, SqlDbType.Structured);
                            parameter.TypeName = this.Database.SchemaName + "." + mapping.GetTableTypeName(relationType);
                            parameter.Value = compositesRoleDataRecords;
                            command.ExecuteNonQuery();
                        }
                    }

                    if (flushRemovedRoleByAssociation != null)
                    {
                        var relationType = roleType.RelationType;
                        var mapping = this.database.Mapping;
                        var cmdText = @"
DELETE _x
FROM " + (this.Database.SchemaName + "." + mapping.GetTableName(roleType.RelationType)) + @" AS _x
INNER JOIN " + Mapping.ParameterNameForRelationTable + @" AS _y
ON _x." + Mapping.ColumnNameForAssociation + @"=_y." + Mapping.TableTypeColumnNameForAssociation + @"
WHERE _x." + Mapping.ColumnNameForRole + @" = _y." + Mapping.TableTypeColumnNameForRole + @";
";
                        using (var command = this.CreateCommand(cmdText))
                        {
                            var compositesRoleDataRecords = new CompositesRoleDataRecords(mapping, roleType, flushRemovedRoleByAssociation);
                            var parameter = command.Parameters.Add(Mapping.ParameterNameForRelationTable, SqlDbType.Structured);
                            parameter.TypeName = this.Database.SchemaName + "." + mapping.GetTableTypeName(relationType);
                            parameter.Value = compositesRoleDataRecords;
                            command.ExecuteNonQuery();
                        }
                    }
                }

                this.manyToManyOriginalRoleByAssociationByRoleType.Remove(roleType);
            }
        }

        private void FlushRelationDelete(IRoleType roleType, IList<ObjectId> flushDeleted)
        {
            if (flushDeleted != null)
            {
                var mapping = this.database.Mapping;
                var cmdText = @"
DELETE FROM " + this.Database.SchemaName + "." + mapping.GetTableName(roleType.RelationType) + @"
WHERE " + Mapping.ColumnNameForAssociation + @" IN ( SELECT * FROM " + Mapping.ParameterNameForAssociation + ");";
                using (var command = this.CreateCommand(cmdText))
                {
                    var objectDataRecords = new ObjectDataRecords(mapping, flushDeleted);
                    var parameter = command.Parameters.Add(Mapping.ParameterNameForAssociation, SqlDbType.Structured);
                    parameter.TypeName = this.Database.SchemaName + "." + Mapping.TableTypeNameForObjects;
                    parameter.Value = objectDataRecords;
                    command.ExecuteNonQuery();
                }
            }
        }

        private void UpdateCacheIds()
        {
            if (this.changedObjects != null)
            {
                var schema = this.database.Mapping;
                var cmdText = @"
UPDATE " + this.Database.SchemaName + "." + Mapping.TableNameForObjects + @"
SET " + Mapping.ColumnNameForCache + " = " + Mapping.ColumnNameForCache + @" - 1
WHERE " + Mapping.ColumnNameForObject + " = " + Mapping.ParameterNameForObject;
                using (var command = this.CreateCommand(cmdText))
                {
                    var objectParam = command.Parameters.Add(Mapping.ParameterNameForObject, schema.SqlDbTypeForObject);

                    foreach (var changedObject in this.changedObjects)
                    {
                        objectParam.Value = changedObject.Value;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private void OnObjectChanged(ObjectId association)
        {
            if (this.changedObjects == null)
            {
                this.changedObjects = new HashSet<ObjectId>();
            }

            this.changedObjects.Add(association);
        }

        private void Reset()
        {
            this.classByObjectId = null;
            this.cacheIdByObjectId = null;

            this.objectsToFetch = null;

            this.newObjects = null;
            this.deletedObjects = null;
            this.flushDeletedObjects = null;
            this.changedObjects = null;

            this.unitRoleByAssociationByRoleType = null;
            this.unitFlushAssociationsByRoleType = null;

            this.oneToOneRoleByAssociationByRoleType = null;
            this.oneToOneAssociationByRoleByAssociationType = null;
            this.oneToOneFlushAssociationsByRoleType = null;

            this.manyToOneRoleByAssociationByRoleType = null;
            this.manyToOneAssociationByRoleByAssociationType = null;
            this.manyToOneFlushAssociationsByRoleType = null;
            this.manyToOneTriggerFlushRolesByAssociationType = null;

            this.oneToManyCurrentRoleByAssociationByRoleType = null;
            this.oneToManyOriginalRoleByAssociationByRoleType = null;
            this.oneToManyAssociationByRoleByAssociationType = null;

            this.manyToManyCurrentRoleByAssociationByRoleType = null;
            this.manyToManyOriginalRoleByAssociationByRoleType = null;
            this.manyToManyAssociationByRoleByAssociationType = null;
            this.manyToManyTriggerFlushRolesByAssociationType = null;

            this.changeSet = null;
        }

        private void LazyDisconnect()
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

                try
                {
                    if (this.connection != null)
                    {
                        this.connection.Close();
                    }
                }
                finally
                {
                    this.connection = null;
                }
            }
        }

        private void AddObjectToFetchIfNotCached(ObjectId objectId)
        {
            if (this.cacheIdByObjectId == null || !this.cacheIdByObjectId.ContainsKey(objectId))
            {
                this.AddObjectToFetch(objectId);
            }
        }

        private void AddObjectToFetch(ObjectId objectId)
        {
            if (this.objectsToFetch == null)
            {
                this.objectsToFetch = new HashSet<ObjectId>();
            }

            this.objectsToFetch.Add(objectId);
        }
        
        #region Lazy Dictionaries
        private Dictionary<ObjectId, object> GetUnitRoleByAssociation(IRoleType roleType)
        {
            if (this.unitRoleByAssociationByRoleType == null)
            {
                this.unitRoleByAssociationByRoleType = new Dictionary<IRoleType, Dictionary<ObjectId, object>>();
            }

            Dictionary<ObjectId, object> roleByAssociation;
            if (!this.unitRoleByAssociationByRoleType.TryGetValue(roleType, out roleByAssociation))
            {
                roleByAssociation = new Dictionary<ObjectId, object>();
                this.unitRoleByAssociationByRoleType[roleType] = roleByAssociation;
            }

            return roleByAssociation;
        }

        private HashSet<ObjectId> GetUnitFlushAssociations(IRoleType roleType)
        {
            if (this.unitFlushAssociationsByRoleType == null)
            {
                this.unitFlushAssociationsByRoleType = new Dictionary<IRoleType, HashSet<ObjectId>>();
            }

            HashSet<ObjectId> flushAssociations;
            if (!this.unitFlushAssociationsByRoleType.TryGetValue(roleType, out flushAssociations))
            {
                flushAssociations = new HashSet<ObjectId>();
                this.unitFlushAssociationsByRoleType[roleType] = flushAssociations;
            }

            return flushAssociations;
        }

        private Dictionary<ObjectId, ObjectId> GetOneToOneRoleByAssociation(IRoleType roleType)
        {
            if (this.oneToOneRoleByAssociationByRoleType == null)
            {
                this.oneToOneRoleByAssociationByRoleType = new Dictionary<IRoleType, Dictionary<ObjectId, ObjectId>>();
            }

            Dictionary<ObjectId, ObjectId> roleByAssociation;
            if (!this.oneToOneRoleByAssociationByRoleType.TryGetValue(roleType, out roleByAssociation))
            {
                roleByAssociation = new Dictionary<ObjectId, ObjectId>();
                this.oneToOneRoleByAssociationByRoleType[roleType] = roleByAssociation;
            }

            return roleByAssociation;
        }

        private HashSet<ObjectId> GetOneToOneFlushAssociations(IRoleType roleType)
        {
            if (this.oneToOneFlushAssociationsByRoleType == null)
            {
                this.oneToOneFlushAssociationsByRoleType = new Dictionary<IRoleType, HashSet<ObjectId>>();
            }

            HashSet<ObjectId> flushAssociations;
            if (!this.oneToOneFlushAssociationsByRoleType.TryGetValue(roleType, out flushAssociations))
            {
                flushAssociations = new HashSet<ObjectId>();
                this.oneToOneFlushAssociationsByRoleType[roleType] = flushAssociations;
            }

            return flushAssociations;
        }

        private Dictionary<ObjectId, ObjectId> GetOneToOneAssociationByRole(IAssociationType associationType)
        {
            if (this.oneToOneAssociationByRoleByAssociationType == null)
            {
                this.oneToOneAssociationByRoleByAssociationType = new Dictionary<IAssociationType, Dictionary<ObjectId, ObjectId>>();
            }

            Dictionary<ObjectId, ObjectId> associationByRole;
            if (!this.oneToOneAssociationByRoleByAssociationType.TryGetValue(associationType, out associationByRole))
            {
                associationByRole = new Dictionary<ObjectId, ObjectId>();
                this.oneToOneAssociationByRoleByAssociationType[associationType] = associationByRole;
            }

            return associationByRole;
        }

        private Dictionary<ObjectId, ObjectId> GetManyToOneRoleByAssociation(IRoleType roleType)
        {
            if (this.manyToOneRoleByAssociationByRoleType == null)
            {
                this.manyToOneRoleByAssociationByRoleType = new Dictionary<IRoleType, Dictionary<ObjectId, ObjectId>>();
            }

            Dictionary<ObjectId, ObjectId> roleByAssociation;
            if (!this.manyToOneRoleByAssociationByRoleType.TryGetValue(roleType, out roleByAssociation))
            {
                roleByAssociation = new Dictionary<ObjectId, ObjectId>();
                this.manyToOneRoleByAssociationByRoleType[roleType] = roleByAssociation;
            }

            return roleByAssociation;
        }

        private HashSet<ObjectId> GetManyToOneFlushAssociations(IRoleType roleType)
        {
            if (this.manyToOneFlushAssociationsByRoleType == null)
            {
                this.manyToOneFlushAssociationsByRoleType = new Dictionary<IRoleType, HashSet<ObjectId>>();
            }

            HashSet<ObjectId> flushAssociations;
            if (!this.manyToOneFlushAssociationsByRoleType.TryGetValue(roleType, out flushAssociations))
            {
                flushAssociations = new HashSet<ObjectId>();
                this.manyToOneFlushAssociationsByRoleType[roleType] = flushAssociations;
            }

            return flushAssociations;
        }

        private Dictionary<ObjectId, ObjectId[]> GetManyToOneAssociationByRole(IAssociationType associationType)
        {
            if (this.manyToOneAssociationByRoleByAssociationType == null)
            {
                this.manyToOneAssociationByRoleByAssociationType = new Dictionary<IAssociationType, Dictionary<ObjectId, ObjectId[]>>();
            }

            Dictionary<ObjectId, ObjectId[]> associationByRole;
            if (!this.manyToOneAssociationByRoleByAssociationType.TryGetValue(associationType, out associationByRole))
            {
                associationByRole = new Dictionary<ObjectId, ObjectId[]>();
                this.manyToOneAssociationByRoleByAssociationType[associationType] = associationByRole;
            }

            return associationByRole;
        }

        private HashSet<ObjectId> GetManyToOneTriggerFlushRoles(IAssociationType associationType)
        {
            if (this.manyToOneTriggerFlushRolesByAssociationType == null)
            {
                this.manyToOneTriggerFlushRolesByAssociationType = new Dictionary<IAssociationType, HashSet<ObjectId>>();
            }

            HashSet<ObjectId> triggerFlushRoles;
            if (!this.manyToOneTriggerFlushRolesByAssociationType.TryGetValue(associationType, out triggerFlushRoles))
            {
                triggerFlushRoles = new HashSet<ObjectId>();
                this.manyToOneTriggerFlushRolesByAssociationType[associationType] = triggerFlushRoles;
            }

            return triggerFlushRoles;
        }

        private Dictionary<ObjectId, ObjectId[]> GetOneToManyCurrentRoleByAssociation(IRoleType roleType)
        {
            if (this.oneToManyCurrentRoleByAssociationByRoleType == null)
            {
                this.oneToManyCurrentRoleByAssociationByRoleType = new Dictionary<IRoleType, Dictionary<ObjectId, ObjectId[]>>();
            }

            Dictionary<ObjectId, ObjectId[]> roleByAssociation;
            if (!this.oneToManyCurrentRoleByAssociationByRoleType.TryGetValue(roleType, out roleByAssociation))
            {
                roleByAssociation = new Dictionary<ObjectId, ObjectId[]>();
                this.oneToManyCurrentRoleByAssociationByRoleType[roleType] = roleByAssociation;
            }

            return roleByAssociation;
        }

        private Dictionary<ObjectId, ObjectId[]> GetOneToManyOriginalRoleByAssociation(IRoleType roleType)
        {
            if (this.oneToManyOriginalRoleByAssociationByRoleType == null)
            {
                this.oneToManyOriginalRoleByAssociationByRoleType = new Dictionary<IRoleType, Dictionary<ObjectId, ObjectId[]>>();
            }

            Dictionary<ObjectId, ObjectId[]> roleByAssociation;
            if (!this.oneToManyOriginalRoleByAssociationByRoleType.TryGetValue(roleType, out roleByAssociation))
            {
                roleByAssociation = new Dictionary<ObjectId, ObjectId[]>();
                this.oneToManyOriginalRoleByAssociationByRoleType[roleType] = roleByAssociation;
            }

            return roleByAssociation;
        }

        private Dictionary<ObjectId, ObjectId> GetOneToManyAssociationByRole(IAssociationType associationType)
        {
            if (this.oneToManyAssociationByRoleByAssociationType == null)
            {
                this.oneToManyAssociationByRoleByAssociationType = new Dictionary<IAssociationType, Dictionary<ObjectId, ObjectId>>();
            }

            Dictionary<ObjectId, ObjectId> associationByRole;
            if (!this.oneToManyAssociationByRoleByAssociationType.TryGetValue(associationType, out associationByRole))
            {
                associationByRole = new Dictionary<ObjectId, ObjectId>();
                this.oneToManyAssociationByRoleByAssociationType[associationType] = associationByRole;
            }

            return associationByRole;
        }

        private Dictionary<ObjectId, ObjectId[]> GetManyToManyCurrentRoleByAssociation(IRoleType roleType)
        {
            if (this.manyToManyCurrentRoleByAssociationByRoleType == null)
            {
                this.manyToManyCurrentRoleByAssociationByRoleType = new Dictionary<IRoleType, Dictionary<ObjectId, ObjectId[]>>();
            }

            Dictionary<ObjectId, ObjectId[]> roleByAssociation;
            if (!this.manyToManyCurrentRoleByAssociationByRoleType.TryGetValue(roleType, out roleByAssociation))
            {
                roleByAssociation = new Dictionary<ObjectId, ObjectId[]>();
                this.manyToManyCurrentRoleByAssociationByRoleType[roleType] = roleByAssociation;
            }

            return roleByAssociation;
        }

        private Dictionary<ObjectId, ObjectId[]> GetManyToManyOriginalRoleByAssociation(IRoleType roleType)
        {
            if (this.manyToManyOriginalRoleByAssociationByRoleType == null)
            {
                this.manyToManyOriginalRoleByAssociationByRoleType = new Dictionary<IRoleType, Dictionary<ObjectId, ObjectId[]>>();
            }

            Dictionary<ObjectId, ObjectId[]> roleByAssociation;
            if (!this.manyToManyOriginalRoleByAssociationByRoleType.TryGetValue(roleType, out roleByAssociation))
            {
                roleByAssociation = new Dictionary<ObjectId, ObjectId[]>();
                this.manyToManyOriginalRoleByAssociationByRoleType[roleType] = roleByAssociation;
            }

            return roleByAssociation;
        }
        
        private Dictionary<ObjectId, ObjectId[]> GetManyToManyAssociationByRole(IAssociationType associationType)
        {
            if (this.manyToManyAssociationByRoleByAssociationType == null)
            {
                this.manyToManyAssociationByRoleByAssociationType = new Dictionary<IAssociationType, Dictionary<ObjectId, ObjectId[]>>();
            }

            Dictionary<ObjectId, ObjectId[]> associationByRole;
            if (!this.manyToManyAssociationByRoleByAssociationType.TryGetValue(associationType, out associationByRole))
            {
                associationByRole = new Dictionary<ObjectId, ObjectId[]>();
                this.manyToManyAssociationByRoleByAssociationType[associationType] = associationByRole;
            }

            return associationByRole;
        }

        private HashSet<ObjectId> GetManyToManyTriggerFlushRoles(IAssociationType associationType)
        {
            if (this.manyToManyTriggerFlushRolesByAssociationType == null)
            {
                this.manyToManyTriggerFlushRolesByAssociationType = new Dictionary<IAssociationType, HashSet<ObjectId>>();
            }

            HashSet<ObjectId> triggerFlushRoles;
            if (!this.manyToManyTriggerFlushRolesByAssociationType.TryGetValue(associationType, out triggerFlushRoles))
            {
                triggerFlushRoles = new HashSet<ObjectId>();
                this.manyToManyTriggerFlushRolesByAssociationType[associationType] = triggerFlushRoles;
            }

            return triggerFlushRoles;
        }
        #endregion
    }
}