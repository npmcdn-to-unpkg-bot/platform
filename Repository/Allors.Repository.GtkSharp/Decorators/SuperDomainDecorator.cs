// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SuperDomainDecorator.cs" company="Allors bvba">
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

namespace Allors.Meta.GtkSharp.Decorators
{
    using System;
    using System.ComponentModel;

    public class SuperDomainDecorator
    {
        private readonly Domain domain;

        public SuperDomainDecorator(Domain domain)
        {
            this.domain = domain;
        }

        [Category("\u200BGeneral")]
        public Guid Id
        {
            get { return this.domain.Id; }
        }

        [Category("\u200BGeneral")]
        public string Name
        {
            get
            {
                return this.domain.Name;
            }
        }
    }
}