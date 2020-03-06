using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Amap.Api.Maps;
using SampleApp.Controls;
using SampleApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AMapView), typeof(AMapViewRenderer))]
namespace SampleApp.Droid.Renderers
{
    public class AMapViewRenderer:ViewRenderer<AMapView, MapView>
    {
        public AMapViewRenderer(Context context):base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AMapView> e)
        {
            var view = (Context as MainActivity).mapView;
            SetNativeControl(view);
        }
    }
}