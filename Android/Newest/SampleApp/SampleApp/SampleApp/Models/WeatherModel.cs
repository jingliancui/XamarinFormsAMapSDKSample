using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.Models
{
    public class WeatherModel
    {
        public string ReportTime { get; set; }
        public string Weather { get; set; }
        public string Temperature { get; set; }
        public string WindDirection { get; set; }
        public string WindPower { get; set; }
        public string Humidity { get; set; }
    }
}
