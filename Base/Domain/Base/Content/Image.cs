// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Image.cs" company="Allors bvba">
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
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;

    public partial class Image
    {
        public void BaseDelete(DeletableDelete method)
        {
            this.RemoveOriginal();
            this.RemoveResponsive();
        }

        public void BaseDerive(ObjectDerive method)
        {
            var derivation = method.Derivation;

            if (this.ExistOriginal)
            {
                // Stream should be left open for Save to work
                using (Stream stream = new MemoryStream(this.Original.Content))
                {
                    var original = new Bitmap(stream);
                    var mediaType = new MediaTypes(this.Strategy.Session).Jpeg;

                    var responsive = CreateResponsive(original, mediaType);
                    if (!this.ExistResponsive || !responsive.SequenceEqual(this.Responsive.Content))
                    {
                        if (this.ExistResponsive)
                        {
                            this.Responsive.Delete();
                        }

                        this.Responsive = new MediaBuilder(this.Strategy.Session).WithContent(responsive).WithMediaType(mediaType).Build();
                    }
                }
            }
            else
            {
                this.RemoveResponsive();
            }
        }

        private static byte[] CoreCreateResponsive(Bitmap original, MediaType mediaType)
        {
            var responsive = original;
            if (original.Height > 600)
            {
                responsive = original.ScaleToHeight(600);
            }

            var encoder = ImageCodecInfo.GetImageEncoders().FirstOrDefault(e => e.MimeType == mediaType.Name);
            var encoderParams = new EncoderParameters(1);
            var quality = Encoder.Quality;
            encoderParams.Param[0] = new EncoderParameter(quality, 30L);

            return responsive.Save(encoder, encoderParams);
        }
    }
}