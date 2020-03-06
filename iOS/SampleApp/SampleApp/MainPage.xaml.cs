using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SampleApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void MapSampleBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new MapPage());
        }

        async void LocationSampleBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new LocationPage());
        }

        async void NaviSampleBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new NaviPage());
        }

        async void TrackSampleBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new TrackPage());
        }
    }
}
