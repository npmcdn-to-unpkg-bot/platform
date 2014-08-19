//------------------------------------------------------------------------------------------------- 
// <copyright file="Default.cs" company="Allors bvba">
// Copyright 2002-2010 Allors bvba.
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
// <summary>Defines the Default type.</summary>
//------------------------------------------------------------------------------------------------

namespace Allors.R1.Adapters.Special.Npgsql
{
    using System;

    using Domain;

    using NUnit.Framework;

    [TestFixture]
    public abstract class UnitTest : Special.UnitTest
    {
        public DateTime MAXIMUM_DATETIME
        {
            get { return DateTime.MaxValue.ToUniversalTime(); }
        }

        public DateTime MINIMUM_DATETIME
        {
            get { return DateTime.MinValue.ToUniversalTime(); ; }
        }

        protected override bool UseDoubleMaximum
        {
            get
            {
                return false;
            }
        }

        protected override bool UseDoubleMinimum
        {
            get
            {
                return false;
            }
        }

        [Test]
        public override void AllorsDateTime()
        {
            // year, day & month
            {
                var values = C1.Create(this.Session);
                values.C1AllorsDateTime = new DateTime(1973, 03, 27);
                values.I1AllorsDateTime = new DateTime(1973, 03, 27);
                Assert.AreEqual(new DateTime(1973, 03, 27).ToUniversalTime(), values.C1AllorsDateTime);
                Assert.AreEqual(new DateTime(1973, 03, 27).ToUniversalTime(), values.I1AllorsDateTime);
            }

            // Minimum
            {
                var values = C1.Create(this.Session);
                values.C1AllorsDateTime = MINIMUM_DATETIME;
                values.I1AllorsDateTime = MINIMUM_DATETIME;
                Assert.AreEqual(MINIMUM_DATETIME, values.C1AllorsDateTime);
                Assert.AreEqual(MINIMUM_DATETIME, values.I1AllorsDateTime);
            }

            // Maximum
            {
                var values = C1.Create(this.Session);
                values.C1AllorsDateTime = MAXIMUM_DATETIME;
                values.I1AllorsDateTime = MAXIMUM_DATETIME;
                Assert.AreEqual(MAXIMUM_DATETIME, values.C1AllorsDateTime);
                Assert.AreEqual(MAXIMUM_DATETIME, values.I1AllorsDateTime);
            }

            // initial empty
            {
                var values = C1.Create(this.Session);

                DateTime? value = DateTime.Now;
                var valueOld = value;
                var exceptionThrown = false;
                try
                {
                    value = values.C1AllorsDateTime;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                exceptionThrown = false;
                try
                {
                    value = values.I1AllorsDateTime;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                exceptionThrown = false;
                try
                {
                    value = values.S1AllorsDateTime;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                Assert.AreEqual(valueOld, value);

                Assert.IsFalse(values.ExistC1AllorsDateTime);
                Assert.IsFalse(values.ExistI1AllorsDateTime);
                Assert.IsFalse(values.ExistS1AllorsDateTime);

                this.Session.Commit();

                value = DateTime.Now;
                valueOld = value;
                exceptionThrown = false;
                try
                {
                    value = values.C1AllorsDateTime;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                exceptionThrown = false;
                try
                {
                    value = values.I1AllorsDateTime;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                exceptionThrown = false;
                try
                {
                    value = values.S1AllorsDateTime;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                Assert.AreEqual(valueOld, value);

                Assert.IsFalse(values.ExistC1AllorsDateTime);
                Assert.IsFalse(values.ExistI1AllorsDateTime);
                Assert.IsFalse(values.ExistS1AllorsDateTime);
            }

            // reset empty
            {
                var values = C1.Create(this.Session);
                values.C1AllorsDateTime = DateTime.Now;
                values.I1AllorsDateTime = DateTime.Now;
                values.S1AllorsDateTime = DateTime.Now;

                this.Session.Commit();

                Assert.IsTrue(values.ExistC1AllorsDateTime);
                Assert.IsTrue(values.ExistI1AllorsDateTime);
                Assert.IsTrue(values.ExistS1AllorsDateTime);

                values.RemoveC1AllorsDateTime();
                values.RemoveI1AllorsDateTime();
                values.RemoveS1AllorsDateTime();

                DateTime? value = DateTime.Now;
                var valueOld = value;
                var exceptionThrown = false;
                try
                {
                    value = values.C1AllorsDateTime;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                exceptionThrown = false;
                try
                {
                    value = values.I1AllorsDateTime;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                exceptionThrown = false;
                try
                {
                    value = values.S1AllorsDateTime;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                Assert.AreEqual(valueOld, value);

                Assert.IsFalse(values.ExistC1AllorsDateTime);
                Assert.IsFalse(values.ExistI1AllorsDateTime);
                Assert.IsFalse(values.ExistS1AllorsDateTime);

                this.Session.Commit();

                value = DateTime.Now;
                valueOld = value;
                exceptionThrown = false;
                try
                {
                    value = values.C1AllorsDateTime;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                exceptionThrown = false;
                try
                {
                    value = values.I1AllorsDateTime;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                exceptionThrown = false;
                try
                {
                    value = values.S1AllorsDateTime;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                Assert.AreEqual(valueOld, value);

                Assert.IsFalse(values.ExistC1AllorsDateTime);
                Assert.IsFalse(values.ExistI1AllorsDateTime);
                Assert.IsFalse(values.ExistS1AllorsDateTime);
            }
        }

        [Test]
        public override void AllorsDecimal()
        {
            // Positive
            {
                C1 values = C1.Create(this.Session);
                values.C1AllorsDecimal = 10.10m;
                values.I1AllorsDecimal = 10.10m;
                values.S1AllorsDecimal = 10.10m;

                this.Session.Commit();

                Assert.AreEqual(10.10m, values.C1AllorsDecimal);
                Assert.AreEqual(10.10m, values.I1AllorsDecimal);
                Assert.AreEqual(10.10m, values.S1AllorsDecimal);
            }

            // Negative
            {
                C1 values = C1.Create(this.Session);
                values.C1AllorsDecimal = -10.10m;
                values.I1AllorsDecimal = -10.10m;
                values.S1AllorsDecimal = -10.10m;

                this.Session.Commit();
                ;

                Assert.AreEqual(-10.10m, values.C1AllorsDecimal);
                Assert.AreEqual(-10.10m, values.I1AllorsDecimal);
                Assert.AreEqual(-10.10m, values.S1AllorsDecimal);
            }

            // Zero
            {
                C1 values = C1.Create(this.Session);
                values.C1AllorsDecimal = 0m;
                values.I1AllorsDecimal = 0m;
                values.S1AllorsDecimal = 0m;

                this.Session.Commit();
                ;

                Assert.AreEqual(0m, values.C1AllorsDecimal);
                Assert.AreEqual(0m, values.I1AllorsDecimal);
                Assert.AreEqual(0m, values.S1AllorsDecimal);
            }

            // initial empty
            {
                C1 values = C1.Create(this.Session);

                decimal? value = -1;
                var exceptionThrown = false;
                try
                {
                    value = values.C1AllorsDecimal;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                exceptionThrown = false;
                try
                {
                    value = values.I1AllorsDecimal;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                exceptionThrown = false;
                try
                {
                    value = values.S1AllorsDecimal;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                Assert.AreEqual(-1, value);

                Assert.IsFalse(values.ExistC1AllorsDecimal);
                Assert.IsFalse(values.ExistI1AllorsDecimal);
                Assert.IsFalse(values.ExistS1AllorsDecimal);

                this.Session.Commit();

                value = -1;
                exceptionThrown = false;
                try
                {
                    value = values.C1AllorsDecimal;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                exceptionThrown = false;
                try
                {
                    value = values.I1AllorsDecimal;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                exceptionThrown = false;
                try
                {
                    value = values.S1AllorsDecimal;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                Assert.AreEqual(-1, value);

                Assert.IsFalse(values.ExistC1AllorsDecimal);
                Assert.IsFalse(values.ExistI1AllorsDecimal);
                Assert.IsFalse(values.ExistS1AllorsDecimal);
            }

            // reset empty
            {
                C1 values = C1.Create(this.Session);
                values.C1AllorsDecimal = 10.10m;
                values.I1AllorsDecimal = 10.10m;
                values.S1AllorsDecimal = 10.10m;

                this.Session.Commit();

                Assert.IsTrue(values.ExistC1AllorsDecimal);
                Assert.IsTrue(values.ExistI1AllorsDecimal);
                Assert.IsTrue(values.ExistS1AllorsDecimal);

                values.RemoveC1AllorsDecimal();
                values.RemoveI1AllorsDecimal();
                values.RemoveS1AllorsDecimal();

                decimal? value = -1;
                var exceptionThrown = false;
                try
                {
                    value = values.C1AllorsDecimal;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                exceptionThrown = false;
                try
                {
                    value = values.I1AllorsDecimal;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                exceptionThrown = false;
                try
                {
                    value = values.S1AllorsDecimal;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                Assert.AreEqual(-1, value);

                Assert.IsFalse(values.ExistC1AllorsDecimal);
                Assert.IsFalse(values.ExistI1AllorsDecimal);
                Assert.IsFalse(values.ExistS1AllorsDecimal);

                this.Session.Commit();
                value = -1;
                exceptionThrown = false;
                try
                {
                    value = values.C1AllorsDecimal;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                exceptionThrown = false;
                try
                {
                    value = values.I1AllorsDecimal;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                exceptionThrown = false;
                try
                {
                    value = values.S1AllorsDecimal;
                }
                catch { exceptionThrown = true; }
                Assert.IsTrue(exceptionThrown);
                Assert.AreEqual(-1, value);

                Assert.IsFalse(values.ExistC1AllorsDecimal);
                Assert.IsFalse(values.ExistI1AllorsDecimal);
                Assert.IsFalse(values.ExistS1AllorsDecimal);
            }
        }
    }
}