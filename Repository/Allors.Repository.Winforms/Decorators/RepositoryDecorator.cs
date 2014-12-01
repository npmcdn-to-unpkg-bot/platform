// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryDecorator.cs" company="Allors bvba">
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

namespace Allors.Meta.WinForms.Decorators
{
    using System;
    using System.ComponentModel;

    using Allors.Meta;

    [TypeConverter(typeof(PropertySorter))]
    public class RepositoryDecorator
    {
        private readonly XmlRepository repository;

        public RepositoryDecorator(XmlRepository repository)
        {
            this.repository = repository;
        }

        [Category("\u200BGeneral")]
        [PropertyOrder(1)]
        public Guid Id
        {
            get { return this.repository.Domain.Id; }
        }

        [Category("\u200BGeneral")]
        [PropertyOrder(2)]
        public string Name
        {
            get
            {
                return this.repository.Domain.Name;
            }

            set
            {
                this.repository.Domain.Name = value;
                this.SendChangedEvent();
            }
        }

        private void SendChangedEvent()
        {
            this.repository.Domain.SendChangedEvent();
        }
    }
}