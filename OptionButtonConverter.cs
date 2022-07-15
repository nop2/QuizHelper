using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfAppDemo1
{
    public class IOptionButtonConverter : IValueConverter
    {
        static SolidColorBrush[] SolidColorBrushes { get; } = { new SolidColorBrush(Colors.BlanchedAlmond), new SolidColorBrush(Colors.LightGreen) };
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (bool) value == false)
            {
                return SolidColorBrushes[0];
            }

            return SolidColorBrushes[1];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}