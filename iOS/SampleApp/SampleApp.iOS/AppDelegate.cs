using System;
using System.Collections.Generic;
using System.Linq;
using AMapFoundationKit;
using AMapLocationKit;
using CoreLocation;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace SampleApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private AMapLocationKit.AMapLocationManager locationManager;
        private AMapTrackKit.AMapTrackManager trackManager;
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            var aAMapFoundationKit = AMapFoundationKit.Constants.AMapFoundationVersion;
            //var maMapKitVersion = MAMapKit.Constants.MAMapKitVersion;
            var aMapSearchVersion = AMapSearchKit.Constants.AMapSearchVersion;
            var aMapLocationVersion = AMapLocationKit.Constants.AMapLocationVersion;
            var aMapNaviKit = AMapNaviKit.Constants.AMapNaviVersion;
            var internalMaMapKitVersion = AMapNaviKit.Constants.MAMapKitVersion;
            var aMapTrackVersion = AMapTrackKit.Constants.AMapTrackVersion;
            AMapServices.SharedServices.ApiKey = "填写你申请的key";
            AMapServices.SharedServices.EnableHTTPS = true;

            locationManager = new AMapLocationKit.AMapLocationManager
            {
                Delegate = new SampleAppLocationManagerDelegate(),
                LocatingWithReGeocode = true
            };

            locationManager.StartUpdatingLocation();

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            MessagingCenter.Subscribe<object>(this, "starttrack", d =>
            {
                var moptions = new AMapTrackKit.AMapTrackManagerOptions
                {
                    ServiceID = "123"//Service ID 需要根据需要进行修改
                };
                trackManager = new AMapTrackKit.AMapTrackManager(moptions)
                {
                    Delegate = new SampleAppTrackManagerDelegate(),
                    AllowsBackgroundLocationUpdates = true,
                    PausesLocationUpdatesAutomatically = false,                    
                };
                trackManager.ChangeGatherAndPackTimeInterval(1, 1);
                trackManager.SetLocalCacheMaxSize(50);
                var sOptions = new AMapTrackKit.AMapTrackManagerServiceOption
                {
                    TerminalID="456",//Terminal ID 需要根据需要进行修改
                };
                trackManager.StartServiceWithOptions(sOptions);
                trackManager.StartGatherAndPack();
            });

            return base.FinishedLaunching(app, options);
        }
    }

    public class SampleAppTrackManagerDelegate : AMapTrackKit.AMapTrackManagerDelegate
    {
        [Export("onStartService:")]
        public void OnStartService(AMapTrackKit.AMapTrackErrorCode errorCode)
        {
            MessagingCenter.Send(new object(), "trackresult1", errorCode.ToString());
        }

        [Export("onStartGatherAndPack:")]
        public void OnStartGatherAndPack(AMapTrackKit.AMapTrackErrorCode errorCode)
        {
            MessagingCenter.Send(new object(), "trackresult2", errorCode.ToString());
        }

        [Export("onStopService:")]
        public void OnStopService(AMapTrackKit.AMapTrackErrorCode errorCode)
        {
            MessagingCenter.Send(new object(), "trackresult3", errorCode.ToString());
        }

        [Export("onStopGatherAndPack:")]
        public void OnStopGatherAndPack(AMapTrackKit.AMapTrackErrorCode errorCode)
        {
            MessagingCenter.Send(new object(), "trackresult4", errorCode.ToString());
        }
    }

    public class SampleAppLocationManagerDelegate : AMapLocationManagerDelegate
    {
        public override void AmapLocationManagerDoRequireLocationAuth(AMapLocationManager manager, CLLocationManager locationManager)
        {
            locationManager.RequestAlwaysAuthorization();
        }
        public override void AmapLocationManagerDidUpdateLocationReGeocode(AMapLocationManager manager, CLLocation location, AMapLocationReGeocode reGeocode)
        {
            if (reGeocode != null)
            {
                //地址
                var address = $"{reGeocode.Country}{reGeocode.Province}{reGeocode.City}";
                var addData = location.Coordinate.Longitude.ToString() + location.Coordinate.Latitude.ToString();
                MessagingCenter.Send(new object(), "receievedloc", new LocationModel { Address = address, AddressData = addData });
            }
        }
    }
}
