using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TextProcessor
{
    public class ProgressToVisibilityConverter : IMultiValueConverter
    {
        /// <summary>
        /// If (float)value[0] >= (float)value[1] returns Visibility.Visible, else returns Visibility.Collapsed
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Length == 2 && value[0] is int progress && value[1] is int maxProgress)
            {
                if (progress >= maxProgress)
                    return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
