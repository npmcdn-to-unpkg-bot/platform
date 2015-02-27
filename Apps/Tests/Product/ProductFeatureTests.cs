//------------------------------------------------------------------------------------------------- 
// <copyright file="ProductFeatureTests.cs" company="Allors bvba">
// Copyright 2002-2009 Allors bvba.
// 
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
// 
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// <summary>Defines the MediaTests type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    
    
    using Allors.Domain;

    using NUnit.Framework;

    [TestFixture]
    public class ProductFeatureTests : DomainTest
    {
        [Test]
        public void GivenColor_WhenDeriving_ThenRequiredRelationsMustExist()
        {
            var builder = new ColourBuilder(this.DatabaseSession);
            var color = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                            .WithText("color")
                                            .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                            .Build());
            color = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenDimension_WhenDeriving_ThenRequiredRelationsMustExist()
        {
            var builder = new DimensionBuilder(this.DatabaseSession);
            var dimension = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            var unitOfMeasure = new UnitOfMeasureBuilder(this.DatabaseSession)
                .WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                            .WithText("uom")
                                            .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                            .Build())
                .Build();

            builder.WithUnitOfMeasure(unitOfMeasure);
            dimension = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenModel_WhenDeriving_ThenRequiredRelationsMustExist()
        {
            var builder = new ModelBuilder(this.DatabaseSession);
            var model = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                            .WithText("model")
                                            .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                            .Build());
            model = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenServiceFeature_WhenDeriving_ThenRequiredRelationsMustExist()
        {
            var builder = new ServiceFeatureBuilder(this.DatabaseSession);
            var serviceFeature = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                            .WithText("service feature")
                                            .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                            .Build());
            serviceFeature = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenSizeConstant_WhenDeriving_ThenRequiredRelationsMustExist()
        {
            var builder = new SizeBuilder(this.DatabaseSession);
            var size = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                            .WithText("size")
                                            .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                            .Build());
            size = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenSoftwareFeature_WhenDeriving_ThenRequiredRelationsMustExist()
        {
            var builder = new SoftwareFeatureBuilder(this.DatabaseSession);
            var softwareFeature = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                            .WithText("software feature")
                                            .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                            .Build());
            softwareFeature = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }

        [Test]
        public void GivenProductQualityConstant_WhenDeriving_ThenRequiredRelationsMustExist()
        {
            var builder = new ProductQualityBuilder(this.DatabaseSession);
            var productQuality = builder.Build();

            Assert.IsTrue(this.DatabaseSession.Derive().HasErrors);

            this.DatabaseSession.Rollback();

            builder.WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                            .WithText("product quality")
                                            .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                            .Build());
            productQuality = builder.Build();

            Assert.IsFalse(this.DatabaseSession.Derive().HasErrors);
        }
    }
}
