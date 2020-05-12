using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());            
        }

        public async Task ServiceId() 
        {
            using (var httpclient = new HttpClient())
            {
                var response = await httpclient.GetAsync("https://tsapi.amap.com/v1/track/service/list?key=你的web服务的key，不是高德Android那个key");
                var resultStr = await response.Content.ReadAsStringAsync();
            }
        }

        public async Task CreateServiceId() 
        {
            using (var httpclient = new HttpClient())
            {
                var list = new List<System.Collections.Generic.KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("name","测试id123"),
                    new KeyValuePair<string, string>("key","你的web服务的key，不是高德Android那个key"),
                };
                var response = await httpclient.PostAsync("https://tsapi.amap.com/v1/track/service/add", new System.Net.Http.FormUrlEncodedContent(list));
                var resultStr = await response.Content.ReadAsStringAsync();                
            }
        }

        protected override void OnStart()
        {
            
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
