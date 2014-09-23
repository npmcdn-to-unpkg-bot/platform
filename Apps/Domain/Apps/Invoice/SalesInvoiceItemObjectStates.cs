// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SalesInvoiceItemObjectStates.cs" company="Allors bvba">
//   Copyright 2002-2012 Allors bvba.
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

    using Allors.Domain;

    public partial class SalesInvoiceItemObjectStates
    {
        public static readonly Guid SentId = new Guid("9DC3A779-8734-4c19-BFF5-0DE9F29A584B");
        public static readonly Guid PaidId = new Guid("875AD2E4-BF44-46f4-9CD6-5F5C5BD43ADC");
        public static readonly Guid PartiallyPaidId = new Guid("2C6A00F7-466A-4689-A7E0-2D5660804B15");
        public static readonly Guid ReadyForPostingId = new Guid("9585A2C8-5B4D-4063-A8E7-E1310DFE439D");
        public static readonly Guid WrittenOffId = new Guid("F4408FD5-CCA3-44ea-BC00-4FFECC5D1EB9");
        public static readonly Guid CancelledId = new Guid("D521BBFA-1E18-453c-862F-28EBC0DA10C1");

        private UniquelyIdentifiableCache<SalesInvoiceItemObjectState> stateCache;

        public SalesInvoiceItemObjectState PartiallyPaid
        {
            get { return this.StateCache.Get(PartiallyPaidId); }
        }

        public SalesInvoiceItemObjectState Sent
        {
            get { return this.StateCache.Get(SentId); }
        }

        public SalesInvoiceItemObjectState Paid
        {
            get { return this.StateCache.Get(PaidId); }
        }

        public SalesInvoiceItemObjectState ReadyForPosting
        {
            get { return this.StateCache.Get(ReadyForPostingId); }
        }

        public SalesInvoiceItemObjectState WrittenOff
        {
            get { return this.StateCache.Get(WrittenOffId); }
        }

        public SalesInvoiceItemObjectState Cancelled
        {
            get { return this.StateCache.Get(CancelledId); }
        }

        private UniquelyIdentifiableCache<SalesInvoiceItemObjectState> StateCache
        {
            get
            {
                return this.stateCache ?? (this.stateCache = new UniquelyIdentifiableCache<SalesInvoiceItemObjectState>(this.Session));
            }
        }

        protected override void AppsSetup(Setup setup)
        {
            base.AppsSetup(setup);

            var englishLocale = new Locales(Session).EnglishGreatBritain;
            var dutchLocale = new Locales(Session).DutchNetherlands;

            new SalesInvoiceItemObjectStateBuilder(Session)
                .WithUniqueId(PartiallyPaidId)
                .WithName("Partially Paid")
                .WithLocalisedName(new LocalisedTextBuilder(Session).WithText("Partially Paid").WithLocale(englishLocale).Build())
                .WithLocalisedName(new LocalisedTextBuilder(Session).WithText("Gedeeltelijk betaald").WithLocale(dutchLocale).Build())
                .Build();

            new SalesInvoiceItemObjectStateBuilder(Session)
                .WithUniqueId(SentId)
                .WithName("Sent")
                .WithLocalisedName(new LocalisedTextBuilder(Session).WithText("Sent").WithLocale(englishLocale).Build())
                .WithLocalisedName(new LocalisedTextBuilder(Session).WithText("Verzonden").WithLocale(dutchLocale).Build())
                .Build();

            new SalesInvoiceItemObjectStateBuilder(Session)
                .WithUniqueId(PaidId)
                .WithName("Paid")
                .WithLocalisedName(new LocalisedTextBuilder(Session).WithText("Paid").WithLocale(englishLocale).Build())
                .WithLocalisedName(new LocalisedTextBuilder(Session).WithText("Betaald").WithLocale(dutchLocale).Build())
                .Build();

            new SalesInvoiceItemObjectStateBuilder(Session)
                .WithUniqueId(ReadyForPostingId)
                .WithName("Ready For Posting")
                .WithLocalisedName(new LocalisedTextBuilder(Session).WithText("Ready For Posting").WithLocale(englishLocale).Build())
                .WithLocalisedName(new LocalisedTextBuilder(Session).WithText("Gereed voor verzending").WithLocale(dutchLocale).Build())
                .Build();

            new SalesInvoiceItemObjectStateBuilder(Session)
                .WithUniqueId(WrittenOffId)
                .WithName("Written Off")
                .WithLocalisedName(new LocalisedTextBuilder(Session).WithText("Written Off").WithLocale(englishLocale).Build())
                .WithLocalisedName(new LocalisedTextBuilder(Session).WithText("Afgeschreven").WithLocale(dutchLocale).Build())
                .Build();

            new SalesInvoiceItemObjectStateBuilder(Session)
                .WithUniqueId(CancelledId)
                .WithName("Cancelled")
                .WithLocalisedName(new LocalisedTextBuilder(Session).WithText("Cancelled").WithLocale(englishLocale).Build())
                .WithLocalisedName(new LocalisedTextBuilder(Session).WithText("Geannuleerd").WithLocale(dutchLocale).Build())
                .Build();
        }

        protected override void AppsSecure(Security config)
        {
            base.AppsSecure(config);
            
            var full = new[] { Operation.Read, Operation.Write, Operation.Execute };

            config.GrantAdministrator(this.ObjectType, full);
        }
    }
}
