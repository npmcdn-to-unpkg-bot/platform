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

        private Dictionary<IRoleType, Dictionary<ObjectId, object>> unitRoleByAssociationByRoleType;
        private Dictionary<IRoleType, Dictionary<ObjectId, object>> changedUnitRoleByAssociationByRoleType;
    
        public DatabaseSession(Database database)
        {
            this.database = database;
        }

        public event SessionCommittedEventHandler Committed;

        public event SessionCommittingEventHandler Committing;

        public event SessionRollingBackEventHandler RollingBack;

        public event SessionRolledBackEventHandler RolledBack;

        public IPopulation Population 
        {
            get
            {
                return this.database;
            }
        }

        public IDatabase Database 
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
                this.Flush();

                if (this.transaction != null)
                {
                    this.transaction.Commit();
                    this.transaction = null;
                }
            }
            finally
            {
                this.Release();
            }
        }

        public void Rollback()
        {
            try
            {
                this.unitRoleByAssociationByRoleType = null;
                this.changedUnitRoleByAssociationByRoleType = null;
            }
            finally
            {
                this.Release();
            }
        }

        public T Create<T>() where T : IObject
        {
            throw new NotImplementedException();
        }

        public IObject Create(IClass objectType)
        {
            this.LazyConnect();
            var schema = this.database.Schema;

            var cmdText = @"
INSERT INTO " + Schema.TableNameForObjects + " (" + Schema.ColumnNameForType + ", " + Schema.ColumnNameForCache + @")
VALUES (" + Schema.ParameterNameForType + ", " + Schema.ParameterNameForCache + @");

SELECT " + Schema.ColumnNameForObject + @" = SCOPE_IDENTITY();
";
            using (var command = new SqlCommand(cmdText, this.connection, this.transaction))
            {
                command.Parameters.Add(Schema.ParameterNameForType, Schema.SqlDbTypeForType).Value = objectType.Id;
                command.Parameters.Add(Schema.ParameterNameForCache, Schema.SqlDbTypeForCache).Value = int.MaxValue;

                command.Prepare();
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
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            this.Rollback();
        }
        
        internal virtual object GetUnitRole(ObjectId association, IRoleType roleType)
        {
            if (this.changedUnitRoleByAssociationByRoleType == null)
            {
                this.changedUnitRoleByAssociationByRoleType = new Dictionary<IRoleType, Dictionary<ObjectId, object>>();
            }

            Dictionary<ObjectId, object> changedUnitRoleByAssociation;
            if (this.changedUnitRoleByAssociationByRoleType.TryGetValue(roleType, out changedUnitRoleByAssociation))
            {
                object changedUnitRole;
                if (changedUnitRoleByAssociation.TryGetValue(association, out changedUnitRole))
                {
                    return changedUnitRole;
                }
            }

            if (this.unitRoleByAssociationByRoleType == null)
            {
                this.unitRoleByAssociationByRoleType = new Dictionary<IRoleType, Dictionary<ObjectId, object>>();
            }

            Dictionary<ObjectId, object> unitRoleByAssociation;
            if (!this.unitRoleByAssociationByRoleType.TryGetValue(roleType, out unitRoleByAssociation))
            {
                unitRoleByAssociation = new Dictionary<ObjectId, object>();
                this.unitRoleByAssociationByRoleType[roleType] = unitRoleByAssociation;
            }

            object unitRole;
            if (!unitRoleByAssociation.TryGetValue(association, out unitRole))
            {
                unitRole = this.FetchUnitRole(association, roleType);
                unitRoleByAssociation[association] = unitRole;
            }

            return unitRole;
        }

        internal virtual void SetUnitRole(ObjectId association, IRoleType roleType, object role)
        {
            var existingUnitRole = this.GetUnitRole(association, roleType);
            if (!Equals(role, existingUnitRole))
            {
                Dictionary<ObjectId, object> changedUnitRoleByAssociation;
                if (!this.changedUnitRoleByAssociationByRoleType.TryGetValue(roleType, out changedUnitRoleByAssociation))
                {
                    changedUnitRoleByAssociation = new Dictionary<ObjectId, object>();
                    this.changedUnitRoleByAssociationByRoleType[roleType] = changedUnitRoleByAssociation;
                }

                changedUnitRoleByAssociation[association] = role;

                Dictionary<ObjectId, object> unitRoleByAssociation;
                if (this.unitRoleByAssociationByRoleType.TryGetValue(roleType, out unitRoleByAssociation))
                {
                    unitRoleByAssociation[association] = role;
                }
            }
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

                command.Prepare();

                return command.ExecuteScalar();
            }
        }

        private void Flush()
        {
            this.LazyConnect();
            var schema = this.database.Schema;

            if (this.changedUnitRoleByAssociationByRoleType != null)
            {
                foreach (var keyValuePair in this.changedUnitRoleByAssociationByRoleType)
                {
                    var roleType = keyValuePair.Key;
                    var changedUnitRoleByAssociation = keyValuePair.Value;

                    IList<ObjectId> toDelete = null;
                    IList<ObjectId> toChange = null;
                    
                    foreach (var keyValuePair2 in changedUnitRoleByAssociation)
                    {
                        var association = keyValuePair2.Key;
                        var changedUnitRole = keyValuePair2.Value;

                        if (changedUnitRole == null)
                        {
                            if (toDelete == null)
                            {
                                toDelete = new List<ObjectId>();
                            }

                            toDelete.Add(association);
                        }
                        else
                        {
                            if (toChange == null)
                            {
                                toChange = new List<ObjectId>();
                            }

                            toChange.Add(association);
                        }
                    }

                    if (toDelete != null)
                    {
                        var cmdText = @"
DELETE FROM " + schema.GetTableName(roleType.RelationType) + @"
WHERE " + Schema.ColumnNameForAssociation + @" = " + Schema.ParameterNameForAssociation;
                        using (var command = new SqlCommand(cmdText, this.connection, this.transaction))
                        {
                            var associationParam = command.Parameters.Add(Schema.ParameterNameForAssociation, schema.SqlDbTypeForId);

                            foreach (var association in toDelete)
                            {
                                associationParam.Value = association.Value;
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    if (toChange != null)
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

                            foreach (var association in toChange)
                            {
                                var changedUnitRole = changedUnitRoleByAssociation[association];

                                associationParam.Value = association.Value;
                                roleParam.Value = changedUnitRole;
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }

                this.changedUnitRoleByAssociationByRoleType = null;
            }
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

        private void Release()
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