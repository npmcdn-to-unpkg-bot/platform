//------------------------------------------------------------------------------------------------- 
// <copyright file="Method.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
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
// <summary>Defines the IObjectType type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Tools.Repository
{
    using System;
    using System.Collections.Generic;

    public class Method
    {
        public string Name { get; }

        public Method DefiningMethod { get; internal set; }
        
        public Type DefiningType { get; internal set; }

        public Dictionary<string, Attribute> AttributeByName { get; }

        public Method(Type definingType, string name)
        {
            this.AttributeByName = new Dictionary<string, Attribute>();

            this.DefiningType = definingType;
            this.Name = name;
        }
    }
}