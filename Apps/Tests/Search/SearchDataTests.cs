// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchDataTests.cs" company="Allors bvba">
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

namespace Allors.Domain
{
    using System.Collections.Generic;

    using Allors.Domain;

    using NUnit.Framework;

    [TestFixture]
    public class SearchDataTests : DomainTest
    {
        [Test]
        public void TwoCharacterSearchText()
        {
            var person = new PersonBuilder(this.DatabaseSession).WithLastName("Ab").Build();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(1, person.SearchData.SearchFragments.Count);
            Assert.AreEqual("ab", person.SearchData.SearchFragments[0].LowerCaseText);
        }

        [Test]
        public void ThreeCharacterSearchText()
        {
            var person = new PersonBuilder(this.DatabaseSession).WithLastName("AbC").Build();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(3, person.SearchData.SearchFragments.Count);

            var stringFragments = new HashSet<string>();
            foreach (SearchFragment searchFragment in person.SearchData.SearchFragments)
            {
                stringFragments.Add(searchFragment.LowerCaseText);
            }

            Assert.IsTrue(stringFragments.Contains("ab"));
            Assert.IsTrue(stringFragments.Contains("bc"));
            Assert.IsTrue(stringFragments.Contains("abc"));
        }

        [Test]
        public void FourCharacterSearchText()
        {
            var person = new PersonBuilder(this.DatabaseSession).WithLastName("AbCd").Build();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(6, person.SearchData.SearchFragments.Count);

            var stringFragments = new HashSet<string>();
            foreach (SearchFragment searchFragment in person.SearchData.SearchFragments)
            {
                stringFragments.Add(searchFragment.LowerCaseText);
            }

            Assert.IsTrue(stringFragments.Contains("ab"));
            Assert.IsTrue(stringFragments.Contains("bc"));
            Assert.IsTrue(stringFragments.Contains("cd"));
            Assert.IsTrue(stringFragments.Contains("abc"));
            Assert.IsTrue(stringFragments.Contains("bcd"));
            Assert.IsTrue(stringFragments.Contains("abcd"));
        }

        [Test]
        public void FourCharacterSearchTextsDividedBySpaces()
        {
            var person = new PersonBuilder(this.DatabaseSession).WithLastName("AbCd eFgh").Build();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(12, person.SearchData.SearchFragments.Count);

            var stringFragments = new HashSet<string>();
            foreach (SearchFragment searchFragment in person.SearchData.SearchFragments)
            {
                stringFragments.Add(searchFragment.LowerCaseText);
            }

            Assert.IsTrue(stringFragments.Contains("ab"));
            Assert.IsTrue(stringFragments.Contains("bc"));
            Assert.IsTrue(stringFragments.Contains("cd"));
            Assert.IsTrue(stringFragments.Contains("abc"));
            Assert.IsTrue(stringFragments.Contains("bcd"));
            Assert.IsTrue(stringFragments.Contains("abcd"));
            Assert.IsTrue(stringFragments.Contains("ef"));
            Assert.IsTrue(stringFragments.Contains("fg"));
            Assert.IsTrue(stringFragments.Contains("gh"));
            Assert.IsTrue(stringFragments.Contains("efg"));
            Assert.IsTrue(stringFragments.Contains("fgh"));
            Assert.IsTrue(stringFragments.Contains("efgh"));
        }

        [Test]
        public void FiveCharacterSearchText()
        {
            var person = new PersonBuilder(this.DatabaseSession).WithLastName("AbCdE").Build();

            this.DatabaseSession.Derive(true);

            Assert.AreEqual(10, person.SearchData.SearchFragments.Count);

            var stringFragments = new HashSet<string>();
            foreach (SearchFragment searchFragment in person.SearchData.SearchFragments)
            {
                stringFragments.Add(searchFragment.LowerCaseText);
            }

            Assert.IsTrue(stringFragments.Contains("ab"));
            Assert.IsTrue(stringFragments.Contains("bc"));
            Assert.IsTrue(stringFragments.Contains("cd"));
            Assert.IsTrue(stringFragments.Contains("de"));
            Assert.IsTrue(stringFragments.Contains("abc"));
            Assert.IsTrue(stringFragments.Contains("bcd"));
            Assert.IsTrue(stringFragments.Contains("cde"));
            Assert.IsTrue(stringFragments.Contains("abcd"));
            Assert.IsTrue(stringFragments.Contains("bcde"));
            Assert.IsTrue(stringFragments.Contains("abcde"));
        }
    }
}