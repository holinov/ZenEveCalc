using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Zen.EveCalc.Controls
{
    public class FavoriteBorderConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var val = (bool) value;
            return val ? new Thickness(5, 0, 0, 0) : new Thickness(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}