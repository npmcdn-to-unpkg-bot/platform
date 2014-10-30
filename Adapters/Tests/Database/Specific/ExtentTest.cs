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
                var extent = this.LocalExtent(Classes.C1);
                var all = extent.Filter.AddAnd();
                all.AddGreaterThan(RoleTypes.C1AllorsInteger, 0);
                all.AddLessThan(RoleTypes.C1AllorsInteger, 2);

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
                (extent = this.LocalExtent(Interfaces.I12)).Filter.AddAnd()
                    .AddGreaterThan(RoleTypes.I12AllorsInteger, 0)
                    .AddLessThan(RoleTypes.I12AllorsInteger, 2);

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
                (extent = this.LocalExtent(Interfaces.S1234)).Filter.AddAnd()
                    .AddGreaterThan(RoleTypes.S1234AllorsInteger, 0)
                    .AddLessThan(RoleTypes.S1234AllorsInteger, 2);

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
                var extent = this.LocalExtent(Classes.C1);
                var all = extent.Filter.AddAnd();
                all.AddLessThan(RoleTypes.C1AllorsInteger, 2);

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
                (extent = this.LocalExtent(Interfaces.I12)).Filter.AddAnd().AddLessThan(RoleTypes.I12AllorsInteger, 2);

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
                (extent = this.LocalExtent(Interfaces.S1234)).Filter.AddAnd()
                    .AddLessThan(RoleTypes.S1234AllorsInteger, 2);

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
                        var inExtent = this.LocalExtent(Classes.C1);
                        inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(Classes.C1);
                            inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                            var inExtentB = this.LocalExtent(Classes.C1);
                            inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        var extent = this.LocalExtent(Classes.C2);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(AssociationTypes.C1C2many2many, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(AssociationTypes.C1C2many2many, inExtent);
                        }

                        Assert.AreEqual(0, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, false, false, false);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // Full
                        inExtent = this.LocalExtent(Classes.C1);
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(Classes.C1);
                            var inExtentB = this.LocalExtent(Classes.C1);
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(Classes.C2);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(AssociationTypes.C1C2many2many, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(AssociationTypes.C1C2many2many, inExtent);
                        }

                        Assert.AreEqual(3, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, true, true, true);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // Filtered
                        inExtent = this.LocalExtent(Classes.C1);
                        inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(Classes.C1);
                            inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                            var inExtentB = this.LocalExtent(Classes.C1);
                            inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(Classes.C2);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(AssociationTypes.C1C2many2many, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(AssociationTypes.C1C2many2many, inExtent);
                        }

                        Assert.AreEqual(1, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, true, false, false);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // ContainedIn Extent over Interface
                        // Empty
                        inExtent = this.LocalExtent(Interfaces.I12);
                        inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(Interfaces.I12);
                            inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                            var inExtentB = this.LocalExtent(Interfaces.I12);
                            inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(Classes.C2);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(AssociationTypes.C1C2many2many, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(AssociationTypes.C1C2many2many, inExtent);
                        }

                        Assert.AreEqual(0, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, false, false, false);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // Full
                        inExtent = this.LocalExtent(Interfaces.I12);
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(Interfaces.I12);
                            var inExtentB = this.LocalExtent(Interfaces.I12);
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(Classes.C2);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(AssociationTypes.C1C2many2many, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(AssociationTypes.C1C2many2many, inExtent);
                        }

                        Assert.AreEqual(3, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, true, true, true);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // Filtered
                        inExtent = this.LocalExtent(Interfaces.I12);
                        inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(Interfaces.I12);
                            inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                            var inExtentB = this.LocalExtent(Interfaces.I12);
                            inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(Classes.C2);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(AssociationTypes.C1C2many2many, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(AssociationTypes.C1C2many2many, inExtent);
                        }

                        Assert.AreEqual(1, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, true, false, false);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // RelationType from Class to Interface

                        // ContainedIn Extent over Class
                        // Empty
                        inExtent = this.LocalExtent(Classes.C1);
                        inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(Classes.C1);
                            inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                            var inExtentB = this.LocalExtent(Classes.C1);
                            inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(Classes.C2);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(AssociationTypes.C1I12many2many, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(AssociationTypes.C1I12many2many, inExtent);
                        }

                        Assert.AreEqual(0, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, false, false, false);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // Full
                        inExtent = this.LocalExtent(Classes.C1);
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(Classes.C1);
                            var inExtentB = this.LocalExtent(Classes.C1);
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(Classes.C2);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(AssociationTypes.C1I12many2many, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(AssociationTypes.C1I12many2many, inExtent);
                        }

                        Assert.AreEqual(3, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, true, true, true);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // Filtered
                        inExtent = this.LocalExtent(Classes.C1);
                        inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(Classes.C1);
                            inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                            var inExtentB = this.LocalExtent(Classes.C1);
                            inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(Classes.C2);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(AssociationTypes.C1I12many2many, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(AssociationTypes.C1I12many2many, inExtent);
                        }

                        Assert.AreEqual(1, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, true, false, false);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // ContainedIn Extent over Interface
                        // Empty
                        inExtent = this.LocalExtent(Interfaces.I12);
                        inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(Interfaces.I12);
                            inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                            var inExtentB = this.LocalExtent(Interfaces.I12);
                            inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(Classes.C2);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(AssociationTypes.C1I12many2many, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(AssociationTypes.C1I12many2many, inExtent);
                        }

                        Assert.AreEqual(0, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, false, false, false);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // Full
                        inExtent = this.LocalExtent(Interfaces.I12);
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(Interfaces.I12);
                            var inExtentB = this.LocalExtent(Interfaces.I12);
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(Classes.C2);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(AssociationTypes.C1I12many2many, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(AssociationTypes.C1I12many2many, inExtent);
                        }

                        Assert.AreEqual(3, extent.Count);
                        this.AssertC1(extent, false, false, false, false);
                        this.AssertC2(extent, false, true, true, true);
                        this.AssertC3(extent, false, false, false, false);
                        this.AssertC4(extent, false, false, false, false);

                        // Filtered
                        inExtent = this.LocalExtent(Interfaces.I12);
                        inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        if (useOperator)
                        {
                            var inExtentA = this.LocalExtent(Interfaces.I12);
                            inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                            var inExtentB = this.LocalExtent(Interfaces.I12);
                            inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                            inExtent = this.Session.Union(inExtentA, inExtentB);
                        }

                        extent = this.LocalExtent(Classes.C2);
                        if (useEnumerable)
                        {
                            var enumerable = (IEnumerable<IObject>)((Extent<IObject>)inExtent);
                            extent.Filter.AddContainedIn(AssociationTypes.C1I12many2many, enumerable);
                        }
                        else
                        {
                            extent.Filter.AddContainedIn(AssociationTypes.C1I12many2many, inExtent);
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
                var extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddContains(AssociationTypes.C1C2many2many, this.c1C);

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

                extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddContains(AssociationTypes.C1C2many2many, this.c1C);
                extent.Filter.AddContains(AssociationTypes.C1C2many2many, this.c1D);

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
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddContains(AssociationTypes.C1I12many2many, this.c1C);

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
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddContains(AssociationTypes.S1234S1234many2many, this.c1B);

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
                var extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddExists(AssociationTypes.C1C2many2many);

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
                extent = this.LocalExtent(Interfaces.I2);
                extent.Filter.AddExists(AssociationTypes.I1I2many2many);

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
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddExists(AssociationTypes.S1234S1234many2many);

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
                extent = this.LocalExtent(Classes.C2);

                var exception = false;
                try
                {
                    extent.Filter.AddExists(AssociationTypes.C1C1many2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.I12);

                exception = false;
                try
                {
                    extent.Filter.AddExists(AssociationTypes.I12I34many2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.S12);

                exception = false;
                try
                {
                    extent.Filter.AddExists(AssociationTypes.S1234S1234many2many);
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
                    var inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1C2many2one, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1C2many2one, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, true, true, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1C2many2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, true, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1C2many2one, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1C2many2one, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, true, true, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1C2many2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, true, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from Class to Interface

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1I12many2one, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1I12many2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, true, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1I12many2one, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1I12many2one, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1I12many2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, true, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1I12many2one, inExtent);

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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddContains(AssociationTypes.C1C1many2one, this.c1C);

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

                extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddContains(AssociationTypes.C1C2many2one, this.c1C);

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

                extent = this.LocalExtent(Classes.C4);
                extent.Filter.AddContains(AssociationTypes.C3C4many2one, this.c3C);

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
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddContains(AssociationTypes.C1I12many2one, this.c1C);

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
                    var inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1C2one2many, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1C2one2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1C2one2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, true, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1C2one2many, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1C2one2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1C2one2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, true, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from Class to Interface

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1I12one2many, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1I12one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1I12one2many, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1I12one2many, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1I12one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1I12one2many, inExtent);

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
                var extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddEquals(AssociationTypes.C1C2one2many, this.c1B);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddEquals(AssociationTypes.C1C2one2many, this.c1C);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(Interfaces.I2);
                extent.Filter.AddEquals(AssociationTypes.I1I2one2many, this.c1B);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Interfaces.I2);
                extent.Filter.AddEquals(AssociationTypes.I1I2one2many, this.c1C);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddEquals(AssociationTypes.S1234S1234one2many, this.c1B);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddEquals(AssociationTypes.S1234S1234one2many, this.c3C);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddEquals(AssociationTypes.C3C2one2many, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.I12);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(AssociationTypes.C3C2one2many, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.S12);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(AssociationTypes.C3C2one2many, this.c2A);
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
                var extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddExists(AssociationTypes.C1C2one2many);

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
                extent = this.LocalExtent(Interfaces.I2);
                extent.Filter.AddExists(AssociationTypes.I1I2one2many);

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
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddExists(AssociationTypes.S1234S1234one2many);

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
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddExists(AssociationTypes.C3C2one2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.I12);

                exception = false;
                try
                {
                    extent.Filter.AddExists(AssociationTypes.C3C2one2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.S12);

                exception = false;
                try
                {
                    extent.Filter.AddExists(AssociationTypes.C3C2one2many);
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
                var extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddInstanceof(AssociationTypes.C1C2one2many, Classes.C1);

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
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddInstanceof(AssociationTypes.C1I12one2many, Classes.C1);

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
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddInstanceof(AssociationTypes.S1234S1234one2many, Classes.C1);

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
                    var inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(AssociationTypes.C1C1one2one, inExtent);

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

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1C2one2one, inExtent);

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

                    inExtent = this.LocalExtent(Classes.C3);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C3);
                        var inExtentB = this.LocalExtent(Classes.C3);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C4);
                    extent.Filter.AddContainedIn(AssociationTypes.C3C4one2one, inExtent);

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
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(AssociationTypes.C1C1one2one, inExtent);

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

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1C2one2one, inExtent);

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

                    inExtent = this.LocalExtent(Interfaces.I34);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I34);
                        var inExtentB = this.LocalExtent(Interfaces.I34);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C4);
                    extent.Filter.AddContainedIn(AssociationTypes.C3C4one2one, inExtent);

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
                    inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.I12C2one2one, inExtent);

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
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.I12C2one2one, inExtent);

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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddEquals(AssociationTypes.C1C1one2one, this.c1B);

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

                extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddEquals(AssociationTypes.C1C2one2one, this.c1B);

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

                extent = this.LocalExtent(Classes.C4);
                extent.Filter.AddEquals(AssociationTypes.C3C4one2one, this.c3B);

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
                extent = this.LocalExtent(Interfaces.I2);
                extent.Filter.AddEquals(AssociationTypes.I1I2one2one, this.c1B);

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
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddEquals(AssociationTypes.S1234S1234one2one, this.c1C);

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
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddEquals(AssociationTypes.C3C2one2one, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.I12);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(AssociationTypes.C3C2one2one, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.S12);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(AssociationTypes.C3C2one2one, this.c2A);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddExists(AssociationTypes.C1C1one2one);

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

                extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddExists(AssociationTypes.C1C2one2one);

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

                extent = this.LocalExtent(Classes.C4);
                extent.Filter.AddExists(AssociationTypes.C3C4one2one);

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
                extent = this.LocalExtent(Interfaces.I2);
                extent.Filter.AddExists(AssociationTypes.I1I2one2one);

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
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddExists(AssociationTypes.S1234S1234one2one);

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
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddExists(AssociationTypes.C3C2one2one);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.I12);

                exception = false;
                try
                {
                    extent.Filter.AddExists(AssociationTypes.C3C2one2one);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.S12);

                exception = false;
                try
                {
                    extent.Filter.AddExists(AssociationTypes.C3C2one2one);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddInstanceof(AssociationTypes.C1C1one2one, Classes.C1);

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

                extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddInstanceof(AssociationTypes.C1C2one2one, Classes.C1);

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

                extent = this.LocalExtent(Classes.C4);
                extent.Filter.AddInstanceof(AssociationTypes.C3C4one2one, Classes.C3);

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
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddInstanceof(AssociationTypes.C1I12one2one, Classes.C1);

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
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddInstanceof(AssociationTypes.S1234S1234one2one, Classes.C1);

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
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddInstanceof(AssociationTypes.C1C1one2one, Interfaces.I1);

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

                extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddInstanceof(AssociationTypes.C1C2one2one, Interfaces.I1);

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

                extent = this.LocalExtent(Classes.C4);
                extent.Filter.AddInstanceof(AssociationTypes.C3C4one2one, Interfaces.I3);

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
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddInstanceof(AssociationTypes.C1I12one2one, Interfaces.I1);

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
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddInstanceof(AssociationTypes.S1234S1234one2one, Interfaces.S1234);

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
                var extent = this.LocalExtent(Classes.C1);

                extent.Filter.AddLike(RoleTypes.C1AllorsString, "%nada%");

                var any1 = extent.Filter.AddOr();
                any1.AddGreaterThan(RoleTypes.C1AllorsInteger, 0);
                any1.AddLessThan(RoleTypes.C1AllorsInteger, 3);

                var any2 = extent.Filter.AddOr();
                any2.AddGreaterThan(RoleTypes.C1AllorsInteger, 0);
                any2.AddLessThan(RoleTypes.C1AllorsInteger, 3);

                extent.ToArray(typeof(C1));

                // Role + Value for Shared Interface
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddExists(RoleTypes.C1C1one2many);

                extent.Filter.AddExists(RoleTypes.I12AllorsInteger);
                extent.Filter.AddNot().AddExists(RoleTypes.I12AllorsInteger);
                extent.Filter.AddEquals(RoleTypes.I12AllorsInteger, 0);
                extent.Filter.AddLessThan(RoleTypes.I12AllorsInteger, 0);
                extent.Filter.AddGreaterThan(RoleTypes.I12AllorsInteger, 0);
                extent.Filter.AddBetween(RoleTypes.I12AllorsInteger, 0, 1);

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
                var firstExtent = this.LocalExtent(Classes.C2);
                firstExtent.Filter.AddLike(RoleTypes.I12AllorsString, "Abra%");

                var secondExtent = this.LocalExtent(Classes.C2);
                secondExtent.Filter.AddLike(RoleTypes.I12AllorsString, "Abracadabra");

                var inExtent = this.Session.Except(firstExtent, secondExtent);

                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddContainedIn(RoleTypes.C1C2one2many, inExtent);

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
                firstExtent = this.LocalExtent(Classes.C1);
                firstExtent.Filter.AddLike(RoleTypes.I12AllorsString, "Abra%");

                secondExtent = this.LocalExtent(Classes.C1);
                secondExtent.Filter.AddLike(RoleTypes.I12AllorsString, "Abracadabra");

                inExtent = this.Session.Except(firstExtent, secondExtent);

                extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddContainedIn(AssociationTypes.C1C2one2many, inExtent);

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
                firstExtent = this.LocalExtent(Classes.C1);
                firstExtent.Filter.AddNot().AddExists(RoleTypes.C1AllorsString);

                secondExtent = this.LocalExtent(Classes.C1);
                secondExtent.Filter.AddLike(RoleTypes.C1AllorsString, "Abracadabra");

                var unionExtent = this.Session.Union(firstExtent, secondExtent);
                var topExtent = this.LocalExtent(Classes.C1);

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
                firstExtent = this.LocalExtent(Classes.C1);
                firstExtent.Filter.AddExists(RoleTypes.C1AllorsString);

                secondExtent = this.LocalExtent(Classes.C1);
                secondExtent.Filter.AddLike(RoleTypes.C1AllorsString, "Abracadabra");

                var intersectExtent = this.Session.Intersect(firstExtent, secondExtent);
                topExtent = this.LocalExtent(Classes.C1);

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
                    this.LocalExtent(Classes.C1),
                    this.LocalExtent(Classes.C1));
                secondExtent = this.Session.Intersect(
                    this.LocalExtent(Classes.C1),
                    this.LocalExtent(Classes.C1));

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
                    this.LocalExtent(Classes.C1),
                    this.LocalExtent(Classes.C1));
                secondExtent = this.Session.Intersect(
                    this.LocalExtent(Classes.C1),
                    this.LocalExtent(Classes.C1));

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

                var extent = this.LocalExtent(Interfaces.InterfaceWithoutConcreteClass);

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
                var extent = this.LocalExtent(Classes.C1);

                extent.Filter.AddEquals(this.c1A);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent.Filter.AddEquals(this.c1B);

                Assert.AreEqual(0, extent.Count);

                // interface
                extent = this.LocalExtent(Interfaces.I1);

                extent.Filter.AddEquals(this.c1A);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent.Filter.AddEquals(this.c1B);

                Assert.AreEqual(0, extent.Count);

                // shared interface
                extent = this.LocalExtent(Interfaces.I12);

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
                var extent = this.LocalExtent(Classes.C1);
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
                extent = this.LocalExtent(Interfaces.I1);
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
                extent = this.LocalExtent(Interfaces.I12);
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
                var extent = this.LocalExtent(Classes.C1);
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
                extent = this.LocalExtent(Interfaces.I1);
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
                extent = this.LocalExtent(Interfaces.I12);
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
                var extent = this.LocalExtent(Classes.C1);
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
                extent = this.LocalExtent(Interfaces.I1);
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
                extent = this.LocalExtent(Interfaces.I12);
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
                var firstExtent = this.LocalExtent(Classes.C1);

                var secondExtent = this.LocalExtent(Classes.C1);
                secondExtent.Filter.AddLike(RoleTypes.C1AllorsString, "Abracadabra");

                var extent = this.Session.Except(firstExtent, secondExtent);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, true, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // interface
                firstExtent = this.LocalExtent(Interfaces.I12);
                firstExtent.Filter.AddLike(RoleTypes.I12AllorsString, "Abra%");

                secondExtent = this.LocalExtent(Interfaces.I12);
                secondExtent.Filter.AddLike(RoleTypes.I12AllorsString, "Abracadabra");

                extent = this.Session.Except(firstExtent, secondExtent);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Shortcut
                firstExtent = this.c1B.Strategy.GetCompositeRoles(RoleTypes.C1C1one2many);
                secondExtent = this.c1B.Strategy.GetCompositeRoles(RoleTypes.C1C1one2many);
                extent = this.Session.Except(firstExtent, secondExtent);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                firstExtent = this.c1B.Strategy.GetCompositeRoles(RoleTypes.C1C1one2many);
                secondExtent = this.c1C.Strategy.GetCompositeRoles(RoleTypes.C1C1one2many);
                extent = this.Session.Except(firstExtent, secondExtent);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Different Classes
                firstExtent = this.LocalExtent(Classes.C1);
                secondExtent = this.LocalExtent(Classes.C2);

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

                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddExists(RoleTypes.C1AllorsString);
                Assert.AreEqual(3, extent.Count);
                extent.Filter.AddLike(RoleTypes.C1AllorsString, "Abra");
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddInstanceof(Classes.C1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class + Interface
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddInstanceof(Classes.C1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class + Shared Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddInstanceof(Classes.C1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Inteface + Class
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddInstanceof(Interfaces.I1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface + Interface
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddInstanceof(Interfaces.I1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddInstanceof(Interfaces.I12);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, true, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface + Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddInstanceof(Interfaces.I1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddInstanceof(Interfaces.I12);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, true, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddInstanceof(Interfaces.S1234);

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
                var firstExtent = this.LocalExtent(Classes.C1);

                var secondExtent = this.LocalExtent(Classes.C1);
                secondExtent.Filter.AddLike(RoleTypes.C1AllorsString, "Abracadabra");

                var extent = this.Session.Intersect(firstExtent, secondExtent);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Shortcut
                firstExtent = this.c1B.Strategy.GetCompositeRoles(RoleTypes.C1C1one2many);
                secondExtent = this.c1B.Strategy.GetCompositeRoles(RoleTypes.C1C1one2many);
                extent = this.Session.Intersect(firstExtent, secondExtent);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                firstExtent = this.c1B.Strategy.GetCompositeRoles(RoleTypes.C1C1one2many);
                secondExtent = this.c1C.Strategy.GetCompositeRoles(RoleTypes.C1C1one2many);
                extent = this.Session.Intersect(firstExtent, secondExtent);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);
                
                // Different Classes
                firstExtent = this.LocalExtent(Classes.C1);
                secondExtent = this.LocalExtent(Classes.C2);

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
                    var extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddEquals(RoleTypes.S1234ClassName, "c1");
                    extent.Filter.AddContains(RoleTypes.C1C3one2many, this.c3B);
                    extent.AddSort(RoleTypes.S1234ClassName);
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
                var extent = this.LocalExtent(Classes.C1);
                var none = extent.Filter.AddNot().AddAnd();
                none.AddGreaterThan(RoleTypes.C1AllorsInteger, 0);
                none.AddLessThan(RoleTypes.C1AllorsInteger, 2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                (extent = this.LocalExtent(Interfaces.I12)).Filter.AddNot()
                    .AddAnd()
                    .AddGreaterThan(RoleTypes.I12AllorsInteger, 0)
                    .AddLessThan(RoleTypes.I12AllorsInteger, 2);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                (extent = this.LocalExtent(Interfaces.S1234)).Filter.AddNot()
                    .AddAnd()
                    .AddGreaterThan(RoleTypes.S1234AllorsInteger, 0)
                    .AddLessThan(RoleTypes.S1234AllorsInteger, 2);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Class
                extent = this.LocalExtent(Classes.C1);
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
                    var inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C2many2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C2many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C2many2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C2many2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C2many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C2many2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from C1 to I12

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1I12many2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1I12many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1I12many2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1I12many2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1I12many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1I12many2many, inExtent);

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
                var extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddNot().AddExists(AssociationTypes.C1C2many2many);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(Interfaces.I2);
                extent.Filter.AddNot().AddExists(AssociationTypes.I1I2many2many);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddExists(AssociationTypes.S1234S1234many2many);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, true, false, false, false);
                this.AssertC4(extent, true, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(Classes.C2);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(AssociationTypes.C1C1many2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.I12);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(AssociationTypes.I12I34many2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.S12);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(AssociationTypes.S1234S1234many2many);
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
                    var inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C2many2one, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C2many2one, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C2many2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C2many2one, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C2many2one, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C2many2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from Class to Interface

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1I12many2one, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1I12many2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, false, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1I12many2one, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1I12many2one, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1I12many2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, false, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1I12many2one, inExtent);

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
                    var inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C2one2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C2one2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C2one2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C2one2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C2one2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C2one2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from C1 to I12

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1I12one2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1I12one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1I12one2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1I12one2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, true, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1I12one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, true, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1I12one2many, inExtent);

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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddEquals(AssociationTypes.C1C1one2many, this.c1B);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, true, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddEquals(AssociationTypes.C1C1one2many, this.c1C);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, true, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddNot().AddEquals(AssociationTypes.C1C2one2many, this.c1B);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddNot().AddEquals(AssociationTypes.C1C2one2many, this.c1C);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(Interfaces.I2);
                extent.Filter.AddNot().AddEquals(AssociationTypes.I1I2one2many, this.c1B);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Interfaces.I2);
                extent.Filter.AddNot().AddEquals(AssociationTypes.I1I2one2many, this.c1C);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddEquals(AssociationTypes.S1234S1234one2many, this.c1B);

                Assert.AreEqual(15, extent.Count);
                this.AssertC1(extent, true, false, true, true);
                this.AssertC2(extent, true, true, true, true);
                this.AssertC3(extent, true, true, true, true);
                this.AssertC4(extent, true, true, true, true);

                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddEquals(AssociationTypes.S1234S1234one2many, this.c3C);

                Assert.AreEqual(14, extent.Count);
                this.AssertC1(extent, true, true, false, false);
                this.AssertC2(extent, true, true, true, true);
                this.AssertC3(extent, true, true, true, true);
                this.AssertC4(extent, true, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(AssociationTypes.C3C2one2many, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.I12);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(AssociationTypes.C3C2one2many, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.S12);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(AssociationTypes.C3C2one2many, this.c2A);
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
                var extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddNot().AddExists(AssociationTypes.C1C2one2many);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(Interfaces.I2);
                extent.Filter.AddNot().AddExists(AssociationTypes.I1I2one2many);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddExists(AssociationTypes.S1234S1234one2many);

                Assert.AreEqual(13, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, true, true, true, true);
                this.AssertC3(extent, true, true, true, true);
                this.AssertC4(extent, true, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(AssociationTypes.C3C2one2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.I12);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(AssociationTypes.C3C2one2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.S1234);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(AssociationTypes.C3C2one2many);
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
                    var inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C1one2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C1one2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from C1 to C2

                    // ContainedIn Extent over Class
                    inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C2one2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Class
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1C2one2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from C3 to C4

                    // ContainedIn Extent over Class
                    inExtent = this.LocalExtent(Classes.C3);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C3);
                        var inExtentB = this.LocalExtent(Classes.C3);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C4);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C3C4one2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, true, false, false, false);

                    // ContainedIn Extent over Interface
                    inExtent = this.LocalExtent(Interfaces.I34);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I34);
                        var inExtentB = this.LocalExtent(Interfaces.I34);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C4);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C3C4one2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, true, false, false, false);

                    // RelationType from I12 to C2

                    // ContainedIn Extent over Class
                    inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.I12C2one2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, true, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.I12C2one2one, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Extent over Interface

                    // RelationType from C1 to I12

                    // ContainedIn Extent over Class
                    inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Interfaces.I12);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1I12one2one, inExtent);

                    Assert.AreEqual(5, extent.Count);
                    this.AssertC1(extent, true, false, true, true);
                    this.AssertC2(extent, true, false, false, true);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Interfaces.I12);
                    extent.Filter.AddNot().AddContainedIn(AssociationTypes.C1I12one2one, inExtent);

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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddEquals(AssociationTypes.C1C1one2one, this.c1B);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, true, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddNot().AddEquals(AssociationTypes.C1C2one2one, this.c1B);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C4);
                extent.Filter.AddNot().AddEquals(AssociationTypes.C3C4one2one, this.c3B);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, true, false, true, true);

                // Interface
                extent = this.LocalExtent(Interfaces.I2);
                extent.Filter.AddNot().AddEquals(AssociationTypes.I1I2one2one, this.c1B);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddEquals(AssociationTypes.S1234S1234one2one, this.c1C);

                Assert.AreEqual(15, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, true, false, true, true);
                this.AssertC3(extent, true, true, true, true);
                this.AssertC4(extent, true, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(AssociationTypes.C3C2one2one, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.I12);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(AssociationTypes.C3C2one2one, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.S12);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(AssociationTypes.C3C2one2one, this.c2A);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddExists(AssociationTypes.C1C1one2one);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddNot().AddExists(AssociationTypes.C1C2one2one);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddNot().AddExists(AssociationTypes.C1C2one2one);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C4);
                extent.Filter.AddNot().AddExists(AssociationTypes.C3C4one2one);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, true, false, false, false);

                // Interface
                extent = this.LocalExtent(Interfaces.I2);
                extent.Filter.AddNot().AddExists(AssociationTypes.I1I2one2one);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddExists(AssociationTypes.S1234S1234one2one);

                Assert.AreEqual(7, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, true, false, false, false);
                this.AssertC4(extent, true, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(AssociationTypes.C3C2one2one);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.I12);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(AssociationTypes.C3C2one2one);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.S1234);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(AssociationTypes.C3C2one2one);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddInstanceof(AssociationTypes.C1C1one2one, Classes.C1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddNot().AddInstanceof(AssociationTypes.C1C2one2one, Classes.C1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C4);
                extent.Filter.AddNot().AddInstanceof(AssociationTypes.C3C4one2one, Classes.C3);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, true, false, false, false);

                // Interface
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddInstanceof(AssociationTypes.C1I12one2one, Classes.C1);

                Assert.AreEqual(5, extent.Count);
                this.AssertC1(extent, true, false, true, true);
                this.AssertC2(extent, true, false, false, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddInstanceof(AssociationTypes.S1234S1234one2one, Classes.C1);

                Assert.AreEqual(13, extent.Count);
                this.AssertC1(extent, true, false, true, true);
                this.AssertC2(extent, true, false, true, true);
                this.AssertC3(extent, true, false, true, true);
                this.AssertC4(extent, true, true, true, true);

                // Interface

                // Class
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddInstanceof(AssociationTypes.C1C1one2one, Interfaces.I1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddNot().AddInstanceof(AssociationTypes.C1C2one2one, Interfaces.I1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C4);
                extent.Filter.AddNot().AddInstanceof(AssociationTypes.C3C4one2one, Interfaces.I3);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, true, false, false, false);

                // Interface
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddInstanceof(AssociationTypes.C1I12one2one, Interfaces.I1);

                Assert.AreEqual(5, extent.Count);
                this.AssertC1(extent, true, false, true, true);
                this.AssertC2(extent, true, false, false, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddInstanceof(AssociationTypes.S1234S1234one2one, Interfaces.S1234);

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
                var extent = this.LocalExtent(Classes.C1);
                var none = extent.Filter.AddNot().AddOr();
                none.AddGreaterThan(RoleTypes.C1AllorsInteger, 1);
                none.AddLessThan(RoleTypes.C1AllorsInteger, 1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                (extent = this.LocalExtent(Interfaces.I12)).Filter.AddNot()
                    .AddOr()
                    .AddGreaterThan(RoleTypes.I12AllorsInteger, 1)
                    .AddLessThan(RoleTypes.I12AllorsInteger, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                (extent = this.LocalExtent(Interfaces.S1234)).Filter.AddNot()
                    .AddOr()
                    .AddGreaterThan(RoleTypes.S1234AllorsInteger, 1)
                    .AddLessThan(RoleTypes.S1234AllorsInteger, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Class
                extent = this.LocalExtent(Classes.C1);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddBetween(RoleTypes.C1AllorsInteger, -10, 0);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddBetween(RoleTypes.C1AllorsInteger, 0, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddBetween(RoleTypes.C1AllorsInteger, 1, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 3 and 10
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddBetween(RoleTypes.C1AllorsInteger, 3, 10);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Between -10 and 0
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddBetween(RoleTypes.I12AllorsInteger, -10, 0);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddBetween(RoleTypes.I12AllorsInteger, 0, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddBetween(RoleTypes.I12AllorsInteger, 1, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 3 and 10
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddBetween(RoleTypes.I12AllorsInteger, 3, 10);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Between -10 and 0
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddBetween(RoleTypes.S1234AllorsInteger, -10, 0);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Between 0 and 1
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddBetween(RoleTypes.S1234AllorsInteger, 0, 1);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Between 1 and 2
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddBetween(RoleTypes.S1234AllorsInteger, 1, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 3 and 10
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddBetween(RoleTypes.S1234AllorsInteger, 3, 10);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Class - Wrong RelationType

                // Between -10 and 0
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddBetween(RoleTypes.C2AllorsInteger, -10, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 0 and 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddBetween(RoleTypes.C2AllorsInteger, 0, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 1 and 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddBetween(RoleTypes.C2AllorsInteger, 1, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 3 and 10
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddBetween(RoleTypes.C2AllorsInteger, 3, 10);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddLessThan(RoleTypes.C1AllorsInteger, 1);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddLessThan(RoleTypes.C1AllorsInteger, 2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 3
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddLessThan(RoleTypes.C1AllorsInteger, 3);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Less Than 1
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddLessThan(RoleTypes.I12AllorsInteger, 1);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddLessThan(RoleTypes.I12AllorsInteger, 2);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 3
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddLessThan(RoleTypes.I12AllorsInteger, 3);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Less Than 1
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddLessThan(RoleTypes.S1234AllorsInteger, 1);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Less Than 2
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddLessThan(RoleTypes.S1234AllorsInteger, 2);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Less Than 3
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddLessThan(RoleTypes.S1234AllorsInteger, 3);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddLessThan(RoleTypes.C2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLessThan(RoleTypes.C2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLessThan(RoleTypes.C2AllorsInteger, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLessThan(RoleTypes.I2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLessThan(RoleTypes.I2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLessThan(RoleTypes.I2AllorsInteger, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLessThan(RoleTypes.S2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLessThan(RoleTypes.S2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLessThan(RoleTypes.S2AllorsInteger, 3);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddGreaterThan(RoleTypes.C1AllorsInteger, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddGreaterThan(RoleTypes.C1AllorsInteger, 1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 2
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddGreaterThan(RoleTypes.C1AllorsInteger, 2);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Greater Than 0
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddGreaterThan(RoleTypes.I12AllorsInteger, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddGreaterThan(RoleTypes.I12AllorsInteger, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 2
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddGreaterThan(RoleTypes.I12AllorsInteger, 2);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Greater Than 0
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddGreaterThan(RoleTypes.S1234AllorsInteger, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddGreaterThan(RoleTypes.S1234AllorsInteger, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Greater Than 2
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddGreaterThan(RoleTypes.S1234AllorsInteger, 2);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Class - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddGreaterThan(RoleTypes.C2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddGreaterThan(RoleTypes.C2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddGreaterThan(RoleTypes.C2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddGreaterThan(RoleTypes.I2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddGreaterThan(RoleTypes.I2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddGreaterThan(RoleTypes.I2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddGreaterThan(RoleTypes.I2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddGreaterThan(RoleTypes.I2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddGreaterThan(RoleTypes.I2AllorsInteger, 2);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddExists(RoleTypes.C1AllorsInteger);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddExists(RoleTypes.I12AllorsInteger);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddExists(RoleTypes.S1234AllorsInteger);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, true, false, false, false);
                this.AssertC4(extent, true, false, false, false);

                // Class - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(RoleTypes.C2AllorsInteger);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(RoleTypes.I2AllorsInteger);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(RoleTypes.S2AllorsInteger);
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
                    var inExtent = this.LocalExtent(Classes.C2);
                    inExtent.Filter.AddEquals(RoleTypes.C2AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C2);
                        inExtentA.Filter.AddEquals(RoleTypes.C2AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Classes.C2);
                        inExtentB.Filter.AddEquals(RoleTypes.C2AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C2many2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, true, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Classes.C2);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C2);
                        var inExtentB = this.LocalExtent(Classes.C2);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C2many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Classes.C2);
                    inExtent.Filter.AddEquals(RoleTypes.C2AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C2);
                        inExtentA.Filter.AddEquals(RoleTypes.C2AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Classes.C2);
                        inExtentB.Filter.AddEquals(RoleTypes.C2AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C2many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C2many2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, true, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C2many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C2many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from C1 to C1

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C1many2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, true, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C1many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C1many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C1many2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, true, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C1many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C1many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from C1 to I12

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1I12many2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, true, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1I12many2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, true, false, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1I12many2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, true, false, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1I12many2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, true, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1I12many2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, true, false, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1I12many2many, inExtent);

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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddExists(RoleTypes.C1C2many2many);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddExists(RoleTypes.I12C2many2many);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddExists(RoleTypes.S1234C2many2many);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, true, true, true, true);
                this.AssertC4(extent, true, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(RoleTypes.C3C2many2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.I12);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(RoleTypes.C3C2many2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.S12);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(RoleTypes.C3C2many2many);
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
                    var inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C1one2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, true, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full Extent
                    inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C1one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, true, false, false, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(Classes.C2);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C2);
                        var inExtentB = this.LocalExtent(Classes.C2);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C2one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, true, false, false, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(Classes.C4);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C4);
                        var inExtentB = this.LocalExtent(Classes.C4);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C3);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C3C4one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, true, false, false, true);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Emtpy Extent
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C1one2many, inExtent);

                    Assert.AreEqual(4, extent.Count);
                    this.AssertC1(extent, true, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full Extent
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C1one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, true, false, false, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C2one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, true, false, false, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(Interfaces.I34);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I34);
                        var inExtentB = this.LocalExtent(Interfaces.I34);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C3);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C3C4one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, true, false, false, true);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from Class to Interface

                    // ContainedIn Extent over Class
                    inExtent = this.LocalExtent(Classes.C2);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C2);
                        var inExtentB = this.LocalExtent(Classes.C2);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.I12C2one2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, true, false, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.I12C2one2many, inExtent);

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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddContains(RoleTypes.C1C2one2many, this.c2C);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, true, true, false, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddContains(RoleTypes.C1I12one2many, this.c2C);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, true, true, false, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddContains(RoleTypes.S1234S1234one2many, this.c1B);

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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddExists(RoleTypes.C1C2one2many);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, true, false, false, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddExists(RoleTypes.I12C2one2many);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, true, false, true, true);
                this.AssertC2(extent, true, true, false, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddExists(RoleTypes.S1234C2one2many);

                Assert.AreEqual(14, extent.Count);
                this.AssertC1(extent, true, false, true, true);
                this.AssertC2(extent, true, true, true, true);
                this.AssertC3(extent, true, true, false, true);
                this.AssertC4(extent, true, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(RoleTypes.C3C2one2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.I12);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(RoleTypes.C3C2one2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.S12);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(RoleTypes.C3C2one2many);
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
                    var inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C1one2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(Classes.C2);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C2);
                        var inExtentB = this.LocalExtent(Classes.C2);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C2one2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(Classes.C4);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C4);
                        var inExtentB = this.LocalExtent(Classes.C4);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C3);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C3C4one2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, true, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C1one2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1C2one2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, true, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(Interfaces.I34);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I34);
                        var inExtentB = this.LocalExtent(Interfaces.I34);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C3);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C3C4one2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, true, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from Class to Interface

                    // ContainedIn Extent over Class
                    inExtent = this.LocalExtent(Classes.C2);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C2);
                        var inExtentB = this.LocalExtent(Classes.C2);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1I12one2one, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, true, true, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddNot().AddContainedIn(RoleTypes.C1I12one2one, inExtent);

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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddEquals(RoleTypes.C1C1one2one, this.c1B);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, true, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddEquals(RoleTypes.C1C2one2one, this.c2B);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, true, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddEquals(RoleTypes.I12C2one2one, this.c2A);

                Assert.AreEqual(7, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddEquals(RoleTypes.S1234C2one2one, this.c2A);

                Assert.AreEqual(15, extent.Count);
                this.AssertC1(extent, true, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, true, true, true, true);
                this.AssertC4(extent, true, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C3C2one2one, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.I12);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C3C2one2one, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.S12);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C3C2one2one, this.c2A);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddExists(RoleTypes.C1C2one2one);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddExists(RoleTypes.C1C2one2one);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddExists(RoleTypes.I12C2one2one);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddExists(RoleTypes.S1234C2one2one);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, true, true, true, true);
                this.AssertC4(extent, true, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(RoleTypes.C3C2one2one);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.I12);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(RoleTypes.C3C2one2one);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.S12);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(RoleTypes.C3C2one2one);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddInstanceof(RoleTypes.C1C1one2one, Classes.C1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddInstanceof(RoleTypes.C1C2one2one, Classes.C2);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddInstanceof(RoleTypes.C1I12one2one, Classes.C2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, true, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddInstanceof(RoleTypes.S1234S1234one2one, Classes.C2);

                Assert.AreEqual(13, extent.Count);
                this.AssertC1(extent, true, true, false, true);
                this.AssertC2(extent, true, true, false, true);
                this.AssertC3(extent, true, true, false, true);
                this.AssertC4(extent, true, true, true, true);

                // Interface

                // Class
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddInstanceof(RoleTypes.C1C2one2one, Interfaces.I2);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddInstanceof(RoleTypes.C1I12one2one, Interfaces.I2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, true, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddInstanceof(RoleTypes.C1I12one2one, Interfaces.I12);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddInstanceof(RoleTypes.S1234S1234one2one, Interfaces.S1234);

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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddEquals(RoleTypes.C1AllorsString, string.Empty);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C3);
                extent.Filter.AddNot().AddEquals(RoleTypes.C3AllorsString, string.Empty);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abra"
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddEquals(RoleTypes.C1AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C3);
                extent.Filter.AddNot().AddEquals(RoleTypes.C3AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abracadabra"
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddEquals(RoleTypes.C1AllorsString, "Abracadabra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C3);
                extent.Filter.AddNot().AddEquals(RoleTypes.C3AllorsString, "Abracadabra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Equal ""
                extent = this.LocalExtent(Interfaces.I1);
                extent.Filter.AddNot().AddEquals(RoleTypes.I1AllorsString, string.Empty);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Interfaces.I3);
                extent.Filter.AddNot().AddEquals(RoleTypes.I3AllorsString, string.Empty);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abra"
                extent = this.LocalExtent(Interfaces.I1);
                extent.Filter.AddNot().AddEquals(RoleTypes.I1AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Interfaces.I3);
                extent.Filter.AddNot().AddEquals(RoleTypes.I3AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abracadabra"
                extent = this.LocalExtent(Interfaces.I1);
                extent.Filter.AddNot().AddEquals(RoleTypes.I1AllorsString, "Abracadabra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Interfaces.I3);
                extent.Filter.AddNot().AddEquals(RoleTypes.I3AllorsString, "Abracadabra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Shared Interface

                // Equal ""
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddEquals(RoleTypes.I12AllorsString, string.Empty);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Interfaces.I34);
                extent.Filter.AddNot().AddEquals(RoleTypes.I34AllorsString, string.Empty);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Equal "Abra"
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddEquals(RoleTypes.I12AllorsString, "Abra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddEquals(RoleTypes.I12AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Interfaces.I23);
                extent.Filter.AddNot().AddEquals(RoleTypes.I23AllorsString, "Abra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddNot().AddEquals(RoleTypes.I23AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C3);
                extent.Filter.AddNot().AddEquals(RoleTypes.I23AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Interfaces.I34);
                extent.Filter.AddNot().AddEquals(RoleTypes.I34AllorsString, "Abra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                extent = this.LocalExtent(Classes.C3);
                extent.Filter.AddNot().AddEquals(RoleTypes.I34AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abracadabra"
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddEquals(RoleTypes.I12AllorsString, "Abracadabra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddEquals(RoleTypes.I12AllorsString, "Abracadabra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Interfaces.I34);
                extent.Filter.AddNot().AddEquals(RoleTypes.I34AllorsString, "Abracadabra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Super Interface

                // Equal ""
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddEquals(RoleTypes.S1234AllorsString, string.Empty);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Equal "Abra"
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddEquals(RoleTypes.S1234AllorsString, "Abra");

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Equal "Abracadabra"
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddEquals(RoleTypes.S1234AllorsString, "Abracadabra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Class - Wrong RelationType

                // Equal ""
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(RoleTypes.C2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(RoleTypes.C2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abracadabra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(RoleTypes.C2AllorsString, "Abracadabra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Equal ""
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(RoleTypes.I2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(RoleTypes.I2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abracadabra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(RoleTypes.I2AllorsString, "Abracadabra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Equal ""
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(RoleTypes.S2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(RoleTypes.S2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abracadabra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddEquals(RoleTypes.S2AllorsString, "Abracadabra");
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddExists(RoleTypes.C1AllorsString);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddExists(RoleTypes.I12AllorsString);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddExists(RoleTypes.S1234AllorsString);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, true, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, true, false, false, false);
                this.AssertC4(extent, true, false, false, false);

                // Class - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(RoleTypes.C2AllorsString);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(RoleTypes.I2AllorsString);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddExists(RoleTypes.S2AllorsString);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddLike(RoleTypes.C1AllorsString, string.Empty);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "Abra"
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddLike(RoleTypes.C1AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "Abracadabra"
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddLike(RoleTypes.C1AllorsString, "Abracadabra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "notfound"
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddLike(RoleTypes.C1AllorsString, "notfound");

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "%ra%"
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddLike(RoleTypes.C1AllorsString, "%ra%");

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "%bra"
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddLike(RoleTypes.C1AllorsString, "%bra");

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "%cadabra"
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddNot().AddLike(RoleTypes.C1AllorsString, "%cadabra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Like ""
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddLike(RoleTypes.I12AllorsString, string.Empty);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "Abra"
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddLike(RoleTypes.I12AllorsString, "Abra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "Abracadabra"
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddNot().AddLike(RoleTypes.I12AllorsString, "Abracadabra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Like ""
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddLike(RoleTypes.S1234AllorsString, string.Empty);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Like "Abra"
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddLike(RoleTypes.S1234AllorsString, "Abra");

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Like "Abracadabra"
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddNot().AddLike(RoleTypes.S1234AllorsString, "Abracadabra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC1(extent, false, true, false, false);

                // Class - Wrong RelationType

                // Like ""
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddNot().AddLike(RoleTypes.C2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLike(RoleTypes.C2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abracadabra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLike(RoleTypes.C2AllorsString, "Abracadabra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Like ""
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLike(RoleTypes.I2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLike(RoleTypes.I2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abracadabra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLike(RoleTypes.I2AllorsString, "Abracadabra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Like ""
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLike(RoleTypes.S2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLike(RoleTypes.S2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abracadabra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddNot().AddLike(RoleTypes.S2AllorsString, "Abracadabra");
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

                var firstExtent = this.LocalExtent(Classes.C1);
                firstExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");

                var secondExtent = this.LocalExtent(Classes.C1);
                secondExtent.Filter.AddLike(RoleTypes.C1AllorsString, "Abracadabra");

                var extent = this.Session.Union(firstExtent, secondExtent);

                Assert.AreEqual(3, extent.Count);

                firstExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Oops");

                Assert.AreEqual(2, extent.Count);

                secondExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "I did it again");

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
                var extent = this.LocalExtent(Classes.C1);
                var or = extent.Filter.AddOr();

                or.AddAnd();
                or.AddLessThan(RoleTypes.C1AllorsInteger, 2);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Dangling empty Or behind Or
                extent = this.LocalExtent(Classes.C1);
                or = extent.Filter.AddOr();

                or.AddOr();
                or.AddLessThan(RoleTypes.C1AllorsInteger, 2);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Dangling empty Not behind Or
                extent = this.LocalExtent(Classes.C1);
                or = extent.Filter.AddOr();

                or.AddNot();
                or.AddLessThan(RoleTypes.C1AllorsInteger, 2);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Dangling empty And behind And
                extent = this.LocalExtent(Classes.C1);
                var and = extent.Filter.AddAnd();

                and.AddAnd();
                and.AddLessThan(RoleTypes.C1AllorsInteger, 2);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Dangling empty Or behind And
                extent = this.LocalExtent(Classes.C1);
                and = extent.Filter.AddAnd();

                and.AddOr();
                and.AddLessThan(RoleTypes.C1AllorsInteger, 2);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Dangling empty Not behind And
                extent = this.LocalExtent(Classes.C1);
                and = extent.Filter.AddAnd();

                and.AddNot();
                and.AddLessThan(RoleTypes.C1AllorsInteger, 2);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Dangling empty And
                extent = this.LocalExtent(Classes.C1);
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
                var extent = this.LocalExtent(Classes.C1);
                var any = extent.Filter.AddOr();
                any.AddGreaterThan(RoleTypes.C1AllorsInteger, 0);
                any.AddLessThan(RoleTypes.C1AllorsInteger, 3);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                (extent = this.LocalExtent(Interfaces.I12)).Filter.AddOr()
                    .AddGreaterThan(RoleTypes.I12AllorsInteger, 0)
                    .AddLessThan(RoleTypes.I12AllorsInteger, 3);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                (extent = this.LocalExtent(Interfaces.S1234)).Filter.AddOr()
                    .AddGreaterThan(RoleTypes.S1234AllorsInteger, 0)
                    .AddLessThan(RoleTypes.S1234AllorsInteger, 3);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Class Without predicates
                extent = this.LocalExtent(Classes.C1);
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
                Extent<Company> parents = this.LocalExtent(Classes.Company);

                Extent<Company> children = this.LocalExtent(Classes.Company);
                children.Filter.AddContainedIn(AssociationTypes.CompanyChild, (Extent)parents);

                Extent<Person> persons = this.LocalExtent(Classes.Person);
                var or = persons.Filter.AddOr();
                or.AddContainedIn(RoleTypes.PersonCompany, (Extent)parents);
                or.AddContainedIn(RoleTypes.PersonCompany, (Extent)children);

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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddExists(RoleTypes.C1AllorsInteger);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddExists(RoleTypes.I12AllorsInteger);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddExists(RoleTypes.S1234AllorsInteger);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddExists(RoleTypes.C2AllorsInteger);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddExists(RoleTypes.I2AllorsInteger);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddExists(RoleTypes.S2AllorsInteger);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddBetween(RoleTypes.C1AllorsInteger, RoleTypes.C1IntegerBetweenA, RoleTypes.C1IntegerBetweenB);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // TODO: Greater than Role
                // Interface

                // Between -10 and 0
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddBetween(RoleTypes.I12AllorsInteger, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddBetween(RoleTypes.I12AllorsInteger, 0, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddBetween(RoleTypes.I12AllorsInteger, 1, 2);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 3 and 10
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddBetween(RoleTypes.I12AllorsInteger, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Between -10 and 0
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddBetween(RoleTypes.S1234AllorsInteger, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddBetween(RoleTypes.S1234AllorsInteger, 0, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddBetween(RoleTypes.S1234AllorsInteger, 1, 2);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Between 3 and 10
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddBetween(RoleTypes.S1234AllorsInteger, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType

                // Between -10 and 0
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddBetween(RoleTypes.C2AllorsInteger, -10, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 0 and 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(RoleTypes.C2AllorsInteger, 0, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 1 and 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(RoleTypes.C2AllorsInteger, 1, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 3 and 10
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(RoleTypes.C2AllorsInteger, 3, 10);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddLessThan(RoleTypes.C1AllorsInteger, RoleTypes.C1IntegerLessThan);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // TODO: Less than Role
                // Interface

                // Less Than 1
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddLessThan(RoleTypes.I12AllorsInteger, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddLessThan(RoleTypes.I12AllorsInteger, 2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 3
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddLessThan(RoleTypes.I12AllorsInteger, 3);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Less Than 1
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddLessThan(RoleTypes.S1234AllorsInteger, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddLessThan(RoleTypes.S1234AllorsInteger, 2);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Less Than 3
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddLessThan(RoleTypes.S1234AllorsInteger, 3);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Class - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.C2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.C2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.C2AllorsInteger, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.I2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.I2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.I2AllorsInteger, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.S2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.S2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.S2AllorsInteger, 3);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddGreaterThan(RoleTypes.C1AllorsInteger, RoleTypes.C1IntegerGreaterThan);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // TODO: Greater than Role
                // Interface

                // Greater Than 0
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddGreaterThan(RoleTypes.I12AllorsInteger, 0);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddGreaterThan(RoleTypes.I12AllorsInteger, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 2
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddGreaterThan(RoleTypes.I12AllorsInteger, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);
                
                // Super Interface

                // Greater Than 0
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddGreaterThan(RoleTypes.S1234AllorsInteger, 0);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Greater Than 1
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddGreaterThan(RoleTypes.S1234AllorsInteger, 1);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Greater Than 2
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddGreaterThan(RoleTypes.S1234AllorsInteger, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.C2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.C2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.C2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsInteger, 2);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddBetween(RoleTypes.C1AllorsInteger, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddBetween(RoleTypes.C1AllorsInteger, 0, 1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddBetween(RoleTypes.C1AllorsInteger, 1, 2);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 3 and 10
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddBetween(RoleTypes.C1AllorsInteger, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Between -10 and 0
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddBetween(RoleTypes.I12AllorsInteger, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddBetween(RoleTypes.I12AllorsInteger, 0, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddBetween(RoleTypes.I12AllorsInteger, 1, 2);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 3 and 10
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddBetween(RoleTypes.I12AllorsInteger, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Between -10 and 0
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddBetween(RoleTypes.S1234AllorsInteger, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddBetween(RoleTypes.S1234AllorsInteger, 0, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddBetween(RoleTypes.S1234AllorsInteger, 1, 2);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Between 3 and 10
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddBetween(RoleTypes.S1234AllorsInteger, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType

                // Between -10 and 0
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddBetween(RoleTypes.C2AllorsInteger, -10, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 0 and 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(RoleTypes.C2AllorsInteger, 0, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 1 and 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(RoleTypes.C2AllorsInteger, 1, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 3 and 10
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(RoleTypes.C2AllorsInteger, 3, 10);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddLessThan(RoleTypes.C1AllorsInteger, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddLessThan(RoleTypes.C1AllorsInteger, 2);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 3
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddLessThan(RoleTypes.C1AllorsInteger, 3);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Less Than 1
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddLessThan(RoleTypes.I12AllorsInteger, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddLessThan(RoleTypes.I12AllorsInteger, 2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 3
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddLessThan(RoleTypes.I12AllorsInteger, 3);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Less Than 1
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddLessThan(RoleTypes.S1234AllorsInteger, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);
                
                // Less Than 2
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddLessThan(RoleTypes.S1234AllorsInteger, 2);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Less Than 3
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddLessThan(RoleTypes.S1234AllorsInteger, 3);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Class - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.C2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.C2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.C2AllorsInteger, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.I2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.I2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.I2AllorsInteger, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.S2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.S2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.S2AllorsInteger, 3);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddGreaterThan(RoleTypes.C1AllorsInteger, 0);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddGreaterThan(RoleTypes.C1AllorsInteger, 1);
                
                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 2
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddGreaterThan(RoleTypes.C1AllorsInteger, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                // Greater Than 0
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddGreaterThan(RoleTypes.I12AllorsInteger, 0);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddGreaterThan(RoleTypes.I12AllorsInteger, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 2
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddGreaterThan(RoleTypes.I12AllorsInteger, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                // Greater Than 0
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddGreaterThan(RoleTypes.S1234AllorsInteger, 0);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Greater Than 1
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddGreaterThan(RoleTypes.S1234AllorsInteger, 1);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Greater Than 2
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddGreaterThan(RoleTypes.S1234AllorsInteger, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType
                // Greater Than 0
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.C2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.C2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.C2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                // Greater Than 0
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                // Greater Than 0
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsInteger, 2);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddEquals(RoleTypes.C1AllorsInteger, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);


                // Equal 1
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddEquals(RoleTypes.C1AllorsInteger, 1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 2
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddEquals(RoleTypes.C1AllorsInteger, 2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                // Equal 0
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddEquals(RoleTypes.I12AllorsInteger, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddEquals(RoleTypes.I12AllorsInteger, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 2
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddEquals(RoleTypes.I12AllorsInteger, 2);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                // Equal 0
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddEquals(RoleTypes.S1234AllorsInteger, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddEquals(RoleTypes.S1234AllorsInteger, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Equal 2
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddEquals(RoleTypes.S1234AllorsInteger, 2);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Class - Wrong RelationType
                // Equal 0
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                // Equal 0
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.I2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.I2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.I2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                // Equal 0
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.S2AllorsInteger, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.S2AllorsInteger, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.S2AllorsInteger, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleFloatBetweenValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Between -10 and 0
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddBetween(RoleTypes.C1AllorsFloat, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddBetween(RoleTypes.C1AllorsFloat, 0, 1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddBetween(RoleTypes.C1AllorsFloat, 1, 2);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 3 and 10
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddBetween(RoleTypes.C1AllorsFloat, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Between -10 and 0
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddBetween(RoleTypes.I12AllorsFloat, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddBetween(RoleTypes.I12AllorsFloat, 0, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddBetween(RoleTypes.I12AllorsFloat, 1, 2);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 3 and 10
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddBetween(RoleTypes.I12AllorsFloat, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Between -10 and 0
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddBetween(RoleTypes.S1234AllorsFloat, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddBetween(RoleTypes.S1234AllorsFloat, 0, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddBetween(RoleTypes.S1234AllorsFloat, 1, 2);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Between 3 and 10
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddBetween(RoleTypes.S1234AllorsFloat, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType

                // Between -10 and 0
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddBetween(RoleTypes.C2AllorsFloat, -10, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 0 and 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(RoleTypes.C2AllorsFloat, 0, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 1 and 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(RoleTypes.C2AllorsFloat, 1, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 3 and 10
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(RoleTypes.C2AllorsFloat, 3, 10);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleFloatLessThanValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Less Than 1
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddLessThan(RoleTypes.C1AllorsFloat, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddLessThan(RoleTypes.C1AllorsFloat, 2);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 3
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddLessThan(RoleTypes.C1AllorsFloat, 3);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Less Than 1
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddLessThan(RoleTypes.I12AllorsFloat, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddLessThan(RoleTypes.I12AllorsFloat, 2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 3
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddLessThan(RoleTypes.I12AllorsFloat, 3);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Less Than 1
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddLessThan(RoleTypes.S1234AllorsFloat, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddLessThan(RoleTypes.S1234AllorsFloat, 2);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Less Than 3
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddLessThan(RoleTypes.S1234AllorsFloat, 3);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Class - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.C2AllorsFloat, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.C2AllorsFloat, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.C2AllorsFloat, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.I2AllorsFloat, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.I2AllorsFloat, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.I2AllorsFloat, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.S2AllorsFloat, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.S2AllorsFloat, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.S2AllorsFloat, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleFloatGreaterThanValue()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class

                // Greater Than 0
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddGreaterThan(RoleTypes.C1AllorsFloat, 0);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddGreaterThan(RoleTypes.C1AllorsFloat, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 2
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddGreaterThan(RoleTypes.C1AllorsFloat, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Greater Than 0
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddGreaterThan(RoleTypes.I12AllorsFloat, 0);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddGreaterThan(RoleTypes.I12AllorsFloat, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 2
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddGreaterThan(RoleTypes.I12AllorsFloat, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Greater Than 0
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddGreaterThan(RoleTypes.S1234AllorsFloat, 0);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Greater Than 1
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddGreaterThan(RoleTypes.S1234AllorsFloat, 1);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Greater Than 2
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddGreaterThan(RoleTypes.S1234AllorsFloat, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.C2AllorsFloat, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.C2AllorsFloat, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.C2AllorsFloat, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsFloat, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsFloat, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsFloat, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsFloat, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsFloat, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsFloat, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
            }
        }

        [Test]
        public void RoleFloatEquals()
        {
            foreach (var init in this.Inits)
            {
                init();
                this.Populate();

                // Class
                // Equal 0
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddEquals(RoleTypes.C1AllorsFloat, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddEquals(RoleTypes.C1AllorsFloat, 1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 2
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddEquals(RoleTypes.C1AllorsFloat, 2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                // Equal 0
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddEquals(RoleTypes.I12AllorsFloat, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddEquals(RoleTypes.I12AllorsFloat, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 2
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddEquals(RoleTypes.I12AllorsFloat, 2);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                // Equal 0
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddEquals(RoleTypes.S1234AllorsFloat, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddEquals(RoleTypes.S1234AllorsFloat, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Equal 2
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddEquals(RoleTypes.S1234AllorsFloat, 2);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Class - Wrong RelationType
                // Equal 0
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C2AllorsFloat, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C2AllorsFloat, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C2AllorsFloat, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                // Equal 0
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.I2AllorsFloat, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.I2AllorsFloat, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.I2AllorsFloat, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                // Equal 0
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.S2AllorsFloat, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.S2AllorsFloat, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.S2AllorsFloat, 2);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddBetween(RoleTypes.C1AllorsDecimal, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddBetween(RoleTypes.C1AllorsDecimal, 0, 1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddBetween(RoleTypes.C1AllorsDecimal, 1, 2);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 3 and 10
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddBetween(RoleTypes.C1AllorsDecimal, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Between -10 and 0
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddBetween(RoleTypes.I12AllorsDecimal, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddBetween(RoleTypes.I12AllorsDecimal, 0, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddBetween(RoleTypes.I12AllorsDecimal, 1, 2);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 3 and 10
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddBetween(RoleTypes.I12AllorsDecimal, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Between -10 and 0
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddBetween(RoleTypes.S1234AllorsDecimal, -10, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Between 0 and 1
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddBetween(RoleTypes.S1234AllorsDecimal, 0, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Between 1 and 2
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddBetween(RoleTypes.S1234AllorsDecimal, 1, 2);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Between 3 and 10
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddBetween(RoleTypes.S1234AllorsDecimal, 3, 10);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType

                // Between -10 and 0
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddBetween(RoleTypes.C2AllorsDecimal, -10, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 0 and 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(RoleTypes.C2AllorsDecimal, 0, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 1 and 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(RoleTypes.C2AllorsDecimal, 1, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Between 3 and 10
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddBetween(RoleTypes.C2AllorsDecimal, 3, 10);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddLessThan(RoleTypes.C1AllorsDecimal, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddLessThan(RoleTypes.C1AllorsDecimal, 2);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 3
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddLessThan(RoleTypes.C1AllorsDecimal, 3);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Less Than 1
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddLessThan(RoleTypes.I12AllorsDecimal, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddLessThan(RoleTypes.I12AllorsDecimal, 2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 3
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddLessThan(RoleTypes.I12AllorsDecimal, 3);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Less Than 1
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddLessThan(RoleTypes.S1234AllorsDecimal, 1);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Less Than 2
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddLessThan(RoleTypes.S1234AllorsDecimal, 2);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Less Than 3
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddLessThan(RoleTypes.S1234AllorsDecimal, 3);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Class - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.C2AllorsDecimal, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.C2AllorsDecimal, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.C2AllorsDecimal, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.I2AllorsDecimal, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.I2AllorsDecimal, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.I2AllorsDecimal, 3);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Less Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.S2AllorsDecimal, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.S2AllorsDecimal, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Less Than 3
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLessThan(RoleTypes.S2AllorsDecimal, 3);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddGreaterThan(RoleTypes.C1AllorsDecimal, 0);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddGreaterThan(RoleTypes.C1AllorsDecimal, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 2
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddGreaterThan(RoleTypes.C1AllorsDecimal, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Greater Than 0
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddGreaterThan(RoleTypes.I12AllorsDecimal, 0);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 1
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddGreaterThan(RoleTypes.I12AllorsDecimal, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Greater Than 2
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddGreaterThan(RoleTypes.I12AllorsDecimal, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Greater Than 0
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddGreaterThan(RoleTypes.S1234AllorsDecimal, 0);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Greater Than 1
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddGreaterThan(RoleTypes.S1234AllorsDecimal, 1);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Greater Than 2
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddGreaterThan(RoleTypes.S1234AllorsDecimal, 2);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.C2AllorsDecimal, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.C2AllorsDecimal, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.C2AllorsDecimal, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsDecimal, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsDecimal, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsDecimal, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Greater Than 0
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsDecimal, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsDecimal, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Greater Than 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddGreaterThan(RoleTypes.I2AllorsDecimal, 2);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddEquals(RoleTypes.C1AllorsDecimal, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddEquals(RoleTypes.C1AllorsDecimal, 1);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 2
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddEquals(RoleTypes.C1AllorsDecimal, 2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                // Equal 0
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddEquals(RoleTypes.I12AllorsDecimal, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddEquals(RoleTypes.I12AllorsDecimal, 1);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 2
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddEquals(RoleTypes.I12AllorsDecimal, 2);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                // Equal 0
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddEquals(RoleTypes.S1234AllorsDecimal, 0);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false); 

                // Equal 1
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddEquals(RoleTypes.S1234AllorsDecimal, 1);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false); 
                
                // Equal 2
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddEquals(RoleTypes.S1234AllorsDecimal, 2);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true); 

                // Class - Wrong RelationType
                // Equal 0
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C2AllorsDecimal, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C2AllorsDecimal, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C2AllorsDecimal, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                // Equal 0
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.I2AllorsDecimal, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.I2AllorsDecimal, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.I2AllorsDecimal, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                // Equal 0
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.S2AllorsDecimal, 0);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.S2AllorsDecimal, 1);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.S2AllorsDecimal, 2);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddEquals(RoleTypes.C1AllorsInteger, Zero2Four.Zero);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddEquals(RoleTypes.C1AllorsInteger, Zero2Four.One);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 2
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddEquals(RoleTypes.C1AllorsInteger, Zero2Four.Two);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Equal 0
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddEquals(RoleTypes.I12AllorsInteger, Zero2Four.Zero);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddEquals(RoleTypes.I12AllorsInteger, Zero2Four.One);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 2
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddEquals(RoleTypes.I12AllorsInteger, Zero2Four.Two);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Equal 0
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddEquals(RoleTypes.S1234AllorsInteger, Zero2Four.Zero);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal 1
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddEquals(RoleTypes.S1234AllorsInteger, Zero2Four.One);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Equal 2
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddEquals(RoleTypes.S1234AllorsInteger, Zero2Four.Two);

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Class - Wrong RelationType

                // Equal 0
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C2AllorsInteger, Zero2Four.Zero);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C2AllorsInteger, Zero2Four.One);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C2AllorsInteger, Zero2Four.Two);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Equal 0
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.I2AllorsInteger, Zero2Four.Zero);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.I2AllorsInteger, Zero2Four.One);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.I2AllorsInteger, Zero2Four.Two);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Equal 0
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.S2AllorsInteger, Zero2Four.Zero);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 1
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.S2AllorsInteger, Zero2Four.One);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal 2
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.S2AllorsInteger, Zero2Four.Two);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Wrong type
                extent = this.LocalExtent(Classes.C1);

                var exceptionThrown = false;
                C1 first = null;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C1AllorsBinary, Zero2Four.Zero);
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
                var extent = this.LocalExtent(Classes.C1);
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C1C2one2one, RoleTypes.I1C1one2one);
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
                    var inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1C1many2many, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1C1many2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1C1many2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Shortcut
                    inExtent = this.c1C.Strategy.GetCompositeRoles(RoleTypes.C1C1one2many);

                    // if (useOperator)
                    // {
                    // var inExtentA = c1_1.Strategy.GetCompositeRoles(RoleTypes.C1C1one2many);
                    // var inExtentB = c1_1.Strategy.GetCompositeRoles(RoleTypes.C1C1one2many);
                    // inExtent = DatabaseSession.Union(inExtentA, inExtentB);
                    // }
                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1C1many2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1C1many2many, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1C1many2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1C1many2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from Class to Interface

                    // ContainedIn Extent over Class
                    // Empty
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1I12many2many, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1I12many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, true, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1I12many2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, true, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    // Empty
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1I12many2many, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1I12many2many, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Filtered
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1I12many2many, inExtent);

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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddContains(RoleTypes.C1C2many2many, this.c2C);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddContains(RoleTypes.C1C2many2many, this.c2B);
                extent.Filter.AddContains(RoleTypes.C1C2many2many, this.c2C);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false); 

                // Interface
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddContains(RoleTypes.C1I12many2many, this.c2C);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddContains(RoleTypes.S1234S1234many2many, this.c1A);

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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddExists(RoleTypes.C1C2many2many);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddExists(RoleTypes.I12C2many2many);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddExists(RoleTypes.S1234C2many2many);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddExists(RoleTypes.C3C2many2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.I12);

                exception = false;
                try
                {
                    extent.Filter.AddExists(RoleTypes.C3C2many2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.S12);

                exception = false;
                try
                {
                    extent.Filter.AddExists(RoleTypes.C3C2many2many);
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
                    var inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1C1one2many, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full Extent
                    inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1C1one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, true, true, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(Classes.C2);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C2);
                        var inExtentB = this.LocalExtent(Classes.C2);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1C2one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, true, true, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(Classes.C4);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C4);
                        var inExtentB = this.LocalExtent(Classes.C4);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C3);
                    extent.Filter.AddContainedIn(RoleTypes.C3C4one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, true, true, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Shared Interface
                    // Emtpy Extent
                    inExtent = this.LocalExtent(Interfaces.I12);
                    inExtent.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        inExtentA.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtentB.Filter.AddEquals(RoleTypes.I12AllorsString, "Nothing here!");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1C1one2many, inExtent);

                    Assert.AreEqual(0, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full Extent
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1C1one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, true, true, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1C2one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, true, true, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(Interfaces.I34);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I34);
                        var inExtentB = this.LocalExtent(Interfaces.I34);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C3);
                    extent.Filter.AddContainedIn(RoleTypes.C3C4one2many, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, true, true, false);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from Interface to Class

                    // ContainedIn Extent over Class
                    inExtent = this.LocalExtent(Classes.C2);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C2);
                        var inExtentB = this.LocalExtent(Classes.C2);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.I12C2one2many, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, true, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Interface
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.I12C2one2many, inExtent);

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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddContains(RoleTypes.C1C2one2many, this.c2C);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, true, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddContains(RoleTypes.C1I12one2many, this.c2C);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, true, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddContains(RoleTypes.S1234S1234one2many, this.c1B);

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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddExists(RoleTypes.C1C2one2many);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, true, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddExists(RoleTypes.I12C2one2many);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, true, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);
               
                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddExists(RoleTypes.S1234C2one2many);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, true, false);
                this.AssertC4(extent, false, false, false, false);
 
                // Class - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddExists(RoleTypes.C3C2one2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.I12);

                exception = false;
                try
                {
                    extent.Filter.AddExists(RoleTypes.C3C2one2many);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.S12);

                exception = false;
                try
                {
                    extent.Filter.AddExists(RoleTypes.C3C2one2many);
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
                    var inExtent = this.LocalExtent(Classes.C1);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1C1one2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(Classes.C2);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C2);
                        var inExtentB = this.LocalExtent(Classes.C2);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1C2one2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(Classes.C4);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C4);
                        var inExtentB = this.LocalExtent(Classes.C4);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C3);
                    extent.Filter.AddContainedIn(RoleTypes.C3C4one2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, true, true, true);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Shared Interface
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1C1one2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1C2one2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(Interfaces.I34);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I34);
                        var inExtentB = this.LocalExtent(Interfaces.I34);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C3);
                    extent.Filter.AddContainedIn(RoleTypes.C3C4one2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, true, true, true);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from Class to Interface

                    // ContainedIn Extent over Class
                    inExtent = this.LocalExtent(Classes.C2);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C2);
                        var inExtentB = this.LocalExtent(Classes.C2);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1I12one2one, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Shared Interface
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1I12one2one, inExtent);

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
                    var inExtent = this.LocalExtent(Classes.C1);
                    inExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C1);
                        inExtentA.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        var inExtentB = this.LocalExtent(Classes.C1);
                        inExtentB.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    var extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1C1many2one, inExtent);

                    Assert.AreEqual(1, extent.Count);
                    this.AssertC1(extent, false, true, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // Full
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1C1many2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1C2many2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, true, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    inExtent = this.LocalExtent(Interfaces.I34);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I34);
                        var inExtentB = this.LocalExtent(Interfaces.I34);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C3);
                    extent.Filter.AddContainedIn(RoleTypes.C3C4many2one, inExtent);

                    Assert.AreEqual(3, extent.Count);
                    this.AssertC1(extent, false, false, false, false);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, true, true, true);
                    this.AssertC4(extent, false, false, false, false);

                    // RelationType from Class to Interface

                    // ContainedIn Extent over Class
                    inExtent = this.LocalExtent(Classes.C2);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Classes.C2);
                        var inExtentB = this.LocalExtent(Classes.C2);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1I12many2one, inExtent);

                    Assert.AreEqual(2, extent.Count);
                    this.AssertC1(extent, false, false, true, true);
                    this.AssertC2(extent, false, false, false, false);
                    this.AssertC3(extent, false, false, false, false);
                    this.AssertC4(extent, false, false, false, false);

                    // ContainedIn Extent over Shared Interface
                    inExtent = this.LocalExtent(Interfaces.I12);
                    if (useOperator)
                    {
                        var inExtentA = this.LocalExtent(Interfaces.I12);
                        var inExtentB = this.LocalExtent(Interfaces.I12);
                        inExtent = this.Session.Union(inExtentA, inExtentB);
                    }

                    extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1I12many2one, inExtent);

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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddEquals(RoleTypes.C1C1one2one, this.c1B);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddEquals(RoleTypes.C1C2one2one, this.c2B);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddEquals(RoleTypes.I12C2one2one, this.c2A);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddEquals(RoleTypes.S1234C2one2one, this.c2A);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C3C2one2one, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.I12);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C3C2one2one, this.c2A);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.S12);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C3C2one2one, this.c2A);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddExists(RoleTypes.C1C1one2one);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddExists(RoleTypes.C1C2one2one);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C3);
                extent.Filter.AddExists(RoleTypes.C3C4one2one);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, false, false, false);
 
                // Interface
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddExists(RoleTypes.I12C2one2one);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddExists(RoleTypes.S1234C2one2one);

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, true, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Class - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddExists(RoleTypes.C3C2one2one);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.I12);

                exception = false;
                try
                {
                    extent.Filter.AddExists(RoleTypes.C3C2one2one);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Interfaces.S12);

                exception = false;
                try
                {
                    extent.Filter.AddExists(RoleTypes.C3C2one2one);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddInstanceof(RoleTypes.C1C1one2one, Classes.C1);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddInstanceof(RoleTypes.C1C2one2one, Classes.C2);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddInstanceof(RoleTypes.C1I12one2one, Classes.C2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddInstanceof(RoleTypes.S1234S1234one2one, Classes.C2);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, false, true, false);
                this.AssertC2(extent, false, false, true, false);
                this.AssertC3(extent, false, false, true, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Class
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddInstanceof(RoleTypes.C1C2one2one, Interfaces.I2);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddInstanceof(RoleTypes.C1I12one2one, Interfaces.I2);

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddInstanceof(RoleTypes.C1I12one2one, Interfaces.I12);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddInstanceof(RoleTypes.S1234S1234one2one, Interfaces.S1234);

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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddEquals(RoleTypes.C1AllorsString, RoleTypes.C1AllorsString);

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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddEquals(RoleTypes.C1AllorsString, string.Empty);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C3);
                extent.Filter.AddEquals(RoleTypes.C3AllorsString, string.Empty);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abra"
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C3);
                extent.Filter.AddEquals(RoleTypes.C3AllorsString, "Abra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abracadabra"
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abracadabra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C3);
                extent.Filter.AddEquals(RoleTypes.C3AllorsString, "Abracadabra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, false, false);

                // Exclusive Interface

                // Equal ""
                extent = this.LocalExtent(Interfaces.I1);
                extent.Filter.AddEquals(RoleTypes.I1AllorsString, string.Empty);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Interfaces.I3);
                extent.Filter.AddEquals(RoleTypes.I3AllorsString, string.Empty);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abra"
                extent = this.LocalExtent(Interfaces.I1);
                extent.Filter.AddEquals(RoleTypes.I1AllorsString, "Abra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Interfaces.I3);
                extent.Filter.AddEquals(RoleTypes.I3AllorsString, "Abra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abracadabra"
                extent = this.LocalExtent(Interfaces.I1);
                extent.Filter.AddEquals(RoleTypes.I1AllorsString, "Abracadabra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Interfaces.I3);
                extent.Filter.AddEquals(RoleTypes.I3AllorsString, "Abracadabra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, false, false);

                // Shared Interface

                // Equal ""
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddEquals(RoleTypes.I12AllorsString, string.Empty);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Interfaces.I34);
                extent.Filter.AddEquals(RoleTypes.I34AllorsString, string.Empty);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abra"
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddEquals(RoleTypes.I12AllorsString, "Abra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Interfaces.I23);
                extent.Filter.AddEquals(RoleTypes.I23AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C2);
                extent.Filter.AddEquals(RoleTypes.I23AllorsString, "Abra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C3);
                extent.Filter.AddEquals(RoleTypes.I23AllorsString, "Abra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Interfaces.I34);
                extent.Filter.AddEquals(RoleTypes.I34AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                extent = this.LocalExtent(Classes.C3);
                extent.Filter.AddEquals(RoleTypes.I34AllorsString, "Abra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abracadabra"
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddEquals(RoleTypes.I12AllorsString, "Abracadabra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddEquals(RoleTypes.I12AllorsString, "Abracadabra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                extent = this.LocalExtent(Interfaces.I34);
                extent.Filter.AddEquals(RoleTypes.I34AllorsString, "Abracadabra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Super Interface

                // Equal ""
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddEquals(RoleTypes.S1234AllorsString, string.Empty);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Equal "Abra"
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddEquals(RoleTypes.S1234AllorsString, "Abra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Equal "Abracadabra"
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddEquals(RoleTypes.S1234AllorsString, "Abracadabra");

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true);

                // Class - Wrong RelationType

                // Equal ""
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abracadabra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.C2AllorsString, "Abracadabra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Equal ""
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.I2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.I2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abracadabra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.I2AllorsString, "Abracadabra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Equal ""
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.S2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.S2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Equal "Abracadabra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddEquals(RoleTypes.S2AllorsString, "Abracadabra");
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddExists(RoleTypes.C1AllorsString);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddExists(RoleTypes.I12AllorsString);

                Assert.AreEqual(6, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddExists(RoleTypes.S1234AllorsString);

                Assert.AreEqual(12, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, true, true, true);
                this.AssertC3(extent, false, true, true, true);
                this.AssertC4(extent, false, true, true, true);

                // Class - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddExists(RoleTypes.C2AllorsString);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddExists(RoleTypes.I2AllorsString);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddExists(RoleTypes.S2AllorsString);
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
                var extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddLike(RoleTypes.C1AllorsString, string.Empty);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "Abra"
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddLike(RoleTypes.C1AllorsString, "Abra");

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "Abracadabra"
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddLike(RoleTypes.C1AllorsString, "Abracadabra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "notfound"
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddLike(RoleTypes.C1AllorsString, "notfound");

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "%ra%"
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddLike(RoleTypes.C1AllorsString, "%ra%");

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "%bra"
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddLike(RoleTypes.C1AllorsString, "%bra");

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "%cadabra"
                extent = this.LocalExtent(Classes.C1);
                extent.Filter.AddLike(RoleTypes.C1AllorsString, "%cadabra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Interface

                // Like ""
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddLike(RoleTypes.I12AllorsString, string.Empty);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "Abra"
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddLike(RoleTypes.I12AllorsString, "Abra");

                Assert.AreEqual(2, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "Abracadabra"
                extent = this.LocalExtent(Interfaces.I12);
                extent.Filter.AddLike(RoleTypes.I12AllorsString, "Abracadabra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Super Interface

                // Like ""
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddLike(RoleTypes.S1234AllorsString, string.Empty);

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Like "Abra"
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddLike(RoleTypes.S1234AllorsString, "Abra");

                Assert.AreEqual(4, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, true, false, false);
                this.AssertC3(extent, false, true, false, false);
                this.AssertC4(extent, false, true, false, false);

                // Like "Abracadabra"
                extent = this.LocalExtent(Interfaces.S1234);
                extent.Filter.AddLike(RoleTypes.S1234AllorsString, "Abracadabra");

                Assert.AreEqual(8, extent.Count);
                this.AssertC1(extent, false, false, true, true);
                this.AssertC2(extent, false, false, true, true);
                this.AssertC3(extent, false, false, true, true);
                this.AssertC4(extent, false, false, true, true); 

                // Class - Wrong RelationType

                // Like ""
                extent = this.LocalExtent(Classes.C1);

                var exception = false;
                try
                {
                    extent.Filter.AddLike(RoleTypes.C2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLike(RoleTypes.C2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abracadabra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLike(RoleTypes.C2AllorsString, "Abracadabra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Interface - Wrong RelationType

                // Like ""
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLike(RoleTypes.I2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLike(RoleTypes.I2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abracadabra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLike(RoleTypes.I2AllorsString, "Abracadabra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Super Interface - Wrong RelationType

                // Like ""
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLike(RoleTypes.S2AllorsString, string.Empty);
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLike(RoleTypes.S2AllorsString, "Abra");
                }
                catch
                {
                    exception = true;
                }

                Assert.IsTrue(exception);

                // Like "Abracadabra"
                extent = this.LocalExtent(Classes.C1);

                exception = false;
                try
                {
                    extent.Filter.AddLike(RoleTypes.S2AllorsString, "Abracadabra");
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

                var sharedExtent = this.LocalExtent(Classes.C2);
                sharedExtent.Filter.AddLike(RoleTypes.C2AllorsString, "%");
                var firstExtent = this.LocalExtent(Classes.C1);
                firstExtent.Filter.AddContainedIn(RoleTypes.C1C2many2many, sharedExtent);
                var secondExtent = this.LocalExtent(Classes.C1);
                secondExtent.Filter.AddContainedIn(RoleTypes.C1C2many2many, sharedExtent);
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

                var extent = this.LocalExtent(Classes.C1);
                extent.AddSort(RoleTypes.C1AllorsString);

                var sortedObjects = (C1[])extent.ToArray(typeof(C1));
                Assert.AreEqual(4, sortedObjects.Length);
                Assert.AreEqual(this.c1C, sortedObjects[0]);
                Assert.AreEqual(this.c1D, sortedObjects[1]);
                Assert.AreEqual(this.c1B, sortedObjects[2]);
                Assert.AreEqual(this.c1A, sortedObjects[3]);

                extent = this.LocalExtent(Classes.C1);
                extent.AddSort(RoleTypes.C1AllorsString, SortDirection.Ascending);

                sortedObjects = (C1[])extent.ToArray(typeof(C1));
                Assert.AreEqual(4, sortedObjects.Length);
                Assert.AreEqual(this.c1C, sortedObjects[0]);
                Assert.AreEqual(this.c1D, sortedObjects[1]);
                Assert.AreEqual(this.c1B, sortedObjects[2]);
                Assert.AreEqual(this.c1A, sortedObjects[3]);

                extent = this.LocalExtent(Classes.C1);
                extent.AddSort(RoleTypes.C1AllorsString, SortDirection.Descending);

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
                        var firstExtent = this.LocalExtent(Classes.C1);
                        firstExtent.Filter.AddLike(RoleTypes.C1AllorsString, "1");
                        var secondExtent = this.LocalExtent(Classes.C1);
                        extent = this.Session.Union(firstExtent, secondExtent);
                        secondExtent.Filter.AddLike(RoleTypes.C1AllorsString, "3");
                        extent.AddSort(RoleTypes.C1AllorsString);

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

                var extent = this.LocalExtent(Classes.C1);
                extent.AddSort(RoleTypes.C1AllorsString);
                extent.AddSort(RoleTypes.C1AllorsInteger);

                var sortedObjects = (C1[])extent.ToArray(typeof(C1));
                Assert.AreEqual(4, sortedObjects.Length);
                Assert.AreEqual(this.c1D, sortedObjects[0]);
                Assert.AreEqual(this.c1B, sortedObjects[1]);
                Assert.AreEqual(this.c1C, sortedObjects[2]);
                Assert.AreEqual(this.c1A, sortedObjects[3]);

                extent = this.LocalExtent(Classes.C1);
                extent.AddSort(RoleTypes.C1AllorsString);
                extent.AddSort(RoleTypes.C1AllorsInteger, SortDirection.Ascending);

                sortedObjects = (C1[])extent.ToArray(typeof(C1));
                Assert.AreEqual(4, sortedObjects.Length);
                Assert.AreEqual(this.c1D, sortedObjects[0]);
                Assert.AreEqual(this.c1B, sortedObjects[1]);
                Assert.AreEqual(this.c1C, sortedObjects[2]);
                Assert.AreEqual(this.c1A, sortedObjects[3]);

                extent = this.LocalExtent(Classes.C1);
                extent.AddSort(RoleTypes.C1AllorsString);
                extent.AddSort(RoleTypes.C1AllorsInteger, SortDirection.Descending);

                sortedObjects = (C1[])extent.ToArray(typeof(C1));
                Assert.AreEqual(4, sortedObjects.Length);
                Assert.AreEqual(this.c1B, sortedObjects[0]);
                Assert.AreEqual(this.c1D, sortedObjects[1]);
                Assert.AreEqual(this.c1C, sortedObjects[2]);
                Assert.AreEqual(this.c1A, sortedObjects[3]);

                extent = this.LocalExtent(Classes.C1);
                extent.AddSort(RoleTypes.C1AllorsString, SortDirection.Descending);
                extent.AddSort(RoleTypes.C1AllorsInteger, SortDirection.Descending);

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

                var extent = this.LocalExtent(Interfaces.I4);
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
                var firstExtent = this.LocalExtent(Classes.C1);
                firstExtent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abra");

                var secondExtent = this.LocalExtent(Classes.C1);
                secondExtent.Filter.AddLike(RoleTypes.C1AllorsString, "Abracadabra");

                var extent = this.Session.Union(firstExtent, secondExtent);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Shortcut
                firstExtent = this.c1B.Strategy.GetCompositeRoles(RoleTypes.C1C1one2many);
                secondExtent = this.c1B.Strategy.GetCompositeRoles(RoleTypes.C1C1one2many);
                extent = this.Session.Union(firstExtent, secondExtent);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                firstExtent = this.c1B.Strategy.GetCompositeRoles(RoleTypes.C1C1one2many);
                secondExtent = this.c1C.Strategy.GetCompositeRoles(RoleTypes.C1C1one2many);
                extent = this.Session.Union(firstExtent, secondExtent);

                Assert.AreEqual(3, extent.Count);
                this.AssertC1(extent, false, true, true, true);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // Different Classes
                firstExtent = this.LocalExtent(Classes.C1);
                secondExtent = this.LocalExtent(Classes.C2);

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
                Extent<Company> parents = this.LocalExtent(Classes.Company);

                Extent<Company> children = this.LocalExtent(Classes.Company);
                children.Filter.AddContainedIn(AssociationTypes.CompanyChild, (Extent)parents);

                Extent<Company> allCompanies = this.Session.Union(parents, children);

                Extent<Person> persons = this.LocalExtent(Classes.Person);
                persons.Filter.AddContainedIn(RoleTypes.PersonCompany, (Extent)allCompanies);

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
                    var inExtent = this.LocalExtent(Classes.C1);

                    var extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContainedIn(AssociationTypes.C1AllorsBoolean, inExtent);
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
                    var extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContains(AssociationTypes.C1C2one2many, this.c1C);
                }
                catch (ArgumentException)
                {
                    exceptionThrown = true;
                }

                Assert.IsTrue(exceptionThrown);

                exceptionThrown = false;
                try
                {
                    var extent = this.LocalExtent(Classes.C2);
                    extent.Filter.AddContains(AssociationTypes.C1C2one2one, this.c1C);
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
                    var extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddEquals(AssociationTypes.C1C1many2many, this.c1B);
                }
                catch (ArgumentException)
                {
                    exceptionThrown = true;
                }

                Assert.IsTrue(exceptionThrown);

                exceptionThrown = false;
                try
                {
                    var extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddEquals(AssociationTypes.C1C1many2one, this.c1B);
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
                    var extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddBetween(RoleTypes.C1C2one2one, 0, 1);
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
                    var extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContains(RoleTypes.C1AllorsString, this.c2C);
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
                    var extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddEquals(RoleTypes.C1C2one2many, this.c2B);
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
                    var extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddEquals(RoleTypes.C1C2many2many, this.c2B);
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
                    var extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddGreaterThan(RoleTypes.C1C2one2one, 0);
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
                    var inExtent = this.LocalExtent(Classes.C1);

                    var extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddContainedIn(RoleTypes.C1AllorsString, inExtent);
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
                    var extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddLessThan(RoleTypes.C1C2one2one, 1);
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
                    var extent = this.LocalExtent(Classes.C1);
                    extent.Filter.AddLike(RoleTypes.C1AllorsBoolean, string.Empty);
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
                var extent = this.c1B.Strategy.GetCompositeRoles(RoleTypes.C1C1one2many);

                Assert.AreEqual(1, extent.Count);
                this.AssertC1(extent, false, true, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // With Filter
                // Shortcut
                extent = this.c1B.Strategy.GetCompositeRoles(RoleTypes.C1C1one2many);
                extent.Filter.AddEquals(RoleTypes.C1AllorsString, "Abracadabra");

                Assert.AreEqual(0, extent.Count);
                this.AssertC1(extent, false, false, false, false);
                this.AssertC2(extent, false, false, false, false);
                this.AssertC3(extent, false, false, false, false);
                this.AssertC4(extent, false, false, false, false);

                // With Sort
                // Shortcut
                extent = this.c1B.Strategy.GetCompositeRoles(RoleTypes.C1C1one2many);
                extent.AddSort(RoleTypes.C1AllorsInteger);

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

                var c2s = this.LocalExtent(Classes.C2);
                c2s.Filter.AddContains(RoleTypes.C2C3Many2Many, c3);

                Extent<C1> c1s = this.LocalExtent(Classes.C1);
                c1s.Filter.AddContainedIn(RoleTypes.C1C2many2one, (Extent)c2s);

                Assert.AreEqual(1, c1s.Count);
                Assert.AreEqual(c1, c1s[0]);
            }
        }

        [Test]
        public void RoleContainsOne2ManySharedLeafClassAndContained()
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

                var c3s = this.LocalExtent(Classes.C3);
                c3s.Filter.AddContains(RoleTypes.C3C4one2many, c4);

                Extent<C2> c2s = this.LocalExtent(Classes.C2);
                c2s.Filter.AddContainedIn(RoleTypes.C2C3Many2One, (Extent)c3s);

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

                var c2s = this.LocalExtent(Classes.C2);
                c2s.Filter.AddContains(AssociationTypes.C3C2many2many, c3);

                Extent<C1> c1s = this.LocalExtent(Classes.C1);
                c1s.Filter.AddContainedIn(RoleTypes.C1C2many2one, (Extent)c2s);

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
        protected virtual Extent LocalExtent(IComposite objectType)
        {
            return this.Session.Extent(objectType);
        }

        private static Unit GetAllorsString(IObjectType objectType)
        {
            return (Unit)objectType.MetaPopulation.Find(UnitIds.StringId);
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