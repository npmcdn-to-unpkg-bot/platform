// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrintQueues.cs" company="Allors bvba">
//   Copyright 2002-2016 Allors bvba.
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

namespace Allors.Domain
{
    using System;

    using Allors.Meta;

    public partial class PrintQueues
    {
        public static readonly Guid DefaultPrintQueueId = new Guid("0B1920EA-DE2E-4962-A7C5-996306390ED7");

        public PrintQueue DefaultPrintQueue => this.FindBy(M.UniquelyIdentifiable.UniqueId, DefaultPrintQueueId);

        protected override void BaseSetup(Setup setup)
        {
            base.BaseSetup(setup);

            var defaultPrintQueue = new PrintQueueBuilder(this.Session).WithName("Default Print Queue").WithUniqueId(DefaultPrintQueueId).Build();

            var singleton = Singleton.Instance(this.Session);
            singleton.DefaultPrintQueue = defaultPrintQueue;
        }

        protected override void BaseSecure(Security config)
        {
            base.BaseSecure(config);

            var full = new[] { Operations.Read, Operations.Write, Operations.Execute };

            config.GrantAdministrator(this.ObjectType, full);
        }

        public PrintQueue Locate(Printable printable)
        {
            return this.DefaultPrintQueue;
        }
    }
}