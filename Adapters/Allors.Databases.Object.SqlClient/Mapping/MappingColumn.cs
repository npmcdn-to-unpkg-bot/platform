// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MappingColumn.cs" company="Allors bvba">
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
    using System.Data;

    using Allors.Meta;

    public class MappingColumn
    {
        internal readonly string Name;
        internal readonly DbType DbType;

        internal readonly bool IsIdentity;
        internal readonly bool IsKey;
        internal readonly MappingIndexType IndexType;

        internal readonly MappingParameter Param;
        internal readonly string StatementName;

        internal readonly int? Precision;
        internal readonly int? Scale;
        internal readonly int? Size;

        internal readonly IRelationType RelationType;

        internal MappingColumn(Mapping mapping, string name, DbType dbType, bool isIdentity, bool isKey, MappingIndexType indexType)
            : this(mapping, name, dbType, isIdentity, isKey, indexType, null, null, null, null)
        {
        }

        internal MappingColumn(Mapping mapping, string name, DbType dbType, bool isIdentity, bool isKey, MappingIndexType indexType, IRelationType relationType)
            : this(mapping, name, dbType, isIdentity, isKey, indexType, relationType, null, null, null)
        {
        }

        internal MappingColumn(Mapping mapping, string name, DbType dbType, bool isIdentity, bool isKey, MappingIndexType indexType, IRelationType relationType, int? size, int? precision, int? scale)
        {
            this.Name = name.ToLowerInvariant();
            this.StatementName = mapping.EscapeIfReserved(this.Name);
            this.Param = mapping.CreateParameter(name, dbType);

            this.IsKey = isKey;
            this.IsIdentity = isIdentity;
            this.IndexType = indexType;

            this.Size = size;
            this.Precision = precision;
            this.Scale = scale;

            this.DbType = dbType;

            this.RelationType = relationType;
        }

        /// <summary>
        /// The string value.
        /// </summary>
        /// <returns>The string value</returns>
        public override string ToString()
        {
            return this.StatementName;
        }
    }
}