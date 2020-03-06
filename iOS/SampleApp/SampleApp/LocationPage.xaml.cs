using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SampleApp
{
    public partial class LocationPage : ContentPage
    {
        public LocationPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<object,LocationModel>(this, "receievedloc", (s,d) =>
            {
                Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
                {
                    AddressLabel.Text = d.Address;
                });
            });

        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<object>(this, "receievedloc");
            
        }
    }
}
