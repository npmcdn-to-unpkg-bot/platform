// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MethodTest.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
// 
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
// 
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Applications is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors
{
    using global::Allors.Domain;

    using NUnit.Framework;

    [TestFixture]
    public class MethodTest : DomainTest
    {
        [Test]
        public void Default()
        {
            var organisation = new OrganisationBuilder(this.DatabaseSession).Build();

            var method = organisation.JustDoIt(
                m =>
                    {
                        m.a = 1;
                        m.b = 2;
                    });

            Assert.IsTrue(organisation.IsMethodCalled.Value);
            Assert.AreEqual(3, method.result);
        }
    }
}
