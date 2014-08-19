// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CacheTest.cs" company="Allors bvba">
//   Copyright 2002-2012 Allors bvba.
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

namespace Allors.R1.Adapters.Special.SqlClient.LongId.ReadCommitted
{
    using Allors.R1.Adapters.Database.Sql;

    using NUnit.Framework;

    using IDatabase = Allors.R1.IDatabase;

    [TestFixture]
    public class CacheTest : Special.CacheTest
    {
        private readonly Profile profile = new Profile();

        protected override DatabaseSession CreateSession()
        {
            return (DatabaseSession)this.profile.CreateSession();
        }

        protected override IDatabase CreateDatabase()
        {
            return this.profile.CreateDatabase();
        }

        [TearDown]
        protected void Dispose()
        {
            this.profile.Dispose();
        }
    }
}
