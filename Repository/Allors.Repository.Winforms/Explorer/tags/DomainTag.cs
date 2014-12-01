// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NamespaceTag.cs" company="Allors bvba">
// Copyright 2002-2012 Allors bvba.
// 
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
// 
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Applications is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Allors.Meta.WinForms
{
    using Allors.Meta;

    public sealed class DomainTag : Tag
    {
        public DomainTag(XmlRepository repository, Domain domain)
        {
            this.Repository = repository;
            this.Domain = domain;
        }

        public XmlRepository Repository { get; private set; }

        public Domain Domain { get; private set; }

        public override bool Equals(object obj)
        {
            var that = obj as DomainTag;
            return that != null && this.Repository.Equals(that.Repository) && this.Domain.Id.Equals(that.Domain.Id);
        }

        public override int GetHashCode()
        {
            return this.Repository.GetHashCode() ^ this.Domain.Id.GetHashCode();
        }
    }
}