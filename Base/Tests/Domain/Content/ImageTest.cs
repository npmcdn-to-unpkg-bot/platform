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
    using System.Drawing;
    using System.IO;

    using Allors;
    using Allors.Domain;

    using global::System.Reflection;

    using NUnit.Framework;

    using Should;

    [TestFixture]
    public class ImageTest : ContentTests
    {
        [Test]
        public void Image()
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

            var image = new ImageBuilder(this.Session).WithOriginal(media).Build();

            this.Session.Derive(true);
        }

        [Test]
        public void Orientation()
        {
            for (var i = 1; i <= 8; i++)
            {
                var resourceName = string.Format("Tests.Resources.orientation.Landscape_{0}.jpg", i);
                var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);

                byte[] content;
                using (var output = new MemoryStream())
                {
                    resource.CopyTo(output);
                    content = output.ToArray();
                }

                var media = new MediaBuilder(this.Session).WithContent(content).Build();
                media.MediaType = new MediaTypes(this.Session).Infer(media.Content);

                var image = new ImageBuilder(this.Session).WithOriginal(media).Build();

                this.Session.Derive(true);

                using (Stream stream = new MemoryStream(image.Responsive.Content))
                {
                    var responsive = new Bitmap(stream);
                    responsive.Width.ShouldEqual(600);
                    responsive.Height.ShouldEqual(450);
                }
            }
        }
    }
}
