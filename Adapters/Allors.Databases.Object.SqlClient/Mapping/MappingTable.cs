// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MappingTable.cs" company="Allors bvba">
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

    public sealed class MappingTable : IEnumerable<MappingColumn>
    {
        internal readonly MapingTableKind Kind;
        internal readonly string Name;
        internal readonly IRelationType RelationType;
        internal readonly Mapping Mapping;
        internal readonly IObjectType ObjectType;
        internal readonly string StatementName;

        private readonly Dictionary<string, MappingColumn> columnsByName;

        internal MappingTable(Mapping mapping, string name, MapingTableKind kind, IObjectType objectType) 
            : this(mapping, name, kind)
        {
            this.ObjectType = objectType;
        }

        internal MappingTable(Mapping mapping, string name, MapingTableKind kind, IRelationType relationType)
            : this(mapping, name, kind)
        {
            this.RelationType = relationType;
        }

        internal MappingTable(Mapping mapping, string name, MapingTableKind kind)
        {
            this.Mapping = mapping;
            this.Name = name.ToLowerInvariant();
            this.StatementName = mapping.EscapeIfReserved(this.Name);
            this.Kind = kind;

            this.columnsByName = new Dictionary<string, MappingColumn>();
        }

        internal MappingColumn FirstKeyColumn
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

        internal MappingColumn this[string columnName]
        {
            get { return this.columnsByName[columnName.ToLowerInvariant()]; }
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable<MappingColumn>)this).GetEnumerator();
        }

        IEnumerator<MappingColumn> IEnumerable<MappingColumn>.GetEnumerator()
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

        internal void AddColumn(MappingColumn column)
        {
            this.columnsByName[column.Name] = column;
        }

        internal bool Contains(string columName)
        {
            return this.columnsByName.ContainsKey(columName.ToLowerInvariant());
        }
    }
}