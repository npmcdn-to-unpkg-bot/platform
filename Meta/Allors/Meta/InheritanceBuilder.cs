//------------------------------------------------------------------------------------------------- 
// <copyright file="InheritanceBuilder.cs" company="Allors bvba">
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
// <summary>Defines the ObjectType type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta.Builders
{
    using System;

    internal partial class InheritanceBuilder : MetaObjectBuilder<Inheritance>
    {
        private Composite subtype;
        private Interface supertype;

        internal InheritanceBuilder(Domain domain, Guid id)
            : base(domain, id)
        {
        }

        internal InheritanceBuilder WithSubtype(Composite value)
        {
            this.subtype = value;
            return this;
        }

        internal InheritanceBuilder WithSupertype(Interface value)
        {
            this.supertype = value;
            return this;
        }

        internal void AllorsBuild(Inheritance instance)
        {
            instance.Subtype = this.subtype;
            instance.Supertype = this.supertype;
        }
    }
}