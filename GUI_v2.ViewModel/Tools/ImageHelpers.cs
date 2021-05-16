using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace GUI_v2.Tools
{
    public static class ImageHelpers
    {
        public static BitmapImage WriteableBitmapToBitmapImage(WriteableBitmap wbm)
        {
            BitmapImage bmImage = new BitmapImage();
            using (MemoryStream stream = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(wbm));
                encoder.Save(stream);
                bmImage.BeginInit();
                bmImage.CacheOption = BitmapCacheOption.OnLoad;
                bmImage.StreamSource = stream;
                bmImage.EndInit();
                bmImage.Freeze();
            }
            return bmImage;
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        public static BitmapSource ToBitmapImage(Bitmap bitmap)
        {

            IntPtr hBitmap = bitmap.GetHbitmap();
            BitmapSource i = Imaging.CreateBitmapSourceFromHBitmap(
                               hBitmap,
                               IntPtr.Zero,
                               Int32Rect.Empty,
                               BitmapSizeOptions.FromEmptyOptions());
                bitmap.Dispose();
                DeleteObject(hBitmap);

                return i;
            
        }

   

        public static Bitmap BytesToBitmap(byte[] data)
        {
            Bitmap bmp;
            using (var ms = new MemoryStream(data))
            {
                bmp = new Bitmap(ms);
            }
            return bmp;
        }
        public static BitmapImage BytesToBitmapImage(Byte[] data)
        {
            Stream ms = new MemoryStream(data);
            BitmapImage img = new BitmapImage();
            try
            {
                img.BeginInit();
                img.StreamSource = ms;
                img.EndInit();
                return img;
            }
            catch
            {
                return null;
            }
            
        }




    }
}
