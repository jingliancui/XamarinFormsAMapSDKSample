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
using Com.Amap.Api.Navi;
using Com.Amap.Api.Navi.Enums;
using Com.Amap.Api.Navi.Model;
using SampleApp.Controls;
using SampleApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(XAMapNaviView), typeof(AMapNaviViewRenderer))]
namespace SampleApp.Droid.Renderers
{
    public class AMapNaviViewRenderer:ViewRenderer<XAMapNaviView, Android.Widget.RelativeLayout>
    {
        public AMapNaviViewRenderer(Context context):base(context)
        {
        }

        private Android.Widget.RelativeLayout mRelativeLayout;        

        protected override void OnElementChanged(ElementChangedEventArgs<XAMapNaviView> e)
        {
            mRelativeLayout = (Context as MainActivity).naviRelativeLayout;
            (Context as MainActivity).navi.StartNavi(NaviType.Emulator);
            SetNativeControl(mRelativeLayout);
        }
        protected override void OnDetachedFromWindow()
        {
            base.OnDetachedFromWindow();
            mRelativeLayout.RemoveFromParent();
        }
    }
}

