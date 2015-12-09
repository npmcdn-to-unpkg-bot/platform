// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultConnection.cs" company="Allors bvba">
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

namespace Allors.Adapters.Object.SqlClient
{
    using System.Data.SqlClient;

    public sealed class DefaultConnection : Connection
    {
        public DefaultConnection(Database database)
            : base(database)
        {
        }

        protected override Command CreateCommand(Mapping mapping, SqlCommand sqlCommand)
        {
            return new DefaultCommand(mapping, sqlCommand);
        }

        #region Events
        protected override void OnCreateSqlConnection()
        {
        }

        protected override void OnCreatedSqlConnection()
        {
        }

        protected override void OnOpenSqlConnection()
        {
        }

        protected override void OnOpenedSqlConnection()
        {
        }

        protected override void OnCloseSqlConnection()
        {
        }

        protected override void OnClosedSqlConnection()
        {
        }

        protected override void OnCreateSqlTransaction()
        {
        }

        protected override void OnCreatedSqlTransaction()
        {
        }

        protected override void OnCommit()
        {
        }

        protected override void OnCommitted()
        {
        }

        protected override void OnRollback()
        {
        }

        protected override void OnRolledback()
        {
        }

        protected override void OnCreateSqlCommand()
        {
        }

        protected override void OnCreatedSqlCommand()
        {
        }
        #endregion
    }
}