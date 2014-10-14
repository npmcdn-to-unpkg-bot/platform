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
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta
{
    using System;

    public partial class ClassBuilder : Builder<Class>
    {
        private string singularName;
        private string pluralName;

        public ClassBuilder(IDomain domain, Guid id)
            : base(domain, id)
        {
        }

        public ClassBuilder WithSingularName(string value)
        {
            this.singularName = value;
            return this;
        }

        public ClassBuilder WithPluralName(string value)
        {
            this.pluralName = value;
            return this;
        }

        private void AllorsBuild(Class instance)
        {
            instance.SingularName = this.singularName;
            instance.PluralName = this.pluralName;
        }
    }
}