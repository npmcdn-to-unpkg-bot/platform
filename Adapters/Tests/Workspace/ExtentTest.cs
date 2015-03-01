//------------------------------------------------------------------------------------------------- 
// <copyright file="ExtentTest.cs" company="Allors bvba">
// Copyright 2002-2012 Allors bvba.
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
// <summary>Defines the ExtentTest type.</summary>
//-------------------------------------------------------------------------------------------------
namespace Allors.Workspaces
{
    using System;

    using Allors.Meta;

    using Allors;

    using Allors.Domain;

    using NUnit.Framework;

    // TODO: Test WorkspacesExtent()
    public abstract class ExtentTest : Databases.ExtentTest
    {
        protected override bool[] UseOperator
        {
            get { return new[] { false }; }
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void Unsupported()
        {
            foreach (var init in this.Inits)
            {
                init();

                this.Session.Extent<C1>();
            }
        }

        [Test]
        public void EmptyPopulation()
        {
            foreach (var init in this.Inits)
            {
                init();

                var workspace = (IWorkspace)this.CreatePopulation();
                using (var session = workspace.CreateSession())
                {
                    Assert.AreEqual(0, session.LocalExtent(Classes.C1).Count);
                }

                using (var session = workspace.CreateSession())
                {
                    Assert.AreEqual(0, session.LocalExtent().Length);
                }
            }
        }

        public override void Union()
        {
        }

        public override void Except()
        {
        }

        public override void Intersect()
        {
        }

        public override void Operation()
        {
        }

        public override void Combination()
        {
        }

        public override void Shared()
        {
        }

        public override void CombinationWithMultipleOperations()
        {
        }

        protected override Extent LocalExtent(IComposite objectType)
        {
            var workspaceSession = (IWorkspaceSession)this.Session;
            return workspaceSession.LocalExtent(objectType);
        }

        protected abstract IPopulation CreatePopulation();
    }
}