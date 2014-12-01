//------------------------------------------------------------------------------------------------- 
// <copyright file="Domain.cs" company="Allors bvba">
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
    using Allors.Meta.Meta.Xml;

    public sealed partial class Domain
    {
        public void SyncToMeta(DomainXml domainXml)
        {
        }

        public void SyncFromMeta(DomainXml domainXml)
        {
        }

        public void SendChangedEvent()
        {
        }

        public Domain Inherit(Domain source)
        {
            var domain = (Domain)this.MetaPopulation.Find(source.Id);
            if (domain == null)
            {
                domain = new Domain(this.MetaPopulation, source.Id) { Name = source.Name };
                this.AddDirectSuperdomain(domain);

                foreach (var sourceSuperDomain in source.DirectSuperdomains)
                {
                    domain.Inherit(sourceSuperDomain);
                }

                foreach (var sourceInterface in source.DefinedInterfaces)
                {
                    var @interface = (Interface)this.MetaPopulation.Find(sourceInterface.Id) ?? new InterfaceBuilder(domain, sourceInterface.Id).Build();
                    @interface.Copy(sourceInterface);
                }

                foreach (var sourceClass in source.DefinedClasses)
                {
                    var @class = (Class)this.MetaPopulation.Find(sourceClass.Id) ?? new ClassBuilder(domain, sourceClass.Id).Build();
                    @class.Copy(sourceClass);
                }

                foreach (var sourceInherintance in source.DefinedInheritances)
                {
                    var inheritance = (Inheritance)this.MetaPopulation.Find(sourceInherintance.Id) ?? new InheritanceBuilder(domain, sourceInherintance.Id).Build();
                    inheritance.Copy(sourceInherintance);
                }

                foreach (var sourceRelationType in source.DefinedRelationTypes)
                {
                    var relationType = (RelationType)this.MetaPopulation.Find(sourceRelationType.Id) ?? new RelationTypeBuilder(domain, sourceRelationType.Id, sourceRelationType.AssociationType.Id, sourceRelationType.RoleType.Id).Build();
                    relationType.Copy(sourceRelationType);
                }
            }
            else
            {
                this.AddDirectSuperdomain(domain);
            }

            return domain;
        }
    }
}