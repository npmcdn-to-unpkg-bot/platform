// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Profile.cs" company="Allors bvba">
//   Copyright 2002-2010 Allors bvba.
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

namespace Allors.Database.Special.SqlClient.IntegerId.Snapshot
{
    using System;

    using Allors.Meta;

    using Domain;

    public class Profile : SqlClient.Profile
    {
        public override string DatabaseName
        {
            get { return "SqlClientIntegerIdSnapshot"; }
        }

        public IDatabase CreateDatabase(Domain domain, bool init)
        {
            var domainAssembly = typeof(C1).Assembly;
            var properties = new[]
                {
                    new PropertyDefinition("allors.database.sql.connection", "Integrated Security=SSPI;Data Source=(local);Initial Catalog=adapters;", "System.String"),
                    new PropertyDefinition("allors.database.sqlclient.isolation.level", "Snapshot", "System.Data.IsolationLevel, System.Data")
                };
            var database = new Database.SqlClient.IntegerId.Database(domain, domainAssembly, Guid.NewGuid(), properties);

            if (init)
            {
                database.Init();
            }

            return database;
        }

    }
}