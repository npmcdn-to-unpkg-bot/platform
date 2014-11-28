// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Load.cs" company="Allors bvba">
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

namespace Allors.Databases.Object.SqlClient
{
    using System.Text;
    using System.Xml;

    public class Load : Adapters.Database.Sql.Load
    {
        private readonly Database database;

        public Load(Database database, ObjectNotLoadedEventHandler objectNotLoaded, RelationNotLoadedEventHandler relationNotLoaded, XmlReader reader)
            : base(database, objectNotLoaded, relationNotLoaded, reader)
        {
            this.database = database;
        }

        protected override void LoadObjectsPostProcess(ManagementSession session)
        {
            var sql = new StringBuilder();

            sql.Append("SET IDENTITY_INSERT " + this.database.Schema.Objects.StatementName + " ON");
            lock (this)
            {
                using (var command = session.CreateCommand(sql.ToString()))
                {
                    command.ExecuteNonQuery();
                }
            }

            base.LoadObjectsPostProcess(session);

            sql = new StringBuilder();
            sql.Append("SET IDENTITY_INSERT " + this.database.Schema.Objects.StatementName + " OFF");
            lock (this)
            {
                using (var command = session.CreateCommand(sql.ToString()))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}