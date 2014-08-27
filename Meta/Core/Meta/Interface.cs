//------------------------------------------------------------------------------------------------- 
// <copyright file="Interface.cs" company="Allors bvba">
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

namespace Allors.Meta
{
    using System;

    /// <summary>
    /// An <see cref="ObjectType"/> defines the state and behavior for
    /// a set of <see cref="IObject"/>s.
    /// </summary>
    public partial class Interface : CompositeType
    {

        public Class derivedExclusiveRootClass;

        public override Class DerivedExclusiveRootClass
        {
            get
            {
                return this.derivedExclusiveRootClass;
            }
        }

        public Interface(Domain domain, Guid objectTypeId)
            : base(domain, objectTypeId)
        {
            this.Domain.OnInterfaceCreated(this);
        }

        /// <summary>
        /// Derive exclusive concrete leaf classes.
        /// </summary>
        /// <param name="concreteLeafClasses">The concrete leaf classes.</param>
        internal void DeriveExclusiveRootClass()
        {
            this.derivedExclusiveRootClass = null;
            if (this.DerivedRootClasses.Count == 1)
            {
                this.derivedExclusiveRootClass = this.DerivedRootClasses[0];
            }
        }
    }
}