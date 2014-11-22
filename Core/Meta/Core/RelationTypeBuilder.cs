//------------------------------------------------------------------------------------------------- 
// <copyright file="RelationTypeBuilder.cs" company="Allors bvba">
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

    public partial class RelationTypeBuilder : Builder<RelationType>
    {
        private Guid associationTypeId;
        private Guid roleTypeId;

        private Composite associationObjectType;
        private ObjectType roleObjectType;
        private Multiplicity multiplicity;
        private bool isDerived;
        private bool? isIndexed;

        private string singularName;
        private string pluralName;
        private int? precision;
        private int? scale;
        private int? size;

        private bool isRequired;

        public RelationTypeBuilder(Domain domain, Guid id, Guid associationTypeId, Guid roleTypeId)
            : base(domain, id)
        {
            this.associationTypeId = associationTypeId;
            this.roleTypeId = roleTypeId;
        }

        public RelationTypeBuilder WithObjectTypes(Composite associationObjectType, ObjectType roleObjectType)
        {
            this.associationObjectType = associationObjectType;
            this.roleObjectType = roleObjectType;
            return this;
        }

        public RelationTypeBuilder WithSingularName(string value)
        {
            this.singularName = value;
            return this;
        }

        public RelationTypeBuilder WithPluralName(string value)
        {
            this.pluralName = value;
            return this;
        }

        public RelationTypeBuilder WithMultiplicity(Multiplicity value)
        {
            this.multiplicity = value;
            return this;
        }

        public RelationTypeBuilder WithIsDerived(bool value)
        {
            this.isDerived = value;
            return this;
        }

        public RelationTypeBuilder WithIsIndexed(bool value)
        {
            this.isIndexed = value;
            return this;
        }

        public RelationTypeBuilder WithPrecision(int value)
        {
            this.precision = value;
            return this;
        }

        public RelationTypeBuilder WithScale(int value)
        {
            this.scale = value;
            return this;
        }

        public RelationTypeBuilder WithSize(int value)
        {
            this.size = value;
            return this;
        }

        public RelationTypeBuilder WithIsRequired(bool value)
        {
            this.isRequired = value;
            return this;
        }

        private void AllorsBuild(RelationType instance)
        {
            instance.AssociationType.ObjectType = this.associationObjectType;
            instance.RoleType.ObjectType = this.roleObjectType;
            instance.IsDerived = this.isDerived;
            if (this.isIndexed.HasValue)
            {
                instance.IsIndexed = this.isIndexed.Value;
            }
            else
            {
                if (this.roleObjectType != null && this.roleObjectType.IsComposite)
                {
                    instance.IsIndexed = true;
                }
            }

            instance.AssignedMultiplicity = this.multiplicity;

            instance.RoleType.AssignedSingularName = this.singularName;
            instance.RoleType.AssignedPluralName = this.pluralName;
            instance.RoleType.Precision = this.precision;
            instance.RoleType.Scale = this.scale;
            instance.RoleType.Size = this.size;

            instance.RoleType.IsRequired = this.isRequired;
        }
    }
}