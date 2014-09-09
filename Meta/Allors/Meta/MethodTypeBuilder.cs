//------------------------------------------------------------------------------------------------- 
// <copyright file="MethodTypeBuilder.cs" company="Allors bvba">
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

    internal partial class MethodTypeBuilder : Builder<MethodType>
    {
        private Composite objectType;
        private string name;

        internal MethodTypeBuilder(Domain domain, Guid id)
            : base(domain, id)
        {
        }

        public MethodTypeBuilder WithObjectType(Composite value)
        {
            this.objectType = value;
            return this;
        }

        internal MethodTypeBuilder WithName(string value)
        {
            this.name = value;
            return this;
        }

        internal void AllorsBuild(MethodType instance)
        {
            instance.ObjectType = this.objectType;
            instance.Name = this.name;
        }
    }
}