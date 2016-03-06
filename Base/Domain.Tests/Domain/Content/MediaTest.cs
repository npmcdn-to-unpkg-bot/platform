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
    using System;
    using System.IO;

    using Allors;
    using Allors.Domain;

    using global::System.Reflection;

    using NUnit.Framework;

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
        public void Hash()
        {
            var binary = new byte[] { 0, 1, 2, 3 };
            var octetStream = new MediaTypes(this.Session).OctetStream;

            this.Session.Commit();

            var media = new MediaBuilder(this.Session).WithContent(binary).WithMediaType(octetStream).Build();

            this.Session.Derive(true);

            Assert.IsTrue(media.ExistMediaContent);
            Assert.IsTrue(media.MediaContent.ExistHash);
        }

        [Test]
        public void HashedContent()
        {
            var binary = new byte[] { 0, 1, 2, 3 };
            var sameBinary = new byte[] { 0, 1, 2, 3 };
            var differentBinary = new byte[] { 1, 0, 2, 3 };

            var octetStream = new MediaTypes(this.Session).OctetStream;
            var media1 = new MediaBuilder(this.Session).WithContent(binary).WithMediaType(octetStream).Build();
            var media2 = new MediaBuilder(this.Session).WithContent(sameBinary).WithMediaType(octetStream).Build();
            var media3 = new MediaBuilder(this.Session).WithContent(differentBinary).WithMediaType(octetStream).Build();

            this.Session.Derive(true);

            Assert.AreEqual(media1.MediaContent.Hash, media2.MediaContent.Hash);
            Assert.AreNotEqual(media1.MediaContent.Hash, media3.MediaContent.Hash);
        }

        [Test]
        public void Infer()
        {
            var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Tests.Resources.logo.png");

            byte[] content;
            using (var output = new MemoryStream())
            {
                resource.CopyTo(output);
                content = output.ToArray();
            }

            var media = new MediaBuilder(this.Session).WithContent(content).Build();
            media.MediaType = new MediaTypes(this.Session).Infer(media.Content);

            Assert.IsTrue(media.MediaType.IsImage);
            Assert.IsTrue(media.MediaType.Equals(new MediaTypes(this.Session).Png));
        }

        [Test]
        public void PdfWithJpegExtension()
        {
            var media = new MediaBuilder(this.Session).Build();

            var input = Assembly.GetExecutingAssembly().GetManifestResourceStream("Tests.Resources.PdfAs.jpg");
            if (input != null)
            {
                using (var output = new MemoryStream())
                {
                    input.CopyTo(output);
                    media.Content = output.ToArray();

                    media.MediaType = new MediaTypes(this.Session).Infer(media.Content);
                }
            }

            this.Session.Derive(true);

            Assert.AreEqual("application/pdf", media.MediaType.Name);
        }
    }
}
