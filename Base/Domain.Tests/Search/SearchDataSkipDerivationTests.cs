// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchDataSkipDerivationTests.cs" company="Allors bvba">
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
    using NUnit.Framework;

    [TestFixture]
    public class SearchDataSkipDerivationTests : DomainTest
    {
        public override void Init()
        {
            this.Init(false);
        }

        [Test]
        public void BulkDeriveWithSkipDerivation()
        {
            SearchDatas.SkipDerivation = true;
            try
            {
                new SearchDataBuilder(this.DatabaseSession).WithCharacterBoundaryText("abc").Build();
                new SearchDataBuilder(this.DatabaseSession).WithCharacterBoundaryText("bcd").Build();

                var searchFragments = new SearchFragments(this.DatabaseSession).Extent();
                Assert.AreEqual(0, searchFragments.Count);

                this.DatabaseSession.Derive(true);
                this.DatabaseSession.Commit();
                SearchDatas.Derive(this.DatabaseSession.Database);

                this.DatabaseSession.Derive(true);
                this.DatabaseSession.Commit();

                searchFragments = new SearchFragments(this.DatabaseSession).Extent();
                Assert.AreEqual(5, searchFragments.Count);
            }
            finally
            {
                SearchDatas.SkipDerivation = false;
            }
        }

        [Test]
        public void BulkDeriveWithoutSkipDerivation()
        {
            SearchDatas.SkipDerivation = true;
            try
            {
                new SearchDataBuilder(this.DatabaseSession).WithCharacterBoundaryText("abc").Build();
                new SearchDataBuilder(this.DatabaseSession).WithCharacterBoundaryText("bcd").Build();

                var searchFragments = new SearchFragments(this.DatabaseSession).Extent();
                Assert.AreEqual(0, searchFragments.Count);

                this.DatabaseSession.Derive(true);
                this.DatabaseSession.Commit();

                SearchDatas.SkipDerivation = false;

                this.DatabaseSession.Derive(true);
                this.DatabaseSession.Commit();
                
                SearchDatas.Derive(this.DatabaseSession.Database);

                this.DatabaseSession.Derive(true);
                this.DatabaseSession.Commit();
                
                searchFragments = new SearchFragments(this.DatabaseSession).Extent();
                Assert.AreEqual(5, searchFragments.Count);
            }
            finally
            {
                SearchDatas.SkipDerivation = false;
            }
        }

        [Test]
        public void InlineAndBulkDeriveWithSkipDerivation()
        {
            new SearchDataBuilder(this.DatabaseSession).WithCharacterBoundaryText("abc").Build();

            this.DatabaseSession.Derive(true);

            var searchFragments = new SearchFragments(this.DatabaseSession).Extent();
            Assert.AreEqual(3, searchFragments.Count);

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            SearchDatas.SkipDerivation = true;
            try
            {
                new SearchDataBuilder(this.DatabaseSession).WithCharacterBoundaryText("bcd").Build();

                this.DatabaseSession.Derive(true);

                searchFragments = new SearchFragments(this.DatabaseSession).Extent();
                Assert.AreEqual(3, searchFragments.Count);

                this.DatabaseSession.Derive(true);
                this.DatabaseSession.Commit();

                SearchDatas.Derive(this.DatabaseSession.Database);

                searchFragments = new SearchFragments(this.DatabaseSession).Extent();
                Assert.AreEqual(5, searchFragments.Count);
            }
            finally
            {
                SearchDatas.SkipDerivation = false;
            }
        }

        [Test]
        public void InlineAndBulkDeriveWithoutSkipDerivation()
        {
            new SearchDataBuilder(this.DatabaseSession).WithCharacterBoundaryText("abc").Build();

            this.DatabaseSession.Derive(true);

            var searchFragments = new SearchFragments(this.DatabaseSession).Extent();
            Assert.AreEqual(3, searchFragments.Count);

            this.DatabaseSession.Derive(true);
            this.DatabaseSession.Commit();

            SearchDatas.SkipDerivation = true;
            try
            {
                new SearchDataBuilder(this.DatabaseSession).WithCharacterBoundaryText("bcd").Build();

                searchFragments = new SearchFragments(this.DatabaseSession).Extent();
                Assert.AreEqual(3, searchFragments.Count);

                SearchDatas.SkipDerivation = false;

                SearchDatas.Derive(this.DatabaseSession.Database);

                this.DatabaseSession.Derive(true);
                this.DatabaseSession.Commit();

                searchFragments = new SearchFragments(this.DatabaseSession).Extent();
                Assert.AreEqual(5, searchFragments.Count);
            }
            finally
            {
                SearchDatas.SkipDerivation = false;
            }
        }
    }
}