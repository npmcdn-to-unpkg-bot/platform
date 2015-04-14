// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SchemaTable.cs" company="Allors bvba">
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

namespace Allors.Databases.Object.SqlClient
{
    using System.Collections;
    using System.Collections.Generic;

    using Allors.Meta;

    internal sealed class SchemaTable : IEnumerable<SchemaColumn>
    {
        internal readonly SchemaTableKind Kind;
        internal readonly string Name;
        internal readonly IRelationType RelationType;
        internal readonly Schema Schema;
        internal readonly IObjectType ObjectType;
        internal readonly string StatementName;

        private readonly Dictionary<string, SchemaColumn> columnsByName;

        internal SchemaTable(Schema schema, string name, SchemaTableKind kind, IObjectType objectType) 
            : this(schema, name, kind)
        {
            this.ObjectType = objectType;
        }

        internal SchemaTable(Schema schema, string name, SchemaTableKind kind, IRelationType relationType)
            : this(schema, name, kind)
        {
            this.RelationType = relationType;
        }

        internal SchemaTable(Schema schema, string name, SchemaTableKind kind)
        {
            this.Schema = schema;
            this.Name = name.ToLowerInvariant();
            this.StatementName = schema.EscapeIfReserved(this.Name);
            this.Kind = kind;

            this.columnsByName = new Dictionary<string, SchemaColumn>();
        }

        internal SchemaColumn FirstKeyColumn
        {
            get
            {
                foreach (var dictionaryEntry in this.columnsByName)
                {
                    var column = dictionaryEntry.Value;
                    if (column.IsKey)
                    {
                        return column;
                    }
                }

                return null;
            }
        }

        internal SchemaColumn this[string columnName]
        {
            get { return this.columnsByName[columnName.ToLowerInvariant()]; }
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable<SchemaColumn>)this).GetEnumerator();
        }

        IEnumerator<SchemaColumn> IEnumerable<SchemaColumn>.GetEnumerator()
        {
            return this.columnsByName.Values.GetEnumerator();
        }

        /// <summary>
        /// The string value.
        /// </summary>
        /// <returns>The string value</returns>
        public override string ToString()
        {
            return this.StatementName;
        }

        internal void AddColumn(SchemaColumn column)
        {
            this.columnsByName[column.Name] = column;
        }

        internal bool Contains(string columName)
        {
            return this.columnsByName.ContainsKey(columName.ToLowerInvariant());
        }
    }
}