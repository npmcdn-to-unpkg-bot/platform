//------------------------------------------------------------------------------------------------- 
// <copyright file="SolutionTest.cs" company="Allors bvba">
// Copyright 2002-2013 Allors bvba.
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
// <summary>Defines the AssociationTest type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors.Configure
{
    using System.IO;

    using Allors.Configure.Domain;

    using NUnit.Framework;

    [TestFixture]
    public class SolutionTest
    {
        [Test]
        public void Build()
        {
            var fileInfo = new FileInfo("../../../Allors.sln");
            var solution = new Solution(fileInfo, null, null);

            Assert.AreEqual(8, solution.ProjectByLowerCaseName.Count);

            foreach (var project in solution.ProjectByLowerCaseName.Values)
            {
                Assert.AreEqual(project.LowerCaseName.Equals("meta") ? 4 : 0, project.DomainXmlByLowerCaseName.Count);
            }
        }
    }
}