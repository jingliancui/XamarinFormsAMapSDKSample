using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SampleApp
{
    public partial class TrackPage : ContentPage
    {
        public TrackPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<object, string>(this, "trackresult1", (d, s) =>
            {
                Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
                {
                    OnStartServiceLabel.Text = s;
                });
            });
            MessagingCenter.Subscribe<object, string>(this, "trackresult2", (d, s) =>
            {
                Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
                {
                    OnStartPackLabel.Text = s;
                });
            });
            MessagingCenter.Subscribe<object, string>(this, "trackresult3", (d, s) =>
            {
                Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
                {
                    OnStopServiceLabel.Text = s;
                });
            });
            MessagingCenter.Subscribe<object, string>(this, "trackresult4", (d, s) =>
            {
                Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
                {
                    OnStopPackLabel.Text = s;
                });
            });
            MessagingCenter.Send(new object(), "starttrack");            
        }

    }
}
