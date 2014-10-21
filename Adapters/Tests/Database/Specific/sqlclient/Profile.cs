// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Profile.cs" company="Allors bvba">
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

namespace Allors.Adapters.Special.SqlClient
{
    using System.Text;

    public abstract class Profile : Special.Profile
    {
//        public void DropTable(string tableName)
//        {
//            using (var connection = ((Database)this.CreateDatabase()).CreateDbConnection())
//            {
//                connection.Open();
//                using (var command = connection.CreateCommand())
//                {
//                    var sql = new StringBuilder();
//                    sql.Append("IF EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'" + tableName + "'))\n");
//                    sql.Append("DROP TABLE " + tableName);
//                    command.CommandText = sql.ToString();
//                    command.ExecuteNonQuery();
//                }
//            }
//        }

//        public bool ExistIndex(string table, string column)
//        {
//            using (var connection = ((Adapters.Database.SqlClient.Database)this.CreateDatabase()).CreateDbConnection())
//            {
//                connection.Open();
//                using (var command = connection.CreateCommand())
//                {
//                    var sql = new StringBuilder();
//                    sql.Append("SELECT COUNT(*)\n");
//                    sql.Append("FROM sys.indexes AS idx\n");
//                    sql.Append("JOIN sys.index_columns idxcol\n");
//                    sql.Append("ON idx.object_id = idxcol.object_id AND idx.index_id=idxcol.index_id\n");
//                    sql.Append("WHERE idx.type = 2 -- Non Clusterd\n");
//                    sql.Append("and key_ordinal = 1 -- 1 based\n");

//                    sql.Append("and object_name(idx.object_id) = '" + table + "'\n");
//                    sql.Append("and col_name(idx.object_id,idxcol.column_id) = '" + column + "'\n");

//                    command.CommandText = sql.ToString();
//                    var count = (int)command.ExecuteScalar();

//                    return count != 0;
//                }
//            }
//        }

//        public bool ExistProcedure(string procedure)
//        {
//            using (var connection = ((Adapters.Database.SqlClient.Database)this.CreateDatabase()).CreateDbConnection())
//            {
//                connection.Open();
//                using (var command = connection.CreateCommand())
//                {
//                    var sql = new StringBuilder();
//                    sql.Append(
//@"SELECT count(name)
//FROM sys.procedures
//WHERE name='" + procedure + "'");

//                    command.CommandText = sql.ToString();
//                    var count = (int)command.ExecuteScalar();

//                    return count != 0;
//                }
//            }
//        }

//        public bool ExistPrimaryKey(string table, string column)
//        {
//            using (var connection = ((Adapters.Database.SqlClient.Database)this.CreateDatabase()).CreateDbConnection())
//            {
//                connection.Open();
//                using (var command = connection.CreateCommand())
//                {
//                    var sql = new StringBuilder();
//                    sql.Append(
//@"SELECT count(*)
//FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
//WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1
//AND table_name = '" + table + "'");

//                    command.CommandText = sql.ToString();
//                    var count = (int)command.ExecuteScalar();

//                    return count != 0;
//                }
//            }
//        }

//        public bool IsInteger(string table, string column)
//        {
//            return this.GetDataType(table, column).Equals("int");
//        }

//        public bool IsLong(string table, string column)
//        {
//            return this.GetDataType(table, column).Equals("bigint");
//        }

//        public bool IsUnique(string table, string column)
//        {
//            return this.GetDataType(table, column).Equals("uniqueidentifier");
//        }

//        private string GetDataType(string table, string column)
//        {
//            using (var connection = ((Adapters.Database.SqlClient.Database)this.CreateDatabase()).CreateDbConnection())
//            {
//                connection.Open();
//                using (var command = connection.CreateCommand())
//                {
//                    var sql = new StringBuilder();
//                    sql.Append("SELECT DATA_TYPE\n");
//                    sql.Append("FROM INFORMATION_SCHEMA.COLUMNS\n");
//                    sql.Append("WHERE COLUMN_NAME = '" + column + "'\n");
//                    sql.Append("AND TABLE_NAME = '" + table + "'\n");

//                    command.CommandText = sql.ToString();
//                    var dataType = (string)command.ExecuteScalar();

//                    return dataType;
//                }
//            }
//        }
    }
}