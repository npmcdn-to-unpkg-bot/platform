// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoadCompositeRelationsFactory.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
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
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Adapters.Database.SqlClient.Commands.Procedure
{
    using System.Collections.Generic;
    using System.Data;

    using Allors.Adapters.Database.Sql;
    using Allors.Adapters.Database.Sql.Commands;
    using Allors.Meta;

    internal class LoadCompositeRelationsFactory : ILoadCompositeRelationsFactory
    {
        internal readonly SqlClient.ManagementSession ManagementSession;
        
        internal LoadCompositeRelationsFactory(SqlClient.ManagementSession session)
        {
            this.ManagementSession = session;
        }

        public ILoadCompositeRelations Create(RoleType roleType)
        {
            var associationType = roleType.AssociationType;

            string sql;
            if (roleType.IsMany)
            {
                if (associationType.IsMany || !roleType.RelationType.ExistExclusiveRootClasses)
                {
                    sql = Schema.AllorsPrefix + "A_" + roleType.FullSingularName;
                }
                else
                {
                    var compositeType = (Composite)roleType.ObjectType;
                    sql = Schema.AllorsPrefix + "A_" + compositeType.ExclusiveRootClass.Name + "_" + associationType.Name;
                }
            }
            else
            {
                if (!roleType.RelationType.ExistExclusiveRootClasses)
                {
                    sql = Schema.AllorsPrefix + "S_" + roleType.FullSingularName;
                }
                else
                {
                    sql = Schema.AllorsPrefix + "S_" + associationType.ObjectType.ExclusiveRootClass.Name + "_" + roleType.RootName;
                }
            }

            return new LoadCompositeRelations(this, sql);
        }

        private class LoadCompositeRelations : Commands.Command, ILoadCompositeRelations
        {
            private readonly LoadCompositeRelationsFactory factory;
            private readonly string sql;

            public LoadCompositeRelations(LoadCompositeRelationsFactory factory, string sql)
            {
                this.factory = factory;
                this.sql = sql;
            }

            public void Execute(IList<CompositeRelation> relations)
            {
                var database = this.factory.ManagementSession.SqlClientDatabase;

                var command = this.factory.ManagementSession.CreateSqlCommand(this.sql);
                command.CommandType = CommandType.StoredProcedure;
                this.AddInTable(command, database.SqlClientSchema.CompositeRelationTableParam, database.CreateRelationTable(relations));

                command.ExecuteNonQuery();
            }
        }
    }
}