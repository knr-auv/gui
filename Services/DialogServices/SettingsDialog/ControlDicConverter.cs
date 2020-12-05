using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Services.Converters
{
    public class ControlDicConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = (String)value;
            string ret = "";
            ret += char.ToUpper(text[0]);
            foreach(char c in text.Substring(1))
            {
                if (char.IsUpper(c))
                {
                    ret += " ";
                }
                ret += c;
            }
            
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
