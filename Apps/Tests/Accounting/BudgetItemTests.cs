//------------------------------------------------------------------------------------------------- 
// <copyright file="BudgetItemTests.cs" company="Allors bvba">
// Copyright 2002-2009 Allors bvba.
// 
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
// 
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// <summary></summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using NUnit.Framework;

    [TestFixture]
    public class BudgetItemTests : DomainTest
    {
        [Test]
        public void GivenBudgetItem_WhenDeriving_ThenDisplayNameIsSet()
        {
            var budgetItem = new BudgetItemBuilder(this.DatabaseSession).WithAmount(123).WithPurpose("investment").Build();

            this.DatabaseSession.Derive(true);

            var expectedvalue = string.Format("{0} - {1}", "investment", "123");
            Assert.AreEqual(expectedvalue, budgetItem.DisplayName);
        }
    }
}
