using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WPF.Shared.Converters
{
    public class BoolToVisibleOrCollapsed : IValueConverter
    {
        #region Convert
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility result = default;

            bool bValue = (bool)value;
            if (bValue)
            {
                result = Visibility.Visible;
            }
            else
            {
                result = Visibility.Collapsed;
            }
            return result;
        }
        #endregion Convert

        #region ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool result = default;
            Visibility visibility = (Visibility)value;

            if (visibility == Visibility.Visible)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        #endregion ConvertBack
    }
}
