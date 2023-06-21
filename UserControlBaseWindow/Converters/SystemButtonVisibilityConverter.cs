using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace UserControlBaseWindow.Converters
{
    public class SystemButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ResizeMode resizeMode))
                return Visibility.Collapsed;

            var visibility = Visibility.Collapsed;
            switch (resizeMode)
            {
                case ResizeMode.CanResize:
                case ResizeMode.CanResizeWithGrip:
                    visibility = Visibility.Visible;
                    break;
                case ResizeMode.NoResize:
                case ResizeMode.CanMinimize:
                    visibility = Visibility.Collapsed;
                    break;
                default:
                    break;
            }
            return visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SystemButtonMinimizeVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ResizeMode resizeMode))
                return Visibility.Collapsed;

            return resizeMode == ResizeMode.NoResize ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
