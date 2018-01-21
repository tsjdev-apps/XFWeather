using Xamarin.Forms;
using XFWeather.Init;

namespace XFWeather
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            Bootstrapper.Initialize();
            
			MainPage = new XFWeather.MainPage();
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
