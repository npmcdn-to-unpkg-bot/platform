//------------------------------------------------------------------------------------------------- 
// <copyright file="MediaTest.cs" company="Allors bvba">
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
    using System.IO;
    using System.Reflection;

    using Allors;
    using Allors.Domain;

    using NUnit.Framework;

    using Should;

    [TestFixture]
    public class MediaTest : ContentTests
    {
        [Test]
        public void DefaultValues()
        {
            var media = new MediaBuilder(this.Session).Build();
            Assert.IsTrue(media.ExistUniqueId);
        }

        [Test]
        public void BuilderWithBlob()
        {
            var binary = new byte[] { 0, 1, 2, 3 };

            this.Session.Commit();

            var media = new MediaBuilder(this.Session).WithBlob(binary).Build();

            this.Session.Derive(true);

            Assert.IsTrue(media.ExistMediaContent);
            media.MediaContent.Type.ShouldEqual("application/octet-stream");
        }

        [Test]
        public void BuilderWithPng()
        {
            var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Tests.Resources.logo.png");

            byte[] content;
            using (var output = new MemoryStream())
            {
                resource?.CopyTo(output);
                content = output.ToArray();
            }

            var media = new MediaBuilder(this.Session).WithBlob(content).Build();

            Assert.IsTrue(media.ExistMediaContent);
            media.MediaContent.Type.ShouldEqual("image/png");
        }

        [Test]
        public void BuilderWithPdfWithJpegExtension()
        {
            var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Tests.Resources.PdfAs.jpg");

            byte[] content;
            using (var output = new MemoryStream())
            {
                resource?.CopyTo(output);
                content = output.ToArray();
            }

            var media = new MediaBuilder(this.Session).WithBlob(content).Build();

            Assert.IsTrue(media.ExistMediaContent);
            media.MediaContent.Type.ShouldEqual("application/pdf");
}
    }
}
