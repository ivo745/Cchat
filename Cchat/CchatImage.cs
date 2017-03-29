using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace Cchat
{
    public static class CchatImage
    {
        private static ImageFormat GetImageFormat(byte[] value)
        {
            var png = new byte[] { 137, 80, 78, 71 };

            if (png.SequenceEqual(value.Take(png.Length)))
                return ImageFormat.Png;

            return null;
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