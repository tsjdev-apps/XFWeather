using System;
using System.Globalization;
using CommonServiceLocator;
using Xamarin.Forms;
using XFWeather.ViewModels;

namespace XFWeather.Converters
{
    public class TemperatureToFormattedTemperatureConverter : IValueConverter
    {
        private readonly WeatherViewModel _weatherViewModel;

        public TemperatureToFormattedTemperatureConverter()
        {
            _weatherViewModel = ServiceLocator.Current.GetInstance<WeatherViewModel>();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var units = _weatherViewModel.IsMetric ? "C" : "F";
            return $"Temperature: {value ?? 0}° {units}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
