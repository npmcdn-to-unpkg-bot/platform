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

namespace Allors.Databases.Object.SqlClient.Commands.Procedure
{
    using System.Collections.Generic;
    using System.Data;

    using Allors.Databases.Object.SqlClient;
    using Allors.Meta;

    public class LoadCompositeRelationsFactory
    {
        internal readonly SqlClient.ManagementSession ManagementSession;
        
        internal LoadCompositeRelationsFactory(SqlClient.ManagementSession session)
        {
            this.ManagementSession = session;
        }

        public LoadCompositeRelations Create(IRoleType roleType)
        {
            var associationType = roleType.AssociationType;

            string sql;
            if (roleType.IsMany)
            {
                if (associationType.IsMany || !roleType.RelationType.ExistExclusiveLeafClasses)
                {
                    sql = Schema.AllorsPrefix + "A_" + roleType.SingularFullName;
                }
                else
                {
                    sql = Schema.AllorsPrefix + "A_" + ((IComposite)roleType.ObjectType).ExclusiveLeafClass.Name + "_" + associationType.SingularFullName;
                }
            }
            else
            {
                if (!roleType.RelationType.ExistExclusiveLeafClasses)
                {
                    sql = Schema.AllorsPrefix + "S_" + roleType.SingularFullName;
                }
                else
                {
                    sql = Schema.AllorsPrefix + "S_" + associationType.ObjectType.ExclusiveLeafClass.Name + "_" + roleType.SingularFullName;
                }
            }

            return new LoadCompositeRelations(this, sql);
        }

        public class LoadCompositeRelations : Commands.Command
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