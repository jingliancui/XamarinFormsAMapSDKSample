using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.Amap.Api.Maps;
using Com.Amap.Api.Location;
using Com.Amap.Api.Maps.Model;
using Com.Autonavi.Base.Amap.Mapcore;

namespace SampleApp.Droid
{
    [Activity(Label = "SampleApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //3dmap相关
        public MapView mapView;

        //定位相关
        public AMapLocationClient mLocationClient;
        private MyLocationStyle myLocationStyle;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            
            //3dmap相关
            mapView = new MapView(this);
            mapView.OnCreate(savedInstanceState);

            //定位相关
            mLocationClient = new AMapLocationClient(this);
            mLocationClient.SetLocationListener(new AMapLocationListener());
            mLocationClient.StartLocation();
            myLocationStyle = new MyLocationStyle();
            myLocationStyle.InvokeInterval(2000);
            mapView.Map.MyLocationStyle = myLocationStyle;
            mapView.Map.MyLocationEnabled = true;
            myLocationStyle.InvokeMyLocationType(MyLocationStyle.LocationTypeFollow);
            myLocationStyle.ShowMyLocation(true);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());            
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AMapLocationListener : Java.Lang.Object, IAMapLocationListener
    {
        public void OnLocationChanged(AMapLocation amapLocation)
        {
            if (amapLocation != null)
            {
                if (amapLocation.ErrorCode == 0)
                {
                    //解析定位结果
                    var a = 0;
                }
            }
        }
    }
}