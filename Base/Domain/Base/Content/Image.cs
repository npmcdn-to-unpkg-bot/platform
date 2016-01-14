// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Image.cs" company="Allors bvba">
//   Copyright 2002-2016 Allors bvba.
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
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;

    public partial class Image
    {
        public void BaseDelete(DeletableDelete method)
        {
            this.Original.Delete();
            this.Responsive.Delete();
            this.Thumbnail.Delete();
        }

        public void BaseOnDerive(ObjectOnDerive method)
        {
            var derivation = method.Derivation;

            if (this.ExistOriginal)
            {
                this.CreateResponsive();
                this.CreateThumbnail();
            }
            else
            {
                this.RemoveResponsive();
                this.RemoveThumbnail();
            }
        }

        public void BaseCreateResponsive(ImageCreateResponsive method)
        {
            var mediaType = new MediaTypes(this.Strategy.Session).Jpeg;

            byte[] content;

            // Stream should be left open for Save to work
            using (Stream stream = new MemoryStream(this.Original.Content))
            {
                var responsive = new Bitmap(stream);

                responsive = responsive.Rotate();
                responsive = responsive.MaxHeight(method.MaxHeight ?? 600);

                var encoder = ImageCodecInfo.GetImageEncoders().FirstOrDefault(e => e.MimeType == mediaType.Name);
                var encoderParams = new EncoderParameters(1);
                var qualityParam = Encoder.Quality;
                encoderParams.Param[0] = new EncoderParameter(qualityParam, 72L);

                content = responsive.Save(encoder, encoderParams);
            }

            if (!this.ExistResponsive || !content.SequenceEqual(this.Responsive.Content))
            {
                if (this.ExistResponsive)
                {
                    this.Responsive.Delete();
                }

                this.Responsive = new MediaBuilder(this.Strategy.Session).WithContent(content).WithMediaType(mediaType).Build();
            }
        }

        public void BaseCreateThumbnail(ImageCreateThumbnail method)
        {
            var mediaType = new MediaTypes(this.Strategy.Session).Png;

            byte[] content;

            // Stream should be left open for Save to work
            using (Stream stream = new MemoryStream(this.Original.Content))
            {
                var thumbnail = new Bitmap(stream);

                thumbnail = thumbnail.Rotate();
                thumbnail = thumbnail.MaxHeight(method.MaxHeight ?? 150);

                content = thumbnail.Save(ImageFormat.Png);
            }

            if (!this.ExistThumbnail || !content.SequenceEqual(this.Thumbnail.Content))
            {
                if (this.ExistThumbnail)
                {
                    this.Thumbnail.Delete();
                }

                this.Thumbnail = new MediaBuilder(this.Strategy.Session).WithContent(content).WithMediaType(mediaType).Build();
            }
        }
    }
}