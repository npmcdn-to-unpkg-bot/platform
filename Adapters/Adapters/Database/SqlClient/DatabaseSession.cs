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
    using System.ComponentModel;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;

    using Allors.Meta;

    public class DatabaseSession : IDatabaseSession
    {
        private static readonly ObjectId[] EmptyObjectIds = { };
        private static readonly IObject[] EmptyObjects = { };

        private readonly Database database;

        private SqlConnection connection;
        private SqlTransaction transaction;

        private Dictionary<ObjectId, Strategy> strategyByObjectId;
        private Dictionary<ObjectId, int> cacheIdByObjectId;

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

        private Dictionary<IRoleType, Dictionary<ObjectId, ObjectId[]>> manyToManyRoleByAssociationByRoleType;
        private Dictionary<IRoleType, HashSet<ObjectId>> manyToManyFlushAssociationsByRoleType;
        private Dictionary<IAssociationType, Dictionary<ObjectId, ObjectId[]>> manyToManyAssociationByRoleByAssociationType;
        private Dictionary<IAssociationType, HashSet<ObjectId>> manyToManyTriggerFlushRolesByAssociationType;

        public DatabaseSession(Database database)
        {
            this.database = database;
        }

        public event SessionCommittedEventHandler Committed;

        public event SessionCommittingEventHandler Committing;

        public event SessionRollingBackEventHandler RollingBack;

        public event SessionRolledBackEventHandler RolledBack;

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

        public object this[string name]
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IChangeSet Checkpoint()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Extent Intersect(Extent firstOperand, Extent secondOperand)
        {
            throw new NotImplementedException();
        }

        public Extent Union(Extent firstOperand, Extent secondOperand)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            try
            {
                this.Flush();

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
            throw new NotImplementedException();
        }

        public IObject Create(IClass objectType)
        {
            const string CmdText = @"
INSERT INTO " + Schema.TableNameForObjects + " (" + Schema.ColumnNameForType + ", " + Schema.ColumnNameForCache + @")
VALUES (" + Schema.ParameterNameForType + ", " + Schema.ParameterNameForCache + @");

SELECT " + Schema.ColumnNameForObject + @" = SCOPE_IDENTITY();
";
            using (var command = this.CreateCommand(CmdText))
            {
                command.Parameters.Add(Schema.ParameterNameForType, Schema.SqlDbTypeForType).Value = objectType.Id;
                command.Parameters.Add(Schema.ParameterNameForCache, Schema.SqlDbTypeForCache).Value = int.MaxValue;
                var result = command.ExecuteScalar().ToString();
                var objectId = this.database.ObjectIds.Parse(result);
                var strategy = new Strategy(this, objectType, objectId, true, false);
                return strategy.GetObject();
            }
        }

        public IObject[] Create(IClass objectType, int count)
        {
            // TODO: Optimize
            var arrayType = this.Database.ObjectFactory.GetTypeForObjectType(objectType);
            var domainObjects = (IObject[])Array.CreateInstance(arrayType, count);
            for (var i = 0; i < count; i++)
            {
                domainObjects[i] = this.Create(objectType);
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
            List<IObject> objects = null;
            foreach (var objectId in objectIds)
            {
                var strategy = this.InstantiateStrategy(objectId);
                if (strategy != null)
                {
                    if (objects == null)
                    {
                        objects = new List<IObject>();
                    }

                    objects.Add(strategy.GetObject());
                }
            }

            return objects != null ? objects.ToArray() : EmptyObjects;
        }

        public IObject Insert(IClass objectType, string objectId)
        {
            throw new NotImplementedException();
        }

        public IObject Insert(IClass objectType, ObjectId objectId)
        {
            throw new NotImplementedException();
        }

        public IStrategy InstantiateStrategy(ObjectId objectId)
        {
            this.LazyConnect();

            if (this.strategyByObjectId != null)
            {
                Strategy strategy;
                if (this.strategyByObjectId.TryGetValue(objectId, out strategy))
                {
                    return strategy;
                }
            }

            return this.FetchStrategy(objectId, false, null);
        }

        public void Dispose()
        {
            this.Rollback();
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
                role = this.FetchUnitRole(association, roleType);
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
                role = this.FetchCompositeRole(association, roleType);
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
                    foreach (var kvp in roleByAssociation)
                    {
                        if (role.Equals(kvp.Value))
                        {
                            existingAssociation = kvp.Key;
                            break;
                        }
                    }
                }

                if (existingAssociation != null)
                {
                    roleByAssociation[existingAssociation] = null;
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
                role = this.FetchCompositeRole(association, roleType);
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
                role = this.FetchCompositeRoles(association, roleType);
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
                    foreach (var kvp in currentRoleByAssociation)
                    {
                        if (role.Equals(kvp.Value))
                        {
                            existingAssociation = kvp.Key;
                            break;
                        }
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
            var roleByAssociation = this.GetManyToManyRoleByAssociation(roleType);

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
            var existingRoles = new List<ObjectId>(this.GetCompositeRolesManyToMany(association, roleType));
            if (!existingRoles.Contains(role))
            {
                var roleByAssociation = this.GetManyToManyRoleByAssociation(roleType);
                var flushAssociations = this.GetManyToManyFlushAssociations(roleType);

                var associationByRole = this.GetManyToManyAssociationByRole(roleType.AssociationType);

                //// Existing Association -> Role
                //ObjectId[] existingAssociation;
                //if (!associationByRole.TryGetValue(role, out existingAssociation))
                //{
                //    foreach (var kvp in roleByAssociation)
                //    {
                //        if (role.Equals(kvp.Value))
                //        {
                //            existingAssociation = kvp.Key;
                //            break;
                //        }
                //    }
                //}

                //if (existingAssociation != null)
                //{
                //    object rolesOfExistingAssociation;
                //    if (roleByAssociation.TryGetValue((ObjectId)existingAssociation, out rolesOfExistingAssociation))
                //    {
                //        var newRolesOfExistingAssociation = new List<ObjectId>((ObjectId[])rolesOfExistingAssociation);
                //        newRolesOfExistingAssociation.Remove(role);
                //        roleByAssociation[(ObjectId)existingAssociation] = newRolesOfExistingAssociation;
                //    }
                //}

                //// Association <- Role
                //associationByRole[role] = association;

                // Association -> Roles
                existingRoles.Add(role);
                roleByAssociation[association] = existingRoles.ToArray();
                flushAssociations.Add(association);
            }
        }

        internal void RemoveCompositeRoleManyToMany(ObjectId association, IRoleType roleType, ObjectId role)
        {
            var existingRoles = new List<ObjectId>(this.GetCompositeRolesManyToMany(association, roleType));
            if (existingRoles.Contains(role))
            {
                var roleByAssociation = this.GetManyToManyRoleByAssociation(roleType);
                var flushAssociations = this.GetManyToManyFlushAssociations(roleType);

                var associationByRole = this.GetManyToManyAssociationByRole(roleType.AssociationType);

                // Association <- Role
                associationByRole[role] = null;

                // Association -> Roles
                existingRoles.Remove(role);
                roleByAssociation[association] = existingRoles.ToArray();
                flushAssociations.Add(association);
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
            this.LazyConnect();
            return new SqlCommand(cmdText, this.connection, this.transaction);
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

            if (this.manyToManyFlushAssociationsByRoleType != null)
            {
                foreach (var roleType in new List<IRoleType>(this.manyToManyFlushAssociationsByRoleType.Keys))
                {
                    this.FlushManyToMany(roleType);
                }
            }
        }

        internal Strategy InstantiateExistingStrategy(ObjectId objectId)
        {
            if (this.strategyByObjectId != null)
            {
                Strategy strategy;
                if (this.strategyByObjectId.TryGetValue(objectId, out strategy))
                {
                    return strategy;
                }
            }

            return this.FetchStrategy(objectId, false, false);
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

        private static void SplitFlushAssociations(IEnumerable<ObjectId> flushAssociations, IReadOnlyDictionary<ObjectId, ObjectId[]> roleByAssociation, out IList<ObjectId> flushDeleted, out IList<ObjectId> flushChanged)
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

        private Strategy FetchStrategy(ObjectId objectId, bool isNew, bool? isDeleted)
        {
            var cmdText = @"
SELECT  " + Schema.ColumnNameForType + ", " + Schema.ColumnNameForCache + @"
FROM " + Schema.TableNameForObjects + @"
WHERE " + Schema.ColumnNameForObject + @" = " + Schema.ParameterNameForObject + @"
";
            using (var command = this.CreateCommand(cmdText))
            {
                command.Parameters.Add(Schema.ParameterNameForObject, this.database.Schema.SqlDbTypeForId).Value = objectId.Value;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var typeId = reader.GetGuid(0);
                        var cache = reader.GetInt32(1);
                        var type = (IClass)this.database.ObjectFactory.MetaPopulation.Find(typeId);

                        if (this.strategyByObjectId == null)
                        {
                            this.strategyByObjectId = new Dictionary<ObjectId, Strategy>();
                        }

                        if (this.cacheIdByObjectId == null)
                        {
                            this.cacheIdByObjectId = new Dictionary<ObjectId, int>();
                        }

                        var strategy = new Strategy(this, type, objectId, isNew, isDeleted);

                        this.strategyByObjectId[objectId] = strategy;
                        this.cacheIdByObjectId[objectId] = cache;

                        return strategy;
                    }
                }

                return null;
            }
        }

        private object FetchUnitRole(ObjectId association, IRoleType roleType)
        {
            var schema = this.database.Schema;

            var cmdText = @"
SELECT " + Schema.ColumnNameForRole + @"
FROM " + schema.GetTableName(roleType.RelationType) + @"
WHERE " + Schema.ColumnNameForAssociation + @"=" + Schema.ParameterNameForAssociation + @";
";
            using (var command = this.CreateCommand(cmdText))
            {
                command.Parameters.Add(Schema.ParameterNameForAssociation, schema.SqlDbTypeForId).Value = association.Value;
                return command.ExecuteScalar();
            }
        }

        private ObjectId FetchCompositeRole(ObjectId association, IRoleType roleType)
        {
            var schema = this.database.Schema;

            var cmdText = @"
SELECT " + Schema.ColumnNameForRole + @"
FROM " + schema.GetTableName(roleType.RelationType) + @"
WHERE " + Schema.ColumnNameForAssociation + @"=" + Schema.ParameterNameForAssociation + @";
";
            using (var command = this.CreateCommand(cmdText))
            {
                command.Parameters.Add(Schema.ParameterNameForAssociation, schema.SqlDbTypeForId).Value = association.Value;
                var result = command.ExecuteScalar();
                if (result != null)
                {
                    var objectId = this.database.ObjectIds.Parse(result.ToString());
                    return objectId;
                }
            }

            return null;
        }

        private ObjectId[] FetchCompositeRoles(ObjectId association, IRoleType roleType)
        {
            var schema = this.database.Schema;

            var cmdText = @"
SELECT " + Schema.ColumnNameForRole + @"
FROM " + schema.GetTableName(roleType.RelationType) + @"
WHERE " + Schema.ColumnNameForAssociation + @"=" + Schema.ParameterNameForAssociation + @";
";
            List<ObjectId> roles = null;
            using (var command = this.CreateCommand(cmdText))
            {
                command.Parameters.Add(Schema.ParameterNameForAssociation, schema.SqlDbTypeForId).Value = association.Value;
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
                    }
                }
            }

            return roles != null ? roles.ToArray() : EmptyObjectIds;
        }

        private ObjectId FetchCompositeAssociation(ObjectId role, IAssociationType associationType)
        {
            var schema = this.database.Schema;

            var cmdText = @"
SELECT " + Schema.ColumnNameForAssociation + @"
FROM " + schema.GetTableName(associationType.RelationType) + @"
WHERE " + Schema.ColumnNameForRole + @"=" + Schema.ParameterNameForRole + @";
";
            using (var command = this.CreateCommand(cmdText))
            {
                command.Parameters.Add(Schema.ParameterNameForRole, schema.SqlDbTypeForId).Value = role.Value;
                var result = command.ExecuteScalar();
                if (result != null)
                {
                    var objectId = this.database.ObjectIds.Parse(result.ToString());
                    return objectId;
                }
            }

            return null;
        }

        private ObjectId[] FetchCompositeAssociations(ObjectId role, IAssociationType associationType)
        {
            var schema = this.database.Schema;

            var cmdText = @"
SELECT " + Schema.ColumnNameForAssociation + @"
FROM " + schema.GetTableName(associationType.RelationType) + @"
WHERE " + Schema.ColumnNameForRole + @"=" + Schema.ParameterNameForRole + @";
";
            List<ObjectId> associations = null;
            using (var command = this.CreateCommand(cmdText))
            {
                command.Parameters.Add(Schema.ParameterNameForRole, schema.SqlDbTypeForId).Value = role.Value;
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
                    }
                }
            }

            return associations != null ? associations.ToArray() : EmptyObjectIds;
        }

        private void FlushUnit(IRoleType roleType)
        {
            if (this.unitFlushAssociationsByRoleType != null)
            {
                HashSet<ObjectId> flushAssociations;
                if(this.unitFlushAssociationsByRoleType.TryGetValue(roleType, out flushAssociations))
                {
                    var unitByAssociation = this.unitRoleByAssociationByRoleType[roleType];

                    IList<ObjectId> flushDeleted;
                    IList<ObjectId> flushChanged;
                    SplitFlushAssociations(flushAssociations, unitByAssociation, out flushDeleted, out flushChanged);

                    if (flushDeleted != null)
                    {
                        var schema = this.database.Schema;
                        var cmdText = @"
DELETE FROM " + schema.GetTableName(roleType.RelationType) + @"
WHERE " + Schema.ColumnNameForAssociation + @" = " + Schema.ParameterNameForAssociation;
                        using (var command = this.CreateCommand(cmdText))
                        {
                            var associationParam = command.Parameters.Add(Schema.ParameterNameForAssociation, schema.SqlDbTypeForId);

                            foreach (var association in flushDeleted)
                            {
                                associationParam.Value = association.Value;
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    if (flushChanged != null)
                    {
                        var schema = this.database.Schema;
                        var cmdText = @"
MERGE " + schema.GetTableName(roleType.RelationType) + @" AS _X
USING (SELECT " + Schema.ParameterNameForAssociation + @" AS " + Schema.ColumnNameForAssociation + @") AS _Y
    ON _X." + Schema.ColumnNameForAssociation + @" = _Y." + Schema.ColumnNameForAssociation + @"
WHEN MATCHED THEN
UPDATE
        SET " + Schema.ColumnNameForRole + @" = " + Schema.ParameterNameForRole + @"
WHEN NOT MATCHED THEN
INSERT (" + Schema.ColumnNameForAssociation + @", " + Schema.ColumnNameForRole + @")
VALUES(" + Schema.ParameterNameForAssociation + @", " + Schema.ParameterNameForRole + @");
";
                        using (var command = this.CreateCommand(cmdText))
                        {
                            var associationParam = command.Parameters.Add(Schema.ParameterNameForAssociation, schema.SqlDbTypeForId);
                            var roleParam = command.Parameters.Add(Schema.ParameterNameForRole, schema.GetSqlDbType(roleType));

                            foreach (var association in flushChanged)
                            {
                                var changedUnitRole = unitByAssociation[association];

                                associationParam.Value = association.Value;
                                roleParam.Value = changedUnitRole;
                                command.ExecuteNonQuery();
                            }
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

                    if (flushDeleted != null)
                    {
                        var schema = this.database.Schema;
                        var cmdText = @"
DELETE FROM " + schema.GetTableName(roleType.RelationType) + @"
WHERE " + Schema.ColumnNameForAssociation + @" = " + Schema.ParameterNameForAssociation;
                        using (var command = this.CreateCommand(cmdText))
                        {
                            var associationParam = command.Parameters.Add(Schema.ParameterNameForAssociation, schema.SqlDbTypeForId);

                            foreach (var association in flushDeleted)
                            {
                                associationParam.Value = association.Value;
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    if (flushChanged != null)
                    {
                        var schema = this.database.Schema;
                        var cmdText = @"
DELETE FROM " + schema.GetTableName(roleType.RelationType) + @"
WHERE " + Schema.ColumnNameForRole + @" = " + Schema.ParameterNameForRole + @";

MERGE " + schema.GetTableName(roleType.RelationType) + @" AS _X
USING (SELECT " + Schema.ParameterNameForAssociation + @" AS " + Schema.ColumnNameForAssociation + @") AS _Y
    ON _X." + Schema.ColumnNameForAssociation + @" = _Y." + Schema.ColumnNameForAssociation + @"
WHEN MATCHED THEN
UPDATE
        SET " + Schema.ColumnNameForRole + @" = " + Schema.ParameterNameForRole + @"
WHEN NOT MATCHED THEN
INSERT (" + Schema.ColumnNameForAssociation + @", " + Schema.ColumnNameForRole + @")
VALUES(" + Schema.ParameterNameForAssociation + @", " + Schema.ParameterNameForRole + @");
";
                        using (var command = this.CreateCommand(cmdText))
                        {
                            var associationParam = command.Parameters.Add(Schema.ParameterNameForAssociation, schema.SqlDbTypeForId);
                            var roleParam = command.Parameters.Add(Schema.ParameterNameForRole, schema.GetSqlDbType(roleType));

                            foreach (var association in flushChanged)
                            {
                                var changedCompositeRole = roleByAssociation[association];

                                associationParam.Value = association.Value;
                                roleParam.Value = ((ObjectId)changedCompositeRole).Value;
                                command.ExecuteNonQuery();
                            }
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
                    var unitByAssociation = this.manyToOneRoleByAssociationByRoleType[roleType];

                    IList<ObjectId> flushDeleted;
                    IList<ObjectId> flushChanged;
                    SplitFlushAssociations(flushAssociations, unitByAssociation, out flushDeleted, out flushChanged);

                    if (flushDeleted != null)
                    {
                        var schema = this.database.Schema;
                        var cmdText = @"
DELETE FROM " + schema.GetTableName(roleType.RelationType) + @"
WHERE " + Schema.ColumnNameForAssociation + @" = " + Schema.ParameterNameForAssociation;
                        using (var command = this.CreateCommand(cmdText))
                        {
                            var associationParam = command.Parameters.Add(Schema.ParameterNameForAssociation, schema.SqlDbTypeForId);

                            foreach (var association in flushDeleted)
                            {
                                associationParam.Value = association.Value;
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    if (flushChanged != null)
                    {
                        var schema = this.database.Schema;
                        var cmdText = @"
MERGE " + schema.GetTableName(roleType.RelationType) + @" AS _X
USING (SELECT " + Schema.ParameterNameForAssociation + @" AS " + Schema.ColumnNameForAssociation + @") AS _Y
    ON _X." + Schema.ColumnNameForAssociation + @" = _Y." + Schema.ColumnNameForAssociation + @"
WHEN MATCHED THEN
UPDATE
        SET " + Schema.ColumnNameForRole + @" = " + Schema.ParameterNameForRole + @"
WHEN NOT MATCHED THEN
INSERT (" + Schema.ColumnNameForAssociation + @", " + Schema.ColumnNameForRole + @")
VALUES(" + Schema.ParameterNameForAssociation + @", " + Schema.ParameterNameForRole + @");
";
                        using (var command = new SqlCommand(cmdText, this.connection, this.transaction))
                        {
                            var associationParam = command.Parameters.Add(Schema.ParameterNameForAssociation, schema.SqlDbTypeForId);
                            var roleParam = command.Parameters.Add(Schema.ParameterNameForRole, schema.GetSqlDbType(roleType));

                            foreach (var association in flushChanged)
                            {
                                var changedCompositeRole = unitByAssociation[association];

                                associationParam.Value = association.Value;
                                roleParam.Value = ((ObjectId)changedCompositeRole).Value;
                                command.ExecuteNonQuery();
                            }
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

                    if (flushDeleted != null)
                    {
                        var schema = this.database.Schema;
                        var cmdText = @"
DELETE FROM " + schema.GetTableName(roleType.RelationType) + @"
WHERE " + Schema.ColumnNameForAssociation + @" = " + Schema.ParameterNameForAssociation;
                        using (var command = this.CreateCommand(cmdText))
                        {
                            var associationParam = command.Parameters.Add(Schema.ParameterNameForAssociation, schema.SqlDbTypeForId);

                            foreach (var association in flushDeleted)
                            {
                                associationParam.Value = association.Value;
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    if (flushAddedRoleByAssociation != null)
                    {
                        var schema = this.database.Schema;
                        var cmdText = @"
DELETE FROM " + schema.GetTableName(roleType.RelationType) + @"
WHERE " + Schema.ColumnNameForRole + @" = " + Schema.ParameterNameForRole + @";

INSERT INTO " + schema.GetTableName(roleType.RelationType) + @" (" + Schema.ColumnNameForAssociation + @", " + Schema.ColumnNameForRole + @")
VALUES(" + Schema.ParameterNameForAssociation + @", " + Schema.ParameterNameForRole + @");
";
                        using (var command = this.CreateCommand(cmdText))
                        {
                            var associationParam = command.Parameters.Add(Schema.ParameterNameForAssociation, schema.SqlDbTypeForId);
                            var roleParam = command.Parameters.Add(Schema.ParameterNameForRole, schema.GetSqlDbType(roleType));

                            foreach (var kvp in flushAddedRoleByAssociation)
                            {
                                var association = kvp.Key;
                                var roles = kvp.Value;

                                foreach (var role in roles)
                                {
                                    associationParam.Value = association.Value;
                                    roleParam.Value = role.Value;
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    if (flushRemovedRoleByAssociation != null)
                    {
                        var schema = this.database.Schema;
                        var cmdText = @"
DELETE FROM " + schema.GetTableName(roleType.RelationType) + @"
WHERE " + Schema.ColumnNameForAssociation + @" = " + Schema.ParameterNameForAssociation + @"
AND " + Schema.ColumnNameForRole + @" = " + Schema.ParameterNameForRole + @";
";
                        using (var command = this.CreateCommand(cmdText))
                        {
                            var associationParam = command.Parameters.Add(Schema.ParameterNameForAssociation, schema.SqlDbTypeForId);
                            var roleParam = command.Parameters.Add(Schema.ParameterNameForRole, schema.GetSqlDbType(roleType));

                            foreach (var kvp in flushRemovedRoleByAssociation)
                            {
                                var association = kvp.Key;
                                var roles = kvp.Value;

                                foreach (var role in roles)
                                {
                                    associationParam.Value = association.Value;
                                    roleParam.Value = role.Value;
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }

                this.oneToManyOriginalRoleByAssociationByRoleType.Remove(roleType);
            }
        }

        private void FlushManyToMany(IRoleType roleType)
        {
            if (this.manyToManyFlushAssociationsByRoleType != null)
            {
                HashSet<ObjectId> flushAssociations;
                if (this.manyToManyFlushAssociationsByRoleType.TryGetValue(roleType, out flushAssociations))
                {
                    var unitByAssociation = this.manyToManyRoleByAssociationByRoleType[roleType];

                    IList<ObjectId> flushDeleted;
                    IList<ObjectId> flushChanged;
                    SplitFlushAssociations(flushAssociations, unitByAssociation, out flushDeleted, out flushChanged);

                    if (flushDeleted != null)
                    {
                        var schema = this.database.Schema;
                        var cmdText = @"
DELETE FROM " + schema.GetTableName(roleType.RelationType) + @"
WHERE " + Schema.ColumnNameForAssociation + @" = " + Schema.ParameterNameForAssociation;
                        using (var command = this.CreateCommand(cmdText))
                        {
                            var associationParam = command.Parameters.Add(Schema.ParameterNameForAssociation, schema.SqlDbTypeForId);

                            foreach (var association in flushDeleted)
                            {
                                associationParam.Value = association.Value;
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    if (flushChanged != null)
                    {
                        var schema = this.database.Schema;
                        var cmdText = @"
MERGE " + schema.GetTableName(roleType.RelationType) + @" AS _X
USING (SELECT " + Schema.ParameterNameForAssociation + @" AS " + Schema.ColumnNameForAssociation + @") AS _Y
    ON _X." + Schema.ColumnNameForAssociation + @" = _Y." + Schema.ColumnNameForAssociation + @"
WHEN MATCHED THEN
UPDATE
        SET " + Schema.ColumnNameForRole + @" = " + Schema.ParameterNameForRole + @"
WHEN NOT MATCHED THEN
INSERT (" + Schema.ColumnNameForAssociation + @", " + Schema.ColumnNameForRole + @")
VALUES(" + Schema.ParameterNameForAssociation + @", " + Schema.ParameterNameForRole + @");
";
                        using (var command = this.CreateCommand(cmdText))
                        {
                            var associationParam = command.Parameters.Add(Schema.ParameterNameForAssociation, schema.SqlDbTypeForId);
                            var roleParam = command.Parameters.Add(Schema.ParameterNameForRole, schema.GetSqlDbType(roleType));

                            foreach (var association in flushChanged)
                            {
                                var changedCompositeRole = unitByAssociation[association];

                                associationParam.Value = association.Value;
                                roleParam.Value = changedCompositeRole;
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }

                this.manyToManyFlushAssociationsByRoleType.Remove(roleType);
            }
        }

        private void Reset()
        {
            this.strategyByObjectId = null;
            this.cacheIdByObjectId = null;

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

            this.manyToManyRoleByAssociationByRoleType = null;
            this.manyToManyAssociationByRoleByAssociationType = null;
            this.manyToManyFlushAssociationsByRoleType = null;
            this.manyToManyTriggerFlushRolesByAssociationType = null;
        }

        private void LazyConnect()
        {
            if (this.connection == null)
            {
                this.connection = new SqlConnection(this.database.ConnectionString);
                this.connection.Open();
                this.transaction = this.connection.BeginTransaction();
            }
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

        private Dictionary<ObjectId, ObjectId[]> GetManyToManyRoleByAssociation(IRoleType roleType)
        {
            if (this.manyToManyRoleByAssociationByRoleType == null)
            {
                this.manyToManyRoleByAssociationByRoleType = new Dictionary<IRoleType, Dictionary<ObjectId, ObjectId[]>>();
            }

            Dictionary<ObjectId, ObjectId[]> roleByAssociation;
            if (!this.manyToManyRoleByAssociationByRoleType.TryGetValue(roleType, out roleByAssociation))
            {
                roleByAssociation = new Dictionary<ObjectId, ObjectId[]>();
                this.manyToManyRoleByAssociationByRoleType[roleType] = roleByAssociation;
            }

            return roleByAssociation;
        }

        private HashSet<ObjectId> GetManyToManyFlushAssociations(IRoleType roleType)
        {
            if (this.manyToManyFlushAssociationsByRoleType == null)
            {
                this.manyToManyFlushAssociationsByRoleType = new Dictionary<IRoleType, HashSet<ObjectId>>();
            }

            HashSet<ObjectId> flushAssociations;
            if (!this.manyToManyFlushAssociationsByRoleType.TryGetValue(roleType, out flushAssociations))
            {
                flushAssociations = new HashSet<ObjectId>();
                this.manyToManyFlushAssociationsByRoleType[roleType] = flushAssociations;
            }

            return flushAssociations;
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