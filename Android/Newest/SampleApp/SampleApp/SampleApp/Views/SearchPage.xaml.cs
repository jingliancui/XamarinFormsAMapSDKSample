using SampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        public const string QueryWeather = "QueryWeather";
        public const string QueryWeatherOk = "QueryWeatherOk";

        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<object, WeatherModel>(this, QueryWeatherOk, (sender, args) => 
            {
                Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() => 
                {
                    ReportTimeLabel.Text = $"{args.ReportTime}发布";
                    WeatherLabel.Text = args.Weather;
                    TemperatureLabel.Text = $"{args.Temperature}°";
                    WindDirectionLabel.Text = $"吹{args.WindDirection}风";
                    WindPowerLabel.Text = $"风力{args.WindPower}级";
                    HumidityLabel.Text = $"湿度{args.Humidity}";
                });
            });
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<object>(this, QueryWeatherOk);
        }

        private void WeatherBtn_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(new object(), QueryWeather);
        }
    }
}