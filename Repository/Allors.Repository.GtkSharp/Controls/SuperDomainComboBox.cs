//------------------------------------------------------------------------------------------------- 
// <copyright file="SuperDomainComboBox.cs" company="Allors bvba">
// Copyright 2002-2009 Allors bvba.
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

namespace Allors.Meta.GtkSharp
{
    using System.Collections.Generic;

    using Allors.Meta;

    [System.ComponentModel.ToolboxItem(true)]
    public class SuperDomainComboBox : MetaComboBox<Domain>
    {
        public SuperDomainComboBox(XmlRepository repository, Domain subDomain)
            : base(repository)
        {
            this.BeginUpdate();
            try
            {
                var superDomains = new HashSet<Domain>(subDomain.SuperDomains);
                
                foreach (var domain in this.Repository.Domain.Domains)
                {
                    if (!domain.IsAllorsUnitDomain && superDomains.Contains(domain))
                    {
                        this.ListStore.AppendValues(domain.AllorsObjectId, domain.Name);
                    }
                }
            }
            finally
            {
                this.EndUpdate();
            }
        }
    }
}