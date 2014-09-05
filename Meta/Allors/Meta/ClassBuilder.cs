//------------------------------------------------------------------------------------------------- 
// <copyright file="ClassBuilder.cs" company="Allors bvba">
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

    internal partial class ClassBuilder : Builder<Class>
    {
        private string singularName;
        private string pluralName;

        internal ClassBuilder(Domain domain, Guid id)
            : base(domain, id)
        {
        }

        internal ClassBuilder WithSingularName(string value)
        {
            this.singularName = value;
            return this;
        }

        internal ClassBuilder WithPluralName(string value)
        {
            this.pluralName = value;
            return this;
        }

        internal void AllorsBuild(Class instance)
        {
            instance.SingularName = this.singularName;
            instance.PluralName = this.pluralName;
        }
    }
}