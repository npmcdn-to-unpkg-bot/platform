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

namespace Allors.Adapters.Relation.SqlClient
{
    using System;
    using Allors.Domain;
    using NUnit.Framework;

    [TestFixture]
    public abstract class UnitTest : Adapters.UnitTest
    {
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