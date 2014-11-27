// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Database.cs" company="Allors bvba">
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

namespace Allors.Databases.Object.SqlClient.LongId
{
    using System.Collections.Generic;

    using Allors.Adapters;
    using Allors.Adapters.Database.Sql;
    using Allors.Meta;

    using Microsoft.SqlServer.Server;

    public class Database : SqlClient.Database 
    {
        private readonly IObjectIds allorsObjectIds;
       
        private Schema schema;

        public Database(SqlClient.Configuration configuration)
            : base(configuration)
        {
            this.allorsObjectIds = new ObjectLongIds();
        }

        public override IObjectIds AllorsObjectIds
        {
            get { return this.allorsObjectIds; }
        }
        
        public override SqlClient.Schema SqlClientSchema
        {
            get
            {
                if (this.ObjectFactory.MetaPopulation != null)
                {
                    if (this.schema == null)
                    {
                        this.schema = new Schema(this);
                    }
                }

                return this.schema;
            }
        }

        public override void ResetSchema()
        {
            this.schema = null;
        }

        internal override IEnumerable<SqlDataRecord> CreateObjectTable(IEnumerable<ObjectId> objectids)
        {
            return new ObjectTableForObjectIds(this.schema, objectids);
        }

        internal override IEnumerable<SqlDataRecord> CreateObjectTable(IEnumerable<Reference> strategies)
        {
            return new ObjectTableForStrategies(this.schema, strategies);
        }

        internal override IEnumerable<SqlDataRecord> CreateObjectTable(Dictionary<Reference, Roles> rolesByReference)
        {
            return new ObjectTableForRolesByReference(this.schema, rolesByReference);
        }

        internal override IEnumerable<SqlDataRecord> CreateRelationTable(IEnumerable<CompositeRelation> relations)
        {
            return new RelationTableForCompositeRelations(this.schema, relations);
        }

        internal override IEnumerable<SqlDataRecord> CreateRelationTable(IRoleType roleType, IEnumerable<UnitRelation> relations)
        {
            return new RelationTableForUnitRelations(this, roleType, relations);
        }
    }
}