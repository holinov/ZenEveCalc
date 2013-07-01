using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Zen.EveCalc.EveDB
{
    public class EveIdToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eveid = (int)value;
            var fileName = string.Format("{0}_{1}.png", eveid, Small ? "32" : "64");
            var uri = new Uri("pack://application:,,,/Zen.EveCalc.EveDB;component/Images/" + fileName);
            var image = new BitmapImage(uri);
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public bool Small { get; set; }
    }
}
