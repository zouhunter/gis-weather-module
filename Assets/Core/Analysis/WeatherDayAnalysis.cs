using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
namespace Weather
{
    public class WeatherDayAnalysis : WeatherAnalysis
    {
        private string url
        {
            get
            {
                return "http://www.weather.com.cn/weather1d/" + GetUrlDress(provinceID, cityID, districtID);
            }

        }
        public WeatherDayAnalysis(string provinceID, string cityID, string districtID) : base(provinceID, cityID, districtID) { }


    }
}

