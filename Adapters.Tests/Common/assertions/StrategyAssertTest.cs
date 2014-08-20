// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StrategyAssertTest.cs" company="Allors bvba">
//   Copyright 2002-2012 Allors bvba.
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

namespace Allors.Adapters.Special.Assertions
{
    using Allors.Adapters.Database.Memory.IntegerId;
    using Allors;

    using global::Domain;

    using NUnit.Framework;

    [TestFixture]
    public class StrategyAssertTest
    {
        private ISession session;

        [Test]
        public void AssociationExistExclusive()
        {
            var anObject = C1.Create(this.session);
            var anotherObject = C2.Create(this.session);

            StrategyAssert.AssociationsExistExclusive(anotherObject);

            anObject.C1C2one2one = anotherObject;

            StrategyAssert.AssociationsExistExclusive(anotherObject, C1Meta.C1C2one2one.AssociationType);

            anObject.C1I2one2one = anotherObject;

            StrategyAssert.AssociationsExistExclusive(anotherObject, C1Meta.C1C2one2one.AssociationType, C1Meta.C1I2one2one.AssociationType);

            anObject.AddC1C2one2many(anotherObject);

            StrategyAssert.AssociationsExistExclusive(
                anotherObject, 
                C1Meta.C1C2one2one.AssociationType,
                C1Meta.C1I2one2one.AssociationType,
                C1Meta.C1C2one2many.AssociationType);

            anObject.AddC1I2one2many(anotherObject);

            StrategyAssert.AssociationsExistExclusive(
                anotherObject,
                C1Meta.C1C2one2one.AssociationType,
                C1Meta.C1I2one2one.AssociationType,
                C1Meta.C1C2one2many.AssociationType,
                C1Meta.C1I2one2many.AssociationType);

            var exceptionOccured = false;
            try
            {
                StrategyAssert.AssociationsExistExclusive(anotherObject, C2Meta.C2C2one2one.AssociationType);
            }
            catch
            {
                exceptionOccured = true;
            }

            Assert.IsTrue(exceptionOccured);
        }

        [Test]
        public void RolesExistExclusive()
        {
            var anObject = C1.Create(this.session);
            var anotherObject = C2.Create(this.session);

            StrategyAssert.RolesExistExclusive(anObject);

            anObject.C1AllorsString = "C1AllorsString";

            StrategyAssert.RolesExistExclusive(anObject, C1Meta.C1AllorsString);

            anObject.A1AllorsString = "A1AllorsString";

            StrategyAssert.RolesExistExclusive(
                anObject,
                C1Meta.C1AllorsString,
                A1Meta.A1AllorsString);

            anObject.I1AllorsString = "I1AllorsString";

            StrategyAssert.RolesExistExclusive(
                anObject,
                C1Meta.C1AllorsString,
                A1Meta.A1AllorsString,
                I1Meta.I1AllorsString);

            anObject.C1C2one2one = anotherObject;

            StrategyAssert.RolesExistExclusive(
                anObject,
                C1Meta.C1AllorsString,
                A1Meta.A1AllorsString,
                I1Meta.I1AllorsString,
                C1Meta.C1C2one2one);

            anObject.C1I2one2one = anotherObject;

            StrategyAssert.RolesExistExclusive(
                anObject,
                C1Meta.C1AllorsString,
                A1Meta.A1AllorsString,
                I1Meta.I1AllorsString,
                C1Meta.C1C2one2one,
                C1Meta.C1I2one2one);

            anObject.AddC1C2one2many(anotherObject);

            StrategyAssert.RolesExistExclusive(
                anObject,
                C1Meta.C1AllorsString,
                A1Meta.A1AllorsString,
                I1Meta.I1AllorsString,
                C1Meta.C1C2one2one,
                C1Meta.C1I2one2one,
                C1Meta.C1C2one2many);

            anObject.AddC1I2one2many(anotherObject);

            StrategyAssert.RolesExistExclusive(
                anObject,
                C1Meta.C1AllorsString,
                A1Meta.A1AllorsString,
                I1Meta.I1AllorsString,
                C1Meta.C1C2one2one,
                C1Meta.C1I2one2one,
                C1Meta.C1C2one2many,
                C1Meta.C1I2one2many);

            var exceptionOccured = false;
            try
            {
                StrategyAssert.RolesExistExclusive(anObject, C2Meta.C2AllorsString);
            }
            catch
            {
                exceptionOccured = true;
            }

            Assert.IsTrue(exceptionOccured);
        }

        [SetUp]
        protected void Init()
        {
            var configuration = new Configuration { ObjectFactory = new ObjectFactory(M.D, typeof(IObject).Assembly, "Domain") };
            var database = new Database(configuration);
            this.session = database.CreateSession();
        }

        [TearDown]
        protected void Dispose()
        {
            this.session = null;
        }
    }
}