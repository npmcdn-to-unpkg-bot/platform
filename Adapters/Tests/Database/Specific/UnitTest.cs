// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest.cs" company="Allors bvba">
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

namespace Allors.Adapters.Special
{
    using System;
    using System.Collections;
    using System.Text;

    using Allors;

    using Allors.Domain;
    using Allors.Meta;

    using NUnit.Framework;

    /// <summary>
    /// WeakReference test must have their own proper method,
    /// otherwise the GC.Collect() doesn't work.
    /// </summary>
    public abstract class UnitTest
    {
        protected virtual bool UseFloatMaximum
        {
            get { return true; }
        }

        protected virtual bool UseFloatMinimum
        {
            get { return true; }
        }

        protected abstract IProfile Profile { get; }

        protected ISession Session
        {
            get
            {
                return this.Profile.Session;
            }
        }

        protected Action[] Markers
        {
            get
            {
                return this.Profile.Markers;
            }
        }

        protected Action[] Inits
        {
            get
            {
                return this.Profile.Inits;
            }
        }

        [Test]
        public void AllorsBoolean()
        {
            foreach (var init in this.Inits)
            {
                init();

                foreach (var mark in this.Markers)
                {
                    {
                        // True
                        var values = C1.Create(this.Session);
                        values.C1AllorsBoolean = true;
                        values.I1AllorsBoolean = true;
                        values.S1AllorsBoolean = true;

                        mark();

                        Assert.AreEqual(true, values.C1AllorsBoolean);
                        Assert.AreEqual(true, values.I1AllorsBoolean);
                        Assert.AreEqual(true, values.S1AllorsBoolean);
                    }

                    {
                        // False
                        var values = C1.Create(this.Session);
                        values.C1AllorsBoolean = false;
                        values.I1AllorsBoolean = false;
                        values.S1AllorsBoolean = false;

                        mark();

                        Assert.AreEqual(false, values.C1AllorsBoolean);
                        Assert.AreEqual(false, values.I1AllorsBoolean);
                        Assert.AreEqual(false, values.S1AllorsBoolean);
                    }

                    {
                        // initial empty
                        var values = C1.Create(this.Session);

                        bool? value = null;

                        mark();
                        value = values.C1AllorsBoolean;
                        Assert.IsNull(value);

                        mark();
                        value = values.I1AllorsBoolean;
                        Assert.IsNull(value);

                        mark();
                        value = values.S1AllorsBoolean;
                        Assert.IsNull(value);

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsBoolean);
                        Assert.IsFalse(values.ExistI1AllorsBoolean);
                        Assert.IsFalse(values.ExistS1AllorsBoolean);

                        this.Session.Commit();

                        value = null;
                        mark();
                        value = values.C1AllorsBoolean;
                        Assert.IsNull(value);

                        mark();
                        value = values.I1AllorsBoolean;
                        Assert.IsNull(value);

                        mark();
                        value = values.S1AllorsBoolean;
                        Assert.IsNull(value);

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsBoolean);
                        Assert.IsFalse(values.ExistI1AllorsBoolean);
                        Assert.IsFalse(values.ExistS1AllorsBoolean);
                    }

                    {
                        // reset empty
                        var values = C1.Create(this.Session);
                        values.C1AllorsBoolean = true;
                        values.I1AllorsBoolean = true;
                        values.S1AllorsBoolean = true;

                        mark();

                        Assert.IsTrue(values.ExistC1AllorsBoolean);
                        Assert.IsTrue(values.ExistI1AllorsBoolean);
                        Assert.IsTrue(values.ExistS1AllorsBoolean);

                        values.RemoveC1AllorsBoolean();
                        values.RemoveI1AllorsBoolean();
                        values.RemoveS1AllorsBoolean();

                        bool? value = null;
                        mark();
                        value = values.C1AllorsBoolean;
                        Assert.IsNull(value);

                        mark();
                        value = values.I1AllorsBoolean;
                        Assert.IsNull(value);

                        mark();
                        value = values.S1AllorsBoolean;
                        Assert.IsNull(value);

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsBoolean);
                        Assert.IsFalse(values.ExistI1AllorsBoolean);
                        Assert.IsFalse(values.ExistS1AllorsBoolean);

                        value = null;

                        mark();
                        value = values.C1AllorsBoolean;
                        Assert.IsNull(value);

                        mark();
                        value = values.I1AllorsBoolean;
                        Assert.IsNull(value);

                        mark();
                        value = values.S1AllorsBoolean;
                        Assert.IsNull(value);

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsBoolean);
                        Assert.IsFalse(values.ExistI1AllorsBoolean);
                        Assert.IsFalse(values.ExistS1AllorsBoolean);
                    }
                }
            }
        }

        [Test]
        public void AllorsBooleanWeakReference()
        {
            foreach (var init in this.Inits)
            {
                init();
                if (this.Session is IDatabaseSession)
                {
                    var c1 = C1.Create(this.Session);
                    var c1Id = c1.Id.ToString();

                    c1.C1AllorsBoolean = true;

                    // Force a Flush
                    Extent<C1> extent = this.Session.Extent(Classes.C1);
                    extent.Filter.AddEquals(RoleTypes.C1AllorsBoolean, true);
                    Assert.IsNotNull(extent.First);

                    // Garbage Collect
                    c1 = null;
                    extent = null;

                    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

                    c1 = (C1)this.Session.Instantiate(c1Id);

                    Assert.AreEqual(true, c1.C1AllorsBoolean);
                }
            }
        }

        [Test]
        public virtual void AllorsDate()
        {
            foreach (var init in this.Inits)
            {
                init();
                foreach (var mark in this.Markers)
                {
                    {
                        // year, day & month
                        var values = C1.Create(this.Session);

                        var date = new DateTime(1973, 03, 27);

                        values.C1AllorsDate = date;
                        values.I1AllorsDate = date;
                        values.S1AllorsDate = date;

                        mark();

                        var dateUniversal = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, DateTimeKind.Utc);

                        Assert.AreEqual(dateUniversal, values.C1AllorsDate);
                        Assert.AreEqual(dateUniversal, values.I1AllorsDate);
                        Assert.AreEqual(dateUniversal, values.S1AllorsDate);
                    }

                    {
                        // Low
                        var values = C1.Create(this.Session);

                        var low = new DateTime(1, 1, 1);

                        values.C1AllorsDate = low;
                        values.I1AllorsDate = low;
                        values.S1AllorsDate = low;

                        mark();

                        var lowUniversal = new DateTime(low.Year, low.Month, low.Day, 0, 0, 0, DateTimeKind.Utc);

                        Assert.AreEqual(lowUniversal, lowUniversal);
                        Assert.AreEqual(lowUniversal, lowUniversal);
                        Assert.AreEqual(lowUniversal, lowUniversal);
                    }

                    {
                        // High
                        var values = C1.Create(this.Session);

                        var high = new DateTime(9999, 1, 1);

                        values.C1AllorsDate = high;
                        values.I1AllorsDate = high;
                        values.S1AllorsDate = high;

                        mark();

                        var highUniversal = new DateTime(high.Year, high.Month, high.Day, 0, 0, 0, DateTimeKind.Utc);

                        Assert.AreEqual(highUniversal, values.C1AllorsDate);
                        Assert.AreEqual(highUniversal, values.I1AllorsDate);
                        Assert.AreEqual(highUniversal, values.S1AllorsDate);
                    }

                    {
                        // Now
                        var now = DateTime.Now;

                        var values = C1.Create(this.Session);
                        
                        values.C1AllorsDate = now;
                        values.I1AllorsDate = now;
                        values.S1AllorsDate = now;

                        mark();
                        
                        var nowUniversal = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, DateTimeKind.Utc);
                        
                        Assert.AreEqual(nowUniversal, values.C1AllorsDate);
                        Assert.AreEqual(nowUniversal, values.I1AllorsDate);
                        Assert.AreEqual(nowUniversal, values.S1AllorsDate);
                    }

                    {
                        // initial empty
                        var values = C1.Create(this.Session);

                        DateTime? value = null;

                        mark();

                        value = values.C1AllorsDate;
                        Assert.IsNull(value);

                        mark();
                        
                        value = values.I1AllorsDate;
                        Assert.IsNull(value);

                        mark();
                        
                        value = values.S1AllorsDate;
                        Assert.IsNull(value);

                        mark();
                        
                        Assert.IsFalse(values.ExistC1AllorsDate);
                        Assert.IsFalse(values.ExistI1AllorsDate);
                        Assert.IsFalse(values.ExistS1AllorsDate);

                        mark();

                        value = null;

                        mark();
                        
                        value = values.C1AllorsDate;
                        Assert.IsNull(value);

                        mark();
                        
                        value = values.I1AllorsDate;
                        Assert.IsNull(value);

                        mark();
                        
                        value = values.S1AllorsDate;
                        Assert.IsNull(value);

                        mark();
                        
                        Assert.IsFalse(values.ExistC1AllorsDate);
                        Assert.IsFalse(values.ExistI1AllorsDate);
                        Assert.IsFalse(values.ExistS1AllorsDate);
                    }

                    {
                        // reset empty
                        var values = C1.Create(this.Session);
                        values.C1AllorsDate = DateTime.Now;
                        values.I1AllorsDate = DateTime.Now;
                        values.S1AllorsDate = DateTime.Now;

                        mark();

                        Assert.IsTrue(values.ExistC1AllorsDate);
                        Assert.IsTrue(values.ExistI1AllorsDate);
                        Assert.IsTrue(values.ExistS1AllorsDate);

                        values.RemoveC1AllorsDate();
                        values.RemoveI1AllorsDate();
                        values.RemoveS1AllorsDate();

                        DateTime? value = null;

                        mark();

                        value = values.C1AllorsDate;
                        Assert.IsNull(value);

                        mark();

                        value = values.I1AllorsDate;
                        Assert.IsNull(value);

                        mark();

                        value = values.S1AllorsDate;
                        Assert.IsNull(value);

                        mark();

                        Assert.IsFalse(values.ExistC1AllorsDate);
                        Assert.IsFalse(values.ExistI1AllorsDate);
                        Assert.IsFalse(values.ExistS1AllorsDate);

                        this.Session.Commit();

                        value = null;
                        mark();
                        value = values.C1AllorsDate;
                        Assert.IsNull(value);

                        mark();
                        value = values.I1AllorsDate;
                        Assert.IsNull(value);

                        mark();
                        value = values.S1AllorsDate;
                        Assert.IsNull(value);

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsDate);
                        Assert.IsFalse(values.ExistI1AllorsDate);
                        Assert.IsFalse(values.ExistS1AllorsDate);
                    }
                }
            }
        }

        [Test]
        public virtual void AllorsDateWithKind()
        {
            foreach (var init in this.Inits)
            {
                init();

                foreach (var mark in this.Markers)
                {
                    var local = new DateTime(1973, 3, 27, 12, 0, 0, DateTimeKind.Local);
                    var unspecified = new DateTime(1973, 3, 27, 12, 0, 0, DateTimeKind.Unspecified);
                    var universal = new DateTime(1973, 3, 27, 12, 0, 0, 0, DateTimeKind.Utc);

                    var c1 = C1.Create(this.Session);
                    c1.C1AllorsDate = local;
                    c1.I1AllorsDate = unspecified;
                    c1.S1AllorsDate = universal;

                    var date = new DateTime(1973, 03, 27, 0, 0, 0, DateTimeKind.Utc);

                    mark();

                    Assert.AreEqual(date, c1.C1AllorsDate);
                    Assert.AreEqual(date, c1.I1AllorsDate);
                    Assert.AreEqual(date, c1.S1AllorsDate);

                    Assert.AreEqual(DateTimeKind.Utc, c1.C1AllorsDate.Value.Kind);
                    Assert.AreEqual(DateTimeKind.Utc, c1.I1AllorsDate.Value.Kind);
                    Assert.AreEqual(DateTimeKind.Utc, c1.S1AllorsDate.Value.Kind);
                }
            }
        }

        [Test]
        public virtual void AllorsDateTimeWeakReference()
        {
            foreach (var init in this.Inits)
            {
                init();
                if (this.Session is IDatabaseSession)
                {
                    var c1 = C1.Create(this.Session);
                    var c1Id = c1.Id.ToString();

                    c1.C1AllorsDate = new DateTime(1973, 03, 27, 1, 1, 1);

                    // Force a Flush
                    Extent<C1> extent = this.Session.Extent(Classes.C1);
                    extent.Filter.AddEquals(RoleTypes.C1AllorsDate, new DateTime(1973, 03, 27, 1, 1, 1));
                    Assert.IsNotNull(extent.First);

                    // Garbage Collect
                    c1 = null;
                    extent = null;

                    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

                    c1 = (C1)this.Session.Instantiate(c1Id);

                    Assert.AreEqual(new DateTime(1973, 03, 27, 1, 1, 1).ToUniversalTime(), c1.C1AllorsDate);
                }
            }
        }

        [Test]
        public virtual void AllorsDecimal()
        {
            foreach (var init in this.Inits)
            {
                init();
                foreach (var mark in this.Markers)
                {
                    {
                        // Positive
                        var values = C1.Create(this.Session);
                        values.C1AllorsDecimal = 10.10m;
                        values.I1AllorsDecimal = 10.10m;
                        values.S1AllorsDecimal = 10.10m;

                        mark();
                        Assert.AreEqual(10.10m, values.C1AllorsDecimal);
                        Assert.AreEqual(10.10m, values.I1AllorsDecimal);
                        Assert.AreEqual(10.10m, values.S1AllorsDecimal);
                    }

                    {
                        // Negative
                        var values = C1.Create(this.Session);
                        values.C1AllorsDecimal = -10.10m;
                        values.I1AllorsDecimal = -10.10m;
                        values.S1AllorsDecimal = -10.10m;

                        mark();
                        Assert.AreEqual(-10.10m, values.C1AllorsDecimal);
                        Assert.AreEqual(-10.10m, values.I1AllorsDecimal);
                        Assert.AreEqual(-10.10m, values.S1AllorsDecimal);
                    }

                    {
                        // Zero
                        var values = C1.Create(this.Session);
                        values.C1AllorsDecimal = 0m;
                        values.I1AllorsDecimal = 0m;
                        values.S1AllorsDecimal = 0m;

                        mark();
                        Assert.AreEqual(0m, values.C1AllorsDecimal);
                        Assert.AreEqual(0m, values.I1AllorsDecimal);
                        Assert.AreEqual(0m, values.S1AllorsDecimal);
                    }

                    {
                        // Minimum
                        var values = C1.Create(this.Session);
                        values.C1AllorsDecimal = decimal.MinValue;
                        values.I1AllorsDecimal = decimal.MinValue;
                        values.S1AllorsDecimal = decimal.MinValue;

                        mark();
                        Assert.AreEqual(decimal.MinValue, values.C1AllorsDecimal);
                        Assert.AreEqual(decimal.MinValue, values.I1AllorsDecimal);
                        Assert.AreEqual(decimal.MinValue, values.S1AllorsDecimal);
                    }

                    {
                        // Maximum
                        var values = C1.Create(this.Session);
                        values.C1AllorsDecimal = decimal.MaxValue;
                        values.I1AllorsDecimal = decimal.MaxValue;
                        values.S1AllorsDecimal = decimal.MaxValue;

                        mark();
                        Assert.AreEqual(decimal.MaxValue, values.C1AllorsDecimal);
                        Assert.AreEqual(decimal.MaxValue, values.I1AllorsDecimal);
                        Assert.AreEqual(decimal.MaxValue, values.S1AllorsDecimal);
                    }

                    {
                        // initial empty
                        var values = C1.Create(this.Session);

                        decimal? value = null;

                        mark();
                        value = values.C1AllorsDecimal;
                        Assert.IsNull(value);

                        mark();
                        value = values.I1AllorsDecimal;
                        Assert.IsNull(value);

                        mark();
                        value = values.S1AllorsDecimal;
                        Assert.IsNull(value);

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsDecimal);
                        Assert.IsFalse(values.ExistI1AllorsDecimal);
                        Assert.IsFalse(values.ExistS1AllorsDecimal);

                        this.Session.Commit();

                        mark();
                        value = values.C1AllorsDecimal;
                        Assert.IsNull(value);

                        mark();
                        value = values.I1AllorsDecimal;
                        Assert.IsNull(value);

                        mark();
                        value = values.S1AllorsDecimal;
                        Assert.IsNull(value);

                        Assert.IsFalse(values.ExistC1AllorsDecimal);
                        Assert.IsFalse(values.ExistI1AllorsDecimal);
                        Assert.IsFalse(values.ExistS1AllorsDecimal);
                    }

                    {
                        // reset empty
                        var values = C1.Create(this.Session);
                        values.C1AllorsDecimal = 10.10m;
                        values.I1AllorsDecimal = 10.10m;
                        values.S1AllorsDecimal = 10.10m;

                        mark();

                        Assert.IsTrue(values.ExistC1AllorsDecimal);
                        Assert.IsTrue(values.ExistI1AllorsDecimal);
                        Assert.IsTrue(values.ExistS1AllorsDecimal);

                        values.RemoveC1AllorsDecimal();
                        values.RemoveI1AllorsDecimal();
                        values.RemoveS1AllorsDecimal();

                        decimal? value = null;

                        mark();
                        value = values.C1AllorsDecimal;
                        Assert.IsNull(value);

                        mark();
                        value = values.I1AllorsDecimal;
                        Assert.IsNull(value);

                        mark();
                        value = values.S1AllorsDecimal;
                        Assert.IsNull(value);

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsDecimal);
                        Assert.IsFalse(values.ExistI1AllorsDecimal);
                        Assert.IsFalse(values.ExistS1AllorsDecimal);

                        mark();
                        value = null;

                        mark();
                        value = values.C1AllorsDecimal;
                        Assert.IsNull(value);

                        mark();
                        value = values.I1AllorsDecimal;
                        Assert.IsNull(value);

                        mark();
                        value = values.S1AllorsDecimal;
                        Assert.IsNull(value);

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsDecimal);
                        Assert.IsFalse(values.ExistI1AllorsDecimal);
                        Assert.IsFalse(values.ExistS1AllorsDecimal);
                    }
                }
            }
        }

        [Test]
        public virtual void AllorsDecimalWeakReference()
        {
            foreach (var init in this.Inits)
            {
                init();
                if (this.Session is IDatabaseSession)
                {
                    var c1 = C1.Create(this.Session);
                    var c1Id = c1.Id.ToString();

                    c1.C1AllorsDecimal = 1M;

                    // Force a Flush
                    Extent<C1> extent = this.Session.Extent(Classes.C1);
                    extent.Filter.AddEquals(RoleTypes.C1AllorsDecimal, 1M);
                    Assert.IsNotNull(extent.First);

                    // Garbage Collect
                    c1 = null;
                    extent = null;

                    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

                    c1 = (C1)this.Session.Instantiate(c1Id);

                    Assert.AreEqual(1M, c1.C1AllorsDecimal);
                }
            }
        }

        [Test]
        public void AllorsFloat()
        {
            foreach (var init in this.Inits)
            {
                init();
                foreach (var mark in this.Markers)
                {
                    {
                        // Positive
                        var values = C1.Create(this.Session);
                        values.C1AllorsFloat = 10.10d;
                        values.I1AllorsFloat = 10.10d;
                        values.S1AllorsFloat = 10.10d;

                        mark();
                        Assert.AreEqual(10.10d, values.C1AllorsFloat);
                        Assert.AreEqual(10.10d, values.I1AllorsFloat);
                        Assert.AreEqual(10.10d, values.S1AllorsFloat);
                    }

                    {
                        // Negative
                        var values = C1.Create(this.Session);
                        values.C1AllorsFloat = -10.10d;
                        values.I1AllorsFloat = -10.10d;
                        values.S1AllorsFloat = -10.10d;

                        mark();
                        Assert.AreEqual(-10.10d, values.C1AllorsFloat);
                        Assert.AreEqual(-10.10d, values.I1AllorsFloat);
                        Assert.AreEqual(-10.10d, values.S1AllorsFloat);
                    }

                    {
                        // Zero
                        var values = C1.Create(this.Session);
                        values.C1AllorsFloat = 0d;
                        values.I1AllorsFloat = 0d;
                        values.S1AllorsFloat = 0d;

                        mark();
                        Assert.AreEqual(0d, values.C1AllorsFloat);
                        Assert.AreEqual(0d, values.I1AllorsFloat);
                        Assert.AreEqual(0d, values.S1AllorsFloat);
                    }

                    // Minimum
                    if (this.UseFloatMinimum)
                    {
                        C1 values = C1.Create(this.Session);
                        values.C1AllorsFloat = double.MinValue;
                        values.I1AllorsFloat = double.MinValue;
                        values.S1AllorsFloat = double.MinValue;

                        mark();
                        Assert.AreEqual(double.MinValue, values.C1AllorsFloat);
                        Assert.AreEqual(double.MinValue, values.I1AllorsFloat);
                        Assert.AreEqual(double.MinValue, values.S1AllorsFloat);
                    }

                    // Maximum
                    if (this.UseFloatMaximum)
                    {
                        var values = C1.Create(this.Session);
                        values.C1AllorsFloat = double.MaxValue;
                        values.I1AllorsFloat = double.MaxValue;
                        values.S1AllorsFloat = double.MaxValue;

                        mark();
                        Assert.AreEqual(double.MaxValue, values.C1AllorsFloat);
                        Assert.AreEqual(double.MaxValue, values.I1AllorsFloat);
                        Assert.AreEqual(double.MaxValue, values.S1AllorsFloat);
                    }

                    {
                        // initial empty
                        var values = C1.Create(this.Session);

                        double? value = null;

                        mark();
                        value = values.C1AllorsFloat;
                        Assert.IsNull(value);

                        mark();
                        value = values.I1AllorsFloat;
                        Assert.IsNull(value);

                        mark();
                        value = values.S1AllorsFloat;
                        Assert.IsNull(value);

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsFloat);
                        Assert.IsFalse(values.ExistI1AllorsFloat);
                        Assert.IsFalse(values.ExistS1AllorsFloat);

                        mark();

                        mark();
                        value = values.C1AllorsFloat;
                        Assert.IsNull(value);

                        mark();
                        value = values.I1AllorsFloat;
                        Assert.IsNull(value);

                        mark();
                        value = values.S1AllorsFloat;
                        Assert.IsNull(value);

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsFloat);
                        Assert.IsFalse(values.ExistI1AllorsFloat);
                        Assert.IsFalse(values.ExistS1AllorsFloat);
                    }

                    {
                        // reset empty
                        var values = C1.Create(this.Session);
                        values.C1AllorsFloat = 10.10d;
                        values.I1AllorsFloat = 10.10d;
                        values.S1AllorsFloat = 10.10d;

                        mark();
                        Assert.IsTrue(values.ExistC1AllorsFloat);
                        Assert.IsTrue(values.ExistI1AllorsFloat);
                        Assert.IsTrue(values.ExistS1AllorsFloat);

                        values.RemoveC1AllorsFloat();
                        values.RemoveI1AllorsFloat();
                        values.RemoveS1AllorsFloat();

                        double? value = null;

                        mark();
                        value = values.C1AllorsFloat;
                        Assert.IsNull(value);

                        mark();
                        value = values.I1AllorsFloat;
                        Assert.IsNull(value);

                        mark();
                        value = values.S1AllorsFloat;
                        Assert.IsNull(value);

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsFloat);
                        Assert.IsFalse(values.ExistI1AllorsFloat);
                        Assert.IsFalse(values.ExistS1AllorsFloat);

                        mark();
                        value = values.C1AllorsFloat;
                        Assert.IsNull(value);

                        mark();
                        value = values.I1AllorsFloat;
                        Assert.IsNull(value);

                        mark();
                        value = values.S1AllorsFloat;
                        Assert.IsNull(value);

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsFloat);
                        Assert.IsFalse(values.ExistI1AllorsFloat);
                        Assert.IsFalse(values.ExistS1AllorsFloat);
                    }
                }
            }
        }

        [Test]
        public void AllorsFloatWeakReference()
        {
            foreach (var init in this.Inits)
            {
                init();
                if (this.Session is IDatabaseSession)
                {
                    C1 c1 = C1.Create(this.Session);
                    var c1Id = c1.Id.ToString();

                    c1.C1AllorsFloat = 1;

                    // Force a Flush
                    Extent<C1> extent = this.Session.Extent(Classes.C1);
                    extent.Filter.AddEquals(RoleTypes.C1AllorsFloat, 1);
                    Assert.IsNotNull(extent.First);

                    // Garbage Collect
                    c1 = null;
                    extent = null;

                    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

                    c1 = (C1)this.Session.Instantiate(c1Id);

                    Assert.AreEqual(1, c1.C1AllorsFloat);
                }
            }
        }

        [Test]
        public void AllorsInteger()
        {
            foreach (var init in this.Inits)
            {
                init();
                foreach (var mark in this.Markers)
                {
                    {
                        // Positive
                        var values = C1.Create(this.Session);
                        values.C1AllorsInteger = 10;
                        values.I1AllorsInteger = 10;
                        values.S1AllorsInteger = 10;

                        mark();
                        Assert.AreEqual(10, values.C1AllorsInteger);
                        Assert.AreEqual(10, values.I1AllorsInteger);
                        Assert.AreEqual(10, values.S1AllorsInteger);
                    }

                    {
                        // Negative
                        var values = C1.Create(this.Session);
                        values.C1AllorsInteger = -10;
                        values.I1AllorsInteger = -10;
                        values.S1AllorsInteger = -10;

                        mark();
                        Assert.AreEqual(-10, values.C1AllorsInteger);
                        Assert.AreEqual(-10, values.I1AllorsInteger);
                        Assert.AreEqual(-10, values.S1AllorsInteger);
                    }

                    {
                        // Zero
                        var values = C1.Create(this.Session);
                        values.C1AllorsInteger = 0;
                        values.I1AllorsInteger = 0;
                        values.S1AllorsInteger = 0;

                        mark();
                        Assert.AreEqual(0, values.C1AllorsInteger);
                        Assert.AreEqual(0, values.I1AllorsInteger);
                        Assert.AreEqual(0, values.S1AllorsInteger);
                    }

                    {
                        // Minimum
                        var values = C1.Create(this.Session);
                        values.C1AllorsInteger = int.MinValue;
                        values.I1AllorsInteger = int.MinValue;
                        values.S1AllorsInteger = int.MinValue;

                        mark();
                        Assert.AreEqual(int.MinValue, values.C1AllorsInteger);
                        Assert.AreEqual(int.MinValue, values.I1AllorsInteger);
                        Assert.AreEqual(int.MinValue, values.S1AllorsInteger);
                    }

                    {
                        // Maximum
                        var values = C1.Create(this.Session);
                        values.C1AllorsInteger = int.MaxValue;
                        values.I1AllorsInteger = int.MaxValue;
                        values.S1AllorsInteger = int.MaxValue;

                        mark();
                        Assert.AreEqual(int.MaxValue, values.C1AllorsInteger);
                        Assert.AreEqual(int.MaxValue, values.I1AllorsInteger);
                        Assert.AreEqual(int.MaxValue, values.S1AllorsInteger);
                    }

                    {
                        // initial empty
                        var values = C1.Create(this.Session);

                        int? value = null;

                        mark();
                        value = values.C1AllorsInteger;
                        Assert.IsNull(value);

                        mark();
                        value = values.I1AllorsInteger;
                        Assert.IsNull(value);

                        mark();
                        value = values.S1AllorsInteger;
                        Assert.IsNull(value);

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsInteger);
                        Assert.IsFalse(values.ExistI1AllorsInteger);
                        Assert.IsFalse(values.ExistS1AllorsInteger);

                        mark();

                        mark();
                        value = values.C1AllorsInteger;
                        Assert.IsNull(value);

                        mark();
                        value = values.I1AllorsInteger;
                        Assert.IsNull(value);

                        mark();
                        value = values.S1AllorsInteger;
                        Assert.IsNull(value);

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsInteger);
                        Assert.IsFalse(values.ExistI1AllorsInteger);
                        Assert.IsFalse(values.ExistS1AllorsInteger);
                    }

                    {
                        // reset empty
                        var values = C1.Create(this.Session);

                        this.Session.Commit();

                        values.C1AllorsInteger = 10;
                        values.I1AllorsInteger = 10;
                        values.S1AllorsInteger = 10;

                        Assert.IsTrue(values.ExistC1AllorsInteger);
                        Assert.IsTrue(values.ExistI1AllorsInteger);
                        Assert.IsTrue(values.ExistS1AllorsInteger);

                        mark();

                        values.RemoveC1AllorsInteger();
                        values.RemoveI1AllorsInteger();
                        values.RemoveS1AllorsInteger();

                        int? value = null;

                        mark();
                        value = values.C1AllorsInteger;
                        Assert.IsNull(value);

                        mark();
                        value = values.I1AllorsInteger;
                        Assert.IsNull(value);

                        mark();
                        value = values.S1AllorsInteger;
                        Assert.IsNull(value);

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsInteger);
                        Assert.IsFalse(values.ExistI1AllorsInteger);
                        Assert.IsFalse(values.ExistS1AllorsInteger);

                        mark();

                        mark();
                        value = values.C1AllorsInteger;
                        Assert.IsNull(value);

                        mark();
                        value = values.I1AllorsInteger;
                        Assert.IsNull(value);

                        mark();
                        value = values.S1AllorsInteger;
                        Assert.IsNull(value);

                        mark();

                        Assert.IsFalse(values.ExistC1AllorsInteger);
                        Assert.IsFalse(values.ExistI1AllorsInteger);
                        Assert.IsFalse(values.ExistS1AllorsInteger);
                    }
                }
            }
        }

        [Test]
        public void AllorsIntegerWeakReference()
        {
            foreach (var init in this.Inits)
            {
                init();
                if (this.Session is IDatabaseSession)
                {
                    var c1 = C1.Create(this.Session);
                    var c1Id = c1.Id.ToString();

                    c1.C1AllorsInteger = 1;

                    // Force a Flush
                    Extent<C1> extent = this.Session.Extent(Classes.C1);
                    extent.Filter.AddEquals(RoleTypes.C1AllorsInteger, 1);
                    Assert.IsNotNull(extent.First);

                    // Garbage Collect
                    c1 = null;
                    extent = null;

                    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

                    c1 = (C1)this.Session.Instantiate(c1Id);

                    Assert.AreEqual(1, c1.C1AllorsInteger);
                }
            }
        }

        [Test]
        public void AllorsLargeString()
        {
            foreach (var init in this.Inits)
            {
                init();
                foreach (var mark in this.Markers)
                {
                    var aLarge = new StringBuilder().Insert(0, "a", 100000).ToString();
                    var bLarge = new StringBuilder().Insert(0, "b", 100000).ToString();
                    var cLarge = new StringBuilder().Insert(0, "c", 100000).ToString();

                    {
                        var values = C1.Create(this.Session);
                        values.C1StringLarge = aLarge;
                        values.I1StringLarge = bLarge;
                        values.S1StringLarge = cLarge;

                        mark();
                        Assert.IsTrue(values.ExistC1StringLarge);
                        Assert.IsTrue(values.ExistI1StringLarge);
                        Assert.IsTrue(values.ExistS1StringLarge);

                        Assert.IsTrue(aLarge.Equals(values.C1StringLarge));
                        Assert.IsTrue(bLarge.Equals(values.I1StringLarge));
                        Assert.IsTrue(cLarge.Equals(values.S1StringLarge));
                    }

                    {
                        // initial empty
                        var values = C1.Create(this.Session);

                        mark();
                        Assert.IsFalse(values.ExistC1StringLarge);
                        Assert.IsFalse(values.ExistI1StringLarge);
                        Assert.IsFalse(values.ExistS1StringLarge);

                        Assert.IsTrue(values.C1StringLarge == null);
                        Assert.IsTrue(values.I1StringLarge == null);
                        Assert.IsTrue(values.S1StringLarge == null);
                    }

                    {
                        // reset empty
                        var values = C1.Create(this.Session);
                        values.C1StringLarge = aLarge;
                        values.I1StringLarge = bLarge;
                        values.S1StringLarge = cLarge;

                        mark();
                        Assert.IsTrue(values.ExistC1StringLarge);
                        Assert.IsTrue(values.ExistI1StringLarge);
                        Assert.IsTrue(values.ExistS1StringLarge);

                        values.RemoveC1StringLarge();
                        values.RemoveI1StringLarge();
                        values.RemoveS1StringLarge();

                        mark();
                        Assert.IsFalse(values.ExistC1StringLarge);
                        Assert.IsFalse(values.ExistI1StringLarge);
                        Assert.IsFalse(values.ExistS1StringLarge);

                        Assert.IsTrue(values.C1StringLarge == null);
                        Assert.IsTrue(values.I1StringLarge == null);
                        Assert.IsTrue(values.S1StringLarge == null);
                    }

                    {
                        // reset null
                        var values = C1.Create(this.Session);
                        values.C1StringLarge = aLarge;
                        values.I1StringLarge = bLarge;
                        values.S1StringLarge = cLarge;

                        mark();
                        Assert.IsTrue(values.ExistC1StringLarge);
                        Assert.IsTrue(values.ExistI1StringLarge);
                        Assert.IsTrue(values.ExistS1StringLarge);

                        values.C1StringLarge = null;
                        values.I1StringLarge = null;
                        values.S1StringLarge = null;

                        mark();
                        Assert.IsFalse(values.ExistC1StringLarge);
                        Assert.IsFalse(values.ExistI1StringLarge);
                        Assert.IsFalse(values.ExistS1StringLarge);

                        Assert.IsTrue(values.C1StringLarge == null);
                        Assert.IsTrue(values.I1StringLarge == null);
                        Assert.IsTrue(values.S1StringLarge == null);
                    }

                    {
                        // large string in small string
                        var exceptionThrown = false;
                        var values = C1.Create(this.Session);
                        try
                        {
                            mark();
                            values.C1AllorsString = aLarge;
                        }
                        catch (ArgumentException)
                        {
                            exceptionThrown = true;
                        }

                        Assert.IsTrue(exceptionThrown);
                        Assert.IsFalse(values.ExistC1AllorsString);

                        exceptionThrown = false;
                        values = C1.Create(this.Session);
                        try
                        {
                            mark();
                            values.I1AllorsString = aLarge;
                        }
                        catch (ArgumentException)
                        {
                            exceptionThrown = true;
                        }

                        Assert.IsTrue(exceptionThrown);
                        Assert.IsFalse(values.ExistI1AllorsString);

                        exceptionThrown = false;
                        values = C1.Create(this.Session);
                        try
                        {
                            mark();
                            values.S1AllorsString = aLarge;
                        }
                        catch (ArgumentException)
                        {
                            exceptionThrown = true;
                        }

                        Assert.IsTrue(exceptionThrown);
                        Assert.IsFalse(values.ExistS1AllorsString);

                        this.Session.Commit();
                    }
                }
            }
        }

        [Test]
        public void AllorsLargeStringWeakReference()
        {
            foreach (var init in this.Inits)
            {
                init();
                if (this.Session is IDatabaseSession)
                {
                    var aLarge = new StringBuilder().Insert(0, "a", 100000).ToString();

                    var c1 = C1.Create(this.Session);
                    var c1Id = c1.Id.ToString();

                    c1.C1StringLarge = aLarge;

                    // Force a Flush
                    Extent<C1> extent = this.Session.Extent(Classes.C1);
                    extent.Filter.AddEquals(RoleTypes.C1StringLarge, aLarge);
                    Assert.IsNotNull(extent.First);

                    // Garbage Collect
                    c1 = null;
                    extent = null;

                    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

                    c1 = (C1)this.Session.Instantiate(c1Id);

                    Assert.AreEqual(aLarge, c1.C1StringLarge);
                }
            }
        }

        [Test]
        public void AllorsSmallBinary()
        {
            foreach (var init in this.Inits)
            {
                init();
                foreach (var mark in this.Markers)
                {
                    var binary1 = new byte[] { 0 };
                    var binary2 = new byte[] { 1, 2 };
                    var binary3 = new byte[] { 3, 4, 5 };

                    {
                        var values = C1.Create(this.Session);
                        values.C1AllorsBinary = binary1;
                        values.I1AllorsBinary = binary2;
                        values.S1AllorsBinary = binary3;

                        mark();
                        Assert.AreEqual(binary1, values.C1AllorsBinary);
                        Assert.AreEqual(binary1, values.C1AllorsBinary);
                        Assert.AreEqual(binary2, values.I1AllorsBinary);
                        Assert.AreEqual(binary2, values.I1AllorsBinary);
                        Assert.AreEqual(binary3, values.S1AllorsBinary);
                        Assert.AreEqual(binary3, values.S1AllorsBinary);
                    }

                    {
                        // initial empty
                        var values = C1.Create(this.Session);

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsBinary);
                        Assert.IsFalse(values.ExistI1AllorsBinary);
                        Assert.IsFalse(values.ExistS1AllorsBinary);

                        Assert.AreEqual(null, values.C1AllorsBinary);
                        Assert.AreEqual(null, values.I1AllorsBinary);
                        Assert.AreEqual(null, values.S1AllorsBinary);
                    }

                    {
                        // reset empty
                        var values = C1.Create(this.Session);
                        values.C1AllorsBinary = binary1;
                        values.I1AllorsBinary = binary2;
                        values.S1AllorsBinary = binary3;

                        mark();
                        Assert.IsTrue(values.ExistC1AllorsBinary);
                        Assert.IsTrue(values.ExistI1AllorsBinary);
                        Assert.IsTrue(values.ExistS1AllorsBinary);

                        values.RemoveC1AllorsBinary();
                        values.RemoveI1AllorsBinary();
                        values.RemoveS1AllorsBinary();

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsBinary);
                        Assert.IsFalse(values.ExistI1AllorsBinary);
                        Assert.IsFalse(values.ExistS1AllorsBinary);

                        Assert.AreEqual(null, values.C1AllorsBinary);
                        Assert.AreEqual(null, values.I1AllorsBinary);
                        Assert.AreEqual(null, values.S1AllorsBinary);
                    }

                    {
                        // reset null
                        var values = C1.Create(this.Session);
                        values.C1AllorsBinary = binary1;
                        values.I1AllorsBinary = binary2;
                        values.S1AllorsBinary = binary3;

                        mark();
                        Assert.IsTrue(values.ExistC1AllorsBinary);
                        Assert.IsTrue(values.ExistI1AllorsBinary);
                        Assert.IsTrue(values.ExistS1AllorsBinary);

                        values.C1AllorsBinary = null;
                        values.I1AllorsBinary = null;
                        values.S1AllorsBinary = null;

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsBinary);
                        Assert.IsFalse(values.ExistI1AllorsBinary);
                        Assert.IsFalse(values.ExistS1AllorsBinary);

                        Assert.AreEqual(null, values.C1AllorsBinary);
                        Assert.AreEqual(null, values.I1AllorsBinary);
                        Assert.AreEqual(null, values.S1AllorsBinary);
                    }

                    {
                        // rollback set
                        var values = C1.Create(this.Session);

                        this.Session.Commit();

                        values.C1AllorsBinary = binary1;
                        values.I1AllorsBinary = binary2;
                        values.S1AllorsBinary = binary3;

                        this.Session.Rollback();

                        Assert.IsFalse(values.ExistC1AllorsBinary);
                        Assert.IsFalse(values.ExistI1AllorsBinary);
                        Assert.IsFalse(values.ExistS1AllorsBinary);
                    }

                    {
                        // rollback reset
                        var values = C1.Create(this.Session);

                        mark();

                        values.C1AllorsBinary = binary1;
                        values.I1AllorsBinary = binary2;
                        values.S1AllorsBinary = binary3;

                        this.Session.Commit();

                        values.C1AllorsBinary = new byte[] { 9 };
                        values.I1AllorsBinary = new byte[] { 9 };
                        values.S1AllorsBinary = new byte[] { 9 };

                        this.Session.Rollback();

                        Assert.IsTrue(values.ExistC1AllorsBinary);
                        Assert.IsTrue(values.ExistI1AllorsBinary);
                        Assert.IsTrue(values.ExistS1AllorsBinary);

                        Assert.AreEqual(binary1, values.C1AllorsBinary);
                        Assert.AreEqual(binary2, values.I1AllorsBinary);
                        Assert.AreEqual(binary3, values.S1AllorsBinary);
                    }

                    {
                        // rollback reset null
                        var values = C1.Create(this.Session);

                        mark();

                        values.C1AllorsBinary = binary1;
                        values.I1AllorsBinary = binary2;
                        values.S1AllorsBinary = binary3;

                        this.Session.Commit();

                        values.RemoveC1AllorsBinary();
                        values.RemoveI1AllorsBinary();
                        values.RemoveS1AllorsBinary();

                        this.Session.Rollback();

                        Assert.IsTrue(values.ExistC1AllorsBinary);
                        Assert.IsTrue(values.ExistI1AllorsBinary);
                        Assert.IsTrue(values.ExistS1AllorsBinary);

                        Assert.AreEqual(binary1, values.C1AllorsBinary);
                        Assert.AreEqual(binary2, values.I1AllorsBinary);
                        Assert.AreEqual(binary3, values.S1AllorsBinary);
                    }
                }
            }
        }

        [Test]
        public void AllorsSmallBinaryWeakReference()
        {
            foreach (var init in this.Inits)
            {
                init();
                if (this.Session is IDatabaseSession)
                {
                    var binary1 = new byte[] { 0 };

                    var c1 = C1.Create(this.Session);
                    var c1Id = c1.Id.ToString();

                    c1.C1AllorsBinary = binary1;

                    // Force a Flush
                    Extent<C1> extent = this.Session.Extent(Classes.C1);
                    extent.Filter.AddEquals(RoleTypes.C1AllorsBinary, binary1);
                    Assert.IsNotNull(extent.First);

                    // Garbage Collect
                    c1 = null;
                    extent = null;

                    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

                    c1 = (C1)this.Session.Instantiate(c1Id);

                    Assert.AreEqual(binary1, c1.C1AllorsBinary);
                }
            }
        }

        [Test]
        public void AllorsSmallString()
        {
            foreach (var init in this.Inits)
            {
                init();
                foreach (var mark in this.Markers)
                {
                    {
                        var values = C1.Create(this.Session);
                        values.C1AllorsString = "a";
                        values.I1AllorsString = "b";
                        values.S1AllorsString = "c";

                        mark();
                        Assert.AreEqual("a", values.C1AllorsString);
                        Assert.AreEqual("a", values.C1AllorsString);
                        Assert.AreEqual("b", values.I1AllorsString);
                        Assert.AreEqual("b", values.I1AllorsString);
                        Assert.AreEqual("c", values.S1AllorsString);
                        Assert.AreEqual("c", values.S1AllorsString);
                    }

                    {
                        // initial empty
                        var values = C1.Create(this.Session);

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsString);
                        Assert.IsFalse(values.ExistI1AllorsString);
                        Assert.IsFalse(values.ExistS1AllorsString);

                        Assert.AreEqual(null, values.C1AllorsString);
                        Assert.AreEqual(null, values.I1AllorsString);
                        Assert.AreEqual(null, values.S1AllorsString);
                    }

                    {
                        // reset empty
                        var values = C1.Create(this.Session);
                        values.C1AllorsString = "a";
                        values.I1AllorsString = "b";
                        values.S1AllorsString = "c";

                        mark();
                        Assert.IsTrue(values.ExistC1AllorsString);
                        Assert.IsTrue(values.ExistI1AllorsString);
                        Assert.IsTrue(values.ExistS1AllorsString);

                        values.RemoveC1AllorsString();
                        values.RemoveI1AllorsString();
                        values.RemoveS1AllorsString();

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsString);
                        Assert.IsFalse(values.ExistI1AllorsString);
                        Assert.IsFalse(values.ExistS1AllorsString);

                        Assert.AreEqual(null, values.C1AllorsString);
                        Assert.AreEqual(null, values.I1AllorsString);
                        Assert.AreEqual(null, values.S1AllorsString);
                    }

                    {
                        // reset null
                        var values = C1.Create(this.Session);
                        values.C1AllorsString = "a";
                        values.I1AllorsString = "b";
                        values.S1AllorsString = "c";

                        mark();
                        Assert.IsTrue(values.ExistC1AllorsString);
                        Assert.IsTrue(values.ExistI1AllorsString);
                        Assert.IsTrue(values.ExistS1AllorsString);

                        values.C1AllorsString = null;
                        values.I1AllorsString = null;
                        values.S1AllorsString = null;

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsString);
                        Assert.IsFalse(values.ExistI1AllorsString);
                        Assert.IsFalse(values.ExistS1AllorsString);

                        Assert.AreEqual(null, values.C1AllorsString);
                        Assert.AreEqual(null, values.I1AllorsString);
                        Assert.AreEqual(null, values.S1AllorsString);
                    }

                    {
                        // rollback set
                        var values = C1.Create(this.Session);

                        this.Session.Commit();

                        values.C1AllorsString = "a";
                        values.I1AllorsString = "b";
                        values.S1AllorsString = "c";

                        this.Session.Rollback();

                        Assert.IsFalse(values.ExistC1AllorsString);
                        Assert.IsFalse(values.ExistI1AllorsString);
                        Assert.IsFalse(values.ExistS1AllorsString);
                    }

                    {
                        // rollback reset
                        var values = C1.Create(this.Session);

                        mark();

                        values.C1AllorsString = "a";
                        values.I1AllorsString = "b";
                        values.S1AllorsString = "c";

                        this.Session.Commit();

                        values.C1AllorsString = "q";
                        values.I1AllorsString = "r";
                        values.S1AllorsString = "s";

                        this.Session.Rollback();

                        Assert.IsTrue(values.ExistC1AllorsString);
                        Assert.IsTrue(values.ExistI1AllorsString);
                        Assert.IsTrue(values.ExistS1AllorsString);

                        Assert.AreEqual("a", values.C1AllorsString);
                        Assert.AreEqual("b", values.I1AllorsString);
                        Assert.AreEqual("c", values.S1AllorsString);
                    }

                    {
                        // rollback reset null
                        var values = C1.Create(this.Session);

                        mark();

                        values.C1AllorsString = "a";
                        values.I1AllorsString = "b";
                        values.S1AllorsString = "c";

                        this.Session.Commit();

                        values.RemoveC1AllorsString();
                        values.RemoveI1AllorsString();
                        values.RemoveS1AllorsString();

                        this.Session.Rollback();

                        Assert.IsTrue(values.ExistC1AllorsString);
                        Assert.IsTrue(values.ExistI1AllorsString);
                        Assert.IsTrue(values.ExistS1AllorsString);

                        Assert.AreEqual("a", values.C1AllorsString);
                        Assert.AreEqual("b", values.I1AllorsString);
                        Assert.AreEqual("c", values.S1AllorsString);
                    }
                }
            }
        }

        [Test]
        public void AllorsSmallStringWeakReference()
        {
            foreach (var init in this.Inits)
            {
                init();
                if (this.Session is IDatabaseSession)
                {
                    var c1 = C1.Create(this.Session);
                    var c1Id = c1.Id.ToString();

                    c1.C1AllorsString = "a";

                    // Force a Flush
                    Extent<C1> extent = this.Session.Extent(Classes.C1);
                    extent.Filter.AddEquals(RoleTypes.C1AllorsString, "a");
                    Assert.IsNotNull(extent.First);

                    // Garbage Collect
                    c1 = null;
                    extent = null;

                    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

                    c1 = (C1)this.Session.Instantiate(c1Id);

                    Assert.AreEqual("a", c1.C1AllorsString);
                }
            }
        }

        [Test]
        public virtual void AllorsUnique()
        {
            foreach (var init in this.Inits)
            {
                init();
                foreach (var mark in this.Markers)
                {
                    {
                        var unique1 = Guid.NewGuid();
                        var unique2 = Guid.NewGuid();
                        var unique3 = Guid.NewGuid();

                        var values = C1.Create(this.Session);
                        values.C1AllorsUnique = unique1;
                        values.I1AllorsUnique = unique2;
                        values.S1AllorsUnique = unique3;

                        mark();
                        Assert.AreEqual(unique1, values.C1AllorsUnique);
                        Assert.AreEqual(unique2, values.I1AllorsUnique);
                        Assert.AreEqual(unique3, values.S1AllorsUnique);
                    }

                    {
                        // Empty Guid
                        var values = C1.Create(this.Session);
                        values.C1AllorsUnique = Guid.Empty;
                        values.I1AllorsUnique = Guid.Empty;
                        values.S1AllorsUnique = Guid.Empty;

                        mark();
                        Assert.AreEqual(Guid.Empty, values.C1AllorsUnique);
                        Assert.AreEqual(Guid.Empty, values.I1AllorsUnique);
                        Assert.AreEqual(Guid.Empty, values.S1AllorsUnique);
                    }

                    {
                        // initial empty
                        var values = C1.Create(this.Session);

                        Guid? value = null;

                        mark();
                        value = values.C1AllorsUnique;
                        Assert.IsNull(value);

                        mark();
                        value = values.I1AllorsUnique;
                        Assert.IsNull(value);

                        mark();
                        value = values.S1AllorsUnique;
                        Assert.IsNull(value);

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsUnique);
                        Assert.IsFalse(values.ExistI1AllorsUnique);
                        Assert.IsFalse(values.ExistS1AllorsUnique);

                        mark();
                        value = values.C1AllorsUnique;
                        Assert.IsNull(value);

                        mark();
                        value = values.I1AllorsUnique;
                        Assert.IsNull(value);

                        mark();
                        value = values.S1AllorsUnique;
                        Assert.IsNull(value);

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsUnique);
                        Assert.IsFalse(values.ExistI1AllorsUnique);
                        Assert.IsFalse(values.ExistS1AllorsUnique);
                    }

                    {
                        // reset empty
                        var values = C1.Create(this.Session);
                        values.C1AllorsUnique = Guid.NewGuid();
                        values.I1AllorsUnique = Guid.NewGuid();
                        values.S1AllorsUnique = Guid.NewGuid();

                        mark();
                        Assert.IsTrue(values.ExistC1AllorsUnique);
                        Assert.IsTrue(values.ExistI1AllorsUnique);
                        Assert.IsTrue(values.ExistS1AllorsUnique);

                        values.RemoveC1AllorsUnique();
                        values.RemoveI1AllorsUnique();
                        values.RemoveS1AllorsUnique();

                        Guid? value = null;

                        mark();
                        value = values.C1AllorsUnique;
                        Assert.IsNull(value);

                        mark();
                        value = values.I1AllorsUnique;
                        Assert.IsNull(value);

                        mark();
                        value = values.S1AllorsUnique;
                        Assert.IsNull(value);

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsUnique);
                        Assert.IsFalse(values.ExistI1AllorsUnique);
                        Assert.IsFalse(values.ExistS1AllorsUnique);

                        mark();

                        mark();
                        value = values.C1AllorsUnique;
                        Assert.IsNull(value);

                        mark();
                        value = values.I1AllorsUnique;
                        Assert.IsNull(value);

                        mark();
                        value = values.S1AllorsUnique;
                        Assert.IsNull(value);

                        mark();
                        Assert.IsFalse(values.ExistC1AllorsUnique);
                        Assert.IsFalse(values.ExistI1AllorsUnique);
                        Assert.IsFalse(values.ExistS1AllorsUnique);
                    }
                }
            }
        }

        [Test]
        public virtual void AllorsUniqueWeakReference()
        {
            foreach (var init in this.Inits)
            {
                init();
                if (this.Session is IDatabaseSession)
                {
                    Guid unique = Guid.NewGuid();

                    C1 c1 = C1.Create(this.Session);
                    var c1Id = c1.Id.ToString();

                    c1.C1AllorsUnique = unique;

                    // Force a Flush
                    Extent<C1> extent = this.Session.Extent(Classes.C1);
                    extent.Filter.AddEquals(RoleTypes.C1AllorsUnique, unique);
                    Assert.IsNotNull(extent.First);

                    // Garbage Collect
                    c1 = null;
                    extent = null;

                    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

                    c1 = (C1)this.Session.Instantiate(c1Id);

                    Assert.AreEqual(unique, c1.C1AllorsUnique);
                }
            }
        }

        [Test]
        public virtual void Dirty()
        {
            foreach (var init in this.Inits)
            {
                init();
                var values = new ArrayList();
                for (int i = 0; i < 4000; i++)
                {
                    values.Add(SingleUnit.Create(this.Session));
                }

                this.Session.Commit();

                foreach (SingleUnit singleValue in values)
                {
                    singleValue.AllorsInteger = int.Parse(singleValue.Strategy.ObjectId.ToString());
                }

                this.Session.Commit();
            }
        }

        [Test]
        public void RelationChecks()
        {
            foreach (var init in this.Inits)
            {
                init();
                foreach (var mark in this.Markers)
                {
                    var c1A = C1.Create(this.Session);
                    var c1B = C1.Create(this.Session);

                    // Illegal Role
                    // Illegal values
                    var exceptionThrown = false;
                    try
                    {
                        mark();
                        c1A.Strategy.SetUnitRole(RoleTypes.C1AllorsBoolean, "Oops");
                    }
                    catch (ArgumentException)
                    {
                        exceptionThrown = true;
                    }

                    Assert.IsTrue(exceptionThrown);

                    exceptionThrown = false;
                    try
                    {
                        mark();
                        c1A.Strategy.SetUnitRole(RoleTypes.C1AllorsDecimal, "Oops");
                    }
                    catch
                    {
                        exceptionThrown = true;
                    }

                    Assert.IsTrue(exceptionThrown);

                    exceptionThrown = false;
                    try
                    {
                        mark();
                        c1A.Strategy.SetUnitRole(RoleTypes.C1AllorsFloat, "Oops");
                    }
                    catch
                    {
                        exceptionThrown = true;
                    }

                    Assert.IsTrue(exceptionThrown);

                    exceptionThrown = false;
                    try
                    {
                        mark();
                        c1A.Strategy.SetUnitRole(RoleTypes.C1AllorsInteger, "Oops");
                    }
                    catch
                    {
                        exceptionThrown = true;
                    }

                    Assert.IsTrue(exceptionThrown);

                    exceptionThrown = false;
                    try
                    {
                        mark();
                        c1A.Strategy.SetUnitRole(RoleTypes.C1AllorsString, 0);
                    }
                    catch
                    {
                        exceptionThrown = true;
                    }

                    Assert.IsTrue(exceptionThrown);

                    exceptionThrown = false;
                    try
                    {
                        mark();
                        c1A.Strategy.SetUnitRole(RoleTypes.C1AllorsInteger, 0L);
                    }
                    catch
                    {
                        exceptionThrown = true;
                    }

                    Assert.IsTrue(exceptionThrown);

                    // Illegal objects
                    exceptionThrown = false;
                    try
                    {
                        mark();
                        c1A.Strategy.SetUnitRole(RoleTypes.C1AllorsBoolean, c1B);
                    }
                    catch
                    {
                        exceptionThrown = true;
                    }

                    Assert.IsTrue(exceptionThrown);

                    exceptionThrown = false;
                    try
                    {
                        mark();
                        c1A.Strategy.SetUnitRole(RoleTypes.C1AllorsDate, c1B);
                    }
                    catch
                    {
                        exceptionThrown = true;
                    }

                    Assert.IsTrue(exceptionThrown);

                    exceptionThrown = false;
                    try
                    {
                        mark();
                        c1A.Strategy.SetUnitRole(RoleTypes.C1AllorsDecimal, c1B);
                    }
                    catch
                    {
                        exceptionThrown = true;
                    }

                    Assert.IsTrue(exceptionThrown);

                    exceptionThrown = false;
                    try
                    {
                        mark();
                        c1A.Strategy.SetUnitRole(RoleTypes.C1AllorsFloat, c1B);
                    }
                    catch
                    {
                        exceptionThrown = true;
                    }

                    Assert.IsTrue(exceptionThrown);

                    exceptionThrown = false;
                    try
                    {
                        mark();
                        c1A.Strategy.SetUnitRole(RoleTypes.C1AllorsInteger, c1B);
                    }
                    catch
                    {
                        exceptionThrown = true;
                    }

                    Assert.IsTrue(exceptionThrown);

                    exceptionThrown = false;
                    try
                    {
                        mark();
                        c1A.Strategy.SetUnitRole(RoleTypes.C1AllorsString, 0);
                    }
                    catch
                    {
                        exceptionThrown = true;
                    }

                    Assert.IsTrue(exceptionThrown);

                    // Illegal AssociationType
                    exceptionThrown = false;
                    try
                    {
                        mark();
                        c1A.Strategy.SetUnitRole(RoleTypes.C2AllorsBoolean, true);
                    }
                    catch
                    {
                        exceptionThrown = true;
                    }

                    Assert.IsTrue(exceptionThrown);

                    exceptionThrown = false;
                    try
                    {
                        mark();
                        c1A.Strategy.SetUnitRole(RoleTypes.C2AllorsDate, DateTime.Now);
                    }
                    catch
                    {
                        exceptionThrown = true;
                    }

                    Assert.IsTrue(exceptionThrown);

                    exceptionThrown = false;
                    try
                    {
                        mark();
                        c1A.Strategy.SetUnitRole(RoleTypes.C2AllorsDecimal, DateTime.Now);
                    }
                    catch
                    {
                        exceptionThrown = true;
                    }

                    Assert.IsTrue(exceptionThrown);

                    exceptionThrown = false;
                    try
                    {
                        mark();
                        c1A.Strategy.SetUnitRole(RoleTypes.C2AllorsFloat, 0.01m);
                    }
                    catch
                    {
                        exceptionThrown = true;
                    }

                    Assert.IsTrue(exceptionThrown);

                    exceptionThrown = false;
                    try
                    {
                        mark();
                        c1A.Strategy.SetUnitRole(RoleTypes.C2AllorsInteger, 1);
                    }
                    catch
                    {
                        exceptionThrown = true;
                    }

                    Assert.IsTrue(exceptionThrown);

                    exceptionThrown = false;
                    try
                    {
                        mark();
                        c1A.Strategy.SetUnitRole(RoleTypes.C2AllorsString, "hello");
                    }
                    catch
                    {
                        exceptionThrown = true;
                    }

                    Assert.IsTrue(exceptionThrown);

                    // Illegal Role
                    exceptionThrown = false;
                    try
                    {
                        mark();
                        c1A.Strategy.SetUnitRole(RoleTypes.C1C1one2one, "Ooops");
                    }
                    catch
                    {
                        exceptionThrown = true;
                    }

                    Assert.IsTrue(exceptionThrown);
                }
            }
        }
    }
}