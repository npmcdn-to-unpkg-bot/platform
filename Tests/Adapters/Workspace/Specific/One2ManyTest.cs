//------------------------------------------------------------------------------------------------- 
// <copyright file="One2ManyTest.cs" company="Allors bvba">
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
namespace Allors.Special
{
    using global::Domain;

    using NUnit.Framework;

    public abstract class One2ManyTest : Adapters.Special.One2ManyTest
    {
        [Test]
        public void Remove()
        {
            foreach (var init in this.Inits)
            {
                init();
                var a = C1.Create(this.WorkspaceSession.DatabaseSession);
                var b = C2.Create(this.WorkspaceSession.DatabaseSession);

                a.AddC1C2one2many(b);

                a = (C1)this.Session.Instantiate(a.Id);
                b = (C2)this.Session.Instantiate(b.Id);

                a.RemoveC1C2one2manies();

                Assert.AreEqual(null, b.C1WhereC2one2many);
            }
        }

        protected abstract IWorkspaceSession WorkspaceSession { get; }
    }
}