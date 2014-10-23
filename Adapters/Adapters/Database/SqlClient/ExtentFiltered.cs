//------------------------------------------------------------------------------------------------- 
// <copyright file="AllorsExtentFilteredSql.cs" company="Allors bvba">
// Copyright 2002-2009 Allors bvba.
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
// <summary>Defines the AllorsExtentFilteredSql type.</summary>
//-------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Data.Common;
using Allors.Meta;

namespace Allors.Adapters.Database.SqlClient
{
    public class AllorsExtentFilteredSql : AllorsExtentSql
    {
        internal readonly DatabaseSession session;
        internal readonly IComposite type;
        internal IAssociationType association;
        private AllorsPredicateAndSql filter;
        internal IRoleType role;
        // IRelationTypes direct from Strategy
        internal Strategy strategy;

        internal AllorsExtentFilteredSql(DatabaseSession session, Strategy strategy, IRoleType role) : this(session, role.ObjectType)
        {
            this.strategy = strategy;
            this.role = role;
        }

        internal AllorsExtentFilteredSql(DatabaseSession session, Strategy strategy, IAssociationType association) : this(session, association.ObjectType)
        {
            this.strategy = strategy;
            this.association = association;
        }

        internal AllorsExtentFilteredSql(DatabaseSession session, IComposite type)
        {
            this.session = session;
            this.type = type;
        }

        public override ICompositePredicate Filter
        {
            get
            {
                LazyLoadFilter();
                return filter;
            }
        }

        internal Schema Schema
        {
            get { return session.Database.Schema; }
        }

        internal override DatabaseSession Session
        {
            get { return session; }
        }

        public override IComposite ObjectType
        {
            get { return type; }
        }

        protected override ArrayList GetObjects()
        {
            if (strategy != null)
            {
                if (role != null)
                {
                    return strategy.ExtentGetCompositeRoles(role);
                }
                return this.strategy.ExtentGetCompositeAssociations(this.association);
            }

            this.session.Sync();

            var statement = new AllorsExtentStatementRootSql(this);
            var objects = new ArrayList();

            this.BuildSql(statement);

            if (statement.Sorter != null)
            {
                statement.Sorter.BuildOrder(statement.Sorter, this.Schema, statement);
            }

            using (var command = statement.CreateSqlCommand())
            {
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ObjectId objectId = this.session.Database.allorsObjectIds.Parse(reader.GetValue(0).ToString());
                        Guid typeId = command.GetUnique(reader, 1);
                        IObjectType instantiateObjectType = this.Session.Database.MetaTypesById[typeId];
                        objects.Add(this.session.InstantiateExistingStrategy(instantiateObjectType, objectId).GetAllorsObject());
                    }

                    reader.Close();
                }
            }

            return objects;
        }

        protected override void LazyLoadFilter()
        {
            if (filter == null)
            {
                filter = new AllorsPredicateAndSql(this);
                strategy = null;
                association = null;
                role = null;
                FlushCache();
            }
        }

        internal override string BuildSql(AllorsExtentStatementSql statement)
        {
            if (filter != null)
            {
                filter.Setup(this, statement);
            }

            string alias = statement.CreateAlias();

            if (statement.IsRoot)
            {
                //TODO: DISTINCT isn't always necessary
                statement.Append("SELECT DISTINCT " + alias + "." + Schema.ColumnNameForObject + ", " + alias + "." + Schema.C);

                if (statement.Sorter != null)
                {
                    statement.Sorter.Setup(this, statement);
                    statement.Sorter.BuildSelect(this, Schema, statement);
                }

                statement.Append(" FROM " + Schema.ColumnNameForType + " " + alias);

                statement.AddJoins(alias);
                statement.AddWhere(alias);

                if (filter != null)
                {
                    filter.BuildWhere(this, Schema, statement, type, alias);
                }
            }
            else
            {
                AllorsExtentStatementChildSql inStatement = (AllorsExtentStatementChildSql) statement;
                if (inStatement.Role != null)
                {
                    IRoleType inRole = inStatement.Role;
                    IAssociationType inAssociation = inRole.AssociationType;
                    statement.Append("SELECT " + inAssociation.SingularFullName + "_A." + Schema.ColumnNameForAssociation);
                }
                else
                {
                    IAssociationType inAssociation = inStatement.Association;
                    IRoleType inRole = inAssociation.RoleType;
                    statement.Append("SELECT " + inRole.SingularFullName + "_R." + Schema.ColumnNameForRole);
                }

                statement.Append(" FROM " + Schema.ColumnNameForType + " " + alias);

                statement.AddJoins(alias);
                statement.AddWhere(alias);

                if (filter != null)
                {
                    filter.BuildWhere(this, Schema, statement, type, alias);
                }

                statement.Append(" AND ");

                if (inStatement.Role != null)
                {
                    IRoleType inRole = inStatement.Role;
                    IAssociationType inAssociation = inRole.AssociationType;
                    statement.Append(inAssociation.SingularFullName + "_A." + Schema.ColumnNameForAssociation + " IS NOT NULL ");
                }
                else
                {
                    IAssociationType inAssociation = inStatement.Association;
                    IRoleType inRole = inAssociation.RoleType;
                    statement.Append(inRole.SingularFullName + "_R." + Schema.ColumnNameForRole + " IS NOT NULL ");
                }
            }

            return alias;
        }

        internal void CheckAssociation(IAssociationType association)
        {
            if (Array.IndexOf(type.AssociationTypes, association) < 0)
            {
                throw new ArgumentException("Extent does not implement association " + association.SingularFullName);
            }
        }

        internal void CheckRole(IRoleType role)
        {
            if (Array.IndexOf(type.RoleTypes, role) < 0)
            {
                throw new ArgumentException("Extent does not implement role " + role.SingularFullName);
            }
        }
    }
}