// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtentTest.cs" company="Allors bvba">
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

namespace Allors.Adapters.Special
{
    using System;
    using System.Collections.Generic;

    using Allors;
    using Allors.Meta;

    using Domain;

    using NUnit.Framework;

    public enum Zero2Four
    {
        Zero = 0, 
        One = 1, 
        Two = 2, 
        Three = 3, 
        Four = 4
    }

    public abstract class ExtentTest
    {
        protected static readonly bool[] TrueFalse = { true, false };

        #region Population
        protected C1 c1A;
        protected C1 c1B;
        protected C1 c1C;
        protected C1 c1D;
        protected C2 c2A;
        protected C2 c2B;
        protected C2 c2C;
        protected C2 c2D;
        protected C3 c3A;
        protected C3 c3B;
        protected C3 c3C;
        protected C3 c3D;
        protected C4 c4A;
        protected C4 c4B;
        protected C4 c4C;
        protected C4 c4D;
        #endregion

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

        protected virtual bool[] UseOperator
        {
            get { return new[] { false, true }; }
        }

        [Test]
        public void AndGreaterThanLessThan()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                var all = extent.Filter.AddAnd();
                all.AddGreaterThan(C1Meta.C1AllorsInteger, 0);
                all.AddLessThan(C1Meta.C1AllorsInteger, 2);

                Assert.AreEqual(1, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Interface
                (extent = this.LocalExtent(I12Meta.ObjectType)).Filter.AddAnd()
                    .AddGreaterThan(I12Meta.I12AllorsInteger, 0)
                    .AddLessThan(I12Meta.I12AllorsInteger, 2);

                Assert.AreEqual(2, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Super Interface
                (extent = this.LocalExtent(S1234Meta.ObjectType)).Filter.AddAnd()
                    .AddGreaterThan(S1234Meta.S1234AllorsInteger, 0)
                    .AddLessThan(S1234Meta.S1234AllorsInteger, 2);

                Assert.AreEqual(4, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsTrue(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsTrue(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));
            }
        }

        [Test]
        public void AndLessThan()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();


                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                var all = extent.Filter.AddAnd();
                all.AddLessThan(C1Meta.C1AllorsInteger, 2);

                Assert.AreEqual(1, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Interface
                (extent = this.LocalExtent(I12Meta.ObjectType)).Filter.AddAnd().AddLessThan(I12Meta.I12AllorsInteger, 2);

                Assert.AreEqual(2, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Super Interface
                (extent = this.LocalExtent(S1234Meta.ObjectType)).Filter.AddAnd()
                    .AddLessThan(S1234Meta.S1234AllorsInteger, 2);

                Assert.AreEqual(4, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsTrue(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsTrue(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));
            }
        }

        [Test]
        public void AssociationMany2ManyContainedIn()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                foreach (var useEnumerable in TrueFalse)
                {
                    foreach (var useOperator in this.UseOperator)
                    {
                        var inExtent = this.LocalExtent(C1Meta.ObjectType);
                        inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                            inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                            var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                            inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        var extent = this.LocalExtent(C2Meta.ObjectType);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(C1Meta.C1C2many2many.AssociationType, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(C1Meta.C1C2many2many.AssociationType, inExtent);
                        }

                        Assert.AreEqual(0, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, false, false, false);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // Full
                        inExtent = this.LocalExtent(C1Meta.ObjectType);
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                            var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(C2Meta.ObjectType);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(C1Meta.C1C2many2many.AssociationType, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(C1Meta.C1C2many2many.AssociationType, inExtent);
                        }

                        Assert.AreEqual(3, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, true, true, true);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // Filtered
                        inExtent = this.LocalExtent(C1Meta.ObjectType);
                        inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                            inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                            var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                            inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(C2Meta.ObjectType);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(C1Meta.C1C2many2many.AssociationType, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(C1Meta.C1C2many2many.AssociationType, inExtent);
                        }

                        Assert.AreEqual(1, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, true, false, false);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // ContainedIn Extent over Interface
                        // Empty
                        inExtent = this.LocalExtent(I12Meta.ObjectType);
                        inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                            inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                            var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                            inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(C2Meta.ObjectType);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(C1Meta.C1C2many2many.AssociationType, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(C1Meta.C1C2many2many.AssociationType, inExtent);
                        }

                        Assert.AreEqual(0, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, false, false, false);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // Full
                        inExtent = this.LocalExtent(I12Meta.ObjectType);
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                            var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(C2Meta.ObjectType);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(C1Meta.C1C2many2many.AssociationType, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(C1Meta.C1C2many2many.AssociationType, inExtent);
                        }

                        Assert.AreEqual(3, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, true, true, true);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // Filtered
                        inExtent = this.LocalExtent(I12Meta.ObjectType);
                        inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                            inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                            var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                            inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(C2Meta.ObjectType);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(C1Meta.C1C2many2many.AssociationType, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(C1Meta.C1C2many2many.AssociationType, inExtent);
                        }

                        Assert.AreEqual(1, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, true, false, false);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // RelationType from Class to Interface

                        // ContainedIn Extent over Class
                        // Empty
                        inExtent = this.LocalExtent(C1Meta.ObjectType);
                        inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                            inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                            var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                            inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(C2Meta.ObjectType);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(C1Meta.C1I12many2many.AssociationType, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(C1Meta.C1I12many2many.AssociationType, inExtent);
                        }

                        Assert.AreEqual(0, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, false, false, false);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // Full
                        inExtent = this.LocalExtent(C1Meta.ObjectType);
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                            var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(C2Meta.ObjectType);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(C1Meta.C1I12many2many.AssociationType, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(C1Meta.C1I12many2many.AssociationType, inExtent);
                        }

                        Assert.AreEqual(3, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, true, true, true);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // Filtered
                        inExtent = this.LocalExtent(C1Meta.ObjectType);
                        inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                            inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                            var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                            inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(C2Meta.ObjectType);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(C1Meta.C1I12many2many.AssociationType, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(C1Meta.C1I12many2many.AssociationType, inExtent);
                        }

                        Assert.AreEqual(1, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, true, false, false);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // ContainedIn Extent over Interface
                        // Empty
                        inExtent = this.LocalExtent(I12Meta.ObjectType);
                        inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                            inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                            var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                            inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(C2Meta.ObjectType);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(C1Meta.C1I12many2many.AssociationType, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(C1Meta.C1I12many2many.AssociationType, inExtent);
                        }

                        Assert.AreEqual(0, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, false, false, false);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // Full
                        inExtent = this.LocalExtent(I12Meta.ObjectType);
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                            var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(C2Meta.ObjectType);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(C1Meta.C1I12many2many.AssociationType, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(C1Meta.C1I12many2many.AssociationType, inExtent);
                        }

                        Assert.AreEqual(3, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, true, true, true);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // Filtered
                        inExtent = this.LocalExtent(I12Meta.ObjectType);
                        inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                            inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                            var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                            inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(C2Meta.ObjectType);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(C1Meta.C1I12many2many.AssociationType, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(C1Meta.C1I12many2many.AssociationType, inExtent);
                        }

                        Assert.AreEqual(1, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, true, false, false);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);
                    }
                }
            }
        }

        [Test]
        public void AssociationMany2ManyContains()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddContains(C1Meta.C1C2many2many.AssociationType, this.c1C);

                Assert.AreEqual(2, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsTrue(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddContains(C1Meta.C1C2many2many.AssociationType, this.c1C);
                extent.Filter.AddContains(C1Meta.C1C2many2many.AssociationType, this.c1D);

                Assert.AreEqual(2, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsTrue(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddContains(C1Meta.C1I12many2many.AssociationType, this.c1C);

                Assert.AreEqual(2, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsTrue(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddContains(S1234Meta.S1234many2many.AssociationType, this.c1B);

                Assert.AreEqual(2, extent.Count);
                Assert.IsTrue(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));
            }
        }

        [Test]
        public void AssociationMany2ManyExist()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddExists(C1Meta.C1C2many2many.AssociationType);

                Assert.AreEqual(3, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsTrue(extent.Contains(this.c2C));
                Assert.IsTrue(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Interface
                extent = this.LocalExtent(I2Meta.ObjectType);
                extent.Filter.AddExists(I1Meta.I1I2many2many.AssociationType);

                Assert.AreEqual(3, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsTrue(extent.Contains(this.c2C));
                Assert.IsTrue(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddExists(S1234Meta.S1234many2many.AssociationType);

                Assert.AreEqual(10, extent.Count);
                Assert.IsTrue(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsTrue(extent.Contains(this.c1C));
                Assert.IsTrue(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsTrue(extent.Contains(this.c2C));
                Assert.IsTrue(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsTrue(extent.Contains(this.c3B));
                Assert.IsTrue(extent.Contains(this.c3C));
                Assert.IsTrue(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Class - Wrong RelationType
                extent = this.LocalExtent(C2Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddExists(C1Meta.C1C1many2many.AssociationType);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(I12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddExists(I12Meta.I12I34many2many.AssociationType);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(S12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddExists(S1234Meta.S1234many2many.AssociationType);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void AssociationMany2OneContainedIn()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                foreach (var useOperator in this.UseOperator)
                {
                    // Extent over Class

                    // RelationType from Class to Class

                    // ContainedIn Extent over Class
                    // Empty
                    var inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C2many2one.AssociationType, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C2many2one.AssociationType, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, true, true, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C2many2one.AssociationType, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, true, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C2many2one.AssociationType, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C2many2one.AssociationType, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, true, true, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C2many2one.AssociationType, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, true, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from Class to Interface

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12many2one.AssociationType, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12many2one.AssociationType, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, true, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12many2one.AssociationType, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12many2one.AssociationType, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12many2one.AssociationType, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, true, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12many2one.AssociationType, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);
                }
            }
        }

        [Test]
        public void AssociationMany2OneContains()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddContains(C1Meta.C1C1many2one.AssociationType, this.c1C);

                Assert.AreEqual(1, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsTrue(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddContains(C1Meta.C1C2many2one.AssociationType, this.c1C);

                Assert.AreEqual(1, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsTrue(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                extent = this.LocalExtent(C4Meta.ObjectType);
                extent.Filter.AddContains(C3Meta.C3C4many2one.AssociationType, this.c3C);

                Assert.AreEqual(1, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsTrue(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddContains(C1Meta.C1I12many2one.AssociationType, this.c1C);

                Assert.AreEqual(1, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsTrue(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // TODO: wrong relation
            }
        }

        [Test]
        public void AssociationOne2ManyContainedIn()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                foreach (var useOperator in this.UseOperator)
                {
                    // Extent over Class

                    // RelationType from Class to Class

                    // ContainedIn Extent over Class
                    // Empty
                    var inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C2one2many.AssociationType, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C2one2many.AssociationType, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C2one2many.AssociationType, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, true, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C2one2many.AssociationType, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C2one2many.AssociationType, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C2one2many.AssociationType, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, true, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from Class to Interface

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12one2many.AssociationType, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12one2many.AssociationType, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12one2many.AssociationType, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12one2many.AssociationType, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12one2many.AssociationType, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12one2many.AssociationType, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);
                }
            }
        }

        [Test]
        public void AssociationOne2ManyEquals()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1C2one2many.AssociationType, this.c1B);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1C2one2many.AssociationType, this.c1C);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(I2Meta.ObjectType);
                extent.Filter.AddEquals(I1Meta.I1I2one2many.AssociationType, this.c1B);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(I2Meta.ObjectType);
                extent.Filter.AddEquals(I1Meta.I1I2one2many.AssociationType, this.c1C);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234one2many.AssociationType, this.c1B);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234one2many.AssociationType, this.c3C);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddEquals(C3Meta.C3C2one2many.AssociationType, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(I12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(C3Meta.C3C2one2many.AssociationType, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(S12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(C3Meta.C3C2one2many.AssociationType, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void AssociationOne2ManyExist()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddExists(C1Meta.C1C2one2many.AssociationType);

                Assert.AreEqual(3, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsTrue(extent.Contains(this.c2C));
                Assert.IsTrue(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Interface
                extent = this.LocalExtent(I2Meta.ObjectType);
                extent.Filter.AddExists(I1Meta.I1I2one2many.AssociationType);

                Assert.AreEqual(3, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsTrue(extent.Contains(this.c2C));
                Assert.IsTrue(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddExists(S1234Meta.S1234one2many.AssociationType);

                Assert.AreEqual(3, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsTrue(extent.Contains(this.c1C));
                Assert.IsTrue(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Class - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddExists(C3Meta.C3C2one2many.AssociationType);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(I12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddExists(C3Meta.C3C2one2many.AssociationType);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(S12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddExists(C3Meta.C3C2one2many.AssociationType);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void AssociationOne2ManyInstanceof()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddInstanceof(C1Meta.C1C2one2many.AssociationType, C1Meta.ObjectType);

                Assert.AreEqual(3, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsTrue(extent.Contains(this.c2C));
                Assert.IsTrue(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddInstanceof(C1Meta.C1I12one2many.AssociationType, C1Meta.ObjectType);

                Assert.AreEqual(3, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsTrue(extent.Contains(this.c2C));
                Assert.IsTrue(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddInstanceof(S1234Meta.S1234one2many.AssociationType, C1Meta.ObjectType);

                Assert.AreEqual(1, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // TODO: wrong relation
            }
        }

        [Test]
        public void AssociationOne2OneContainedIn()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                foreach (var useOperator in this.UseOperator)
                {
                    // Extent over Class
                    // RelationType from Class to Class

                    // ContainedIn Extent over Class
                    var inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C1one2one.AssociationType, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    Assert.IsFalse(extent.Contains(this.c1A));
                    Assert.IsTrue(extent.Contains(this.c1B));
                    Assert.IsTrue(extent.Contains(this.c1C));
                    Assert.IsTrue(extent.Contains(this.c1D));
                    Assert.IsFalse(extent.Contains(this.c2A));
                    Assert.IsFalse(extent.Contains(this.c2B));
                    Assert.IsFalse(extent.Contains(this.c2C));
                    Assert.IsFalse(extent.Contains(this.c2D));
                    Assert.IsFalse(extent.Contains(this.c3A));
                    Assert.IsFalse(extent.Contains(this.c3B));
                    Assert.IsFalse(extent.Contains(this.c3C));
                    Assert.IsFalse(extent.Contains(this.c3D));
                    Assert.IsFalse(extent.Contains(this.c4A));
                    Assert.IsFalse(extent.Contains(this.c4B));
                    Assert.IsFalse(extent.Contains(this.c4C));
                    Assert.IsFalse(extent.Contains(this.c4D));

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C2one2one.AssociationType, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    Assert.IsFalse(extent.Contains(this.c1A));
                    Assert.IsFalse(extent.Contains(this.c1B));
                    Assert.IsFalse(extent.Contains(this.c1C));
                    Assert.IsFalse(extent.Contains(this.c1D));
                    Assert.IsFalse(extent.Contains(this.c2A));
                    Assert.IsTrue(extent.Contains(this.c2B));
                    Assert.IsTrue(extent.Contains(this.c2C));
                    Assert.IsTrue(extent.Contains(this.c2D));
                    Assert.IsFalse(extent.Contains(this.c3A));
                    Assert.IsFalse(extent.Contains(this.c3B));
                    Assert.IsFalse(extent.Contains(this.c3C));
                    Assert.IsFalse(extent.Contains(this.c3D));
                    Assert.IsFalse(extent.Contains(this.c4A));
                    Assert.IsFalse(extent.Contains(this.c4B));
                    Assert.IsFalse(extent.Contains(this.c4C));
                    Assert.IsFalse(extent.Contains(this.c4D));

                    inExtent = this.LocalExtent(C3Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C3Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C3Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C4Meta.ObjectType);
                    extent.Filter.AddContainedIn(C3Meta.C3C4one2one.AssociationType, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    Assert.IsFalse(extent.Contains(this.c1A));
                    Assert.IsFalse(extent.Contains(this.c1B));
                    Assert.IsFalse(extent.Contains(this.c1C));
                    Assert.IsFalse(extent.Contains(this.c1D));
                    Assert.IsFalse(extent.Contains(this.c2A));
                    Assert.IsFalse(extent.Contains(this.c2B));
                    Assert.IsFalse(extent.Contains(this.c2C));
                    Assert.IsFalse(extent.Contains(this.c2D));
                    Assert.IsFalse(extent.Contains(this.c3A));
                    Assert.IsFalse(extent.Contains(this.c3B));
                    Assert.IsFalse(extent.Contains(this.c3C));
                    Assert.IsFalse(extent.Contains(this.c3D));
                    Assert.IsFalse(extent.Contains(this.c4A));
                    Assert.IsTrue(extent.Contains(this.c4B));
                    Assert.IsTrue(extent.Contains(this.c4C));
                    Assert.IsTrue(extent.Contains(this.c4D));

                    // ContainedIn Extent over Interface
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C1one2one.AssociationType, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    Assert.IsFalse(extent.Contains(this.c1A));
                    Assert.IsTrue(extent.Contains(this.c1B));
                    Assert.IsTrue(extent.Contains(this.c1C));
                    Assert.IsTrue(extent.Contains(this.c1D));
                    Assert.IsFalse(extent.Contains(this.c2A));
                    Assert.IsFalse(extent.Contains(this.c2B));
                    Assert.IsFalse(extent.Contains(this.c2C));
                    Assert.IsFalse(extent.Contains(this.c2D));
                    Assert.IsFalse(extent.Contains(this.c3A));
                    Assert.IsFalse(extent.Contains(this.c3B));
                    Assert.IsFalse(extent.Contains(this.c3C));
                    Assert.IsFalse(extent.Contains(this.c3D));
                    Assert.IsFalse(extent.Contains(this.c4A));
                    Assert.IsFalse(extent.Contains(this.c4B));
                    Assert.IsFalse(extent.Contains(this.c4C));
                    Assert.IsFalse(extent.Contains(this.c4D));

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C2one2one.AssociationType, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    Assert.IsFalse(extent.Contains(this.c1A));
                    Assert.IsFalse(extent.Contains(this.c1B));
                    Assert.IsFalse(extent.Contains(this.c1C));
                    Assert.IsFalse(extent.Contains(this.c1D));
                    Assert.IsFalse(extent.Contains(this.c2A));
                    Assert.IsTrue(extent.Contains(this.c2B));
                    Assert.IsTrue(extent.Contains(this.c2C));
                    Assert.IsTrue(extent.Contains(this.c2D));
                    Assert.IsFalse(extent.Contains(this.c3A));
                    Assert.IsFalse(extent.Contains(this.c3B));
                    Assert.IsFalse(extent.Contains(this.c3C));
                    Assert.IsFalse(extent.Contains(this.c3D));
                    Assert.IsFalse(extent.Contains(this.c4A));
                    Assert.IsFalse(extent.Contains(this.c4B));
                    Assert.IsFalse(extent.Contains(this.c4C));
                    Assert.IsFalse(extent.Contains(this.c4D));

                    inExtent = this.LocalExtent(I34Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I34Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I34Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C4Meta.ObjectType);
                    extent.Filter.AddContainedIn(C3Meta.C3C4one2one.AssociationType, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    Assert.IsFalse(extent.Contains(this.c1A));
                    Assert.IsFalse(extent.Contains(this.c1B));
                    Assert.IsFalse(extent.Contains(this.c1C));
                    Assert.IsFalse(extent.Contains(this.c1D));
                    Assert.IsFalse(extent.Contains(this.c2A));
                    Assert.IsFalse(extent.Contains(this.c2B));
                    Assert.IsFalse(extent.Contains(this.c2C));
                    Assert.IsFalse(extent.Contains(this.c2D));
                    Assert.IsFalse(extent.Contains(this.c3A));
                    Assert.IsFalse(extent.Contains(this.c3B));
                    Assert.IsFalse(extent.Contains(this.c3C));
                    Assert.IsFalse(extent.Contains(this.c3D));
                    Assert.IsFalse(extent.Contains(this.c4A));
                    Assert.IsTrue(extent.Contains(this.c4B));
                    Assert.IsTrue(extent.Contains(this.c4C));
                    Assert.IsTrue(extent.Contains(this.c4D));

                    // RelationType from Interface to Class

                    // ContainedIn Extent over Class
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(I12Meta.I12C2one2one.AssociationType, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    Assert.IsFalse(extent.Contains(this.c1A));
                    Assert.IsFalse(extent.Contains(this.c1B));
                    Assert.IsFalse(extent.Contains(this.c1C));
                    Assert.IsFalse(extent.Contains(this.c1D));
                    Assert.IsFalse(extent.Contains(this.c2A));
                    Assert.IsTrue(extent.Contains(this.c2B));
                    Assert.IsTrue(extent.Contains(this.c2C));
                    Assert.IsTrue(extent.Contains(this.c2D));
                    Assert.IsFalse(extent.Contains(this.c3A));
                    Assert.IsFalse(extent.Contains(this.c3B));
                    Assert.IsFalse(extent.Contains(this.c3C));
                    Assert.IsFalse(extent.Contains(this.c3D));
                    Assert.IsFalse(extent.Contains(this.c4A));
                    Assert.IsFalse(extent.Contains(this.c4B));
                    Assert.IsFalse(extent.Contains(this.c4C));
                    Assert.IsFalse(extent.Contains(this.c4D));

                    // ContainedIn Extent over Interface
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(I12Meta.I12C2one2one.AssociationType, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);
                }
            }
        }

        [Test]
        public void AssociationOne2OneEquals()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1C1one2one.AssociationType, this.c1B);

                Assert.AreEqual(1, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1C2one2one.AssociationType, this.c1B);

                Assert.AreEqual(1, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                extent = this.LocalExtent(C4Meta.ObjectType);
                extent.Filter.AddEquals(C3Meta.C3C4one2one.AssociationType, this.c3B);

                Assert.AreEqual(1, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsTrue(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Interface
                extent = this.LocalExtent(I2Meta.ObjectType);
                extent.Filter.AddEquals(I1Meta.I1I2one2one.AssociationType, this.c1B);

                Assert.AreEqual(1, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234one2one.AssociationType, this.c1C);

                Assert.AreEqual(1, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Class - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddEquals(C3Meta.C3C2one2one.AssociationType, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(I12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(C3Meta.C3C2one2one.AssociationType, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(S12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(C3Meta.C3C2one2one.AssociationType, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void AssociationOne2OneExist()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddExists(C1Meta.C1C1one2one.AssociationType);

                Assert.AreEqual(3, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsTrue(extent.Contains(this.c1C));
                Assert.IsTrue(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddExists(C1Meta.C1C2one2one.AssociationType);

                Assert.AreEqual(3, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsTrue(extent.Contains(this.c2C));
                Assert.IsTrue(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                extent = this.LocalExtent(C4Meta.ObjectType);
                extent.Filter.AddExists(C3Meta.C3C4one2one.AssociationType);

                Assert.AreEqual(3, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsTrue(extent.Contains(this.c4B));
                Assert.IsTrue(extent.Contains(this.c4C));
                Assert.IsTrue(extent.Contains(this.c4D));

                // Interface
                extent = this.LocalExtent(I2Meta.ObjectType);
                extent.Filter.AddExists(I1Meta.I1I2one2one.AssociationType);

                Assert.AreEqual(3, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsTrue(extent.Contains(this.c2C));
                Assert.IsTrue(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddExists(S1234Meta.S1234one2one.AssociationType);

                Assert.AreEqual(9, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsTrue(extent.Contains(this.c1C));
                Assert.IsTrue(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsTrue(extent.Contains(this.c2C));
                Assert.IsTrue(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsTrue(extent.Contains(this.c3B));
                Assert.IsTrue(extent.Contains(this.c3C));
                Assert.IsTrue(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Class - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddExists(C3Meta.C3C2one2one.AssociationType);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(I12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddExists(C3Meta.C3C2one2one.AssociationType);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(S12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddExists(C3Meta.C3C2one2one.AssociationType);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void AssociationOne2OneInstanceof()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddInstanceof(C1Meta.C1C1one2one.AssociationType, C1Meta.ObjectType);

                Assert.AreEqual(3, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsTrue(extent.Contains(this.c1C));
                Assert.IsTrue(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddInstanceof(C1Meta.C1C2one2one.AssociationType, C1Meta.ObjectType);

                Assert.AreEqual(3, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsTrue(extent.Contains(this.c2C));
                Assert.IsTrue(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                extent = this.LocalExtent(C4Meta.ObjectType);
                extent.Filter.AddInstanceof(C3Meta.C3C4one2one.AssociationType, C3Meta.ObjectType);

                Assert.AreEqual(3, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsTrue(extent.Contains(this.c4B));
                Assert.IsTrue(extent.Contains(this.c4C));
                Assert.IsTrue(extent.Contains(this.c4D));

                // Interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddInstanceof(C1Meta.C1I12one2one.AssociationType, C1Meta.ObjectType);

                Assert.AreEqual(3, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsTrue(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddInstanceof(S1234Meta.S1234one2one.AssociationType, C1Meta.ObjectType);

                Assert.AreEqual(3, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsTrue(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Interface

                // Class
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddInstanceof(C1Meta.C1C1one2one.AssociationType, I1Meta.ObjectType);

                Assert.AreEqual(3, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsTrue(extent.Contains(this.c1C));
                Assert.IsTrue(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddInstanceof(C1Meta.C1C2one2one.AssociationType, I1Meta.ObjectType);

                Assert.AreEqual(3, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsTrue(extent.Contains(this.c2C));
                Assert.IsTrue(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                extent = this.LocalExtent(C4Meta.ObjectType);
                extent.Filter.AddInstanceof(C3Meta.C3C4one2one.AssociationType, I3Meta.ObjectType);

                Assert.AreEqual(3, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsTrue(extent.Contains(this.c4B));
                Assert.IsTrue(extent.Contains(this.c4C));
                Assert.IsTrue(extent.Contains(this.c4D));

                // Interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddInstanceof(C1Meta.C1I12one2one.AssociationType, I1Meta.ObjectType);

                Assert.AreEqual(3, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsTrue(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddInstanceof(S1234Meta.S1234one2one.AssociationType, S1234Meta.ObjectType);

                Assert.AreEqual(9, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsTrue(extent.Contains(this.c1C));
                Assert.IsTrue(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsTrue(extent.Contains(this.c2C));
                Assert.IsTrue(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsTrue(extent.Contains(this.c3B));
                Assert.IsTrue(extent.Contains(this.c3C));
                Assert.IsTrue(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // TODO: wrong relation
            }
        }

        [Test]
        public virtual void Combination()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Like and any
                var extent = this.LocalExtent(C1Meta.ObjectType);

                extent.Filter.AddLike(C1Meta.C1AllorsString, "%nada%");

                var any1 = extent.Filter.AddOr();
                any1.AddGreaterThan(C1Meta.C1AllorsInteger, 0);
                any1.AddLessThan(C1Meta.C1AllorsInteger, 3);

                var any2 = extent.Filter.AddOr();
                any2.AddGreaterThan(C1Meta.C1AllorsInteger, 0);
                any2.AddLessThan(C1Meta.C1AllorsInteger, 3);

                extent.ToArray(typeof(C1));

                // Role + Value for Shared Interface
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddExists(C1Meta.C1C1one2many);

                extent.Filter.AddExists(I12Meta.I12AllorsInteger);
                extent.Filter.AddNot().AddExists(I12Meta.I12AllorsInteger);
                extent.Filter.AddEquals(I12Meta.I12AllorsInteger, 0);
                extent.Filter.AddLessThan(I12Meta.I12AllorsInteger, 0);
                extent.Filter.AddGreaterThan(I12Meta.I12AllorsInteger, 0);
                extent.Filter.AddBetween(I12Meta.I12AllorsInteger, 0, 1);

                Assert.AreEqual(0, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Role In + Except
                var firstExtent = this.LocalExtent(C2Meta.ObjectType);
                firstExtent.Filter.AddLike(I12Meta.I12AllorsString, "Abra%");

                var secondExtent = this.LocalExtent(C2Meta.ObjectType);
                secondExtent.Filter.AddLike(I12Meta.I12AllorsString, "Abracadabra");

                var inExtent = this.Session.Except(firstExtent, secondExtent);

                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddContainedIn(C1Meta.C1C2one2many, inExtent);

                Assert.AreEqual(1, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // AssociationType In + Except
                firstExtent = this.LocalExtent(C1Meta.ObjectType);
                firstExtent.Filter.AddLike(I12Meta.I12AllorsString, "Abra%");

                secondExtent = this.LocalExtent(C1Meta.ObjectType);
                secondExtent.Filter.AddLike(I12Meta.I12AllorsString, "Abracadabra");

                inExtent = this.Session.Except(firstExtent, secondExtent);

                extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddContainedIn(C1Meta.C1C2one2many.AssociationType, inExtent);

                Assert.AreEqual(1, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsTrue(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Except + Union
                firstExtent = this.LocalExtent(C1Meta.ObjectType);
                firstExtent.Filter.AddNot().AddExists(C1Meta.C1AllorsString);

                secondExtent = this.LocalExtent(C1Meta.ObjectType);
                secondExtent.Filter.AddLike(C1Meta.C1AllorsString, "Abracadabra");

                var unionExtent = this.Session.Union(firstExtent, secondExtent);
                var topExtent = this.LocalExtent(C1Meta.ObjectType);

                extent = this.Session.Except(topExtent, unionExtent);

                Assert.AreEqual(1, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Except + Intersect
                firstExtent = this.LocalExtent(C1Meta.ObjectType);
                firstExtent.Filter.AddExists(C1Meta.C1AllorsString);

                secondExtent = this.LocalExtent(C1Meta.ObjectType);
                secondExtent.Filter.AddLike(C1Meta.C1AllorsString, "Abracadabra");

                var intersectExtent = this.Session.Intersect(firstExtent, secondExtent);
                topExtent = this.LocalExtent(C1Meta.ObjectType);

                extent = this.Session.Except(topExtent, intersectExtent);

                Assert.AreEqual(2, extent.Count);
                Assert.IsTrue(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Intersect + Intersect + Intersect
                firstExtent = this.Session.Intersect(
                    this.LocalExtent(C1Meta.ObjectType),
                    this.LocalExtent(C1Meta.ObjectType));
                secondExtent = this.Session.Intersect(
                    this.LocalExtent(C1Meta.ObjectType),
                    this.LocalExtent(C1Meta.ObjectType));

                extent = this.Session.Intersect(firstExtent, secondExtent);

                Assert.AreEqual(4, extent.Count);
                Assert.IsTrue(extent.Contains(this.c1A));
                Assert.IsTrue(extent.Contains(this.c1B));
                Assert.IsTrue(extent.Contains(this.c1C));
                Assert.IsTrue(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));

                // Except + Intersect + Intersect
                firstExtent = this.Session.Intersect(
                    this.LocalExtent(C1Meta.ObjectType),
                    this.LocalExtent(C1Meta.ObjectType));
                secondExtent = this.Session.Intersect(
                    this.LocalExtent(C1Meta.ObjectType),
                    this.LocalExtent(C1Meta.ObjectType));

                extent = this.Session.Except(firstExtent, secondExtent);

                Assert.AreEqual(0, extent.Count);
                Assert.IsFalse(extent.Contains(this.c1A));
                Assert.IsFalse(extent.Contains(this.c1B));
                Assert.IsFalse(extent.Contains(this.c1C));
                Assert.IsFalse(extent.Contains(this.c1D));
                Assert.IsFalse(extent.Contains(this.c2A));
                Assert.IsFalse(extent.Contains(this.c2B));
                Assert.IsFalse(extent.Contains(this.c2C));
                Assert.IsFalse(extent.Contains(this.c2D));
                Assert.IsFalse(extent.Contains(this.c3A));
                Assert.IsFalse(extent.Contains(this.c3B));
                Assert.IsFalse(extent.Contains(this.c3C));
                Assert.IsFalse(extent.Contains(this.c3D));
                Assert.IsFalse(extent.Contains(this.c4A));
                Assert.IsFalse(extent.Contains(this.c4B));
                Assert.IsFalse(extent.Contains(this.c4C));
                Assert.IsFalse(extent.Contains(this.c4D));
            }
        }
        
        [Test]
        public void NoConcreteClass()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                var extent = this.LocalExtent(InterfaceWithoutConcreteClassMeta.ObjectType);

                Assert.AreEqual(0, extent.Count);
            }
        }

        [Test]
        public void Equals()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // class
                var extent = this.LocalExtent(C1Meta.ObjectType);

                extent.Filter.AddEquals(this.c1A);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent.Filter.AddEquals(this.c1B);

                Assert.AreEqual(0, extent.Count);

                // interface
                extent = this.LocalExtent(I1Meta.ObjectType);

                extent.Filter.AddEquals(this.c1A);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent.Filter.AddEquals(this.c1B);

                Assert.AreEqual(0, extent.Count);

                // shared interface
                extent = this.LocalExtent(I12Meta.ObjectType);

                extent.Filter.AddEquals(this.c1A);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent.Filter.AddEquals(this.c1B);

                Assert.AreEqual(0, extent.Count);
            }
        }

        [Test]
        public void NotAndEquals()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                var not = extent.Filter.AddNot();
                var and = not.AddAnd();
                and.AddEquals(this.c1A);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                and.AddEquals(this.c1B);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // interface
                extent = this.LocalExtent(I1Meta.ObjectType);
                not = extent.Filter.AddNot();
                and = not.AddAnd();
                and.AddEquals(this.c1A);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                and.AddEquals(this.c1B);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // shared interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                not = extent.Filter.AddNot();
                and = not.AddAnd();
                and.AddEquals(this.c1A);

                Assert.AreEqual(7, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, true, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                and.AddEquals(this.c1B);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, true, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);
            }
        }

        [Test]
        public void OrEquals()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                var or = extent.Filter.AddOr();
                or.AddEquals(this.c1A);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                or.AddEquals(this.c1B);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, true, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // interface
                extent = this.LocalExtent(I1Meta.ObjectType);
                or = extent.Filter.AddOr();
                or.AddEquals(this.c1A);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                or.AddEquals(this.c1B);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, true, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // shared interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                or = extent.Filter.AddOr();
                or.AddEquals(this.c1A);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                or.AddEquals(this.c2B);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);
            }
        }

        [Test]
        public void NotOrEquals()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                var not = extent.Filter.AddNot();
                var or = not.AddOr();
                or.AddEquals(this.c1A);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                or.AddEquals(this.c1B);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // interface
                extent = this.LocalExtent(I1Meta.ObjectType);
                not = extent.Filter.AddNot();
                or = not.AddOr();
                or.AddEquals(this.c1A);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                or.AddEquals(this.c1B);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // shared interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                not = extent.Filter.AddNot();
                or = not.AddOr();
                or.AddEquals(this.c1A);

                Assert.AreEqual(7, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, true, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                or.AddEquals(this.c2B);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, true, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);
            }
        }

        [Test]
        public virtual void Except()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // class
                var firstExtent = this.LocalExtent(C1Meta.ObjectType);

                var secondExtent = this.LocalExtent(C1Meta.ObjectType);
                secondExtent.Filter.AddLike(C1Meta.C1AllorsString, "Abracadabra");

                var extent = this.Session.Except(firstExtent, secondExtent);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, true, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // interface
                firstExtent = this.LocalExtent(I12Meta.ObjectType);
                firstExtent.Filter.AddLike(I12Meta.I12AllorsString, "Abra%");

                secondExtent = this.LocalExtent(I12Meta.ObjectType);
                secondExtent.Filter.AddLike(I12Meta.I12AllorsString, "Abracadabra");

                extent = this.Session.Except(firstExtent, secondExtent);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Different Classes
                firstExtent = this.LocalExtent(C1Meta.ObjectType);
                secondExtent = this.LocalExtent(C2Meta.ObjectType);

                var exceptionThrown = false;
                try
                {
                    extent = this.Session.Except(firstExtent, secondExtent);
                }
                catch (ArgumentException e)
                {
                    exceptionThrown = true;
                }

                Assert.IsTrue(exceptionThrown);
            }
        }

        [Test]
        public void Grow()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddExists(C1Meta.C1AllorsString);
                Assert.AreEqual(3, extent.Count);
                extent.Filter.AddLike(C1Meta.C1AllorsString, "Abra");
                Assert.AreEqual(1, extent.Count);

                // TODO: all possible combinations
            }
        }

        [Test]
        public void InstanceOf()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class + Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddInstanceof(C1Meta.ObjectType);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class + Interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddInstanceof(C1Meta.ObjectType);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class + Shared Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddInstanceof(C1Meta.ObjectType);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Inteface + Class
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddInstanceof(I1Meta.ObjectType);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface + Interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddInstanceof(I1Meta.ObjectType);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddInstanceof(I12Meta.ObjectType);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, true, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface + Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddInstanceof(I1Meta.ObjectType);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddInstanceof(I12Meta.ObjectType);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, true, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddInstanceof(S1234Meta.ObjectType);

                Assert.AreEqual(16, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, true, true, true, true);
                this.AssertC3(extent, true, true, true, true);
                this.AssertC4(extent, true, true, true, true);
            }
        }

        [Test]
        public virtual void Intersect()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // class
                var firstExtent = this.LocalExtent(C1Meta.ObjectType);

                var secondExtent = this.LocalExtent(C1Meta.ObjectType);
                secondExtent.Filter.AddLike(C1Meta.C1AllorsString, "Abracadabra");

                var extent = this.Session.Intersect(firstExtent, secondExtent);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Different Classes
                firstExtent = this.LocalExtent(C1Meta.ObjectType);
                secondExtent = this.LocalExtent(C2Meta.ObjectType);

                var exceptionThrown = false;
                try
                {
                    this.Session.Intersect(firstExtent, secondExtent);
                }
                catch (ArgumentException)
                {
                    exceptionThrown = true;
                }

                Assert.IsTrue(exceptionThrown);
            }
        }

        [Test]
        public void Naming()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                {
                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddEquals(S1234Meta.ClassName, "c1");
                    extent.Filter.AddContains(C1Meta.C1C3one2many, this.c3B);
                    extent.AddSort(S1234Meta.ClassName);
                    extent.ToArray(typeof(C1));
                }
            }
        }

        [Test]
        public void NotAnd()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                var none = extent.Filter.AddNot().AddAnd();
                none.AddGreaterThan(C1Meta.C1AllorsInteger, 0);
                none.AddLessThan(C1Meta.C1AllorsInteger, 2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                (extent = this.LocalExtent(I12Meta.ObjectType)).Filter.AddNot()
                    .AddAnd()
                    .AddGreaterThan(I12Meta.I12AllorsInteger, 0)
                    .AddLessThan(I12Meta.I12AllorsInteger, 2);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                (extent = this.LocalExtent(S1234Meta.ObjectType)).Filter.AddNot()
                    .AddAnd()
                    .AddGreaterThan(S1234Meta.S1234AllorsInteger, 0)
                    .AddLessThan(S1234Meta.S1234AllorsInteger, 2);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Class
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddAnd();

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);
            }
        }

        [Test]
        public void NotAssociationMany2ManyContainedIn()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                foreach (var useOperator in this.UseOperator)
                {
                    // Extent over C2

                    // RelationType from C1 to C2

                    // ContainedIn Extent over Class
                    // Empty
                    var inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2many2many.AssociationType, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2many2many.AssociationType, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2many2many.AssociationType, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2many2many.AssociationType, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2many2many.AssociationType, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2many2many.AssociationType, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from C1 to I12

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12many2many.AssociationType, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12many2many.AssociationType, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12many2many.AssociationType, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12many2many.AssociationType, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12many2many.AssociationType, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12many2many.AssociationType, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);
                }
            }
        }

        [Test]
        public void NotAssociationMany2ManyExist()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddNot().AddExists(C1Meta.C1C2many2many.AssociationType);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(I2Meta.ObjectType);
                extent.Filter.AddNot().AddExists(I1Meta.I1I2many2many.AssociationType);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddExists(S1234Meta.S1234many2many.AssociationType);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, true, false, false, false);
                this.AssertC4(extent, true, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(C2Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(C1Meta.C1C1many2many.AssociationType);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(I12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(I12Meta.I12I34many2many.AssociationType);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(S12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(S1234Meta.S1234many2many.AssociationType);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void NotAssociationMany2OneContainedIn()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                foreach (var useOperator in this.UseOperator)
                {
                    // Extent over Class

                    // RelationType from Class to Class

                    // ContainedIn Extent over Class
                    // Empty
                    var inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2many2one.AssociationType, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2many2one.AssociationType, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2many2one.AssociationType, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2many2one.AssociationType, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2many2one.AssociationType, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2many2one.AssociationType, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from Class to Interface

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12many2one.AssociationType, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12many2one.AssociationType, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, false, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12many2one.AssociationType, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12many2one.AssociationType, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12many2one.AssociationType, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, false, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12many2one.AssociationType, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);
                }
            }
        }

        [Test]
        public void NotAssociationOne2ManyContainedIn()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                foreach (var useOperator in this.UseOperator)
                {
                    // Extent over C2

                    // RelationType from C1 to C2

                    // ContainedIn Extent over Class
                    // Empty
                    var inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2one2many.AssociationType, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2one2many.AssociationType, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2one2many.AssociationType, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2one2many.AssociationType, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2one2many.AssociationType, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2one2many.AssociationType, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from C1 to I12

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12one2many.AssociationType, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12one2many.AssociationType, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12one2many.AssociationType, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12one2many.AssociationType, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12one2many.AssociationType, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12one2many.AssociationType, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);
                }
            }
        }

        [Test]
        public void NotAssociationOne2ManyEquals()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(C1Meta.C1C1one2many.AssociationType, this.c1B);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, true, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(C1Meta.C1C1one2many.AssociationType, this.c1C);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, true, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(C1Meta.C1C2one2many.AssociationType, this.c1B);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(C1Meta.C1C2one2many.AssociationType, this.c1C);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(I2Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I1Meta.I1I2one2many.AssociationType, this.c1B);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(I2Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I1Meta.I1I2one2many.AssociationType, this.c1C);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(S1234Meta.S1234one2many.AssociationType, this.c1B);

                Assert.AreEqual(15, extent.Count);
                this.AssertC1(extent, true, false, true, true);
                this.AssertC2(extent, true, true, true, true);
                this.AssertC3(extent, true, true, true, true);
                this.AssertC4(extent, true, true, true, true);

                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(S1234Meta.S1234one2many.AssociationType, this.c3C);

                Assert.AreEqual(14, extent.Count);
                this.AssertC1(extent, true, true, false, false);
                this.AssertC2(extent, true, true, true, true);
                this.AssertC3(extent, true, true, true, true);
                this.AssertC4(extent, true, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(C3Meta.C3C2one2many.AssociationType, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(I12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(C3Meta.C3C2one2many.AssociationType, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(S12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(C3Meta.C3C2one2many.AssociationType, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void NotAssociationOne2ManyExist()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddNot().AddExists(C1Meta.C1C2one2many.AssociationType);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(I2Meta.ObjectType);
                extent.Filter.AddNot().AddExists(I1Meta.I1I2one2many.AssociationType);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddExists(S1234Meta.S1234one2many.AssociationType);

                Assert.AreEqual(13, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, true, true, true, true);
                this.AssertC3(extent, true, true, true, true);
                this.AssertC4(extent, true, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(C3Meta.C3C2one2many.AssociationType);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(I12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(C3Meta.C3C2one2many.AssociationType);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(S1234Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(C3Meta.C3C2one2many.AssociationType);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void NotAssociationOne2OneContainedIn()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                foreach (var useOperator in this.UseOperator)
                {
                    // Extent over Class

                    // RelationType from C1 to C1

                    // ContainedIn Extent over Class
                    var inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C1one2one.AssociationType, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C1one2one.AssociationType, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from C1 to C2

                    // ContainedIn Extent over Class
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2one2one.AssociationType, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Class
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2one2one.AssociationType, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from C3 to C4

                    // ContainedIn Extent over Class
                    inExtent = this.LocalExtent(C3Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C3Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C3Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C4Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C3Meta.C3C4one2one.AssociationType, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, true, false, false, false);

                    // ContainedIn Extent over Interface
                    inExtent = this.LocalExtent(I34Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I34Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I34Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C4Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C3Meta.C3C4one2one.AssociationType, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, true, false, false, false);

                    // RelationType from I12 to C2

                    // ContainedIn Extent over Class
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(I12Meta.I12C2one2one.AssociationType, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(I12Meta.I12C2one2one.AssociationType, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Extent over Interface

                    // RelationType from C1 to I12

                    // ContainedIn Extent over Class
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(I12Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12one2one.AssociationType, inExtent);

                    Assert.AreEqual(5, extent.Count);
                    this.AssertC1(extent, true, false, true, true);
                    this.AssertC2(extent, true, false, false, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(I12Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12one2one.AssociationType, inExtent);

                    Assert.AreEqual(5, extent.Count);
                    this.AssertC1(extent, true, false, true, true);
                    this.AssertC2(extent, true, false, false, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);
                }
            }
        }

        [Test]
        public void NotAssociationOne2OneEquals()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(C1Meta.C1C1one2one.AssociationType, this.c1B);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, true, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(C1Meta.C1C2one2one.AssociationType, this.c1B);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C4Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(C3Meta.C3C4one2one.AssociationType, this.c3B);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, true, false, true, true);

                // Interface
                extent = this.LocalExtent(I2Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I1Meta.I1I2one2one.AssociationType, this.c1B);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(S1234Meta.S1234one2one.AssociationType, this.c1C);

                Assert.AreEqual(15, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, true, false, true, true);
                this.AssertC3(extent, true, true, true, true);
                this.AssertC4(extent, true, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(C3Meta.C3C2one2one.AssociationType, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(I12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(C3Meta.C3C2one2one.AssociationType, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(S12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(C3Meta.C3C2one2one.AssociationType, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void NotAssociationOne2OneExist()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddExists(C1Meta.C1C1one2one.AssociationType);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddNot().AddExists(C1Meta.C1C2one2one.AssociationType);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddNot().AddExists(C1Meta.C1C2one2one.AssociationType);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C4Meta.ObjectType);
                extent.Filter.AddNot().AddExists(C3Meta.C3C4one2one.AssociationType);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, true, false, false, false);

                // Interface
                extent = this.LocalExtent(I2Meta.ObjectType);
                extent.Filter.AddNot().AddExists(I1Meta.I1I2one2one.AssociationType);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddExists(S1234Meta.S1234one2one.AssociationType);

                Assert.AreEqual(7, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, true, false, false, false);
                this.AssertC4(extent, true, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(C3Meta.C3C2one2one.AssociationType);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(I12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(C3Meta.C3C2one2one.AssociationType);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(S1234Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(C3Meta.C3C2one2one.AssociationType);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void NotAssociationOne2OneInstanceof()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddInstanceof(C1Meta.C1C1one2one.AssociationType, C1Meta.ObjectType);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddNot().AddInstanceof(C1Meta.C1C2one2one.AssociationType, C1Meta.ObjectType);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C4Meta.ObjectType);
                extent.Filter.AddNot().AddInstanceof(C3Meta.C3C4one2one.AssociationType, C3Meta.ObjectType);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, true, false, false, false);

                // Interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddInstanceof(C1Meta.C1I12one2one.AssociationType, C1Meta.ObjectType);

                Assert.AreEqual(5, extent.Count);
                this.AssertC1(extent, true, false, true, true);
                this.AssertC2(extent, true, false, false, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddInstanceof(S1234Meta.S1234one2one.AssociationType, C1Meta.ObjectType);

                Assert.AreEqual(13, extent.Count);
                this.AssertC1(extent, true, false, true, true);
                this.AssertC2(extent, true, false, true, true);
                this.AssertC3(extent, true, false, true, true);
                this.AssertC4(extent, true, true, true, true);

                // Interface

                // Class
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddInstanceof(C1Meta.C1C1one2one.AssociationType, I1Meta.ObjectType);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddNot().AddInstanceof(C1Meta.C1C2one2one.AssociationType, I1Meta.ObjectType);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C4Meta.ObjectType);
                extent.Filter.AddNot().AddInstanceof(C3Meta.C3C4one2one.AssociationType, I3Meta.ObjectType);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, true, false, false, false);

                // Interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddInstanceof(C1Meta.C1I12one2one.AssociationType, I1Meta.ObjectType);

                Assert.AreEqual(5, extent.Count);
                this.AssertC1(extent, true, false, true, true);
                this.AssertC2(extent, true, false, false, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddInstanceof(S1234Meta.S1234one2one.AssociationType, S1234Meta.ObjectType);

                Assert.AreEqual(7, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, true, false, false, false);
                this.AssertC4(extent, true, true, true, true);

                // TODO: wrong relation
            }
        }

        [Test]
        public void NotOr()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                var none = extent.Filter.AddNot().AddOr();
                none.AddGreaterThan(C1Meta.C1AllorsInteger, 1);
                none.AddLessThan(C1Meta.C1AllorsInteger, 1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                (extent = this.LocalExtent(I12Meta.ObjectType)).Filter.AddNot()
                    .AddOr()
                    .AddGreaterThan(I12Meta.I12AllorsInteger, 1)
                    .AddLessThan(I12Meta.I12AllorsInteger, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                (extent = this.LocalExtent(S1234Meta.ObjectType)).Filter.AddNot()
                    .AddOr()
                    .AddGreaterThan(S1234Meta.S1234AllorsInteger, 1)
                    .AddLessThan(S1234Meta.S1234AllorsInteger, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Class
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddOr();

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);
            }
        }

        [Test]
        public void NotRoleIntegerBetweenValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Between -10 and 0
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddBetween(C1Meta.C1AllorsInteger, -10, 0);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddBetween(C1Meta.C1AllorsInteger, 0, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddBetween(C1Meta.C1AllorsInteger, 1, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 3 and 10
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddBetween(C1Meta.C1AllorsInteger, 3, 10);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Between -10 and 0
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddBetween(I12Meta.I12AllorsInteger, -10, 0);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddBetween(I12Meta.I12AllorsInteger, 0, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddBetween(I12Meta.I12AllorsInteger, 1, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 3 and 10
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddBetween(I12Meta.I12AllorsInteger, 3, 10);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Between -10 and 0
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddBetween(S1234Meta.S1234AllorsInteger, -10, 0);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Between 0 and 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddBetween(S1234Meta.S1234AllorsInteger, 0, 1);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Between 1 and 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddBetween(S1234Meta.S1234AllorsInteger, 1, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 3 and 10
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddBetween(S1234Meta.S1234AllorsInteger, 3, 10);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Class - Wrong RelationType

                // Between -10 and 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddBetween(C2Meta.C2AllorsInteger, -10, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 0 and 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddBetween(C2Meta.C2AllorsInteger, 0, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 1 and 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddBetween(C2Meta.C2AllorsInteger, 1, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 3 and 10
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddBetween(C2Meta.C2AllorsInteger, 3, 10);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void NotRoleIntegerLessThanValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Less Than 1
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddLessThan(C1Meta.C1AllorsInteger, 1);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddLessThan(C1Meta.C1AllorsInteger, 2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddLessThan(C1Meta.C1AllorsInteger, 3);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Less Than 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddLessThan(I12Meta.I12AllorsInteger, 1);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddLessThan(I12Meta.I12AllorsInteger, 2);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 3
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddLessThan(I12Meta.I12AllorsInteger, 3);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Less Than 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddLessThan(S1234Meta.S1234AllorsInteger, 1);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Less Than 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddLessThan(S1234Meta.S1234AllorsInteger, 2);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Less Than 3
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddLessThan(S1234Meta.S1234AllorsInteger, 3);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddLessThan(C2Meta.C2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLessThan(C2Meta.C2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLessThan(C2Meta.C2AllorsInteger, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLessThan(I2Meta.I2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLessThan(I2Meta.I2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLessThan(I2Meta.I2AllorsInteger, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLessThan(S2Meta.S2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLessThan(S2Meta.S2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLessThan(S2Meta.S2AllorsInteger, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void NotRoleIntegerGreaterThanValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Greater Than 0
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddGreaterThan(C1Meta.C1AllorsInteger, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddGreaterThan(C1Meta.C1AllorsInteger, 1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddGreaterThan(C1Meta.C1AllorsInteger, 2);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Greater Than 0
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddGreaterThan(I12Meta.I12AllorsInteger, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddGreaterThan(I12Meta.I12AllorsInteger, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddGreaterThan(I12Meta.I12AllorsInteger, 2);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Greater Than 0
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddGreaterThan(S1234Meta.S1234AllorsInteger, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddGreaterThan(S1234Meta.S1234AllorsInteger, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Greater Than 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddGreaterThan(S1234Meta.S1234AllorsInteger, 2);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Class - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddGreaterThan(C2Meta.C2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddGreaterThan(C2Meta.C2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddGreaterThan(C2Meta.C2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddGreaterThan(I2Meta.I2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddGreaterThan(I2Meta.I2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddGreaterThan(I2Meta.I2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddGreaterThan(I2Meta.I2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddGreaterThan(I2Meta.I2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddGreaterThan(I2Meta.I2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void NotRoleIntegerExist()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddExists(C1Meta.C1AllorsInteger);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddExists(I12Meta.I12AllorsInteger);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddExists(S1234Meta.S1234AllorsInteger);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, true, false, false, false);
                this.AssertC4(extent, true, false, false, false);

                // Class - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(C2Meta.C2AllorsInteger);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(I2Meta.I2AllorsInteger);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(S2Meta.S2AllorsInteger);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void NotRoleMany2ManyContainedIn()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                foreach (var useOperator in this.UseOperator)
                {
                    // Extent over Class

                    // RelationType from C1 to C2
                    // ContainedIn Extent over Class
                    // Empty
                    var inExtent = this.LocalExtent(C2Meta.ObjectType);
                    inExtent.Filter.AddEquals(C2Meta.C2AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C2Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C2Meta.C2AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(C2Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C2Meta.C2AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2many2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, true, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(C2Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C2Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C2Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(C2Meta.ObjectType);
                    inExtent.Filter.AddEquals(C2Meta.C2AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C2Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C2Meta.C2AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(C2Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C2Meta.C2AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2many2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, true, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from C1 to C1

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C1many2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, true, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C1many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C1many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C1many2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, true, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C1many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C1many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from C1 to I12

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12many2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, true, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12many2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, true, false, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12many2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, true, false, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12many2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, true, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12many2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, true, false, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);
                }
            }
        }

        [Test]
        public void NotRoleMany2ManyExist()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddExists(C1Meta.C1C2many2many);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddExists(I12Meta.I12C2many2many);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddExists(S1234Meta.S1234C2many2many);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, true, true, true, true);
                this.AssertC4(extent, true, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(C3Meta.C3C2many2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(I12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(C3Meta.C3C2many2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(S12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(C3Meta.C3C2many2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void NotRoleOne2ManyContainedIn()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                foreach (var useOperator in this.UseOperator)
                {
                    // Extent over Class
                    // RelationType from Class to Class

                    // ContainedIn Extent over Class
                    // Emtpy Extent
                    var inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C1one2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, true, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full Extent
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C1one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, true, false, false, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(C2Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C2Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C2Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, true, false, false, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(C4Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C4Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C4Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C3Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C3Meta.C3C4one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, true, false, false, true);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Emtpy Extent
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C1one2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, true, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full Extent
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C1one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, true, false, false, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, true, false, false, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(I34Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I34Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I34Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C3Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C3Meta.C3C4one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, true, false, false, true);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from Class to Interface

                    // ContainedIn Extent over Class
                    inExtent = this.LocalExtent(C2Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C2Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C2Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(I12Meta.I12C2one2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, true, false, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(I12Meta.I12C2one2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, true, false, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);
                }
            }
        }

        [Test]
        public void NotRoleOne2ManyContains()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddContains(C1Meta.C1C2one2many, this.c2C);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, true, true, false, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddContains(C1Meta.C1I12one2many, this.c2C);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, true, true, false, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddContains(S1234Meta.S1234one2many, this.c1B);

                Assert.AreEqual(15, extent.Count);
                this.AssertC1(extent, true, false, true, true);
                this.AssertC2(extent, true, true, true, true);
                this.AssertC3(extent, true, true, true, true);
                this.AssertC4(extent, true, true, true, true);

                // TODO: wrong relation
            }
        }

        [Test]
        public void NotRoleOne2ManyExist()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddExists(C1Meta.C1C2one2many);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, true, false, false, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddExists(I12Meta.I12C2one2many);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, true, false, true, true);
                this.AssertC2(extent, true, true, false, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddExists(S1234Meta.S1234C2one2many);

                Assert.AreEqual(14, extent.Count);
                this.AssertC1(extent, true, false, true, true);
                this.AssertC2(extent, true, true, true, true);
                this.AssertC3(extent, true, true, false, true);
                this.AssertC4(extent, true, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(C3Meta.C3C2one2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(I12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(C3Meta.C3C2one2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(S12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(C3Meta.C3C2one2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void NotRoleOne2OneContainedIn()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                foreach (var useOperator in this.UseOperator)
                {
                    // Extent over Class

                    // RelationType from Class to Class

                    // ContainedIn Extent over Class
                    var inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C1one2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(C2Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C2Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C2Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2one2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(C4Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C4Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C4Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C3Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C3Meta.C3C4one2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, true, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C1one2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1C2one2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(I34Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I34Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I34Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C3Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C3Meta.C3C4one2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, true, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from Class to Interface

                    // ContainedIn Extent over Class
                    inExtent = this.LocalExtent(C2Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C2Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C2Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12one2one, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, true, true, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddNot().AddContainedIn(C1Meta.C1I12one2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);
                }
            }
        }

        [Test]
        public void NotRoleOne2OneEquals()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(C1Meta.C1C1one2one, this.c1B);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, true, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(C1Meta.C1C2one2one, this.c2B);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, true, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I12Meta.I12C2one2one, this.c2A);

                Assert.AreEqual(7, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(S1234Meta.S1234C2one2one, this.c2A);

                Assert.AreEqual(15, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, true, true, true, true);
                this.AssertC4(extent, true, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddEquals(C3Meta.C3C2one2one, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(I12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(C3Meta.C3C2one2one, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(S12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(C3Meta.C3C2one2one, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void NotRoleOne2OneExist()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddExists(C1Meta.C1C2one2one);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddExists(C1Meta.C1C2one2one);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddExists(I12Meta.I12C2one2one);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddExists(S1234Meta.S1234C2one2one);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, true, true, true, true);
                this.AssertC4(extent, true, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(C3Meta.C3C2one2one);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(I12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(C3Meta.C3C2one2one);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(S12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(C3Meta.C3C2one2one);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void NotRoleOne2OneInstanceof()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddInstanceof(C1Meta.C1C1one2one, C1Meta.ObjectType);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddInstanceof(C1Meta.C1C2one2one, C2Meta.ObjectType);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddInstanceof(C1Meta.C1I12one2one, C2Meta.ObjectType);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, true, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddInstanceof(S1234Meta.S1234one2one, C2Meta.ObjectType);

                Assert.AreEqual(13, extent.Count);
                this.AssertC1(extent, true, true, false, true);
                this.AssertC2(extent, true, true, false, true);
                this.AssertC3(extent, true, true, false, true);
                this.AssertC4(extent, true, true, true, true);

                // Interface

                // Class
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddInstanceof(C1Meta.C1C2one2one, I2Meta.ObjectType);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddInstanceof(C1Meta.C1I12one2one, I2Meta.ObjectType);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, true, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddInstanceof(C1Meta.C1I12one2one, I12Meta.ObjectType);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddInstanceof(S1234Meta.S1234one2one, S1234Meta.ObjectType);

                Assert.AreEqual(7, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, true, false, false, false);
                this.AssertC4(extent, true, true, true, true);

                // TODO: wrong relation
            }
        }

        [Test]
        public void NotRoleStringEqualsValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Equal ""
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(C1Meta.C1AllorsString, string.Empty);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C3Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(C3Meta.C3AllorsString, string.Empty);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abra"
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(C1Meta.C1AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C3Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(C3Meta.C3AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abracadabra"
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(C1Meta.C1AllorsString, "Abracadabra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C3Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(C3Meta.C3AllorsString, "Abracadabra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Equal ""
                extent = this.LocalExtent(I1Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I1Meta.I1AllorsString, string.Empty);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(I3Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I3Meta.I3AllorsString, string.Empty);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abra"
                extent = this.LocalExtent(I1Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I1Meta.I1AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(I3Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I3Meta.I3AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abracadabra"
                extent = this.LocalExtent(I1Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I1Meta.I1AllorsString, "Abracadabra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(I3Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I3Meta.I3AllorsString, "Abracadabra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Shared Interface

                // Equal ""
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I12Meta.I12AllorsString, string.Empty);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(I34Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I34Meta.I34AllorsString, string.Empty);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Equal "Abra"
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I12Meta.I12AllorsString, "Abra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I12Meta.I12AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(I23Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I23Meta.I23AllorsString, "Abra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I23Meta.I23AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C3Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I23Meta.I23AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(I34Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I34Meta.I34AllorsString, "Abra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                extent = this.LocalExtent(C3Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I34Meta.I34AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abracadabra"
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I12Meta.I12AllorsString, "Abracadabra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I12Meta.I12AllorsString, "Abracadabra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(I34Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(I34Meta.I34AllorsString, "Abracadabra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Super Interface

                // Equal ""
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(S1234Meta.S1234AllorsString, string.Empty);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Equal "Abra"
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(S1234Meta.S1234AllorsString, "Abra");

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Equal "Abracadabra"
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddEquals(S1234Meta.S1234AllorsString, "Abracadabra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Class - Wrong RelationType

                // Equal ""
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(C2Meta.C2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(C2Meta.C2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abracadabra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(C2Meta.C2AllorsString, "Abracadabra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Equal ""
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(I2Meta.I2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(I2Meta.I2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abracadabra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(I2Meta.I2AllorsString, "Abracadabra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Equal ""
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(S2Meta.S2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(S2Meta.S2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abracadabra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(S2Meta.S2AllorsString, "Abracadabra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void NotRoleStringExist()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddExists(C1Meta.C1AllorsString);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddExists(I12Meta.I12AllorsString);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddExists(S1234Meta.S1234AllorsString);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, true, false, false, false);
                this.AssertC4(extent, true, false, false, false);

                // Class - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(C2Meta.C2AllorsString);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(I2Meta.I2AllorsString);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(S2Meta.S2AllorsString);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void NotRoleStringLike()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Like ""
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddLike(C1Meta.C1AllorsString, string.Empty);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "Abra"
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddLike(C1Meta.C1AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "Abracadabra"
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddLike(C1Meta.C1AllorsString, "Abracadabra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "notfound"
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddLike(C1Meta.C1AllorsString, "notfound");

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "%ra%"
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddLike(C1Meta.C1AllorsString, "%ra%");

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "%bra"
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddLike(C1Meta.C1AllorsString, "%bra");

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "%cadabra"
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddNot().AddLike(C1Meta.C1AllorsString, "%cadabra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Like ""
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddLike(I12Meta.I12AllorsString, string.Empty);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "Abra"
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddLike(I12Meta.I12AllorsString, "Abra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "Abracadabra"
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddNot().AddLike(I12Meta.I12AllorsString, "Abracadabra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Like ""
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddLike(S1234Meta.S1234AllorsString, string.Empty);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Like "Abra"
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddLike(S1234Meta.S1234AllorsString, "Abra");

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Like "Abracadabra"
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddNot().AddLike(S1234Meta.S1234AllorsString, "Abracadabra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC1(extent, false, true, false, false);

                // Class - Wrong RelationType

                // Like ""
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddLike(C2Meta.C2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLike(C2Meta.C2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abracadabra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLike(C2Meta.C2AllorsString, "Abracadabra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Like ""
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLike(I2Meta.I2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLike(I2Meta.I2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abracadabra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLike(I2Meta.I2AllorsString, "Abracadabra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Like ""
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLike(S2Meta.S2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLike(S2Meta.S2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abracadabra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLike(S2Meta.S2AllorsString, "Abracadabra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public virtual void Operation()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                var firstExtent = this.LocalExtent(C1Meta.ObjectType);
                firstExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");

                var secondExtent = this.LocalExtent(C1Meta.ObjectType);
                secondExtent.Filter.AddLike(C1Meta.C1AllorsString, "Abracadabra");

                var extent = this.Session.Union(firstExtent, secondExtent);

                Assert.AreEqual(3, extent.Count);

                firstExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Oops");

                Assert.AreEqual(2, extent.Count);

                secondExtent.Filter.AddEquals(C1Meta.C1AllorsString, "I did it again");

                Assert.AreEqual(0, extent.Count);

                // TODO: all possible combinations
            }
        }

        [Test]
        public void Optimizations()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Dangling empty And behind Or
                var extent = this.LocalExtent(C1Meta.ObjectType);
                var or = extent.Filter.AddOr();

                or.AddAnd();
                or.AddLessThan(C1Meta.C1AllorsInteger, 2);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Dangling empty Or behind Or
                extent = this.LocalExtent(C1Meta.ObjectType);
                or = extent.Filter.AddOr();

                or.AddOr();
                or.AddLessThan(C1Meta.C1AllorsInteger, 2);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Dangling empty Not behind Or
                extent = this.LocalExtent(C1Meta.ObjectType);
                or = extent.Filter.AddOr();

                or.AddNot();
                or.AddLessThan(C1Meta.C1AllorsInteger, 2);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Dangling empty And behind And
                extent = this.LocalExtent(C1Meta.ObjectType);
                var and = extent.Filter.AddAnd();

                and.AddAnd();
                and.AddLessThan(C1Meta.C1AllorsInteger, 2);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Dangling empty Or behind And
                extent = this.LocalExtent(C1Meta.ObjectType);
                and = extent.Filter.AddAnd();

                and.AddOr();
                and.AddLessThan(C1Meta.C1AllorsInteger, 2);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Dangling empty Not behind And
                extent = this.LocalExtent(C1Meta.ObjectType);
                and = extent.Filter.AddAnd();

                and.AddNot();
                and.AddLessThan(C1Meta.C1AllorsInteger, 2);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Dangling empty And
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddAnd();

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);
            }
        }

        [Test]
        public void Or()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                var any = extent.Filter.AddOr();
                any.AddGreaterThan(C1Meta.C1AllorsInteger, 0);
                any.AddLessThan(C1Meta.C1AllorsInteger, 3);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                (extent = this.LocalExtent(I12Meta.ObjectType)).Filter.AddOr()
                    .AddGreaterThan(I12Meta.I12AllorsInteger, 0)
                    .AddLessThan(I12Meta.I12AllorsInteger, 3);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                (extent = this.LocalExtent(S1234Meta.ObjectType)).Filter.AddOr()
                    .AddGreaterThan(S1234Meta.S1234AllorsInteger, 0)
                    .AddLessThan(S1234Meta.S1234AllorsInteger, 3);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Class Without predicates
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddOr();

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);
            }
        }

        [Test]
        public void OrContainedIn()
        {
           foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Association (Amgiguous Name)
                Extent<Company> parents = this.LocalExtent(CompanyMeta.ObjectType);

                Extent<Company> children = this.LocalExtent(CompanyMeta.ObjectType);
                children.Filter.AddContainedIn(CompanyMeta.CompanyWhereChild, (Extent)parents);

                Extent<Person> persons = this.LocalExtent(PersonMeta.ObjectType);
                var or = persons.Filter.AddOr();
                or.AddContainedIn(PersonMeta.Company, (Extent)parents);
                or.AddContainedIn(PersonMeta.Company, (Extent)children);

                Assert.AreEqual(0, persons.Count);
            }
        }

        [Test]
        public void RoleIntegerExist()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddExists(C1Meta.C1AllorsInteger);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddExists(I12Meta.I12AllorsInteger);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddExists(S1234Meta.S1234AllorsInteger);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddExists(C2Meta.C2AllorsInteger);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddExists(I2Meta.I2AllorsInteger);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddExists(S2Meta.S2AllorsInteger);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleIntegerBetweenRole()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Between C1
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddBetween(C1Meta.C1AllorsInteger, C1Meta.C1IntegerBetweenA, C1Meta.C1IntegerBetweenB);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // TODO: Greater than Role
                // Interface

                // Between -10 and 0
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddBetween(I12Meta.I12AllorsInteger, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddBetween(I12Meta.I12AllorsInteger, 0, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddBetween(I12Meta.I12AllorsInteger, 1, 2);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 3 and 10
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddBetween(I12Meta.I12AllorsInteger, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Between -10 and 0
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddBetween(S1234Meta.S1234AllorsInteger, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddBetween(S1234Meta.S1234AllorsInteger, 0, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddBetween(S1234Meta.S1234AllorsInteger, 1, 2);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Between 3 and 10
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddBetween(S1234Meta.S1234AllorsInteger, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType

                // Between -10 and 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddBetween(C2Meta.C2AllorsInteger, -10, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 0 and 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(C2Meta.C2AllorsInteger, 0, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 1 and 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(C2Meta.C2AllorsInteger, 1, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 3 and 10
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(C2Meta.C2AllorsInteger, 3, 10);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleIntegerLessThanRole()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Less Than 1
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddLessThan(C1Meta.C1AllorsInteger, C1Meta.C1IntegerLessThan);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // TODO: Less than Role
                // Interface

                // Less Than 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddLessThan(I12Meta.I12AllorsInteger, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddLessThan(I12Meta.I12AllorsInteger, 2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 3
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddLessThan(I12Meta.I12AllorsInteger, 3);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Less Than 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddLessThan(S1234Meta.S1234AllorsInteger, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddLessThan(S1234Meta.S1234AllorsInteger, 2);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Less Than 3
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddLessThan(S1234Meta.S1234AllorsInteger, 3);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Class - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddLessThan(C2Meta.C2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(C2Meta.C2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(C2Meta.C2AllorsInteger, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(I2Meta.I2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(I2Meta.I2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(I2Meta.I2AllorsInteger, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(S2Meta.S2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(S2Meta.S2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(S2Meta.S2AllorsInteger, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleIntegerGreaterThanRole()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // C1
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddGreaterThan(C1Meta.C1AllorsInteger, C1Meta.C1IntegerGreaterThan);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // TODO: Greater than Role
                // Interface

                // Greater Than 0
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddGreaterThan(I12Meta.I12AllorsInteger, 0);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddGreaterThan(I12Meta.I12AllorsInteger, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddGreaterThan(I12Meta.I12AllorsInteger, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);
                
                // Super Interface

                // Greater Than 0
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddGreaterThan(S1234Meta.S1234AllorsInteger, 0);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Greater Than 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddGreaterThan(S1234Meta.S1234AllorsInteger, 1);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Greater Than 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddGreaterThan(S1234Meta.S1234AllorsInteger, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(C2Meta.C2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(C2Meta.C2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(C2Meta.C2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleIntegerBetweenValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Between -10 and 0
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddBetween(C1Meta.C1AllorsInteger, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddBetween(C1Meta.C1AllorsInteger, 0, 1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddBetween(C1Meta.C1AllorsInteger, 1, 2);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 3 and 10
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddBetween(C1Meta.C1AllorsInteger, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Between -10 and 0
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddBetween(I12Meta.I12AllorsInteger, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddBetween(I12Meta.I12AllorsInteger, 0, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddBetween(I12Meta.I12AllorsInteger, 1, 2);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 3 and 10
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddBetween(I12Meta.I12AllorsInteger, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Between -10 and 0
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddBetween(S1234Meta.S1234AllorsInteger, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddBetween(S1234Meta.S1234AllorsInteger, 0, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddBetween(S1234Meta.S1234AllorsInteger, 1, 2);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Between 3 and 10
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddBetween(S1234Meta.S1234AllorsInteger, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType

                // Between -10 and 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddBetween(C2Meta.C2AllorsInteger, -10, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 0 and 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(C2Meta.C2AllorsInteger, 0, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 1 and 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(C2Meta.C2AllorsInteger, 1, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 3 and 10
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(C2Meta.C2AllorsInteger, 3, 10);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleIntegerLessThanValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Less Than 1
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddLessThan(C1Meta.C1AllorsInteger, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddLessThan(C1Meta.C1AllorsInteger, 2);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddLessThan(C1Meta.C1AllorsInteger, 3);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Less Than 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddLessThan(I12Meta.I12AllorsInteger, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddLessThan(I12Meta.I12AllorsInteger, 2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 3
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddLessThan(I12Meta.I12AllorsInteger, 3);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Less Than 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddLessThan(S1234Meta.S1234AllorsInteger, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);
                
                // Less Than 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddLessThan(S1234Meta.S1234AllorsInteger, 2);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Less Than 3
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddLessThan(S1234Meta.S1234AllorsInteger, 3);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Class - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddLessThan(C2Meta.C2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(C2Meta.C2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(C2Meta.C2AllorsInteger, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(I2Meta.I2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(I2Meta.I2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(I2Meta.I2AllorsInteger, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(S2Meta.S2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(S2Meta.S2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(S2Meta.S2AllorsInteger, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleIntegerGreaterThanValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Greater Than 0
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddGreaterThan(C1Meta.C1AllorsInteger, 0);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddGreaterThan(C1Meta.C1AllorsInteger, 1);
                
                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddGreaterThan(C1Meta.C1AllorsInteger, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                // Greater Than 0
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddGreaterThan(I12Meta.I12AllorsInteger, 0);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddGreaterThan(I12Meta.I12AllorsInteger, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddGreaterThan(I12Meta.I12AllorsInteger, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                // Greater Than 0
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddGreaterThan(S1234Meta.S1234AllorsInteger, 0);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Greater Than 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddGreaterThan(S1234Meta.S1234AllorsInteger, 1);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Greater Than 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddGreaterThan(S1234Meta.S1234AllorsInteger, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType
                // Greater Than 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(C2Meta.C2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(C2Meta.C2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(C2Meta.C2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                // Greater Than 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                // Greater Than 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleIntegerEquals()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                // Equal 0
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1AllorsInteger, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);


                // Equal 1
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1AllorsInteger, 1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 2
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1AllorsInteger, 2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                // Equal 0
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddEquals(I12Meta.I12AllorsInteger, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddEquals(I12Meta.I12AllorsInteger, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddEquals(I12Meta.I12AllorsInteger, 2);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                // Equal 0
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234AllorsInteger, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234AllorsInteger, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Equal 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234AllorsInteger, 2);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Class - Wrong RelationType
                // Equal 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddEquals(C2Meta.C2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(C2Meta.C2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(C2Meta.C2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                // Equal 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(I2Meta.I2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(I2Meta.I2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(I2Meta.I2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                // Equal 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(S2Meta.S2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(S2Meta.S2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(S2Meta.S2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleLongBetweenValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Between -10 and 0
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddBetween(C1Meta.C1AllorsLong, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddBetween(C1Meta.C1AllorsLong, 0, 1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false); 

                // Between 1 and 2
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddBetween(C1Meta.C1AllorsLong, 1, 2);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false); 

                // Between 3 and 10
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddBetween(C1Meta.C1AllorsLong, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false); 

                // Interface
                // Between -10 and 0
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddBetween(I12Meta.I12AllorsLong, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddBetween(I12Meta.I12AllorsLong, 0, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false); 

                // Between 1 and 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddBetween(I12Meta.I12AllorsLong, 1, 2);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false); 

                // Between 3 and 10
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddBetween(I12Meta.I12AllorsLong, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false); 

                // Super Interface
                // Between -10 and 0
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddBetween(S1234Meta.S1234AllorsLong, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false); 

                // Between 0 and 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddBetween(S1234Meta.S1234AllorsLong, 0, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false); 

                // Between 1 and 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddBetween(S1234Meta.S1234AllorsLong, 1, 2);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Between 3 and 10
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddBetween(S1234Meta.S1234AllorsLong, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType
                // Between -10 and 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddBetween(C2Meta.C2AllorsLong, -10, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 0 and 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(C2Meta.C2AllorsLong, 0, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 1 and 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(C2Meta.C2AllorsLong, 1, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 3 and 10
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(C2Meta.C2AllorsLong, 3, 10);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleLongLessThanValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Less Than 1
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddLessThan(C1Meta.C1AllorsLong, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddLessThan(C1Meta.C1AllorsLong, 2);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddLessThan(C1Meta.C1AllorsLong, 3);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                // Less Than 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddLessThan(I12Meta.I12AllorsLong, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddLessThan(I12Meta.I12AllorsLong, 2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 3
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddLessThan(I12Meta.I12AllorsLong, 3);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                // Less Than 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddLessThan(S1234Meta.S1234AllorsLong, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddLessThan(S1234Meta.S1234AllorsLong, 2);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Less Than 3
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddLessThan(S1234Meta.S1234AllorsLong, 3);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Class - Wrong RelationType
                // Less Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddLessThan(C2Meta.C2AllorsLong, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(C2Meta.C2AllorsLong, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(C2Meta.C2AllorsLong, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                // Less Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(I2Meta.I2AllorsLong, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(I2Meta.I2AllorsLong, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(I2Meta.I2AllorsLong, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                // Less Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(S2Meta.S2AllorsLong, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(S2Meta.S2AllorsLong, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(S2Meta.S2AllorsLong, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleLongGreaterThanValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Greater Than 0
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddGreaterThan(C1Meta.C1AllorsLong, 0);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddGreaterThan(C1Meta.C1AllorsLong, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddGreaterThan(C1Meta.C1AllorsLong, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Greater Than 0
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddGreaterThan(I12Meta.I12AllorsLong, 0);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddGreaterThan(I12Meta.I12AllorsLong, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddGreaterThan(I12Meta.I12AllorsLong, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Greater Than 0
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddGreaterThan(S1234Meta.S1234AllorsLong, 0);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Greater Than 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddGreaterThan(S1234Meta.S1234AllorsLong, 1);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Greater Than 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddGreaterThan(S1234Meta.S1234AllorsLong, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(C2Meta.C2AllorsLong, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(C2Meta.C2AllorsLong, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(C2Meta.C2AllorsLong, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsLong, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsLong, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsLong, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsLong, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsLong, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsLong, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleLongEquals()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                // Equal 0
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1AllorsLong, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1AllorsLong, 1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 2
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1AllorsLong, 2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                // Equal 0
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddEquals(I12Meta.I12AllorsLong, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddEquals(I12Meta.I12AllorsLong, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddEquals(I12Meta.I12AllorsLong, 2);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                // Equal 0
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234AllorsLong, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234AllorsLong, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Equal 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234AllorsLong, 2);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Class - Wrong RelationType
                // Equal 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddEquals(C2Meta.C2AllorsLong, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(C2Meta.C2AllorsLong, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(C2Meta.C2AllorsLong, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                // Equal 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(I2Meta.I2AllorsLong, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(I2Meta.I2AllorsLong, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(I2Meta.I2AllorsLong, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                // Equal 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(S2Meta.S2AllorsLong, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(S2Meta.S2AllorsLong, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(S2Meta.S2AllorsLong, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleDoubleBetweenValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Between -10 and 0
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddBetween(C1Meta.C1AllorsDouble, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddBetween(C1Meta.C1AllorsDouble, 0, 1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddBetween(C1Meta.C1AllorsDouble, 1, 2);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 3 and 10
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddBetween(C1Meta.C1AllorsDouble, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Between -10 and 0
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddBetween(I12Meta.I12AllorsDouble, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddBetween(I12Meta.I12AllorsDouble, 0, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddBetween(I12Meta.I12AllorsDouble, 1, 2);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 3 and 10
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddBetween(I12Meta.I12AllorsDouble, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Between -10 and 0
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddBetween(S1234Meta.S1234AllorsDouble, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddBetween(S1234Meta.S1234AllorsDouble, 0, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddBetween(S1234Meta.S1234AllorsDouble, 1, 2);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Between 3 and 10
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddBetween(S1234Meta.S1234AllorsDouble, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType

                // Between -10 and 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddBetween(C2Meta.C2AllorsDouble, -10, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 0 and 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(C2Meta.C2AllorsDouble, 0, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 1 and 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(C2Meta.C2AllorsDouble, 1, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 3 and 10
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(C2Meta.C2AllorsDouble, 3, 10);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleDoubleLessThanValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Less Than 1
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddLessThan(C1Meta.C1AllorsDouble, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddLessThan(C1Meta.C1AllorsDouble, 2);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddLessThan(C1Meta.C1AllorsDouble, 3);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Less Than 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddLessThan(I12Meta.I12AllorsDouble, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddLessThan(I12Meta.I12AllorsDouble, 2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 3
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddLessThan(I12Meta.I12AllorsDouble, 3);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Less Than 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddLessThan(S1234Meta.S1234AllorsDouble, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddLessThan(S1234Meta.S1234AllorsDouble, 2);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Less Than 3
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddLessThan(S1234Meta.S1234AllorsDouble, 3);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Class - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddLessThan(C2Meta.C2AllorsDouble, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(C2Meta.C2AllorsDouble, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(C2Meta.C2AllorsDouble, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(I2Meta.I2AllorsDouble, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(I2Meta.I2AllorsDouble, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(I2Meta.I2AllorsDouble, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(S2Meta.S2AllorsDouble, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(S2Meta.S2AllorsDouble, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(S2Meta.S2AllorsDouble, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleDoubleGreaterThanValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Greater Than 0
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddGreaterThan(C1Meta.C1AllorsDouble, 0);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddGreaterThan(C1Meta.C1AllorsDouble, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddGreaterThan(C1Meta.C1AllorsDouble, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Greater Than 0
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddGreaterThan(I12Meta.I12AllorsDouble, 0);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddGreaterThan(I12Meta.I12AllorsDouble, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddGreaterThan(I12Meta.I12AllorsDouble, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Greater Than 0
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddGreaterThan(S1234Meta.S1234AllorsDouble, 0);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Greater Than 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddGreaterThan(S1234Meta.S1234AllorsDouble, 1);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Greater Than 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddGreaterThan(S1234Meta.S1234AllorsDouble, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(C2Meta.C2AllorsDouble, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(C2Meta.C2AllorsDouble, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(C2Meta.C2AllorsDouble, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsDouble, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsDouble, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsDouble, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsDouble, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsDouble, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsDouble, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleDoubleEquals()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                // Equal 0
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1AllorsDouble, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1AllorsDouble, 1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 2
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1AllorsDouble, 2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                // Equal 0
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddEquals(I12Meta.I12AllorsDouble, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddEquals(I12Meta.I12AllorsDouble, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddEquals(I12Meta.I12AllorsDouble, 2);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                // Equal 0
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234AllorsDouble, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234AllorsDouble, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Equal 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234AllorsDouble, 2);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Class - Wrong RelationType
                // Equal 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddEquals(C2Meta.C2AllorsDouble, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(C2Meta.C2AllorsDouble, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(C2Meta.C2AllorsDouble, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                // Equal 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(I2Meta.I2AllorsDouble, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(I2Meta.I2AllorsDouble, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(I2Meta.I2AllorsDouble, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                // Equal 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(S2Meta.S2AllorsDouble, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(S2Meta.S2AllorsDouble, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(S2Meta.S2AllorsDouble, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleDecimalBetweenValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Between -10 and 0
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddBetween(C1Meta.C1AllorsDecimal, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddBetween(C1Meta.C1AllorsDecimal, 0, 1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddBetween(C1Meta.C1AllorsDecimal, 1, 2);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 3 and 10
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddBetween(C1Meta.C1AllorsDecimal, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Between -10 and 0
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddBetween(I12Meta.I12AllorsDecimal, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddBetween(I12Meta.I12AllorsDecimal, 0, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddBetween(I12Meta.I12AllorsDecimal, 1, 2);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 3 and 10
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddBetween(I12Meta.I12AllorsDecimal, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Between -10 and 0
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddBetween(S1234Meta.S1234AllorsDecimal, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddBetween(S1234Meta.S1234AllorsDecimal, 0, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddBetween(S1234Meta.S1234AllorsDecimal, 1, 2);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Between 3 and 10
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddBetween(S1234Meta.S1234AllorsDecimal, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType

                // Between -10 and 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddBetween(C2Meta.C2AllorsDecimal, -10, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 0 and 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(C2Meta.C2AllorsDecimal, 0, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 1 and 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(C2Meta.C2AllorsDecimal, 1, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 3 and 10
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(C2Meta.C2AllorsDecimal, 3, 10);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleDecimalLessThanValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Less Than 1
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddLessThan(C1Meta.C1AllorsDecimal, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddLessThan(C1Meta.C1AllorsDecimal, 2);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddLessThan(C1Meta.C1AllorsDecimal, 3);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Less Than 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddLessThan(I12Meta.I12AllorsDecimal, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddLessThan(I12Meta.I12AllorsDecimal, 2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 3
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddLessThan(I12Meta.I12AllorsDecimal, 3);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Less Than 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddLessThan(S1234Meta.S1234AllorsDecimal, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddLessThan(S1234Meta.S1234AllorsDecimal, 2);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Less Than 3
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddLessThan(S1234Meta.S1234AllorsDecimal, 3);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Class - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddLessThan(C2Meta.C2AllorsDecimal, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(C2Meta.C2AllorsDecimal, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(C2Meta.C2AllorsDecimal, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(I2Meta.I2AllorsDecimal, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(I2Meta.I2AllorsDecimal, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(I2Meta.I2AllorsDecimal, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(S2Meta.S2AllorsDecimal, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(S2Meta.S2AllorsDecimal, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(S2Meta.S2AllorsDecimal, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleDecimalGreaterThanValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Greater Than 0
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddGreaterThan(C1Meta.C1AllorsDecimal, 0);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddGreaterThan(C1Meta.C1AllorsDecimal, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddGreaterThan(C1Meta.C1AllorsDecimal, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Greater Than 0
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddGreaterThan(I12Meta.I12AllorsDecimal, 0);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddGreaterThan(I12Meta.I12AllorsDecimal, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddGreaterThan(I12Meta.I12AllorsDecimal, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Greater Than 0
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddGreaterThan(S1234Meta.S1234AllorsDecimal, 0);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Greater Than 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddGreaterThan(S1234Meta.S1234AllorsDecimal, 1);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Greater Than 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddGreaterThan(S1234Meta.S1234AllorsDecimal, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(C2Meta.C2AllorsDecimal, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(C2Meta.C2AllorsDecimal, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(C2Meta.C2AllorsDecimal, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsDecimal, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsDecimal, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsDecimal, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsDecimal, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsDecimal, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(I2Meta.I2AllorsDecimal, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleDecimalEquals()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                // Equal 0
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1AllorsDecimal, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1AllorsDecimal, 1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 2
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1AllorsDecimal, 2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                // Equal 0
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddEquals(I12Meta.I12AllorsDecimal, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddEquals(I12Meta.I12AllorsDecimal, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddEquals(I12Meta.I12AllorsDecimal, 2);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                // Equal 0
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234AllorsDecimal, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false); 

                // Equal 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234AllorsDecimal, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false); 
                
                // Equal 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234AllorsDecimal, 2);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true); 

                // Class - Wrong RelationType
                // Equal 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddEquals(C2Meta.C2AllorsDecimal, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(C2Meta.C2AllorsDecimal, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(C2Meta.C2AllorsDecimal, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                // Equal 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(I2Meta.I2AllorsDecimal, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(I2Meta.I2AllorsDecimal, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(I2Meta.I2AllorsDecimal, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                // Equal 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(S2Meta.S2AllorsDecimal, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(S2Meta.S2AllorsDecimal, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(S2Meta.S2AllorsDecimal, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }
        
        [Test]
        public void RoleDateTimeBetweenValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                foreach (var flag in TrueFalse)
                {
                    var dateTime1 = new DateTime(2000, 1, 1, 0, 0, 1);
                    var dateTime2 = new DateTime(2000, 1, 1, 0, 0, 2);
                    var dateTime3 = new DateTime(2000, 1, 1, 0, 0, 3);
                    var dateTime4 = new DateTime(2000, 1, 1, 0, 0, 4);
                    var dateTime5 = new DateTime(2000, 1, 1, 0, 0, 5);
                    var dateTime6 = new DateTime(2000, 1, 1, 0, 0, 6);
                    var dateTime7 = new DateTime(2000, 1, 1, 0, 0, 7);
                    var dateTime10 = new DateTime(2000, 1, 1, 0, 0, 10);

                    if (flag)
                    {
                        dateTime1 = dateTime1.ToUniversalTime();
                        dateTime2 = dateTime2.ToUniversalTime();
                        dateTime3 = dateTime3.ToUniversalTime();
                        dateTime4 = dateTime4.ToUniversalTime();
                        dateTime5 = dateTime5.ToUniversalTime();
                        dateTime6 = dateTime6.ToUniversalTime();
                        dateTime7 = dateTime7.ToUniversalTime();
                        dateTime10 = dateTime10.ToUniversalTime();
                    }

                    // Class
                    // Between 1 and 3
                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddBetween(C1Meta.C1AllorsDateTime, dateTime1, dateTime3);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Between 3 and 4
                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddBetween(C1Meta.C1AllorsDateTime, dateTime3, dateTime4);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, true, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Between 4 and 5
                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddBetween(C1Meta.C1AllorsDateTime, dateTime4, dateTime5);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Between 6 and 10
                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddBetween(C1Meta.C1AllorsDateTime, dateTime6, dateTime10);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Interface
                    // Between 1 and 3
                    extent = this.LocalExtent(I12Meta.ObjectType);
                    extent.Filter.AddBetween(I12Meta.I12AllorsDateTime, dateTime1, dateTime3);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Between 3 and 4
                    extent = this.LocalExtent(I12Meta.ObjectType);
                    extent.Filter.AddBetween(I12Meta.I12AllorsDateTime, dateTime3, dateTime4);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, true, false, false);
                    this.AssertC2(extent, false, true, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Between 4 and 5
                    extent = this.LocalExtent(I12Meta.ObjectType);
                    extent.Filter.AddBetween(I12Meta.I12AllorsDateTime, dateTime4, dateTime5);

                    Assert.AreEqual(6, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Between 6 and 10
                    extent = this.LocalExtent(I12Meta.ObjectType);
                    extent.Filter.AddBetween(I12Meta.I12AllorsDateTime, dateTime6, dateTime10);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Super Interface
                    // Between 1 and 3
                    extent = this.LocalExtent(S1234Meta.ObjectType);
                    extent.Filter.AddBetween(S1234Meta.S1234AllorsDateTime, dateTime1, dateTime3);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Between 3 and 4
                    extent = this.LocalExtent(S1234Meta.ObjectType);
                    extent.Filter.AddBetween(S1234Meta.S1234AllorsDateTime, dateTime3, dateTime4);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, true, false, false);
                    this.AssertC2(extent, false, true, false, false);
                    this.AssertC3(extent, false, true, false, false);
                    this.AssertC4(extent, false, true, false, false);

                    // Between 4 and 5
                    extent = this.LocalExtent(S1234Meta.ObjectType);
                    extent.Filter.AddBetween(S1234Meta.S1234AllorsDateTime, dateTime4, dateTime5);

                    Assert.AreEqual(12, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, true, true, true);
                    this.AssertC3(extent, false, true, true, true);
                    this.AssertC4(extent, false, true, true, true);

                    // Between 6 and 10
                    extent = this.LocalExtent(S1234Meta.ObjectType);
                    extent.Filter.AddBetween(S1234Meta.S1234AllorsDateTime, dateTime6, dateTime10);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Class - Wrong RelationType0
                    // Between 1 and 3
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    var exception = false;
                    try
                    {
                        extent.Filter.AddBetween(C2Meta.C2AllorsDateTime, dateTime1, dateTime3);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Between 3 and 4
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddBetween(C2Meta.C2AllorsDateTime, dateTime3, dateTime4);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Between 4 and 5
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddBetween(C2Meta.C2AllorsDateTime, dateTime4, dateTime5);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Between 6 and 10
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddBetween(C2Meta.C2AllorsDateTime, dateTime6, dateTime10);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);
                }
            }
        }

        [Test]
        public void RoleDateTimeLessThanValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                foreach (var flag in TrueFalse)
                {
                    var dateTime1 = new DateTime(2000, 1, 1, 0, 0, 1);
                    var dateTime2 = new DateTime(2000, 1, 1, 0, 0, 2);
                    var dateTime3 = new DateTime(2000, 1, 1, 0, 0, 3);
                    var dateTime4 = new DateTime(2000, 1, 1, 0, 0, 4);
                    var dateTime5 = new DateTime(2000, 1, 1, 0, 0, 5);
                    var dateTime6 = new DateTime(2000, 1, 1, 0, 0, 6);
                    var dateTime7 = new DateTime(2000, 1, 1, 0, 0, 7);
                    var dateTime10 = new DateTime(2000, 1, 1, 0, 0, 10);

                    if (flag)
                    {
                        dateTime1 = dateTime1.ToUniversalTime();
                        dateTime2 = dateTime2.ToUniversalTime();
                        dateTime3 = dateTime3.ToUniversalTime();
                        dateTime4 = dateTime4.ToUniversalTime();
                        dateTime5 = dateTime5.ToUniversalTime();
                        dateTime6 = dateTime6.ToUniversalTime();
                        dateTime7 = dateTime7.ToUniversalTime();
                        dateTime10 = dateTime10.ToUniversalTime();
                    }

                    // Class
                    // Less Than 4
                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddLessThan(C1Meta.C1AllorsDateTime, dateTime4);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Less Than 5
                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddLessThan(C1Meta.C1AllorsDateTime, dateTime5);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, true, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false); 

                    // Less Than 6
                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddLessThan(C1Meta.C1AllorsDateTime, dateTime6);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false); 

                    // Interface
                    // Less Than 4
                    extent = this.LocalExtent(I12Meta.ObjectType);
                    extent.Filter.AddLessThan(I12Meta.I12AllorsDateTime, dateTime4);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Less Than 5
                    extent = this.LocalExtent(I12Meta.ObjectType);
                    extent.Filter.AddLessThan(I12Meta.I12AllorsDateTime, dateTime5);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, true, false, false);
                    this.AssertC2(extent, false, true, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Less Than 6
                    extent = this.LocalExtent(I12Meta.ObjectType);
                    extent.Filter.AddLessThan(I12Meta.I12AllorsDateTime, dateTime6);

                    Assert.AreEqual(6, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false); 

                    // Super Interface
                    // Less Than 4
                    extent = this.LocalExtent(S1234Meta.ObjectType);
                    extent.Filter.AddLessThan(S1234Meta.S1234AllorsDateTime, dateTime4);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false); 

                    // Less Than 5
                    extent = this.LocalExtent(S1234Meta.ObjectType);
                    extent.Filter.AddLessThan(S1234Meta.S1234AllorsDateTime, dateTime5);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, true, false, false);
                    this.AssertC2(extent, false, true, false, false);
                    this.AssertC3(extent, false, true, false, false);
                    this.AssertC4(extent, false, true, false, false); 

                    // Less Than 6
                    extent = this.LocalExtent(S1234Meta.ObjectType);
                    extent.Filter.AddLessThan(S1234Meta.S1234AllorsDateTime, dateTime6);

                    Assert.AreEqual(12, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, true, true, true);
                    this.AssertC3(extent, false, true, true, true);
                    this.AssertC4(extent, false, true, true, true);

                    // Class - Wrong RelationType

                    // Less Than 4
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    var exception = false;
                    try
                    {
                        extent.Filter.AddLessThan(C2Meta.C2AllorsDateTime, dateTime4);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Less Than 5
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddLessThan(C2Meta.C2AllorsDateTime, dateTime5);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Less Than 6
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddLessThan(C2Meta.C2AllorsDateTime, dateTime6);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Interface - Wrong RelationType
                    // Less Than 4
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddLessThan(I2Meta.I2AllorsDateTime, dateTime4);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Less Than 5
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddLessThan(I2Meta.I2AllorsDateTime, dateTime5);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Less Than 6
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddLessThan(I2Meta.I2AllorsDateTime, dateTime6);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Super Interface - Wrong RelationType
                    // Less Than 4
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddLessThan(S2Meta.S2AllorsDateTime, dateTime4);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Less Than 5
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddLessThan(S2Meta.S2AllorsDateTime, dateTime5);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Less Than 6
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddLessThan(S2Meta.S2AllorsDateTime, dateTime6);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);
                }
            }
        }

        [Test]
        public void RoleDateTimeGreaterThanValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                foreach (var flag in TrueFalse)
                {
                    var dateTime1 = new DateTime(2000, 1, 1, 0, 0, 1);
                    var dateTime2 = new DateTime(2000, 1, 1, 0, 0, 2);
                    var dateTime3 = new DateTime(2000, 1, 1, 0, 0, 3);
                    var dateTime4 = new DateTime(2000, 1, 1, 0, 0, 4);
                    var dateTime5 = new DateTime(2000, 1, 1, 0, 0, 5);
                    var dateTime6 = new DateTime(2000, 1, 1, 0, 0, 6);
                    var dateTime7 = new DateTime(2000, 1, 1, 0, 0, 7);
                    var dateTime10 = new DateTime(2000, 1, 1, 0, 0, 10);

                    if (flag)
                    {
                        dateTime1 = dateTime1.ToUniversalTime();
                        dateTime2 = dateTime2.ToUniversalTime();
                        dateTime3 = dateTime3.ToUniversalTime();
                        dateTime4 = dateTime4.ToUniversalTime();
                        dateTime5 = dateTime5.ToUniversalTime();
                        dateTime6 = dateTime6.ToUniversalTime();
                        dateTime7 = dateTime7.ToUniversalTime();
                        dateTime10 = dateTime10.ToUniversalTime();
                    }

                    // Class
                    // Greater Than 3
                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddGreaterThan(C1Meta.C1AllorsDateTime, dateTime3);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false); 

                    // Greater Than 4
                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddGreaterThan(C1Meta.C1AllorsDateTime, dateTime4);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Greater Than 5
                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddGreaterThan(C1Meta.C1AllorsDateTime, dateTime5);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Interface
                    // Greater Than 3
                    extent = this.LocalExtent(I12Meta.ObjectType);
                    extent.Filter.AddGreaterThan(I12Meta.I12AllorsDateTime, dateTime3);

                    Assert.AreEqual(6, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Greater Than 4
                    extent = this.LocalExtent(I12Meta.ObjectType);
                    extent.Filter.AddGreaterThan(I12Meta.I12AllorsDateTime, dateTime4);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, true, true);
                    this.AssertC2(extent, false, false, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Greater Than 5
                    extent = this.LocalExtent(I12Meta.ObjectType);
                    extent.Filter.AddGreaterThan(I12Meta.I12AllorsDateTime, dateTime5);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Super Interface
                    // Greater Than 3
                    extent = this.LocalExtent(S1234Meta.ObjectType);
                    extent.Filter.AddGreaterThan(S1234Meta.S1234AllorsDateTime, dateTime3);

                    Assert.AreEqual(12, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, true, true, true);
                    this.AssertC3(extent, false, true, true, true);
                    this.AssertC4(extent, false, true, true, true);

                    // Greater Than 4
                    extent = this.LocalExtent(S1234Meta.ObjectType);
                    extent.Filter.AddGreaterThan(S1234Meta.S1234AllorsDateTime, dateTime4);

                    Assert.AreEqual(8, extent.Count);
                    this.AssertC1(extent, false, false, true, true);
                    this.AssertC2(extent, false, false, true, true);
                    this.AssertC3(extent, false, false, true, true);
                    this.AssertC4(extent, false, false, true, true); 

                    // Greater Than 5
                    extent = this.LocalExtent(S1234Meta.ObjectType);
                    extent.Filter.AddGreaterThan(S1234Meta.S1234AllorsDateTime, dateTime5);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Class - Wrong RelationType

                    // Greater Than 3
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    var exception = false;
                    try
                    {
                        extent.Filter.AddGreaterThan(C2Meta.C2AllorsDateTime, dateTime3);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Greater Than 4
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddGreaterThan(C2Meta.C2AllorsDateTime, dateTime4);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Greater Than 5
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddGreaterThan(C2Meta.C2AllorsDateTime, dateTime5);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Interface - Wrong RelationType

                    // Greater Than 3
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddGreaterThan(I2Meta.I2AllorsDateTime, dateTime3);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Greater Than 4
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddGreaterThan(I2Meta.I2AllorsDateTime, dateTime4);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Greater Than 5
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddGreaterThan(I2Meta.I2AllorsDateTime, dateTime5);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Super Interface - Wrong RelationType

                    // Greater Than 3
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddGreaterThan(I2Meta.I2AllorsDateTime, dateTime3);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Greater Than 4
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddGreaterThan(I2Meta.I2AllorsDateTime, dateTime4);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Greater Than 5
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddGreaterThan(I2Meta.I2AllorsDateTime, dateTime5);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);
                }
            }
        }

        [Test]
        public void RoleDateTimeEquals()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                foreach (var flag in TrueFalse)
                {
                    var dateTime1 = new DateTime(2000, 1, 1, 0, 0, 1);
                    var dateTime2 = new DateTime(2000, 1, 1, 0, 0, 2);
                    var dateTime3 = new DateTime(2000, 1, 1, 0, 0, 3);
                    var dateTime4 = new DateTime(2000, 1, 1, 0, 0, 4);
                    var dateTime5 = new DateTime(2000, 1, 1, 0, 0, 5);
                    var dateTime6 = new DateTime(2000, 1, 1, 0, 0, 6);
                    var dateTime7 = new DateTime(2000, 1, 1, 0, 0, 7);
                    var dateTime10 = new DateTime(2000, 1, 1, 0, 0, 10);

                    if (flag)
                    {
                        dateTime1 = dateTime1.ToUniversalTime();
                        dateTime2 = dateTime2.ToUniversalTime();
                        dateTime3 = dateTime3.ToUniversalTime();
                        dateTime4 = dateTime4.ToUniversalTime();
                        dateTime5 = dateTime5.ToUniversalTime();
                        dateTime6 = dateTime6.ToUniversalTime();
                        dateTime7 = dateTime7.ToUniversalTime();
                        dateTime10 = dateTime10.ToUniversalTime();
                    }

                    // Class
                    // Equal 3
                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddEquals(C1Meta.C1AllorsDateTime, dateTime3);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Equal 4
                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddEquals(C1Meta.C1AllorsDateTime, dateTime4);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, true, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);
                    
                    // Equal 5
                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddEquals(C1Meta.C1AllorsDateTime, dateTime5);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Interface
                    // Equal 3
                    extent = this.LocalExtent(I12Meta.ObjectType);
                    extent.Filter.AddEquals(I12Meta.I12AllorsDateTime, dateTime3);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Equal 4
                    extent = this.LocalExtent(I12Meta.ObjectType);
                    extent.Filter.AddEquals(I12Meta.I12AllorsDateTime, dateTime4);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, true, false, false);
                    this.AssertC2(extent, false, true, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Equal 5
                    extent = this.LocalExtent(I12Meta.ObjectType);
                    extent.Filter.AddEquals(I12Meta.I12AllorsDateTime, dateTime5);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, true, true);
                    this.AssertC2(extent, false, false, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Super Interface
                    // Equal 3
                    extent = this.LocalExtent(S1234Meta.ObjectType);
                    extent.Filter.AddEquals(S1234Meta.S1234AllorsDateTime, dateTime3);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Equal 4
                    extent = this.LocalExtent(S1234Meta.ObjectType);
                    extent.Filter.AddEquals(S1234Meta.S1234AllorsDateTime, dateTime4);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, true, false, false);
                    this.AssertC2(extent, false, true, false, false);
                    this.AssertC3(extent, false, true, false, false);
                    this.AssertC4(extent, false, true, false, false);

                    // Equal 5
                    extent = this.LocalExtent(S1234Meta.ObjectType);
                    extent.Filter.AddEquals(S1234Meta.S1234AllorsDateTime, dateTime5);

                    Assert.AreEqual(8, extent.Count);
                    this.AssertC1(extent, false, false, true, true);
                    this.AssertC2(extent, false, false, true, true);
                    this.AssertC3(extent, false, false, true, true);
                    this.AssertC4(extent, false, false, true, true);

                    // Class - Wrong RelationType
                    // Equal 3
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    var exception = false;
                    try
                    {
                        extent.Filter.AddEquals(C2Meta.C2AllorsDateTime, dateTime3);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Equal 4
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddEquals(C2Meta.C2AllorsDateTime, dateTime4);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Equal 5
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddEquals(C2Meta.C2AllorsDateTime, dateTime5);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Interface - Wrong RelationType
                    // Equal 3
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddEquals(I2Meta.I2AllorsDateTime, dateTime3);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Equal 4
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddEquals(I2Meta.I2AllorsDateTime, dateTime4);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Equal 5
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddEquals(I2Meta.I2AllorsDateTime, dateTime5);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Super Interface - Wrong RelationType
                    // Equal 3
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddEquals(S2Meta.S2AllorsDateTime, dateTime3);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Equal 4
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddEquals(S2Meta.S2AllorsDateTime, dateTime4);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);

                    // Equal 5
                    extent = this.LocalExtent(C1Meta.ObjectType);

                    exception = false;
                    try
                    {
                        extent.Filter.AddEquals(S2Meta.S2AllorsDateTime, dateTime5);
                    }
                    catch
                    {
                        exception = true;
                    }

                    Assert.IsTrue(exception);
                }
            }
        }

        [Test]
        public void RoleEnumerationEquals()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // AllorsInteger
                // Class

                // Equal 0
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1AllorsInteger, Zero2Four.Zero);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1AllorsInteger, Zero2Four.One);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 2
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1AllorsInteger, Zero2Four.Two);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Equal 0
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddEquals(I12Meta.I12AllorsInteger, Zero2Four.Zero);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddEquals(I12Meta.I12AllorsInteger, Zero2Four.One);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 2
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddEquals(I12Meta.I12AllorsInteger, Zero2Four.Two);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Equal 0
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234AllorsInteger, Zero2Four.Zero);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234AllorsInteger, Zero2Four.One);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Equal 2
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234AllorsInteger, Zero2Four.Two);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Class - Wrong RelationType

                // Equal 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddEquals(C2Meta.C2AllorsInteger, Zero2Four.Zero);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(C2Meta.C2AllorsInteger, Zero2Four.One);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(C2Meta.C2AllorsInteger, Zero2Four.Two);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Equal 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(I2Meta.I2AllorsInteger, Zero2Four.Zero);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(I2Meta.I2AllorsInteger, Zero2Four.One);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(I2Meta.I2AllorsInteger, Zero2Four.Two);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Equal 0
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(S2Meta.S2AllorsInteger, Zero2Four.Zero);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(S2Meta.S2AllorsInteger, Zero2Four.One);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(S2Meta.S2AllorsInteger, Zero2Four.Two);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Wrong type
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exceptionThrown = false;
                C1 first = null;
                try
                {
                    extent.Filter.AddEquals(C1Meta.C1AllorsLong, Zero2Four.Zero);
                    first = (C1)extent.First;
                }
                catch
                {
                    exceptionThrown = true;
                }

                Assert.IsNull(first);
                Assert.IsTrue(exceptionThrown, "Only integer supports Enumeration");
            }
        }

        [Test]
        public void RoleCompositeEqualsRole()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                var exceptionThrown = false;

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                try
                {
                    extent.Filter.AddEquals(C1Meta.C1C2one2one, I1Meta.I1C1one2one);
                }
                catch (ArgumentException)
                {
                    exceptionThrown = true;
                }

                Assert.IsTrue(exceptionThrown);
            }
        }

        [Test]
        public void RoleMany2ManyContainedIn()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                foreach (var useOperator in this.UseOperator)
                {
                    // Extent over Class

                    // RelationType from Class to Class

                    // ContainedIn Extent over Class
                    // Empty
                    var inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C1many2many, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C1many2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C1many2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Shortcut
                    inExtent = this.c1C.Strategy.GetCompositeRoles(C1Meta.C1C1one2many);

                    // if (useOperator)
                    // {
                    // var inExtentA = c1_1.Strategy.GetCompositeRoles(C1Meta.C1C1one2many);
                    // var inExtentB = c1_1.Strategy.GetCompositeRoles(C1Meta.C1C1one2many);
                    // inExtent = Session.Union(inExtentA, inExtentB);
                    // }
                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C1many2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C1many2many, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C1many2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C1many2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from Class to Interface

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12many2many, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, true, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, true, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12many2many, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12many2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12many2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);
                }
            }
        }

        [Test]
        public void RoleMany2ManyContains()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddContains(C1Meta.C1C2many2many, this.c2C);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddContains(C1Meta.C1C2many2many, this.c2B);
                extent.Filter.AddContains(C1Meta.C1C2many2many, this.c2C);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false); 

                // Interface
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddContains(C1Meta.C1I12many2many, this.c2C);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddContains(S1234Meta.S1234many2many, this.c1A);

                Assert.AreEqual(9, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, false, false, false); 

                // TODO: wrong relation
            }
        }

        [Test]
        public void RoleMany2ManyExist()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddExists(C1Meta.C1C2many2many);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddExists(I12Meta.I12C2many2many);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddExists(S1234Meta.S1234C2many2many);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddExists(C3Meta.C3C2many2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(I12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddExists(C3Meta.C3C2many2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(S12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddExists(C3Meta.C3C2many2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleOne2ManyContainedIn()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                foreach (var useOperator in this.UseOperator)
                {
                    // Extent over Class

                    // RelationType from Class to Class

                    // ContainedIn Extent over Class
                    // Emtpy Extent
                    var inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C1one2many, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full Extent
                    inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C1one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, true, true, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(C2Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C2Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C2Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C2one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, true, true, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(C4Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C4Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C4Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C3Meta.ObjectType);
                    extent.Filter.AddContainedIn(C3Meta.C3C4one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, true, true, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Shared Interface
                    // Emtpy Extent
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    inExtent.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        inExtentA.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtentB.Filter.AddEquals(I12Meta.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C1one2many, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full Extent
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C1one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, true, true, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C2one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, true, true, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(I34Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I34Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I34Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C3Meta.ObjectType);
                    extent.Filter.AddContainedIn(C3Meta.C3C4one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, true, true, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from Interface to Class

                    // ContainedIn Extent over Class
                    inExtent = this.LocalExtent(C2Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C2Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C2Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(I12Meta.I12C2one2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, true, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(I12Meta.I12C2one2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, true, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);
                }
            }
        }

        [Test]
        public void RoleOne2ManyContains()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddContains(C1Meta.C1C2one2many, this.c2C);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, true, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddContains(C1Meta.C1I12one2many, this.c2C);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, true, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddContains(S1234Meta.S1234one2many, this.c1B);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // TODO: wrong relation
            }
        }

        [Test]
        public void RoleOne2ManyExist()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddExists(C1Meta.C1C2one2many);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, true, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddExists(I12Meta.I12C2one2many);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, true, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);
               
                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddExists(S1234Meta.S1234C2one2many);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, true, false);
                this.AssertC4(extent, false, false, false, false);
 
                // Class - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddExists(C3Meta.C3C2one2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(I12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddExists(C3Meta.C3C2one2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(S12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddExists(C3Meta.C3C2one2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleOne2OneContainedIn()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                foreach (var useOperator in this.UseOperator)
                {
                    // Extent over Class

                    // RelationType from Class to Class

                    // ContainedIn Extent over Class
                    var inExtent = this.LocalExtent(C1Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C1one2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(C2Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C2Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C2Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C2one2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(C4Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C4Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C4Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C3Meta.ObjectType);
                    extent.Filter.AddContainedIn(C3Meta.C3C4one2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, true, true, true);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Shared Interface
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C1one2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C2one2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(I34Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I34Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I34Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C3Meta.ObjectType);
                    extent.Filter.AddContainedIn(C3Meta.C3C4one2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, true, true, true);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from Class to Interface

                    // ContainedIn Extent over Class
                    inExtent = this.LocalExtent(C2Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C2Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C2Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12one2one, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Shared Interface
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12one2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);
                }
            }
        }

        [Test]
        public void RoleMany2OneContainedIn()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                foreach (var useOperator in this.UseOperator)
                {
                    // Extent over Class

                    // RelationType from Class to Class

                    // ContainedIn Extent over Shared Interface

                    // With filter
                    var inExtent = this.LocalExtent(C1Meta.ObjectType);
                    inExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C1Meta.ObjectType);
                        inExtentA.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(C1Meta.ObjectType);
                        inExtentB.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C1many2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, true, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C1many2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1C2many2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(I34Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I34Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I34Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C3Meta.ObjectType);
                    extent.Filter.AddContainedIn(C3Meta.C3C4many2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, true, true, true);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from Class to Interface

                    // ContainedIn Extent over Class
                    inExtent = this.LocalExtent(C2Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(C2Meta.ObjectType);
                        var inExtentB = this.LocalExtent(C2Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12many2one, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Shared Interface
                    inExtent = this.LocalExtent(I12Meta.ObjectType);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(I12Meta.ObjectType);
                        var inExtentB = this.LocalExtent(I12Meta.ObjectType);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1I12many2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);
                }
            }
        }

        [Test]
        public void RoleOne2OneEquals()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1C1one2one, this.c1B);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1C2one2one, this.c2B);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddEquals(I12Meta.I12C2one2one, this.c2A);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234C2one2one, this.c2A);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddEquals(C3Meta.C3C2one2one, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(I12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(C3Meta.C3C2one2one, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(S12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(C3Meta.C3C2one2one, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleOne2OneExist()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddExists(C1Meta.C1C1one2one);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddExists(C1Meta.C1C2one2one);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C3Meta.ObjectType);
                extent.Filter.AddExists(C3Meta.C3C4one2one);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, false, false, false);
 
                // Interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddExists(I12Meta.I12C2one2one);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddExists(S1234Meta.S1234C2one2one);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddExists(C3Meta.C3C2one2one);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(I12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddExists(C3Meta.C3C2one2one);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(S12Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddExists(C3Meta.C3C2one2one);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleOne2OneInstanceof()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddInstanceof(C1Meta.C1C1one2one, C1Meta.ObjectType);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddInstanceof(C1Meta.C1C2one2one, C2Meta.ObjectType);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddInstanceof(C1Meta.C1I12one2one, C2Meta.ObjectType);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddInstanceof(S1234Meta.S1234one2one, C2Meta.ObjectType);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, false, true, false);
                this.AssertC2(extent, false, false, true, false);
                this.AssertC3(extent, false, false, true, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Class
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddInstanceof(C1Meta.C1C2one2one, I2Meta.ObjectType);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddInstanceof(C1Meta.C1I12one2one, I2Meta.ObjectType);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddInstanceof(C1Meta.C1I12one2one, I12Meta.ObjectType);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddInstanceof(S1234Meta.S1234one2one, S1234Meta.ObjectType);

                Assert.AreEqual(9, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, false, false, false);

                // TODO: wrong relation
            }
        }

        [Test]
        public void RoleStringEqualsRole()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1AllorsString, C1Meta.C1AllorsString);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // TODO: Equal Role
            }
        }

        [Test]
        public void RoleStringEqualsValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Equal ""
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1AllorsString, string.Empty);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C3Meta.ObjectType);
                extent.Filter.AddEquals(C3Meta.C3AllorsString, string.Empty);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abra"
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C3Meta.ObjectType);
                extent.Filter.AddEquals(C3Meta.C3AllorsString, "Abra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abracadabra"
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(C1Meta.C1AllorsString, "Abracadabra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C3Meta.ObjectType);
                extent.Filter.AddEquals(C3Meta.C3AllorsString, "Abracadabra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, false, false);

                // Exclusive Interface

                // Equal ""
                extent = this.LocalExtent(I1Meta.ObjectType);
                extent.Filter.AddEquals(I1Meta.I1AllorsString, string.Empty);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(I3Meta.ObjectType);
                extent.Filter.AddEquals(I3Meta.I3AllorsString, string.Empty);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abra"
                extent = this.LocalExtent(I1Meta.ObjectType);
                extent.Filter.AddEquals(I1Meta.I1AllorsString, "Abra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(I3Meta.ObjectType);
                extent.Filter.AddEquals(I3Meta.I3AllorsString, "Abra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abracadabra"
                extent = this.LocalExtent(I1Meta.ObjectType);
                extent.Filter.AddEquals(I1Meta.I1AllorsString, "Abracadabra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(I3Meta.ObjectType);
                extent.Filter.AddEquals(I3Meta.I3AllorsString, "Abracadabra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, false, false);

                // Shared Interface

                // Equal ""
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddEquals(I12Meta.I12AllorsString, string.Empty);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(I34Meta.ObjectType);
                extent.Filter.AddEquals(I34Meta.I34AllorsString, string.Empty);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abra"
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(I12Meta.I12AllorsString, "Abra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(I23Meta.ObjectType);
                extent.Filter.AddEquals(I23Meta.I23AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C2Meta.ObjectType);
                extent.Filter.AddEquals(I23Meta.I23AllorsString, "Abra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C3Meta.ObjectType);
                extent.Filter.AddEquals(I23Meta.I23AllorsString, "Abra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(I34Meta.ObjectType);
                extent.Filter.AddEquals(I34Meta.I34AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                extent = this.LocalExtent(C3Meta.ObjectType);
                extent.Filter.AddEquals(I34Meta.I34AllorsString, "Abra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abracadabra"
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddEquals(I12Meta.I12AllorsString, "Abracadabra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddEquals(I12Meta.I12AllorsString, "Abracadabra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(I34Meta.ObjectType);
                extent.Filter.AddEquals(I34Meta.I34AllorsString, "Abracadabra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Super Interface

                // Equal ""
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234AllorsString, string.Empty);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abra"
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234AllorsString, "Abra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Equal "Abracadabra"
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddEquals(S1234Meta.S1234AllorsString, "Abracadabra");

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Class - Wrong RelationType

                // Equal ""
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddEquals(C2Meta.C2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(C2Meta.C2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abracadabra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(C2Meta.C2AllorsString, "Abracadabra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Equal ""
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(I2Meta.I2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(I2Meta.I2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abracadabra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(I2Meta.I2AllorsString, "Abracadabra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Equal ""
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(S2Meta.S2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(S2Meta.S2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abracadabra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(S2Meta.S2AllorsString, "Abracadabra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleStringExist()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddExists(C1Meta.C1AllorsString);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddExists(I12Meta.I12AllorsString);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddExists(S1234Meta.S1234AllorsString);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddExists(C2Meta.C2AllorsString);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddExists(I2Meta.I2AllorsString);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddExists(S2Meta.S2AllorsString);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleStringLike()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Like ""
                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddLike(C1Meta.C1AllorsString, string.Empty);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "Abra"
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddLike(C1Meta.C1AllorsString, "Abra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "Abracadabra"
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddLike(C1Meta.C1AllorsString, "Abracadabra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "notfound"
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddLike(C1Meta.C1AllorsString, "notfound");

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "%ra%"
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddLike(C1Meta.C1AllorsString, "%ra%");

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "%bra"
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddLike(C1Meta.C1AllorsString, "%bra");

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "%cadabra"
                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.Filter.AddLike(C1Meta.C1AllorsString, "%cadabra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Like ""
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddLike(I12Meta.I12AllorsString, string.Empty);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "Abra"
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddLike(I12Meta.I12AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "Abracadabra"
                extent = this.LocalExtent(I12Meta.ObjectType);
                extent.Filter.AddLike(I12Meta.I12AllorsString, "Abracadabra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Like ""
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddLike(S1234Meta.S1234AllorsString, string.Empty);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "Abra"
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddLike(S1234Meta.S1234AllorsString, "Abra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Like "Abracadabra"
                extent = this.LocalExtent(S1234Meta.ObjectType);
                extent.Filter.AddLike(S1234Meta.S1234AllorsString, "Abracadabra");

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true); 

                // Class - Wrong RelationType

                // Like ""
                extent = this.LocalExtent(C1Meta.ObjectType);

                var exception = false;
                try
                {
                    extent.Filter.AddLike(C2Meta.C2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLike(C2Meta.C2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abracadabra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLike(C2Meta.C2AllorsString, "Abracadabra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Like ""
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLike(I2Meta.I2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLike(I2Meta.I2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abracadabra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLike(I2Meta.I2AllorsString, "Abracadabra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Like ""
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLike(S2Meta.S2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLike(S2Meta.S2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abracadabra"
                extent = this.LocalExtent(C1Meta.ObjectType);

                exception = false;
                try
                {
                    extent.Filter.AddLike(S2Meta.S2AllorsString, "Abracadabra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public virtual void Shared()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                var sharedExtent = this.LocalExtent(C2Meta.ObjectType);
                sharedExtent.Filter.AddLike(C2Meta.C2AllorsString, "%");
                var firstExtent = this.LocalExtent(C1Meta.ObjectType);
                firstExtent.Filter.AddContainedIn(C1Meta.C1C2many2many, sharedExtent);
                var secondExtent = this.LocalExtent(C1Meta.ObjectType);
                secondExtent.Filter.AddContainedIn(C1Meta.C1C2many2many, sharedExtent);
                var intersectExtent = this.Session.Intersect(firstExtent, secondExtent);
                intersectExtent.ToArray(typeof(C1));
            }
        }

        [Test]
        public virtual void SortOne()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                this.c1B.C1AllorsString = "3";
                this.c1C.C1AllorsString = "1";
                this.c1D.C1AllorsString = "2";

                this.Session.Commit();

                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.AddSort(C1Meta.C1AllorsString);

                var sortedObjects = (C1[])extent.ToArray(typeof(C1));
                Assert.AreEqual(4, sortedObjects.Length);
                Assert.AreEqual(this.c1C, sortedObjects[0]);
                Assert.AreEqual(this.c1D, sortedObjects[1]);
                Assert.AreEqual(this.c1B, sortedObjects[2]);
                Assert.AreEqual(this.c1A, sortedObjects[3]);

                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.AddSort(C1Meta.C1AllorsString, SortDirection.Ascending);

                sortedObjects = (C1[])extent.ToArray(typeof(C1));
                Assert.AreEqual(4, sortedObjects.Length);
                Assert.AreEqual(this.c1C, sortedObjects[0]);
                Assert.AreEqual(this.c1D, sortedObjects[1]);
                Assert.AreEqual(this.c1B, sortedObjects[2]);
                Assert.AreEqual(this.c1A, sortedObjects[3]);

                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.AddSort(C1Meta.C1AllorsString, SortDirection.Descending);

                sortedObjects = (C1[])extent.ToArray(typeof(C1));
                Assert.AreEqual(4, sortedObjects.Length);
                Assert.AreEqual(this.c1A, sortedObjects[0]);
                Assert.AreEqual(this.c1B, sortedObjects[1]);
                Assert.AreEqual(this.c1D, sortedObjects[2]);
                Assert.AreEqual(this.c1C, sortedObjects[3]);

                foreach (var useOperator in this.UseOperator)
                {
                    if (useOperator)
                    {
                        var firstExtent = this.LocalExtent(C1Meta.ObjectType);
                        firstExtent.Filter.AddLike(C1Meta.C1AllorsString, "1");
                        var secondExtent = this.LocalExtent(C1Meta.ObjectType);
                        extent = this.Session.Union(firstExtent, secondExtent);
                        secondExtent.Filter.AddLike(C1Meta.C1AllorsString, "3");
                        extent.AddSort(C1Meta.C1AllorsString);

                        sortedObjects = (C1[])extent.ToArray(typeof(C1));
                        Assert.AreEqual(2, sortedObjects.Length);
                        Assert.AreEqual(this.c1C, sortedObjects[0]);
                        Assert.AreEqual(this.c1B, sortedObjects[1]);
                    }
                }
            }
        }

        [Test]
        public virtual void SortTwo()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                this.c1B.C1AllorsString = "a";
                this.c1C.C1AllorsString = "b";
                this.c1D.C1AllorsString = "a";

                this.c1B.C1AllorsInteger = 2;
                this.c1C.C1AllorsInteger = 1;
                this.c1D.C1AllorsInteger = 0;

                this.Session.Commit();

                var extent = this.LocalExtent(C1Meta.ObjectType);
                extent.AddSort(C1Meta.C1AllorsString);
                extent.AddSort(C1Meta.C1AllorsInteger);

                var sortedObjects = (C1[])extent.ToArray(typeof(C1));
                Assert.AreEqual(4, sortedObjects.Length);
                Assert.AreEqual(this.c1D, sortedObjects[0]);
                Assert.AreEqual(this.c1B, sortedObjects[1]);
                Assert.AreEqual(this.c1C, sortedObjects[2]);
                Assert.AreEqual(this.c1A, sortedObjects[3]);

                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.AddSort(C1Meta.C1AllorsString);
                extent.AddSort(C1Meta.C1AllorsInteger, SortDirection.Ascending);

                sortedObjects = (C1[])extent.ToArray(typeof(C1));
                Assert.AreEqual(4, sortedObjects.Length);
                Assert.AreEqual(this.c1D, sortedObjects[0]);
                Assert.AreEqual(this.c1B, sortedObjects[1]);
                Assert.AreEqual(this.c1C, sortedObjects[2]);
                Assert.AreEqual(this.c1A, sortedObjects[3]);

                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.AddSort(C1Meta.C1AllorsString);
                extent.AddSort(C1Meta.C1AllorsInteger, SortDirection.Descending);

                sortedObjects = (C1[])extent.ToArray(typeof(C1));
                Assert.AreEqual(4, sortedObjects.Length);
                Assert.AreEqual(this.c1B, sortedObjects[0]);
                Assert.AreEqual(this.c1D, sortedObjects[1]);
                Assert.AreEqual(this.c1C, sortedObjects[2]);
                Assert.AreEqual(this.c1A, sortedObjects[3]);

                extent = this.LocalExtent(C1Meta.ObjectType);
                extent.AddSort(C1Meta.C1AllorsString, SortDirection.Descending);
                extent.AddSort(C1Meta.C1AllorsInteger, SortDirection.Descending);

                sortedObjects = (C1[])extent.ToArray(typeof(C1));
                Assert.AreEqual(4, sortedObjects.Length);
                Assert.AreEqual(this.c1A, sortedObjects[0]);
                Assert.AreEqual(this.c1C, sortedObjects[1]);
                Assert.AreEqual(this.c1B, sortedObjects[2]);
                Assert.AreEqual(this.c1D, sortedObjects[3]);
            }
        }

        [Test]
        public void Hierarchy()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                var extent = this.LocalExtent(I4Meta.ObjectType);
                Assert.AreEqual(4, extent.Count);
                this.AssertC4(extent, true, true, true, true);
            }
        }

        [Test]
        public virtual void Union()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Filtered
                var firstExtent = this.LocalExtent(C1Meta.ObjectType);
                firstExtent.Filter.AddEquals(C1Meta.C1AllorsString, "Abra");

                var secondExtent = this.LocalExtent(C1Meta.ObjectType);
                secondExtent.Filter.AddLike(C1Meta.C1AllorsString, "Abracadabra");

                var extent = this.Session.Union(firstExtent, secondExtent);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Shortcut
                firstExtent = this.c1B.Strategy.GetCompositeRoles(C1Meta.C1C1one2many);
                secondExtent = this.c1B.Strategy.GetCompositeRoles(C1Meta.C1C1one2many);
                extent = this.Session.Union(firstExtent, secondExtent);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Different Classes
                firstExtent = this.LocalExtent(C1Meta.ObjectType);
                secondExtent = this.LocalExtent(C2Meta.ObjectType);

                var exceptionThrown = false;
                try
                {
                    this.Session.Union(firstExtent, secondExtent);
                }
                catch (ArgumentException)
                {
                    exceptionThrown = true;
                }

                Assert.IsTrue(exceptionThrown);

                // Name clashes
                Extent<Company> parents = this.LocalExtent(CompanyMeta.ObjectType);

                Extent<Company> children = this.LocalExtent(CompanyMeta.ObjectType);
                children.Filter.AddContainedIn(CompanyMeta.CompanyWhereChild, (Extent)parents);

                Extent<Company> allCompanies = this.Session.Union(parents, children);

                Extent<Person> persons = this.LocalExtent(PersonMeta.ObjectType);
                persons.Filter.AddContainedIn(PersonMeta.Company, (Extent)allCompanies);

                Assert.AreEqual(0, persons.Count);
            }
        }

        [Test]
        public void ValidateAssociationContainedIn()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Wrong Parameters
                var exceptionThrown = false;
                try
                {
                    var inExtent = this.LocalExtent(C1Meta.ObjectType);

                    var extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1AllorsBoolean.AssociationType, inExtent);
                    extent.ToArray();
                }
                catch (ArgumentException)
                {
                    exceptionThrown = true;
                }

                Assert.IsTrue(exceptionThrown);
            }
        }

        [Test]
        public void ValidateAssociationContains()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Wrong Parameters
                var exceptionThrown = false;
                try
                {
                    var extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContains(C1Meta.C1C2one2many.AssociationType, this.c1C);
                }
                catch (ArgumentException)
                {
                    exceptionThrown = true;
                }

                Assert.IsTrue(exceptionThrown);

                exceptionThrown = false;
                try
                {
                    var extent = this.LocalExtent(C2Meta.ObjectType);
                    extent.Filter.AddContains(C1Meta.C1C2one2one.AssociationType, this.c1C);
                }
                catch (ArgumentException)
                {
                    exceptionThrown = true;
                }

                Assert.IsTrue(exceptionThrown);
            }
        }

        [Test]
        public void ValidateAssociationEquals()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Wrong Parameters
                var exceptionThrown = false;
                try
                {
                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddEquals(C1Meta.C1C1many2many.AssociationType, this.c1B);
                }
                catch (ArgumentException)
                {
                    exceptionThrown = true;
                }

                Assert.IsTrue(exceptionThrown);

                exceptionThrown = false;
                try
                {
                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddEquals(C1Meta.C1C1many2one.AssociationType, this.c1B);
                }
                catch (ArgumentException)
                {
                    exceptionThrown = true;
                }

                Assert.IsTrue(exceptionThrown);
            }
        }

        [Test]
        public void ValidateAssociationExist()
        {
            //TODO:
        }

        [Test]
        public void ValidateAssociationNotExist()
        {
            // TODO:
        }

        [Test]
        public void ValidateRoleBetween()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Wrong Parameters
                var exceptionThrown = false;
                try
                {
                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddBetween(C1Meta.C1C2one2one, 0, 1);
                }
                catch (ArgumentException)
                {
                    exceptionThrown = true;
                }

                Assert.IsTrue(exceptionThrown);
            }
        }

        [Test]
        public void ValidateRoleContainsFilter()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Wrong Parameters
                var exceptionThrown = false;
                try
                {
                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContains(C1Meta.C1AllorsString, this.c2C);
                }
                catch (ArgumentException)
                {
                    exceptionThrown = true;
                }

                Assert.IsTrue(exceptionThrown);
            }
        }

        [Test]
        public void ValidateRoleEqualFilter()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Wrong Parameters
                var exceptionThrown = false;
                try
                {
                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddEquals(C1Meta.C1C2one2many, this.c2B);
                }
                catch (ArgumentException)
                {
                    exceptionThrown = true;
                }

                Assert.IsTrue(exceptionThrown);

                // Wrong Parameters
                exceptionThrown = false;
                try
                {
                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddEquals(C1Meta.C1C2many2many, this.c2B);
                }
                catch (ArgumentException)
                {
                    exceptionThrown = true;
                }

                Assert.IsTrue(exceptionThrown);
            }
        }

        [Test]
        public void ValidateRoleExistFilter()
        {
            // TODO:
        }

        [Test]
        public void ValidateRoleGreaterThanFilter()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Wrong Parameters
                var exceptionThrown = false;
                try
                {
                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddGreaterThan(C1Meta.C1C2one2one, 0);
                }
                catch (ArgumentException)
                {
                    exceptionThrown = true;
                }

                Assert.IsTrue(exceptionThrown);
            }
        }

        [Test]
        public void ValidateRoleInFilter()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Wrong Parameters
                var exceptionThrown = false;
                try
                {
                    var inExtent = this.LocalExtent(C1Meta.ObjectType);

                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddContainedIn(C1Meta.C1AllorsString, inExtent);
                }
                catch (ArgumentException)
                {
                    exceptionThrown = true;
                }

                Assert.IsTrue(exceptionThrown);
            }
        }

        [Test]
        public void ValidateRoleLessThanFilter()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Wrong Parameters
                var exceptionThrown = false;
                try
                {
                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddLessThan(C1Meta.C1C2one2one, 1);
                }
                catch (ArgumentException)
                {
                    exceptionThrown = true;
                }

                Assert.IsTrue(exceptionThrown);
            }
        }

        [Test]
        public void ValidateRoleLikeThanFilter()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Wrong Parameters
                var exceptionThrown = false;
                try
                {
                    var extent = this.LocalExtent(C1Meta.ObjectType);
                    extent.Filter.AddLike(C1Meta.C1AllorsBoolean, string.Empty);
                }
                catch (ArgumentException)
                {
                    exceptionThrown = true;
                }

                Assert.IsTrue(exceptionThrown);
            }
        }

        [Test]
        public void ValidateRoleNotExistFilter()
        {
            // TODO:
        }

        [Test]
        public void Shortcut()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Sortcut
                // Shortcut
                var extent = this.c1B.Strategy.GetCompositeRoles(C1Meta.C1C1one2many);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // With Filter
                // Shortcut
                extent = this.c1B.Strategy.GetCompositeRoles(C1Meta.C1C1one2many);
                extent.Filter.AddEquals(C1Meta.C1AllorsString, "Abracadabra");

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // With Sort
                // Shortcut
                extent = this.c1B.Strategy.GetCompositeRoles(C1Meta.C1C1one2many);
                extent.AddSort(C1Meta.C1AllorsInteger);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);
            }
        }

        [Test]
        public void RoleContainsMany2ManyAndContained()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // many2many contains
                var c1 = C1.Create(this.Session);
                var c2 = C2.Create(this.Session);
                var c3 = C3.Create(this.Session);

                c2.AddC3Many2Many(c3);
                c1.C1C2many2one = c2;

                var c2s = this.LocalExtent(C2Meta.ObjectType);
                c2s.Filter.AddContains(C2Meta.C3Many2Many, c3);

                Extent<C1> c1s = this.LocalExtent(C1Meta.ObjectType);
                c1s.Filter.AddContainedIn(C1Meta.C1C2many2one, (Extent)c2s);

                Assert.AreEqual(1, c1s.Count);
                Assert.AreEqual(c1, c1s[0]);
            }
        }

        [Test]
        public void RoleContainsOne2ManySharedRootClassAndContained()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // manymany contains
                var c2 = C2.Create(this.Session);
                var c3 = C3.Create(this.Session);
                var c4 = C4.Create(this.Session);

                c3.AddC3C4one2many(c4);
                c2.C3Many2One = c3;

                var c3s = this.LocalExtent(C3Meta.ObjectType);
                c3s.Filter.AddContains(C3Meta.C3C4one2many, c4);

                Extent<C2> c2s = this.LocalExtent(C2Meta.ObjectType);
                c2s.Filter.AddContainedIn(C2Meta.C3Many2One, (Extent)c3s);

                Assert.AreEqual(1, c2s.Count);
                Assert.AreEqual(c2, c2s[0]);
            }
        }

        [Test]
        public void AssociationContainsMany2ManyAndContained()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // many2many contains
                var c1 = C1.Create(this.Session);
                var c2 = C2.Create(this.Session);
                var c3 = C3.Create(this.Session);

                c3.AddC3C2many2many(c2);
                c1.C1C2many2one = c2;

                var c2s = this.LocalExtent(C2Meta.ObjectType);
                c2s.Filter.AddContains(C2Meta.C3sWhereC2many2many, c3);

                Extent<C1> c1s = this.LocalExtent(C1Meta.ObjectType);
                c1s.Filter.AddContainedIn(C1Meta.C1C2many2one, (Extent)c2s);

                Assert.AreEqual(1, c1s.Count);
                Assert.AreEqual(c1, c1s[0]);
            }
        }
        
        protected void Populate()
        {
            var population = new Population(this.Session);

            this.c1A = population.C1A;
            this.c1B = population.C1B;
            this.c1C = population.C1C;
            this.c1D = population.C1D;
            
            this.c2A = population.C2A;
            this.c2B = population.C2B;
            this.c2C = population.C2C;
            this.c2D = population.C2D;
            
            this.c3A = population.C3A;
            this.c3B = population.C3B;
            this.c3C = population.C3C;
            this.c3D = population.C3D;

            this.c4A = population.C4A;
            this.c4B = population.C4B;
            this.c4C = population.C4C;
            this.c4D = population.C4D;
        }

        // IDatabaseSession.Extent for Repositories and
        // IWorkspaceSession.WorkspaceExtent for Workspaces.
        protected virtual Extent LocalExtent(Composite objectType)
        {
            return this.Session.Extent(objectType);
        }

        private static Unit GetAllorsString(ObjectType objectType)
        {
            return (Unit)objectType.Domain.Find(UnitIds.StringId);
        }

        private void AssertC1(Extent extent, bool assert0, bool assert1, bool assert2, bool assert3)
        {
            if (assert0)
            {
                Assert.IsTrue(extent.Contains(this.c1A), "C1_1");
            }
            else
            {
                Assert.IsFalse(extent.Contains(this.c1A), "C1_1");
            }

            if (assert1)
            {
                Assert.IsTrue(extent.Contains(this.c1B), "C1_2");
            }
            else
            {
                Assert.IsFalse(extent.Contains(this.c1B), "C1_2");
            }

            if (assert2)
            {
                Assert.IsTrue(extent.Contains(this.c1C), "C1_3");
            }
            else
            {
                Assert.IsFalse(extent.Contains(this.c1C), "C1_3");
            }

            if (assert3)
            {
                Assert.IsTrue(extent.Contains(this.c1D), "C1_4");
            }
            else
            {
                Assert.IsFalse(extent.Contains(this.c1D), "C1_4");
            }
        }

        private void AssertC2(Extent extent, bool assert0, bool assert1, bool assert2, bool assert3)
        {
            if (assert0)
            {
                Assert.IsTrue(extent.Contains(this.c2A), "C2_1");
            }
            else
            {
                Assert.IsFalse(extent.Contains(this.c2A), "C2_1");
            }

            if (assert1)
            {
                Assert.IsTrue(extent.Contains(this.c2B), "C2_2");
            }
            else
            {
                Assert.IsFalse(extent.Contains(this.c2B), "C2_2");
            }

            if (assert2)
            {
                Assert.IsTrue(extent.Contains(this.c2C), "C2_3");
            }
            else
            {
                Assert.IsFalse(extent.Contains(this.c2C), "C2_3");
            }

            if (assert3)
            {
                Assert.IsTrue(extent.Contains(this.c2D), "C2_4");
            }
            else
            {
                Assert.IsFalse(extent.Contains(this.c2D), "C2_4");
            }
        }

        private void AssertC3(Extent extent, bool assert0, bool assert1, bool assert2, bool assert3)
        {
            if (assert0)
            {
                Assert.IsTrue(extent.Contains(this.c3A), "C3_1");
            }
            else
            {
                Assert.IsFalse(extent.Contains(this.c3A), "C3_1");
            }

            if (assert1)
            {
                Assert.IsTrue(extent.Contains(this.c3B), "C3_2");
            }
            else
            {
                Assert.IsFalse(extent.Contains(this.c3B), "C3_2");
            }

            if (assert2)
            {
                Assert.IsTrue(extent.Contains(this.c3C), "C3_3");
            }
            else
            {
                Assert.IsFalse(extent.Contains(this.c3C), "C3_3");
            }

            if (assert3)
            {
                Assert.IsTrue(extent.Contains(this.c3D), "C3_4");
            }
            else
            {
                Assert.IsFalse(extent.Contains(this.c3D), "C3_4");
            }
        }

        private void AssertC4(Extent extent, bool assert0, bool assert1, bool assert2, bool assert3)
        {
            if (assert0)
            {
                Assert.IsTrue(extent.Contains(this.c4A), "C4_1");
            }
            else
            {
                Assert.IsFalse(extent.Contains(this.c4A), "C4_1");
            }

            if (assert1)
            {
                Assert.IsTrue(extent.Contains(this.c4B), "C4_2");
            }
            else
            {
                Assert.IsFalse(extent.Contains(this.c4B), "C4_2");
            }

            if (assert2)
            {
                Assert.IsTrue(extent.Contains(this.c4C), "C4_3");
            }
            else
            {
                Assert.IsFalse(extent.Contains(this.c4C), "C4_3");
            }

            if (assert3)
            {
                Assert.IsTrue(extent.Contains(this.c4D), "C4_4");
            }
            else
            {
                Assert.IsFalse(extent.Contains(this.c4D), "C4_4");
            }
        }
    }
}