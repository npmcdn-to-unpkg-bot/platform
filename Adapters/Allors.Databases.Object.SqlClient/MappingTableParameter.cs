// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MappingTableParameter.cs" company="Allors bvba">
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
    internal class MappingTableParameter
    {
        internal readonly string TypeName;
        internal readonly string Name;
        internal readonly string InvocationName;

        internal MappingTableParameter(Mapping mapping, string name, string typeName)
        {
            this.Name = string.Format(mapping.ParamFormat, name);
            this.InvocationName = string.Format(mapping.ParamInvocationFormat, name);
            this.TypeName = typeName;
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