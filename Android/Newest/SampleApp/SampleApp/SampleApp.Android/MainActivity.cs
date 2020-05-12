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
using Com.Amap.Api.Navi;
using System.Security.Permissions;
using Com.Amap.Api.Navi.Model;
using Com.Autonavi.Tbt;
using System.Collections.Generic;
using Xamarin.Forms;
using SampleApp.Views;
using Com.Amap.Api.Services.Weather;
using SampleApp.Models;
using Com.Amap.Api.Track;
using Com.Amap.Api.Track.Query.Model;

namespace SampleApp.Droid
{
    [Activity(Label = "SampleApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IAMapNaviListener
    {
        //3dmap相关
        public MapView mapView;
        //定位相关
        public AMapLocationClient mLocationClient;
        private MyLocationStyle myLocationStyle;
        //导航相关
        public AMapNavi navi;
        public AMapNaviView mAMapNaviView;
        public Android.Widget.RelativeLayout naviRelativeLayout;
        //存储算路起点的列表 故宫坐标
        protected List<NaviLatLng> sList = new List<NaviLatLng>() { new NaviLatLng( 39.917834, 116.397036) };
        //存储算路终点的列表 广州塔坐标
        protected List<NaviLatLng> eList = new List<NaviLatLng>() { new NaviLatLng( 23.106414, 113.324553) };

        //猎鹰相关
        private AMapTrackClient mAMapTrackClient;

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

            //导航相关            
            navi = AMapNavi.GetInstance(this);
            navi.AddAMapNaviListener(this);
            navi.SetEmulatorNaviSpeed(260);//260km/h
            naviRelativeLayout = LayoutInflater.Inflate(Resource.Layout.NaviLayout, null) as Android.Widget.RelativeLayout;
            mAMapNaviView = naviRelativeLayout.FindViewById<AMapNaviView>(Resource.Id.navi_view);
            mAMapNaviView.SetAMapNaviViewListener(new AMapNaviViewListener());            
            mAMapNaviView.OnCreate(savedInstanceState);            

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            MessagingCenter.Subscribe<object>(this, SearchPage.QueryWeather, sender =>
            {
                //检索参数为城市和天气类型，实况天气为WEATHER_TYPE_LIVE、天气预报为WEATHER_TYPE_FORECAST
                var mquery = new WeatherSearchQuery("北京", WeatherSearchQuery.WeatherTypeLive);
                var mweathersearch = new WeatherSearch(this);
                mweathersearch.SetOnWeatherSearchListener(new OnWeatherSearchListener());
                mweathersearch.Query=mquery;
                mweathersearch.SearchWeatherAsyn(); //异步搜索 
            });

            MessagingCenter.Subscribe<object>(this, TrackPage.StartTrack, sender => 
            {
                //猎鹰相关
                mAMapTrackClient = new AMapTrackClient(this);
                //上报周期，采用默认，不设置
                //mAMapTrackClient.SetInterval
                //本地缓存大小，采用默认，不设置
                //mAMapTrackClient.SetCacheSize
                //定位模式，采用默认，不设置
                //mAMapTrackClient.SetLocationMode

                //执行一次，创建serviceid即可
                //await (Xamarin.Forms.Application.Current as App).CreateServiceId();
                //await (Xamarin.Forms.Application.Current as App).ServiceId();
                //137618从ServiceId()获取后复制的
                mAMapTrackClient.QueryTerminal(new QueryTerminalRequest(137618, "id1234"), new OnTrackListener(137618, "id1234", mAMapTrackClient));
            });

            LoadApplication(new App());            
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnResume()
        {
            base.OnResume();
            mAMapNaviView.OnResume();
        }

        protected override void OnPause()
        {
            base.OnPause();
            mAMapNaviView.OnPause();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            mAMapNaviView.OnDestroy();
        }

        public void OnUpdateTrafficFacilityWithAMapNaviTrafficFacilityInfo(AMapNaviTrafficFacilityInfo p0)
        {
            
        }

        public void OnUpdateTrafficFacilityWithAMapNaviTrafficFacilityInfoArray(AMapNaviTrafficFacilityInfo[] p0)
        {
            
        }

        public void OnUpdateTrafficFacilityWithTrafficFacilityInfo(TrafficFacilityInfo p0)
        {
            
        }

        public void HideCross()
        {
            
        }

        public void HideLaneInfo()
        {
            
        }

        public void HideModeCross()
        {
            
        }

        public void NotifyParallelRoad(int p0)
        {
            
        }

        public void OnArriveDestination()
        {
            
        }

        public void OnArrivedWayPoint(int p0)
        {
            
        }

        public void OnCalculateRouteFailureWithAMapCalcRouteResult(AMapCalcRouteResult p0)
        {
            Console.WriteLine("规划路径失败:" + p0.ErrorCode + p0.ErrorDetail);
        }

        public void OnCalculateRouteFailureWithInt(int p0)
        {
            Console.WriteLine("规划路径失败:" + p0);
        }

        public void OnCalculateRouteSuccessWithAMapCalcRouteResult(AMapCalcRouteResult p0)
        {
            Console.WriteLine("规划路径成功");
        }

        public void OnCalculateRouteSuccessWithIntArray(int[] p0)
        {
            Console.WriteLine("规划路径成功");
        }

        public void OnEndEmulatorNavi()
        {
        }

        public void OnGetNavigationTextWithIntAndString(int p0, string p1)
        {
        }

        public void OnGetNavigationTextWithString(string p0)
        {
        }

        public void OnGpsOpenStatus(bool p0)
        {
        }

        public void OnInitNaviFailure()
        {
        }

        public void OnInitNaviSuccess()
        {
            Console.WriteLine("导航初始化成功");
            int strategy = 0;
            try
            {
                strategy = navi.StrategyConvert(true, false, false, false, false);
            }
            catch (Exception e)
            {
                Console.WriteLine("规划路径错误:" + e.Message);
            }
            navi.CalculateDriveRoute(sList, eList,null, strategy);
        }

        public void OnLocationChange(AMapNaviLocation p0)
        {
        }

        public void OnNaviInfoUpdate(NaviInfo p0)
        {
        }

        public void OnNaviInfoUpdated(AMapNaviInfo p0)
        {
        }

        public void OnNaviRouteNotify(AMapNaviRouteNotifyData p0)
        {
        }

        public void OnPlayRing(int p0)
        {
        }

        public void OnReCalculateRouteForTrafficJam()
        {
        }

        public void OnReCalculateRouteForYaw()
        {
        }

        public void OnServiceAreaUpdate(AMapServiceAreaInfo[] p0)
        {
        }

        public void OnStartNavi(int p0)
        {
        }

        public void OnTrafficStatusUpdate()
        {
        }

        public void ShowCross(AMapNaviCross p0)
        {
        }

        public void ShowLaneInfoWithAMapLaneInfo(AMapLaneInfo p0)
        {
        }

        public void ShowLaneInfoWithAMapLaneInfoArrayAndByteArrayAndByteArray(AMapLaneInfo[] p0, byte[] p1, byte[] p2)
        {
        }

        public void ShowModeCross(AMapModelCross p0)
        {
        }

        public void AMapNaviListenerUpdateAimlessModeCongestionInfo(AimLessModeCongestionInfo p0)
        {
        }

        public void AMapNaviListenerUpdateAimlessModeStatistics(AimLessModeStat p0)
        {
        }

        public void UpdateCameraInfo(AMapNaviCameraInfo[] p0)
        {
        }

        public void UpdateIntervalCameraInfo(AMapNaviCameraInfo p0, AMapNaviCameraInfo p1, int p2)
        {
        }
    }

    public class AMapLocationListener : Java.Lang.Object, IAMapLocationListener
    {
        public void OnLocationChanged(AMapLocation p0)
        {
            if (p0 != null)
            {
                if (p0.ErrorCode == 0)
                {
                    //解析定位结果                    
                }
            }
        }
    }

    public class AMapNaviViewListener : Java.Lang.Object, IAMapNaviViewListener
    {
        public void OnLockMap(bool p0)
        {

        }

        public void OnMapTypeChanged(int p0)
        {

        }

        public bool OnNaviBackClick()
        {
            return true;
        }

        public void OnNaviCancel()
        {

        }

        public void OnNaviMapMode(int p0)
        {

        }

        public void OnNaviSetting()
        {

        }

        public void OnNaviTurnClick()
        {

        }

        public void OnNaviViewLoaded()
        {

        }

        public void OnNaviViewShowMode(int p0)
        {

        }

        public void OnNextRoadClick()
        {

        }

        public void OnScanViewButtonClick()
        {

        }
    }

    public class OnWeatherSearchListener : Java.Lang.Object, WeatherSearch.IOnWeatherSearchListener
    {
        public void OnWeatherForecastSearched(LocalWeatherForecastResult p0, int p1)
        {
            
        }

        public void OnWeatherLiveSearched(LocalWeatherLiveResult weatherLiveResult, int rCode)
        {
            if (rCode == 1000)
            {
                if (weatherLiveResult != null && weatherLiveResult.LiveResult != null)
                {
                    var weatherlive = weatherLiveResult.LiveResult;
                    var model = new WeatherModel 
                    {
                        ReportTime= weatherlive.ReportTime,
                        Humidity= weatherlive.Humidity,
                        Temperature= weatherlive.Temperature,
                        Weather= weatherlive.Weather,
                        WindDirection= weatherlive.WindDirection,
                        WindPower= weatherlive.WindPower
                    };

                    //发回给forms层
                    MessagingCenter.Send(new object(), SearchPage.QueryWeatherOk, model);
                }
                else
                {
                    //ToastUtil.show(WeatherSearchActivity.this, R.string.no_result);
                    Console.WriteLine("查询天气无数据");
                }
            }
            else
            {
                Console.WriteLine("查询天气错误:" + rCode);
            }
        }
    }

    public class OnTrackListener : Java.Lang.Object, IOnTrackListener
    {
        private AMapTrackClient mAMapTrackClient;
        private string terminalName;
        private long serviceId;
        public OnTrackListener(long p0,string p1,AMapTrackClient aMapTrackClient)
        {
            terminalName = p1;
            serviceId = p0;
            mAMapTrackClient = aMapTrackClient;
        }

        public void OnAddTrackCallback(AddTrackResponse p0)
        {
            
        }

        public void OnCreateTerminalCallback(AddTerminalResponse p0)
        {
            
        }

        public void OnDistanceCallback(DistanceResponse p0)
        {
            
        }

        public void OnHistoryTrackCallback(HistoryTrackResponse p0)
        {
            
        }

        public void OnLatestPointCallback(LatestPointResponse p0)
        {
            
        }

        public void OnParamErrorCallback(ParamErrorResponse p0)
        {
            
        }

        public void OnQueryTerminalCallback(QueryTerminalResponse queryTerminalResponse)
        {
            if (queryTerminalResponse.IsSuccess!=true)
            {
                Console.WriteLine(queryTerminalResponse.ErrorCode);
                Console.WriteLine(queryTerminalResponse.ErrorDetail);
                Console.WriteLine(queryTerminalResponse.ErrorMsg);
            }
            if (queryTerminalResponse.IsSuccess)
            {
                if (queryTerminalResponse.Tid<=0)
                {
                    mAMapTrackClient.AddTerminal(new AddTerminalRequest(terminalName, serviceId), new OnTrackListener2(mAMapTrackClient, serviceId));
                }
                else
                {
                    mAMapTrackClient.StartTrack(new TrackParam(serviceId, queryTerminalResponse.Tid), new OnTrackLifecycleListener());
                }
            }
        }

        public void OnQueryTrackCallback(QueryTrackResponse p0)
        {
            
        }
    }

    public class OnTrackListener2 : Java.Lang.Object, IOnTrackListener
    {
        private AMapTrackClient mAMapTrackClient;
        private long mserviceId;
        public OnTrackListener2(AMapTrackClient aMapTrackClient,long serviceid)
        {
            mserviceId = serviceid;
            mAMapTrackClient = aMapTrackClient;
        }
        public void OnAddTrackCallback(AddTrackResponse p0)
        {
            
        }

        public void OnCreateTerminalCallback(AddTerminalResponse addTerminalResponse)
        {
            if (addTerminalResponse.IsSuccess)
            {                
                mAMapTrackClient.StartTrack(new TrackParam(mserviceId, addTerminalResponse.Tid), new OnTrackLifecycleListener());
            }
        }

        public void OnDistanceCallback(DistanceResponse p0)
        {
            
        }

        public void OnHistoryTrackCallback(HistoryTrackResponse p0)
        {
            
        }

        public void OnLatestPointCallback(LatestPointResponse p0)
        {
            
        }

        public void OnParamErrorCallback(ParamErrorResponse p0)
        {
            
        }

        public void OnQueryTerminalCallback(QueryTerminalResponse p0)
        {
            
        }

        public void OnQueryTrackCallback(QueryTrackResponse p0)
        {
            
        }
    }

    public class OnTrackLifecycleListener : Java.Lang.Object, IOnTrackLifecycleListener
    {
        public void OnBindServiceCallback(int p0, string p1)
        {
            MessagingCenter.Send(new object(), TrackPage.BindServiceFinished, p1);
        }

        public void OnStartGatherCallback(int p0, string p1)
        {
            
        }

        public void OnStartTrackCallback(int p0, string p1)
        {
            MessagingCenter.Send(new object(), TrackPage.StartTrackFinished, p1);
        }

        public void OnStopGatherCallback(int p0, string p1)
        {
            
        }

        public void OnStopTrackCallback(int p0, string p1)
        {
            
        }
    }
}