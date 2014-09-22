// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PathTests.cs" company="Allors bvba">
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

    using Allors.Meta;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class PathTests : DomainTest
    {
        [Test]
        public void One2Many()
        {
            var c2A = new C2Builder(this.DatabaseSession).WithC2AllorsString("c2A").Build();
            var c2B = new C2Builder(this.DatabaseSession).WithC2AllorsString("c2B").Build();
            var c2C = new C2Builder(this.DatabaseSession).WithC2AllorsString("c2C").Build();

            var c1a = new C1Builder(this.DatabaseSession)
                .WithC1AllorsString("c1A")
                .WithC1C2One2Many(c2A)
                .Build();

            var c1b = new C1Builder(this.DatabaseSession)
                .WithC1AllorsString("c1B")
                .WithC1C2One2Many(c2B)
                .WithC1C2One2Many(c2C)
                .Build();

            this.DatabaseSession.Derive(true);

            var path = new Path(C1s.Meta.C1C2One2Many, C2s.Meta.C2AllorsString);

            var aclMock = new Mock<IAccessControlList>();
            aclMock.Setup(acl => acl.CanRead(It.IsAny<PropertyType>())).Returns(true);
            var acls = new AccessControlListCache(null, (allorsObject, user) => aclMock.Object);

            var result = (ISet<object>)path.Get(c1a, acls);
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result.Contains("c2A"));

            result = (ISet<object>)path.Get(c1b, acls);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Contains("c2B"));
            Assert.IsTrue(result.Contains("c2C"));
        }
    }
}