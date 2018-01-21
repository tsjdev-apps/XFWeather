using CommonServiceLocator;
using Xamarin.Forms;
using XFWeather.Init;
using XFWeather.ViewModels;
using XFWeather.Views;

namespace XFWeather
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            Bootstrapper.Initialize();

            var tabs = new TabbedPage { Title = "XFWeather", BindingContext = ServiceLocator.Current.GetInstance<WeatherViewModel>(), Children = { new WeatherPage(), new ForecastPage() } };
		    MainPage = new NavigationPage(tabs) { BarBackgroundColor = Color.FromHex("3498db"), BarTextColor = Color.White };
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
