//------------------------------------------------------------------------------------------------- 
// <copyright file="MediatypeTest.cs" company="Allors bvba">
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
// <summary>Defines the MediaTests type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Domain
{
    using Allors;
    using Allors.Domain;

    using NUnit.Framework;

    [TestFixture]
    public class MediatypeTest : DomainTest
    {
        [Test]
        public void GivenMimeType_WhenDeriving_ThenNameIsRequired()
        {
            var builder = new MediaTypeBuilder(this.Session);
            var mediaType = builder.Build();

            Assert.IsTrue(this.Session.Derive().HasErrors);

            this.Session.Rollback();

            builder.WithName("image/gif");
            mediaType = builder.Build();

            Assert.IsFalse(this.Session.Derive().HasErrors);
        }
    }
}