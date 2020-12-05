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
              
                CurrentImage = ImageHelpers.BytesToBitmapImage(frame);
              

            }), DispatcherPriority.Render);

        }
        public CameraStreamViewModel() 
        {
            _CurrentImage = new BitmapImage();
            Logo  = (BitmapImage)Application.Current.FindResource("Icon");
            CurrentImage = Logo;
        }
        public void SetLogo()
        {
            
            CurrentImage = Logo;
        }
        private BitmapImage _CurrentImage;
        public BitmapImage CurrentImage
        {
            get { return _CurrentImage; }
            set { SetProperty(ref _CurrentImage, value); }
        }
    }
}
