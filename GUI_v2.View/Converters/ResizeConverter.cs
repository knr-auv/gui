using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace GUI_v2.View.Converters
{
    class ResizeConverter : IValueConverter
    {
      
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string p = (string)parameter;
            var splited = p.Split(' ');
            double image_size_top = Double.Parse(splited[0], CultureInfo.InvariantCulture);
            image_size_top = image_size_top / 100.0 * (double)value  ;
            double image_size_left = Double.Parse(splited[1], CultureInfo.InvariantCulture);
            image_size_left = image_size_left / 100.0 * (double)value;
            return new Thickness(System.Convert.ToDouble(image_size_left), System.Convert.ToDouble(image_size_top), 0, 0);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        
    }
}
