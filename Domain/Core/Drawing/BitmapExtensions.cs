// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BitmapExtensions.cs" company="Allors bvba">
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

namespace System.Drawing
{
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;

    public static partial class BitmapExtensions
    {
        public static Bitmap ScaleToWidth(this Bitmap highRes, int width)
        {
            var ratio = (float)highRes.Height / highRes.Width;
            var height = (int)(width * ratio);
            var scaled = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(scaled))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.DrawImage(highRes, 0, 0, width, height);
            }

            return scaled;
        }

        public static Bitmap ScaleToHeight(this Bitmap highRes, int height)
        {
            var ratio = (float)highRes.Width / highRes.Height;
            var width = (int)(height * ratio);
            var scaled = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(scaled))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.DrawImage(highRes, 0, 0, width, height);
            }

            return scaled;
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
