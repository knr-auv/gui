using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Threading;
using System.Windows.Threading;
using GUI_v2.Tools;
using System.Windows;
using System.Globalization;
using System.Drawing;

namespace GUI_v2.ViewModel.Common
{
    public class CameraStreamViewModel : BaseViewModel
    {

        BitmapImage Logo;
        public void SwapImage(Byte[] frame)
        {
            //   CurrentImage = ImageHelpers.BytesToBitmapImage(frame);
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                Bitmap temp = ImageHelpers.BytesToBitmap(frame);

                 DrawOnImage?.Invoke(temp);
                BitmapSource bImg = ImageHelpers.ToBitmapImage(temp);
                temp.Dispose();
                if (bImg != null) 
                    CurrentImage = bImg;
                
            }), DispatcherPriority.Render);

        }
        private int imageWidth = 0;
        private int imageHeight = 0;
        private int[] size;
        public Action<Bitmap> DrawOnImage;
        public int[] GetImageSize()
        {
            size[0] = imageWidth;
            size[1] = imageHeight;
            return size;
        }
        public CameraStreamViewModel() 
        {
            size = new int[2];
            _CurrentImage = new BitmapImage();
            Logo  = (BitmapImage)Application.Current.FindResource("Icon");
            CurrentImage = Logo;
        }
        public void SetLogo()
        {
            
            CurrentImage = Logo;
        }
        private BitmapSource _CurrentImage;
        public BitmapSource CurrentImage
        {
            get { return _CurrentImage; }
            set {SetProperty(ref _CurrentImage, value); }
        }
    }
}
