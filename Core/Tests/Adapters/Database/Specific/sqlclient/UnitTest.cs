// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest.cs" company="Allors bvba">
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

namespace Allors.Adapters.Special.SqlClient
{
    using System;
    using Allors.Domain;
    using NUnit.Framework;

    [TestFixture]
    public abstract class UnitTest : Special.UnitTest
    {
        protected DateTime MAXIMUM_DATETIME
        {
            get { return new DateTime(9999, 12, 31, 11, 59, 00, 00, DateTimeKind.Utc); }
        }

        protected DateTime MINIMUM_DATETIME
        {
            get { return new DateTime(1753, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc); }
        }

        [Test]
        public override void AllorsDateTime()
        {
            foreach (var init in this.Inits)
            {
                init();

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
                    values.C1AllorsDateTime = this.MINIMUM_DATETIME;
                    values.I1AllorsDateTime = this.MINIMUM_DATETIME;
                    Assert.AreEqual(this.MINIMUM_DATETIME, values.C1AllorsDateTime);
                    Assert.AreEqual(this.MINIMUM_DATETIME, values.I1AllorsDateTime);
                }

                // Maximum
                {
                    var values = C1.Create(this.Session);
                    values.C1AllorsDateTime = this.MAXIMUM_DATETIME;
                    values.I1AllorsDateTime = this.MAXIMUM_DATETIME;
                    Assert.AreEqual(this.MAXIMUM_DATETIME, values.C1AllorsDateTime);
                    Assert.AreEqual(this.MAXIMUM_DATETIME, values.I1AllorsDateTime);
                }

                // initial empty
                {
                    var values = C1.Create(this.Session);

                    DateTime? value = null;

                    value = values.C1AllorsDateTime;
                    Assert.IsNull(value);

                    value = values.I1AllorsDateTime;
                    Assert.IsNull(value);

                    value = values.S1AllorsDateTime;
                    Assert.IsNull(value);

                    Assert.IsFalse(values.ExistC1AllorsDateTime);
                    Assert.IsFalse(values.ExistI1AllorsDateTime);
                    Assert.IsFalse(values.ExistS1AllorsDateTime);

                    this.Session.Commit();

                    value = values.C1AllorsDateTime;
                    Assert.IsNull(value);

                    value = values.I1AllorsDateTime;
                    Assert.IsNull(value);

                    value = values.S1AllorsDateTime;
                    Assert.IsNull(value);

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

                    DateTime? value = null;

                    value = values.C1AllorsDateTime;
                    Assert.IsNull(value);

                    value = values.I1AllorsDateTime;
                    Assert.IsNull(value);

                    value = values.S1AllorsDateTime;
                    Assert.IsNull(value);

                    Assert.IsFalse(values.ExistC1AllorsDateTime);
                    Assert.IsFalse(values.ExistI1AllorsDateTime);
                    Assert.IsFalse(values.ExistS1AllorsDateTime);

                    this.Session.Commit();

                    value = values.C1AllorsDateTime;
                    Assert.IsNull(value);

                    value = values.I1AllorsDateTime;
                    Assert.IsNull(value);

                    value = values.S1AllorsDateTime;
                    Assert.IsNull(value);

                    Assert.IsFalse(values.ExistC1AllorsDateTime);
                    Assert.IsFalse(values.ExistI1AllorsDateTime);
                    Assert.IsFalse(values.ExistS1AllorsDateTime);
                }
            }
        }

        [Test]
        public override void AllorsDecimal()
        {
            foreach (var init in this.Inits)
            {
                init();

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

                    Assert.AreEqual(0m, values.C1AllorsDecimal);
                    Assert.AreEqual(0m, values.I1AllorsDecimal);
                    Assert.AreEqual(0m, values.S1AllorsDecimal);
                }

                // initial empty
                {
                    C1 values = C1.Create(this.Session);

                    decimal? value = null;

                    value = values.C1AllorsDecimal;
                    Assert.IsNull(value);

                    value = values.I1AllorsDecimal;
                    Assert.IsNull(value);

                    value = values.S1AllorsDecimal;
                    Assert.IsNull(value);

                    Assert.IsFalse(values.ExistC1AllorsDecimal);
                    Assert.IsFalse(values.ExistI1AllorsDecimal);
                    Assert.IsFalse(values.ExistS1AllorsDecimal);

                    this.Session.Commit();

                    value = values.C1AllorsDecimal;
                    Assert.IsNull(value);

                    value = values.I1AllorsDecimal;
                    Assert.IsNull(value);

                    value = values.S1AllorsDecimal;
                    Assert.IsNull(value);

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

                    decimal? value = null;
                    value = values.C1AllorsDecimal;
                    Assert.IsNull(value);

                    value = values.I1AllorsDecimal;
                    Assert.IsNull(value);

                    value = values.S1AllorsDecimal;
                    Assert.IsNull(value);

                    Assert.IsFalse(values.ExistC1AllorsDecimal);
                    Assert.IsFalse(values.ExistI1AllorsDecimal);
                    Assert.IsFalse(values.ExistS1AllorsDecimal);

                    this.Session.Commit();

                    value = values.C1AllorsDecimal;
                    Assert.IsNull(value);

                    value = values.I1AllorsDecimal;
                    Assert.IsNull(value);

                    value = values.S1AllorsDecimal;
                    Assert.IsNull(value);

                    Assert.IsFalse(values.ExistC1AllorsDecimal);
                    Assert.IsFalse(values.ExistI1AllorsDecimal);
                    Assert.IsFalse(values.ExistS1AllorsDecimal);
                }
            }
        }
    }
}