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

namespace Allors.Meta
{
    using System;
    using System.Collections.Generic;

    public sealed partial class Class : Composite, IClass
    {
        private readonly Class[] leafClasses;

        internal Class(Domain domain, Guid id)
            : base(domain, id)
        {
            this.leafClasses = new[] { this };
            domain.OnClassCreated(this);
        }

        public override IEnumerable<Class> LeafClasses
        {
            get
            {
                return this.leafClasses;
            }
        }

        public override bool ExistLeafClasses
        {
            get
            {
                return true;
            }
        }

        public override Class ExclusiveLeafClass
        {
            get
            {
                return this;
            }
        }

        public override bool ExistLeafClass(IClass objectType)
        {
            return this.Equals(objectType);
        }
    }
}