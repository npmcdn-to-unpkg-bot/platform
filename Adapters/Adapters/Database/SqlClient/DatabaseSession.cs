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
    using System.Data.SqlClient;

    using Allors.Meta;

    public class DatabaseSession : IDatabaseSession
    {
        private readonly Database database;

        private SqlConnection connection;
        private SqlTransaction transaction;

        private Dictionary<ObjectId, Strategy> strategyByObjectId;

        private Dictionary<IRoleType, Dictionary<ObjectId, object>> roleByAssociationByRoleType;
        private Dictionary<IRoleType, HashSet<ObjectId>> flushAssociationsByRoleType;

        private Dictionary<IAssociationType, Dictionary<ObjectId, object>> associationByRoleByAssociationType;

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
            throw new NotImplementedException();
        }

        public Extent Extent(IComposite objectType)
        {
            throw new NotImplementedException();
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
                if (this.flushAssociationsByRoleType != null)
                {
                    foreach (var roleType in new List<IRoleType>(this.flushAssociationsByRoleType.Keys))
                    {
                        if (roleType.ObjectType is IUnit)
                        {
                            this.FlushUnit(roleType);
                        }
                        else
                        {
                            switch (roleType.RelationType.Multiplicity)
                            {
                                case Multiplicity.OneToOne:
                                    this.FlushCompositeOneToOne(roleType);
                                    break;
                                case Multiplicity.OneToMany:
                                    this.FlushCompositeOneToMany(roleType);
                                    break;
                                case Multiplicity.ManyToOne:
                                    this.FlushCompositeManyToOne(roleType);
                                    break;
                                case Multiplicity.ManyToMany:
                                    this.FlushCompositeManyToMany(roleType);
                                    break;
                            }
                        }
                    }
                }

                this.strategyByObjectId = null;
                this.roleByAssociationByRoleType = null;
                this.associationByRoleByAssociationType = null;
                this.flushAssociationsByRoleType = null;

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

                this.strategyByObjectId = null;
                this.roleByAssociationByRoleType = null;
                this.associationByRoleByAssociationType = null;
                this.flushAssociationsByRoleType = null;
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
            this.LazyConnect();

            var cmdText = @"
INSERT INTO " + Schema.TableNameForObjects + " (" + Schema.ColumnNameForType + ", " + Schema.ColumnNameForCache + @")
VALUES (" + Schema.ParameterNameForType + ", " + Schema.ParameterNameForCache + @");

SELECT " + Schema.ColumnNameForObject + @" = SCOPE_IDENTITY();
";
            using (var command = new SqlCommand(cmdText, this.connection, this.transaction))
            {
                command.Parameters.Add(Schema.ParameterNameForType, Schema.SqlDbTypeForType).Value = objectType.Id;
                command.Parameters.Add(Schema.ParameterNameForCache, Schema.SqlDbTypeForCache).Value = int.MaxValue;
                var result = command.ExecuteScalar().ToString();
                var objectId = this.database.ObjectIds.Parse(result);
                var strategy = new Strategy(this, objectType, objectId, true);
                return strategy.GetObject();
            }
        }

        public IObject[] Create(IClass objectType, int count)
        {
            throw new NotImplementedException();
        }

        public IObject Instantiate(IObject obj)
        {
            throw new NotImplementedException();
        }

        public IObject Instantiate(string objectId)
        {
            throw new NotImplementedException();
        }

        public IObject Instantiate(ObjectId objectId)
        {
            throw new NotImplementedException();
        }

        public IObject[] Instantiate(IObject[] objects)
        {
            throw new NotImplementedException();
        }

        public IObject[] Instantiate(string[] objectIds)
        {
            throw new NotImplementedException();
        }

        public IObject[] Instantiate(ObjectId[] objectIds)
        {
            throw new NotImplementedException();
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
            var schema = this.database.Schema;

            if (this.strategyByObjectId != null)
            {
                Strategy strategy;
                if (this.strategyByObjectId.TryGetValue(objectId, out strategy))
                {
                    return strategy;
                }
            }

            var cmdText = @"
SELECT  " + Schema.ColumnNameForType + ", " + Schema.ColumnNameForCache + @"
FROM " + Schema.TableNameForObjects + @"
WHERE " + Schema.ColumnNameForObject + @" = " + Schema.ParameterNameForObject + @"
";
            using (var command = new SqlCommand(cmdText, this.connection, this.transaction))
            {
                command.Parameters.Add(Schema.ParameterNameForObject, schema.SqlDbTypeForId).Value = objectId.Value;
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

                        var strategy = new Strategy(this, type, objectId, false);
                        this.strategyByObjectId[objectId] = strategy;
                        return strategy;
                    }
                }

                return null;
            }
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
            var roleByAssociation = this.GetRoleByAssociation(roleType);

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
                var roleByAssociation = this.GetRoleByAssociation(roleType);
                roleByAssociation[association] = role;

                var flushAssociations = this.GetFlushAssociations(roleType);
                flushAssociations.Add(association);
            }
        }

        internal void RemoveUnitRole(ObjectId association, IRoleType roleType)
        {
            var existingRole = this.GetUnitRole(association, roleType);
            if (existingRole != null)
            {
                var roleByAssociation = this.GetRoleByAssociation(roleType);
                roleByAssociation[association] = null;

                var flushAssociations = this.GetFlushAssociations(roleType);
                flushAssociations.Add(association);
            }
        }
        #endregion

        #region Composite One <-> One
        internal bool ExistCompositeRoleOneToOne(ObjectId association, IRoleType roleType)
        {
            return this.GetCompositeRoleOneToOne(association, roleType) != null;
        }

        internal ObjectId GetCompositeRoleOneToOne(ObjectId association, IRoleType roleType)
        {
            var roleByAssociation = this.GetRoleByAssociation(roleType);

            object role;
            if (!roleByAssociation.TryGetValue(association, out role))
            {
                role = this.FetchCompositeRole(association, roleType);
                roleByAssociation[association] = role;
            }

            return (ObjectId)role;
        }

        internal void SetCompositeRoleOneToOne(ObjectId association, IRoleType roleType, ObjectId role)
        {
            var existingRole = this.GetCompositeRoleOneToOne(association, roleType);
            if (!Equals(role, existingRole))
            {
                var roleByAssociation = this.GetRoleByAssociation(roleType);
                var associationByRole = this.GetAssociationByRole(roleType.AssociationType);

                // Existing Association -> Role
                object existingAssociation;
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
                    roleByAssociation[(ObjectId)existingAssociation] = null;
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

                var flushAssociations = this.GetFlushAssociations(roleType);
                flushAssociations.Add(association);
            }
        }

        internal void RemoveCompositeRoleOneToOne(ObjectId association, IRoleType roleType)
        {
            var existingRole = this.GetCompositeRoleOneToOne(association, roleType);
            if (existingRole != null)
            {
                var roleByAssociation = this.GetRoleByAssociation(roleType);
                var associationByRole = this.GetAssociationByRole(roleType.AssociationType);

                // Association <- Role
                associationByRole[existingRole] = null;

                // Association -> Role
                roleByAssociation[association] = null;

                var flushAssociations = this.GetFlushAssociations(roleType);
                flushAssociations.Add(association);
            }
        }

        internal bool ExistCompositeAssociationOneToOne(ObjectId role, IAssociationType associationType)
        {
            return this.GetCompositeAssociationOneToOne(role, associationType) != null;
        }

        internal ObjectId GetCompositeAssociationOneToOne(ObjectId role, IAssociationType associationType)
        {
            var associationByRole = this.GetAssociationByRole(associationType);
            object association;
            if (!associationByRole.TryGetValue(role, out association))
            {
                association = this.FetchCompositeAssociation(role, associationType);
                associationByRole[role] = association;
            }

            return (ObjectId)association;
        }
        #endregion

        #region Composite One <-> Many
        internal bool ExistCompositeRoleOneToMany(ObjectId association, IRoleType roleType)
        {
            throw new NotImplementedException();
        }

        internal ObjectId[] GetCompositeRolesOneToMany(ObjectId association, IRoleType roleType)
        {
            throw new NotImplementedException();
        }

        internal void AddCompositeRoleOneToMany(ObjectId association, IRoleType roleType, ObjectId role)
        {
            throw new NotImplementedException();
        }

        internal void RemoveCompositeRoleOneToMany(ObjectId association, IRoleType roleType, ObjectId role)
        {
            throw new NotImplementedException();
        }

        internal void SetCompositeRoleOneToMany(ObjectId association, IRoleType roleType, ObjectId[] roles)
        {
            throw new NotImplementedException();
        }

        internal void RemoveCompositeRolesOneToMany(ObjectId association, IRoleType roleType)
        {
            throw new NotImplementedException();
        }

        internal bool ExistCompositeAssociationOneToMany(ObjectId role, IAssociationType associationType)
        {
            throw new NotImplementedException();
        }

        internal ObjectId GetCompositeAssociationOneToMany(ObjectId role, IAssociationType associationType)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Composite Many <-> One
        internal bool ExistCompositeRoleManyToOne(ObjectId association, IRoleType roleType)
        {
            throw new NotImplementedException();
        }

        internal ObjectId GetCompositeRoleManyToOne(ObjectId association, IRoleType roleType)
        {
            throw new NotImplementedException();
        }

        internal void SetCompositeRoleManyToOne(ObjectId association, IRoleType roleType, ObjectId role)
        {
            throw new NotImplementedException();
        }

        internal void RemoveCompositeRoleManyToOne(ObjectId association, IRoleType roleType)
        {
            throw new NotImplementedException();
        }

        internal bool ExistCompositeAssociationsManyToOne(ObjectId role, IAssociationType associationType)
        {
            throw new NotImplementedException();
        }

        internal ObjectId[] GetCompositeAssociationsManyToOne(ObjectId role, IAssociationType associationType)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Many <-> Many
        internal bool ExistCompositeRoleManyToMany(ObjectId association, IRoleType roleType)
        {
            throw new NotImplementedException();
        }

        internal ObjectId[] GetCompositeRolesManyToMany(ObjectId association, IRoleType roleType)
        {
            throw new NotImplementedException();
        }

        internal void AddCompositeRoleManyToMany(ObjectId association, IRoleType roleType, ObjectId role)
        {
            throw new NotImplementedException();
        }

        internal void RemoveCompositeRoleManyToMany(ObjectId association, IRoleType roleType, ObjectId role)
        {
            throw new NotImplementedException();
        }

        internal void SetCompositeRoleManyToMany(ObjectId association, IRoleType roleType, ObjectId[] roles)
        {
            throw new NotImplementedException();
        }

        internal void RemoveCompositeRolesManyToMany(ObjectId association, IRoleType roleType)
        {
            throw new NotImplementedException();
        }

        internal bool ExistCompositeAssociationsManyToMany(ObjectId role, IAssociationType associationType)
        {
            throw new NotImplementedException();
        }

        internal ObjectId[] GetCompositeAssociationsManyToMany(ObjectId role, IAssociationType associationType)
        {
            throw new NotImplementedException();
        }
        #endregion

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

        private Dictionary<ObjectId, object> GetRoleByAssociation(IRoleType roleType)
        {
            if (this.roleByAssociationByRoleType == null)
            {
                this.roleByAssociationByRoleType = new Dictionary<IRoleType, Dictionary<ObjectId, object>>();
            }

            Dictionary<ObjectId, object> roleByAssociation;
            if (!this.roleByAssociationByRoleType.TryGetValue(roleType, out roleByAssociation))
            {
                roleByAssociation = new Dictionary<ObjectId, object>();
                this.roleByAssociationByRoleType[roleType] = roleByAssociation;
            }

            return roleByAssociation;
        }

        private Dictionary<ObjectId, object> GetAssociationByRole(IAssociationType associationType)
        {
            if (this.associationByRoleByAssociationType == null)
            {
                this.associationByRoleByAssociationType = new Dictionary<IAssociationType, Dictionary<ObjectId, object>>();
            }

            Dictionary<ObjectId, object> associationByRole;
            if (!this.associationByRoleByAssociationType.TryGetValue(associationType, out associationByRole))
            {
                associationByRole = new Dictionary<ObjectId, object>();
                this.associationByRoleByAssociationType[associationType] = associationByRole;
            }

            return associationByRole;
        }

        private HashSet<ObjectId> GetFlushAssociations(IRoleType roleType)
        {
            if (this.flushAssociationsByRoleType == null)
            {
                this.flushAssociationsByRoleType = new Dictionary<IRoleType, HashSet<ObjectId>>();
            }

            HashSet<ObjectId> flushAssociations;
            if (!this.flushAssociationsByRoleType.TryGetValue(roleType, out flushAssociations))
            {
                flushAssociations = new HashSet<ObjectId>();
                this.flushAssociationsByRoleType[roleType] = flushAssociations;
            }
            return flushAssociations;
        }

        private object FetchUnitRole(ObjectId association, IRoleType roleType)
        {
            this.LazyConnect();
            var schema = this.database.Schema;

            var cmdText = @"
SELECT " + Schema.ColumnNameForRole + @"
FROM " + schema.GetTableName(roleType.RelationType) + @"
WHERE " + Schema.ColumnNameForAssociation + @"=" + Schema.ParameterNameForAssociation + @";
";
            using (var command = new SqlCommand(cmdText, this.connection, this.transaction))
            {
                command.Parameters.Add(Schema.ParameterNameForAssociation, schema.SqlDbTypeForId).Value = association.Value;
                return command.ExecuteScalar();
            }
        }

        private ObjectId FetchCompositeRole(ObjectId association, IRoleType roleType)
        {
            this.LazyConnect();
            var schema = this.database.Schema;

            var cmdText = @"
SELECT " + Schema.ColumnNameForRole + @"
FROM " + schema.GetTableName(roleType.RelationType) + @"
WHERE " + Schema.ColumnNameForAssociation + @"=" + Schema.ParameterNameForAssociation + @";
";
            using (var command = new SqlCommand(cmdText, this.connection, this.transaction))
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

        private ObjectId FetchCompositeAssociation(ObjectId role, IAssociationType associationType)
        {
            this.LazyConnect();
            var schema = this.database.Schema;

            var cmdText = @"
SELECT " + Schema.ColumnNameForAssociation + @"
FROM " + schema.GetTableName(associationType.RelationType) + @"
WHERE " + Schema.ColumnNameForRole + @"=" + Schema.ParameterNameForRole + @";
";
            using (var command = new SqlCommand(cmdText, this.connection, this.transaction))
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
        
        private void FlushUnit(IRoleType roleType)
        {
            if (this.flushAssociationsByRoleType != null)
            {
                this.LazyConnect();
                var schema = this.database.Schema;
                
                HashSet<ObjectId> flushAssociations;
                if(this.flushAssociationsByRoleType.TryGetValue(roleType, out flushAssociations))
                {
                    var unitByAssociation = this.roleByAssociationByRoleType[roleType];

                    IList<ObjectId> flushDeleted;
                    IList<ObjectId> flushChanged;
                    SplitFlushAssociations(flushAssociations, unitByAssociation, out flushDeleted, out flushChanged);

                    if (flushDeleted != null)
                    {
                        var cmdText = @"
DELETE FROM " + schema.GetTableName(roleType.RelationType) + @"
WHERE " + Schema.ColumnNameForAssociation + @" = " + Schema.ParameterNameForAssociation;
                        using (var command = new SqlCommand(cmdText, this.connection, this.transaction))
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

        private void FlushCompositeOneToOne(IRoleType roleType)
        {
            if (this.flushAssociationsByRoleType != null)
            {
                this.LazyConnect();
                var schema = this.database.Schema;
                
                HashSet<ObjectId> flushAssociations;
                if(this.flushAssociationsByRoleType.TryGetValue(roleType, out flushAssociations))
                {
                    var unitByAssociation = this.roleByAssociationByRoleType[roleType];

                    IList<ObjectId> flushDeleted;
                    IList<ObjectId> flushChanged;
                    SplitFlushAssociations(flushAssociations, unitByAssociation, out flushDeleted, out flushChanged);

                    if (flushDeleted != null)
                    {
                        var cmdText = @"
DELETE FROM " + schema.GetTableName(roleType.RelationType) + @"
WHERE " + Schema.ColumnNameForAssociation + @" = " + Schema.ParameterNameForAssociation;
                        using (var command = new SqlCommand(cmdText, this.connection, this.transaction))
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
            }
        }

        private void FlushCompositeOneToMany(IRoleType roleType)
        {
        }

        private void FlushCompositeManyToOne(IRoleType roleType)
        {
            if (this.flushAssociationsByRoleType != null)
            {
                this.LazyConnect();
                var schema = this.database.Schema;

                HashSet<ObjectId> flushAssociations;
                if (this.flushAssociationsByRoleType.TryGetValue(roleType, out flushAssociations))
                {
                    var unitByAssociation = this.roleByAssociationByRoleType[roleType];

                    IList<ObjectId> flushDeleted;
                    IList<ObjectId> flushChanged;
                    SplitFlushAssociations(flushAssociations, unitByAssociation, out flushDeleted, out flushChanged);

                    if (flushDeleted != null)
                    {
                        var cmdText = @"
DELETE FROM " + schema.GetTableName(roleType.RelationType) + @"
WHERE " + Schema.ColumnNameForAssociation + @" = " + Schema.ParameterNameForAssociation;
                        using (var command = new SqlCommand(cmdText, this.connection, this.transaction))
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
            }
        }

        private void FlushCompositeManyToMany(IRoleType roleType)
        {
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
    }
}