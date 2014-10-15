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
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta
{
    using System;

    public partial class InheritanceBuilder : Builder<Inheritance>
    {
        private Composite subtype;
        private IInterface supertype;

        public InheritanceBuilder(IDomain domain, Guid id)
            : base(domain, id)
        {
        }

        public InheritanceBuilder WithSubtype(Composite value)
        {
            this.subtype = value;
            return this;
        }

        public InheritanceBuilder WithSupertype(IInterface value)
        {
            this.supertype = value;
            return this;
        }

        private void AllorsBuild(Inheritance instance)
        {
            instance.Subtype = this.subtype;
            instance.Supertype = this.supertype;
        }
    }
}