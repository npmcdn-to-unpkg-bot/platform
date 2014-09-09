//------------------------------------------------------------------------------------------------- 
// <copyright file="UnitBuilder.cs" company="Allors bvba">
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

namespace Allors.Meta.Builders
{
    using System;

    public partial class UnitBuilder : Builder<Unit>
    {
        private string singularName;
        private string pluralName;
        private UnitTags unitTag;

        public UnitBuilder(Domain domain, Guid id)
            : base(domain, id)
        {
        }

        public UnitBuilder WithSingularName(string value)
        {
            this.singularName = value;
            return this;
        }

        public UnitBuilder WithPluralName(string value)
        {
            this.pluralName = value;
            return this;
        }

        public UnitBuilder WithUnitTag(UnitTags value)
        {
            this.unitTag = value;
            return this;
        }

        private void AllorsBuild(Unit instance)
        {
            instance.SingularName = this.singularName;
            instance.PluralName = this.pluralName;
            instance.UnitTag = this.unitTag;
        }
    }
}