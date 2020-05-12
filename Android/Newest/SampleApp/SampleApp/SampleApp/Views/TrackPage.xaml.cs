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
    public partial class TrackPage : ContentPage
    {
        public TrackPage()
        {
            InitializeComponent();
        }

        public const string StartTrack = "StartTrack";

        public const string BindServiceFinished = "BindServiceFinished";

        public const string StartTrackFinished = "StartTrackFinished";

        private void StartTrackBtn_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(new object(), StartTrack);
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<object, string>(this, BindServiceFinished, (sender, args) => 
            {
                Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() => 
                {
                    BindServiceLabel.Text = args;
                });
            });
            MessagingCenter.Subscribe<object, string>(this,StartTrackFinished, (sender, args) =>
            {
                Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
                {
                    StartTrackLabel.Text = args;
                });
            });
        }
    }
}