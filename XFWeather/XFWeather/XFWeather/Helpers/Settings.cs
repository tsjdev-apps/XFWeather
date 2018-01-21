using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace XFWeather.Helpers
{
	public static class Settings
	{
		private static ISettings AppSettings => CrossSettings.Current;

	    #region Setting Constants

        private const string IsMetricKey = "IsMetric";
	    private const bool IsMetricDefault = true;


	    private const string UseGpsKey = "UseGps";
	    private const bool UseGpsDefault = false;


	    private const string CityKey = "City";
	    public const string CityDefault = "Pforzheim";

        #endregion


        public static bool IsMetric
        {
            get => AppSettings.GetValueOrDefault(IsMetricKey, IsMetricDefault);
            set => AppSettings.AddOrUpdateValue(IsMetricKey, value);
        }


        public static bool UseGps
        {
            get => AppSettings.GetValueOrDefault(UseGpsKey, UseGpsDefault);
            set => AppSettings.AddOrUpdateValue(UseGpsKey, value);
        }

        public static string City
        {
            get => AppSettings.GetValueOrDefault(CityKey, CityDefault);
            set => AppSettings.AddOrUpdateValue(CityKey, value);
        }
    }
}