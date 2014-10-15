//------------------------------------------------------------------------------------------------- 
// <copyright file="IMetaPopulation.cs" company="Allors bvba">
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
// <summary>Defines the Domain type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Meta
{
    using System;
    using System.Collections.Generic;

    public interface IMetaPopulation
    {
        void AssertUnlocked();

        void Stale();

        IEnumerable<IComposite> Composites { get; }

        IEnumerable<IClass> Classes { get; }

        IEnumerable<IInheritance> Inheritances { get; }

        IEnumerable<IMethodType> MethodTypes { get; }

        IEnumerable<IRelationType> RelationTypes { get; }

        bool IsValid { get; }

        void Derive();

        void Lock();

        ValidationLog Validate();

        void OnUnitCreated(IUnit unit);

        void OnInterfaceCreated(IInterface @interface);

        void OnClassCreated(IClass @class);

        void OnInheritanceCreated(IInheritance inheritance);

        void OnRelationTypeCreated(IRelationType relationType);

        void OnAssociationTypeCreated(IAssociationType associationType);

        void OnRoleTypeCreated(IRoleType roleType);

        void OnMethodTypeCreated(IMethodType methodType);

        void OnDomainCreated(IDomain domain);

        IMetaObject Find(Guid metaObjectId);
    }
}