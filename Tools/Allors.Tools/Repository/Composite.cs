//------------------------------------------------------------------------------------------------- 
// <copyright file="Class.cs" company="Allors bvba">
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
    using System.Linq;

    public abstract class Composite : Type
    {
        protected Composite(string name)
            : base(name)
        {
            this.AttributeByName = new Dictionary<string, Attribute>();
            this.ImplementedInterfaces = new List<Interface>();
            this.PropertyByName = new Dictionary<string, Property>();
            this.DefinedReversePropertyByName = new Dictionary<string, Property>();
            this.ImplementedReversePropertyByName = new Dictionary<string, Property>();
            this.MethodByName = new Dictionary<string, Method>();
        }

        public abstract IEnumerable<Interface> Interfaces { get; }

        public override string Id => ((IdAttribute)this.AttributeByName["Id"]).Value;

        public Dictionary<string, Attribute> AttributeByName { get; }

        public IList<Interface> ImplementedInterfaces { get; }

        public Dictionary<string, Property> PropertyByName { get; }

        public Dictionary<string, Property> DefinedReversePropertyByName { get; }

        public Dictionary<string, Property> ImplementedReversePropertyByName { get; }

        public Dictionary<string, Method> MethodByName { get; }

        public IEnumerable<Property> Properties => this.PropertyByName.Values;

        public IEnumerable<Property> DefinedProperties => this.PropertyByName.Values.Where(v => v.DefiningProperty == null);

        public IEnumerable<Property> ImplementedProperties => this.PropertyByName.Values.Where(v => v.DefiningProperty != null);

        public IEnumerable<Property> DefinedReverseProperties => this.DefinedReversePropertyByName.Values;

        public IEnumerable<Property> ImplementedReverseProperties => this.ImplementedReversePropertyByName.Values;

        public IEnumerable<Method> Methods => this.MethodByName.Values;

        public IEnumerable<Method> DefinedMethods => this.MethodByName.Values.Where(v => v.DefiningMethod == null);

        public IEnumerable<Method> ImplementedMethods => this.MethodByName.Values.Where(v => v.DefiningMethod != null);
    }
}