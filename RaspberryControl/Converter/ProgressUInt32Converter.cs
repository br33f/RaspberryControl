using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace RaspberryControl.Converter
{
    class ProgressUInt32Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (UInt32)value / (UInt32.MaxValue / 100);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (UInt32)(((double)value / 100) * UInt32.MaxValue);
        }
    }
}