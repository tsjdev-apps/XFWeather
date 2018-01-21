using CommonServiceLocator;
using OpenWeatherMapSharp;
using Unity;
using Unity.Lifetime;
using Unity.ServiceLocation;
using XFWeather.Utils;
using XFWeather.ViewModels;

namespace XFWeather.Init
{
    public static class Bootstrapper
    {
        public static void Initialize()
        {
            var container = new UnityContainer();

            // services
            container.RegisterInstance(new OpenWeatherMapService(Credentials.ApiKey));

            // viewmodels
            container.RegisterType<WeatherViewModel>(new ContainerControlledLifetimeManager());

            var locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}
