// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommunicationEventObjectStates.cs" company="Allors bvba">
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

    public partial class CommunicationEventObjectStates
    {
        public static readonly Guid OpenedId = new Guid("199131EB-18FD-4b8a-9FEC-23789C169FF5");
        public static readonly Guid ReadId = new Guid("55959953-A465-4a45-B2D0-27077B285C77");
        public static readonly Guid ClosedId = new Guid("35612611-62C5-4de5-B138-9C8D874D8916");
        public static readonly Guid CancelledId = new Guid("F236E865-E2CA-43d7-8F17-56C3DC54C191");
        public static readonly Guid InProgressId = new Guid("D1232CEB-1530-451e-BAED-DB1356BC1EB2");

        private UniquelyIdentifiableCache<CommunicationEventObjectState> cache;

        public CommunicationEventObjectState Opened
        {
            get { return this.Cache.Get(OpenedId); }
        }

        public CommunicationEventObjectState Read
        {
            get { return this.Cache.Get(ReadId); }
        }

        public CommunicationEventObjectState InProgress
        {
            get { return this.Cache.Get(InProgressId); }
        }

        public CommunicationEventObjectState Closed
        {
            get { return this.Cache.Get(ClosedId); }
        }

        public CommunicationEventObjectState Cancelled
        {
            get { return this.Cache.Get(CancelledId); }
        }

        private UniquelyIdentifiableCache<CommunicationEventObjectState> Cache
        {
            get
            {
                return this.cache ?? (this.cache = new UniquelyIdentifiableCache<CommunicationEventObjectState>(this.Session));
            }
        }

        protected override void AppsSetup(Setup setup)
        {
            base.AppsSetup(setup);

            var englishLocale = new Locales(this.Session).EnglishGreatBritain;
            var durchLocale = new Locales(this.Session).DutchNetherlands;

            new CommunicationEventObjectStateBuilder(this.Session)
                .WithName("Opened")
                .WithLocalisedName(new LocalisedTextBuilder(this.Session).WithText("Opened").WithLocale(englishLocale).Build())
                .WithLocalisedName(new LocalisedTextBuilder(this.Session).WithText("Geopend").WithLocale(durchLocale).Build())
                .WithUniqueId(OpenedId)
                .Build();

            new CommunicationEventObjectStateBuilder(this.Session)
                .WithName("Read")
                .WithLocalisedName(new LocalisedTextBuilder(this.Session).WithText("Read").WithLocale(englishLocale).Build())
                .WithLocalisedName(new LocalisedTextBuilder(this.Session).WithText("Gelezen").WithLocale(durchLocale).Build())
                .WithUniqueId(ReadId)
                .Build();

            new CommunicationEventObjectStateBuilder(this.Session)
                .WithName("In Progress")
                .WithLocalisedName(new LocalisedTextBuilder(this.Session).WithText("In Progress").WithLocale(englishLocale).Build())
                .WithLocalisedName(new LocalisedTextBuilder(this.Session).WithText("Lopend").WithLocale(durchLocale).Build())
                .WithUniqueId(InProgressId)
                .Build();

            new CommunicationEventObjectStateBuilder(this.Session)
                .WithName("Closed")
                .WithLocalisedName(new LocalisedTextBuilder(this.Session).WithText("Closed").WithLocale(englishLocale).Build())
                .WithLocalisedName(new LocalisedTextBuilder(this.Session).WithText("Afgewerkt").WithLocale(durchLocale).Build())
                .WithUniqueId(ClosedId)
                .Build();

            new CommunicationEventObjectStateBuilder(this.Session)
                .WithName("Cancelled")
                .WithLocalisedName(new LocalisedTextBuilder(this.Session).WithText("Cancelled").WithLocale(englishLocale).Build())
                .WithLocalisedName(new LocalisedTextBuilder(this.Session).WithText("Geannuleerd").WithLocale(durchLocale).Build())
                .WithUniqueId(CancelledId)
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