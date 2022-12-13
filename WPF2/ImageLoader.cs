using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.WebSockets;
using System.Windows.Media.Animation;
using System.IO.Compression;
using System.Data;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace WPF
{
    public static class ImageLoader
    {
        private static Dictionary<string, Bitmap> Cache = new Dictionary<string, Bitmap>();

        public static Bitmap GetBitmap(string url)
        {
            if (Cache.ContainsKey(url))
            {
                return Cache[url];
            }
            Cache.Add(url, new Bitmap(".\\Graphics\\"+url));
            return Cache[url];
        }
        public static void ClearCashe()
        {
            Cache.Clear();
        }
        public static Bitmap MakeEmptyImage(int width, int height)
        {
            Bitmap empty;
            if (Cache.ContainsKey("empty"))
                empty = Cache["empty"];
            else
                empty = new Bitmap(width, height);
            
            Graphics graphics = Graphics.FromImage(empty);
            SolidBrush brush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#A4C2CE"));
            graphics.FillRectangle(brush, 0, 0, width, height);
            Bitmap clone = (Bitmap)empty.Clone();
            return clone;
        }

        public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }

    }
}
