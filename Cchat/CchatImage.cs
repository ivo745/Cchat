using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Cchat
{
    public static class CchatImage
    {
        private enum ImageFormat
        {
            Bmp,
            Jpeg,
            Gif,
            Tiff,
            Png,
            Unknown
        }

        private static ImageFormat GetImageFormat(byte[] value)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            var png = new byte[] { 137, 80, 78, 71 };    // PNG
            var tiff = new byte[] { 73, 73, 42 };         // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };         // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 }; // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 }; // jpeg canon

            if (bmp.SequenceEqual(value.Take(bmp.Length)))
                return ImageFormat.Bmp;

            if (gif.SequenceEqual(value.Take(gif.Length)))
                return ImageFormat.Gif;

            if (png.SequenceEqual(value.Take(png.Length)))
                return ImageFormat.Png;

            if (tiff.SequenceEqual(value.Take(tiff.Length)))
                return ImageFormat.Tiff;

            if (tiff2.SequenceEqual(value.Take(tiff2.Length)))
                return ImageFormat.Tiff;

            if (jpeg.SequenceEqual(value.Take(jpeg.Length)))
                return ImageFormat.Jpeg;

            if (jpeg2.SequenceEqual(value.Take(jpeg2.Length)))
                return ImageFormat.Jpeg;

            return ImageFormat.Unknown;
        }

        public static byte[] GetByteArrayFromImage(Image image)
        {
            if (image == null)
                return null;

            using (MemoryStream mStream = new MemoryStream())
            {
                image.Save(mStream, image.RawFormat);
                return mStream.ToArray();
            }
        }

        public static Image GetImageFromByteArray(byte[] data)
        {
            if (data == null)
                return null;

            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(data, 0, data.Length);
                if (GetImageFormat(data) == ImageFormat.Png)
                {
                    return Image.FromStream(ms, true, true);
                }

                return null;
            }
        }
    }
}
