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

    public sealed class MappingTable : IEnumerable<MappingColumn>
    {
        internal readonly string Name;
        internal readonly string StatementName;
        
        private readonly Dictionary<string, MappingColumn> columnsByName;

        internal MappingTable(Mapping mapping, string name)
        {
            this.Name = name.ToLowerInvariant();
            this.StatementName = mapping.EscapeIfReserved(this.Name);

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
    }
}