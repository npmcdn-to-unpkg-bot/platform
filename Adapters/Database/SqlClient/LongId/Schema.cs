//------------------------------------------------------------------------------------------------- 
// <copyright file="Schema.cs" company="Allors bvba">
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
// <summary>Defines the Schema type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.R1.Adapters.Database.SqlClient.LongId
{
    using System.Data;

    public sealed class Schema : SqlClient.Schema
    {
        public Schema(SqlClient.Database database) : base(database)
        {
            this.OnConstructed();
        }

        protected override DbType ObjectDbType
        {
            get
            {
                return DbType.Int64;
            }
        }

        protected override SqlDbType SqlDbType
        {
            get
            {
                return SqlDbType.BigInt;
            }
        }
    }
}