using System;
using System.Globalization;
using Xamarin.Forms;

namespace XFWeather.Converters
{
    public class DateTimeToFormattedDateTimeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string dateAsString))
                return null;

            return DateTime.Parse(dateAsString).ToLocalTime().ToString("g");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
