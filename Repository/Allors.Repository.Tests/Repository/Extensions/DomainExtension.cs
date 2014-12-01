// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DomainExtension.cs" company="Allors bvba">
//   Copyright 2002-2009 Allors bvba.
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
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Meta
{
    using System;

    public static class DomainExtension
    {
        public static Interface BuildInterface(this Domain domain, Guid id, string singularName, string pluralName)
        {
            return new InterfaceBuilder(domain, id).WithSingularName(singularName).WithPluralName(pluralName).Build();
        }

        public static Class BuildClass(this Domain domain, Guid id, string singularName, string pluralName)
        {
            return new ClassBuilder(domain, id).WithSingularName(singularName).WithPluralName(pluralName).Build();
        }

        public static Inheritance BuildInheritance(this Domain domain, Guid id, Composite subType, Interface superType)
        {
            return new InheritanceBuilder(domain, id).WithSubtype(subType).WithSupertype(superType).Build();
        }

        public static RelationType BuildRelationType(this Domain domain, Guid id, Guid associationTypeId, Composite associationObjectType, Guid roleTypeId, ObjectType roleObjectType)
        {
            return new RelationTypeBuilder(domain, id, associationTypeId, roleTypeId).WithObjectTypes(associationObjectType, roleObjectType).Build();
        }

        public static RelationType BuildRelationType(this Domain domain, Guid id, Guid associationTypeId, Composite associationObjectType, Guid roleTypeId, ObjectType roleObjectType, string singularName, string pluralName)
        {
            return new RelationTypeBuilder(domain, id, associationTypeId, roleTypeId).WithObjectTypes(associationObjectType, roleObjectType).Build();
        }
    }
}