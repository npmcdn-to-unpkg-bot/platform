// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MappingParameter.cs" company="Allors bvba">
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
    using System;
    using System.Data;

    internal class MappingParameter
    {
        internal readonly DbType DbType;
        internal readonly string Name;

        internal MappingParameter(Mapping mapping, string name, DbType type)
        {
            this.Name = string.Format(Mapping.ParamFormat, name);
            this.DbType = type;
        }

        internal SqlDbType SqlDbType
        {
            get
            {
                switch (this.DbType)
                {
                    case DbType.String:
                        return SqlDbType.NVarChar;
                    case DbType.Int32:
                        return SqlDbType.Int;
                    case DbType.Decimal:
                        return SqlDbType.Decimal;
                    case DbType.Double:
                        return SqlDbType.Float;
                    case DbType.Boolean:
                        return SqlDbType.Bit;
                    case DbType.DateTime2:
                        return SqlDbType.DateTime2;
                    case DbType.Guid:
                        return SqlDbType.UniqueIdentifier;
                    case DbType.Binary:
                        return SqlDbType.VarBinary;
                    default:
                        throw new Exception("!UNKNOWN VALUE TYPE!");
                }
            }
        }

        /// <summary>
        /// Returns a String which represents the object instance.
        /// </summary>
        /// <returns>
        /// The string which represents the object instance.
        /// </returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}