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
        public void GivenColor_WhenDeriving_ThenDisplayNameIsSet()
        {
            var color = new ColourBuilder(this.DatabaseSession)
                .WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                            .WithText("Color")
                                            .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                            .Build())
                .Build();

            this.DatabaseSession.Derive(true); 

            Assert.AreEqual("Color", color.DisplayName);
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
        public void GivenDimension_WhenDeriving_ThenDisplayNameIsSet()
        {
            var unitOfMeasure = new UnitOfMeasureBuilder(this.DatabaseSession)
                .WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                            .WithText("uom")
                                            .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                            .Build())
                .Build();

            var dimension = new DimensionBuilder(this.DatabaseSession).WithUnitOfMeasure(unitOfMeasure).WithUnit(3).Build();

            this.DatabaseSession.Derive(true); 

            Assert.AreEqual("3 uom", dimension.DisplayName);
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
        public void GivenModel_WhenDeriving_ThenDisplayNameIsSet()
        {
            var model = new ModelBuilder(this.DatabaseSession)
                .WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                            .WithText("Model")
                                            .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                            .Build())
                .Build();

            this.DatabaseSession.Derive(true); 

            Assert.AreEqual("Model", model.DisplayName);
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
        public void GivenServiceFeature_WhenDeriving_ThenDisplayNameIsSet()
        {
            var serviceFeature = new ServiceFeatureBuilder(this.DatabaseSession)
                .WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                            .WithText("Service Feature")
                                            .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                            .Build())
                .Build();

            this.DatabaseSession.Derive(true); 

            Assert.AreEqual("Service Feature", serviceFeature.DisplayName);
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
        public void GivenSizeConstant_WhenDeriving_ThenDisplayNameIsSet()
        {
            var size = new SizeBuilder(this.DatabaseSession)
                .WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                            .WithText("Size")
                                            .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                            .Build())
                .Build();

            this.DatabaseSession.Derive(true); 

            Assert.AreEqual("Size", size.DisplayName);
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
        public void GivenSoftwareFeature_WhenDeriving_ThenDisplayNameIsSet()
        {
            var softwareFeature = new SoftwareFeatureBuilder(this.DatabaseSession)
                .WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                            .WithText("Software Feature")
                                            .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                            .Build())
                .Build();

            this.DatabaseSession.Derive(true); 

            Assert.AreEqual("Software Feature", softwareFeature.DisplayName);
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

        [Test]
        public void GivenProductQualityConstant_WhenDeriving_ThenDisplayNameIsSet()
        {
            var productQuality = new ProductQualityBuilder(this.DatabaseSession)
                .WithLocalisedName(new LocalisedTextBuilder(this.DatabaseSession)
                                            .WithText("Product Quality")
                                            .WithLocale(Singleton.Instance(this.DatabaseSession).DefaultLocale)
                                            .Build())
                .Build();

            this.DatabaseSession.Derive(true); 

            Assert.AreEqual("Product Quality", productQuality.DisplayName);
        }
    }
}
