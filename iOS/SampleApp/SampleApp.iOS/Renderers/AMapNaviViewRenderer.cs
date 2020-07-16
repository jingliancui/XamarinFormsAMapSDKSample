using System;
using AMapNaviKit;
using Foundation;
using SampleApp.Controls;
using SampleApp.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AMapNaviView), typeof(AMapNaviViewRenderer))]
namespace SampleApp.iOS.Renderers
{
    public class AMapNaviViewRenderer : ViewRenderer<AMapNaviView, AMapNaviKit.AMapNaviDriveView>
    {
        private AMapNaviKit.AMapNaviDriveView driveView;

        protected override void OnElementChanged(ElementChangedEventArgs<AMapNaviView> e)
        {
            AMapNaviKit.AMapNaviDriveManager.SharedInstance.Delegate = new SampleAppNaviDriveManagerDelegate();

            if (driveView == null)
            {
                driveView = new AMapNaviKit.AMapNaviDriveView
                {
                    Delegate = new SampleAppNaviDriveViewDelegate()
                };
            }
            
            AMapNaviKit.AMapNaviDriveManager.SharedInstance.AddDataRepresentative(driveView);

            var startPoint = new AMapNaviKit.AMapNaviPoint { Latitude = 39.98f, Longitude = 116.47f };
            var endPoint = new AMapNaviKit.AMapNaviPoint { Latitude = 39.99f, Longitude = 116.45f };
            var spoints = new AMapNaviKit.AMapNaviPoint[] { startPoint };
            var epoints = new AMapNaviKit.AMapNaviPoint[] { endPoint };
            AMapNaviKit.AMapNaviDriveManager.SharedInstance.CalculateDriveRouteWithStartPoints(spoints, epoints, null, AMapNaviKit.AMapNaviDrivingStrategy.Default);
            SetNativeControl(driveView);
        }
    }

    public class SampleAppNaviDriveViewDelegate : AMapNaviKit.AMapNaviDriveViewDelegate
    {
        
    }
    public class SampleAppNaviDriveManagerDelegate : AMapNaviKit.AMapNaviDriveManagerDelegate
    {
        [Export("driveManagerOnCalculateRouteSuccess:")]
        public void DriveManagerOnCalculateRouteSuccess(AMapNaviKit.AMapNaviDriveManager driveManager)
        {
            //invoke this property to invoke the method startEmulatorNavi of object-c .
            
            var result = AMapNaviKit.AMapNaviDriveManager.SharedInstance.StartEmulatorNavi;
        }
    }
}
