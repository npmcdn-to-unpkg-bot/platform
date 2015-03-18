// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaType.cs" company="Allors bvba">
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
    using System.Drawing.Imaging;

    public partial class MediaType
    {
        public bool IsImage
        {
            get
            {
                return this.ExistName && this.Name.StartsWith("image");
            }
        }

        public ImageFormat ImageFormat
        {
            get
            {
                switch (Name)
                {
                    case "image/gif":
                        return ImageFormat.Gif;

                    case "image/jpeg":
                        return ImageFormat.Jpeg;

                    case "image/bmp":
                        return ImageFormat.Bmp;

                    case "image/png":
                        return ImageFormat.Png;

                    default:
                        return null;
                }
            }
        }

        public void BaseOnBuild(ObjectOnBuild method)
        {
            var builder = method.Builder;

            this.NameToLowerCase();
        }

        public void BaseOnDerive(ObjectOnDerive method)
        {
            var derivation = method.Derivation;

            this.NameToLowerCase();
        }

        private void NameToLowerCase()
        {
            if (this.ExistName)
            {
                this.Name = this.Name.ToLowerInvariant();
            }
        }
    }
}