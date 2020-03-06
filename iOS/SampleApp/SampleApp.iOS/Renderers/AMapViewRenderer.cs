using System;
using AMapLocationKit;
using AMapNaviKit;
using CoreLocation;
using SampleApp.Controls;
using SampleApp.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AMapView), typeof(AMapViewRenderer))]
namespace SampleApp.iOS.Renderers
{
    public class AMapViewRenderer : ViewRenderer<AMapView, MAMapView>
    {
        private MAMapView mapView;
        
        protected override void OnElementChanged(ElementChangedEventArgs<AMapView> e)
        {
            mapView = new MAMapView();
            mapView.ShowsUserLocation(true);
            mapView.SetUserTrackingMode(MAUserTrackingMode.FollowWithHeading, true);
            mapView.UpdateUserLocationRepresentation(new MAUserLocationRepresentation
            {
                EnablePulseAnnimation = true,
                ShowsHeadingIndicator = true
            });
            
            SetNativeControl(mapView);
        }
    }

    
}
