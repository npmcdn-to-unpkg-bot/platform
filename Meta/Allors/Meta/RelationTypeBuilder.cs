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
// <summary>Defines the ObjectType type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta.Builders
{
    using System;

    internal partial class RelationTypeBuilder : MetaObjectBuilder<Inheritance>
    {
        private Composite associationObjectType;
        private ObjectType roleObjectType;
        private Cardinalities cardinality;
        private bool isDerived;
        private bool isIndexed;

        private string singularName;
        private string pluralName;
        private int precision;
        private int scale;
        private int size;

        internal RelationTypeBuilder(Domain domain, Guid id)
            : base(domain, id)
        {
        }

        public RelationTypeBuilder WithObjectTypes(Composite associationObjectType, ObjectType roleObjectType)
        {
            this.roleObjectType = roleObjectType;
            this.associationObjectType = associationObjectType;
            return this;
        }

        internal RelationTypeBuilder WithSingularName(string value)
        {
            this.singularName = value;
            return this;
        }

        internal RelationTypeBuilder WithPluralName(string value)
        {
            this.pluralName = value;
            return this;
        }

        internal RelationTypeBuilder WithCardinality(Cardinalities value)
        {
            this.cardinality = value;
            return this;
        }

        internal RelationTypeBuilder WithIsDerived(bool value)
        {
            this.isDerived = value;
            return this;
        }

        internal RelationTypeBuilder WithIsIndexed(bool value)
        {
            this.isIndexed = value;
            return this;
        }

        internal RelationTypeBuilder WithPrecision(int value)
        {
            this.precision = value;
            return this;
        }

        internal RelationTypeBuilder WithScale(int value)
        {
            this.scale = value;
            return this;
        }

        internal RelationTypeBuilder WithSize(int value)
        {
            this.size = value;
            return this;
        }

        internal void AllorsBuild(RelationType instance)
        {
            instance.AssociationType.ObjectType = this.associationObjectType;
            instance.RoleType.ObjectType = this.roleObjectType;
            instance.IsDerived = this.isDerived;
            instance.IsIndexed = this.isIndexed;

            switch (cardinality)
            {
                case Cardinalities.OneToOne:
                    instance.AssociationType.IsOne = true;
                    instance.RoleType.IsOne = true;
                    break;

                case Cardinalities.OneToMany:
                    instance.AssociationType.IsOne = true;
                    instance.RoleType.IsMany = true;
                    break;

                case Cardinalities.ManyToOne:
                    instance.AssociationType.IsMany = true;
                    instance.RoleType.IsOne = true;
                    break;

                case Cardinalities.ManyToMany:
                    instance.AssociationType.IsMany = true;
                    instance.RoleType.IsMany = true;
                    break;
            }


            instance.RoleType.AssignedSingularName = this.singularName;
            instance.RoleType.AssignedPluralName = this.pluralName;
            instance.RoleType.Precision = this.precision;
            instance.RoleType.Scale = this.scale;
            instance.RoleType.Size = this.size;
        }
    }
}