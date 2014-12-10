// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SchemaTest.cs" company="Allors bvba">
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

namespace Allors.Adapters.Special.Npgsql.IntegerId.ReadCommitted
{
    using Allors.Adapters.Database.Sql;
    using Allors.Meta;

    using NUnit.Framework;

    using IDatabase = IDatabase;

    [TestFixture]
    public class SchemaTest : SchemaIntegerIdTest
    {
        private readonly Profile profile = new Profile();

        protected override IProfile Profile
        {
            get
            {
                return this.profile;
            }
        }

        protected override IDatabase CreateDatabase(IMetaPopulation metaPopulation, bool init)
        {
            return this.profile.CreateDatabase(metaPopulation, init);
        }

        [TearDown]
        protected void Dispose()
        {
            this.profile.Dispose();
        }

        protected override void DropProcedure(string procedure)
        {
            this.profile.DropProcedure(procedure);
        }

        protected override bool ExistProcedure(string procedure)
        {
            return this.profile.ExistProcedure(procedure);
        }

        protected override bool ExistPrimaryKey(string table, string column)
        {
            return this.profile.ExistPrimaryKey(table, column);
        }

        protected override void DropTable(string tableName)
        {
            this.profile.DropTable(tableName);
        }

        protected override bool ExistIndex(string table, string column)
        {
            return this.profile.ExistIndex(table, column);
        }

        protected override bool IsInteger(string table, string column)
        {
            return this.profile.IsInteger(table, column);
        }

        protected override bool IsLong(string table, string column)
        {
            return this.profile.IsLong(table, column);
        }

        protected override bool IsUnique(string table, string column)
        {
            return this.profile.IsUnique(table, column);
        }

        protected override SchemaValidationErrors GetSchemaValidation(IDatabase repository)
        {
            return ((Database)repository).Schema.SchemaValidationErrors;
        }
    }
}