using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFWeather.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForecastPage : ContentPage
    {
        public ForecastPage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
                Icon = new FileImageSource { File = "tab2.png" };
        }
    }
}