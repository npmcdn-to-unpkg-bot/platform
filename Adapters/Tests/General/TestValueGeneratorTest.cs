// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestValueGeneratorTest.cs" company="Allors bvba">
//   Copyright 2002-2012 Allors bvba.
// Dual Licensed under
//   a) the Lesser General Public Licence v3 (LGPL)
//   b) the Allors License
// The LGPL License is included in the file lgpl.txt.
// The Allors License is an addendum to your contract.
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Adapters.General
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class TestValueGeneratorTest
    {
        private readonly TestValueGenerator testValueGenerator = new TestValueGenerator();

        [Test]
        [Category("Dynamic")]
        public void GenerateBoolean()
        {
            bool value = this.testValueGenerator.GenerateBoolean();
            bool differentValueFound = false;

            for (int i = 0; i < 100; i++)
            {
                bool newValue = this.testValueGenerator.GenerateBoolean();
                if (newValue != value)
                {
                    differentValueFound = true;
                    break;
                }
            }

            Assert.IsTrue(differentValueFound);
        }

        [Test]
        [Category("Dynamic")]
        public void GenerateDate()
        {
            DateTime value1 = this.testValueGenerator.GenerateDate();
            DateTime value2 = this.testValueGenerator.GenerateDate();

            Assert.AreNotEqual(value1, value2);
        }

        [Test]
        [Category("Dynamic")]
        public void GenerateDateTime()
        {
            DateTime value1 = this.testValueGenerator.GenerateDateTime();
            DateTime value2 = this.testValueGenerator.GenerateDateTime();

            Assert.AreNotEqual(value1, value2);
        }

        [Test]
        [Category("Dynamic")]
        public void GenerateDecimal()
        {
            decimal value1 = this.testValueGenerator.GenerateDecimal();
            decimal value2 = this.testValueGenerator.GenerateDecimal();

            Assert.AreNotEqual(value1, value2);
        }

        [Test]
        [Category("Dynamic")]
        public void GenerateFloat()
        {
            double value1 = this.testValueGenerator.GenerateDouble();
            double value2 = this.testValueGenerator.GenerateDouble();

            Assert.AreNotEqual(value1, value2);
        }

        [Test]
        [Category("Dynamic")]
        public void GenerateInteger()
        {
            int value1 = this.testValueGenerator.GenerateInteger();
            int value2 = this.testValueGenerator.GenerateInteger();

            Assert.AreNotEqual(value1, value2);
        }

        [Test]
        [Category("Dynamic")]
        public void GeneratePercentage()
        {
            double value1 = this.testValueGenerator.GeneratePercentage();
            double value2 = this.testValueGenerator.GeneratePercentage();

            Assert.AreNotEqual(value1, value2);
        }

        [Test]
        [Category("Dynamic")]
        public void GenerateString()
        {
            string value1 = this.testValueGenerator.GenerateString(0);
            string value2 = this.testValueGenerator.GenerateString(0);

            Assert.AreEqual(0, value1.Length);
            Assert.AreEqual(0, value2.Length);
            Assert.AreEqual(value1, value2);

            value1 = this.testValueGenerator.GenerateString(1);
            value2 = this.testValueGenerator.GenerateString(1);

            Assert.AreEqual(1, value1.Length);
            Assert.AreEqual(1, value2.Length);
            Assert.AreNotEqual(value1, value2);

            value1 = this.testValueGenerator.GenerateString(100);
            value2 = this.testValueGenerator.GenerateString(100);

            Assert.AreEqual(100, value1.Length);
            Assert.AreEqual(100, value2.Length);
            Assert.AreNotEqual(value1, value2);
        }

        [Test]
        [Category("Dynamic")]
        public void GenerateUnique()
        {
            Guid value1 = this.testValueGenerator.GenerateUnique();
            Guid value2 = this.testValueGenerator.GenerateUnique();

            Assert.AreNotEqual(value1, value2);
        }
    }
}