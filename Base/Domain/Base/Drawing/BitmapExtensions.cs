// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BitmapExtensions.cs" company="Allors bvba">
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

namespace System.Drawing
{
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;

    public static partial class BitmapExtensions
    {
        public static Bitmap MaxWidth(this Bitmap bitmap, int maxWidth)
        {
            if (bitmap.Height <= maxWidth)
            {
                return bitmap;
            }

            return ScaleToWidth(bitmap, maxWidth);
        }

        public static Bitmap MaxHeight(this Bitmap bitmap, int maxHeight)
        {
            if (bitmap.Height <= maxHeight)
            {
                return bitmap;
            }

            return ScaleToHeight(bitmap, maxHeight);
        }

        public static Bitmap ScaleToWidth(this Bitmap bitmap, int width)
        {
            var ratio = (float)bitmap.Height / bitmap.Width;
            var height = (int)(width * ratio);
            var scaled = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(scaled))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.DrawImage(bitmap, 0, 0, width, height);
            }

            return scaled;
        }

        public static Bitmap ScaleToHeight(this Bitmap bitmap, int height)
        {
            var ratio = (float)bitmap.Width / bitmap.Height;
            var width = (int)(height * ratio);
            var scaled = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(scaled))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.DrawImage(bitmap, 0, 0, width, height);
            }

            return scaled;
        }

        public static Bitmap Rotate(this Bitmap bitmap)
        {
            const int ExifOrientationPropertyId = 0x112;

            if (bitmap.PropertyIdList.Any(x => x == ExifOrientationPropertyId))
            {
                var orientation = (int)bitmap.GetPropertyItem(ExifOrientationPropertyId).Value[0];
                switch (orientation)
                {
                    case 2:
                        bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        break;
                    case 3:
                        bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case 4:
                        bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
                        break;
                    case 5:
                        bitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
                        break;
                    case 6:
                        bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case 7:
                        bitmap.RotateFlip(RotateFlipType.Rotate270FlipX);
                        break;
                    case 8:
                        bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                }

                bitmap.RemovePropertyItem(ExifOrientationPropertyId);
            }

            return bitmap;
        }

        public static byte[] Save(this Bitmap bitmap, ImageFormat format)
        {
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, format);
                return memoryStream.ToArray();
            }            
        }

        public static byte[] Save(this Bitmap bitmap, ImageCodecInfo encoder, EncoderParameters encoderParams)
        {
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, encoder, encoderParams);
                return memoryStream.ToArray();
            }
        }
    }
}
