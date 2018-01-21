using Plugin.Geolocator;
using Plugin.TextToSpeech;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using OpenWeatherMapSharp;
using OpenWeatherMapSharp.Enums;
using OpenWeatherMapSharp.Models;
using Xamarin.Forms;
using XFWeather.Helpers;

namespace XFWeather.ViewModels
{
    public class WeatherViewModel : BaseViewModel
    {
        private readonly OpenWeatherMapService _openWeatherMapService;

        private string _location = Settings.City;
        public string Location
        {
            get => _location;
            set { _location = value; OnPropertyChanged(); Settings.City = value; }
        }

        private bool _useGps = Settings.UseGps;
        public bool UseGps
        {
            get => _useGps;
            set { _useGps = value; OnPropertyChanged(); Settings.UseGps = value; }
        }

        private bool _isMetric = Settings.IsMetric;
        public bool IsMetric
        {
            get => _isMetric;
            set { _isMetric = value; OnPropertyChanged(); Settings.IsMetric = value; }
        }

        private string _temperature = string.Empty;
        public string Temperature
        {
            get => _temperature;
            set { _temperature = value; OnPropertyChanged(); }
        }

        private string _condition = string.Empty;
        public string Condition
        {
            get => _condition;
            set { _condition = value; OnPropertyChanged(); }
        }

        private WeatherForecastRoot _forecast;
        public WeatherForecastRoot Forecast
        {
            get => _forecast;
            set { _forecast = value; OnPropertyChanged(); }
        }

        private ICommand _getWeather;
        public ICommand GetWeatherCommand => _getWeather ?? (_getWeather = new Command(async () => await ExecuteGetWeatherCommand()));


        public WeatherViewModel(OpenWeatherMapService openWeatherMapService)
        {
            _openWeatherMapService = openWeatherMapService;
        }


        private async Task ExecuteGetWeatherCommand()
        {
            if (IsLoading)
                return;

            IsLoading = true;
            try
            {
                WeatherRoot weatherRoot = null;
                var unit = IsMetric ? Unit.Metric : Unit.Imperial;

                if (UseGps)
                {
                    var gps = await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromSeconds(5));
                    var openWeatherMapServiceGpsResponse = await _openWeatherMapService.GetWeatherAsync(gps.Latitude, gps.Longitude, LanguageCode.EN, unit);

                    if (openWeatherMapServiceGpsResponse.IsSuccess)
                        weatherRoot = openWeatherMapServiceGpsResponse.Response;
                }
                else
                {
                    var openWeatherMapServiceCityResponse = await _openWeatherMapService.GetWeatherAsync(Location.Trim(), LanguageCode.EN, unit);

                    if (openWeatherMapServiceCityResponse.IsSuccess)
                        weatherRoot = openWeatherMapServiceCityResponse.Response;
                }

                if (weatherRoot != null)
                {
                    var openWeatherMapServiceForecastResponse = await _openWeatherMapService.GetForecastAsync(weatherRoot.CityId, LanguageCode.EN, unit);

                    if (openWeatherMapServiceForecastResponse.IsSuccess)
                        Forecast = openWeatherMapServiceForecastResponse.Response;

                    var unitString = IsMetric ? "C" : "F";
                    Temperature = $"Temperature: {weatherRoot.MainWeather?.Temperature ?? 0}°{unitString}";
                    Condition = $"{weatherRoot.Name}: {weatherRoot.Weather?[0]?.Description ?? string.Empty}";

                    await CrossTextToSpeech.Current.Speak(Temperature + " " + Condition);
                }
                else
                {
                    Temperature = "Something went wrong! Please try again.";
                    Condition = string.Empty;
                    Forecast = null;
                }
            }
            catch (Exception ex)
            {
                Temperature = "Unable to get weather!";
                Condition = ex.Message;
                Forecast = null;
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
