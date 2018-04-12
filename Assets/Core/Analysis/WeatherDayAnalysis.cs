using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Weather
{
    public class WeatherDayAnalysis : WeatherAnalysis
    {
        #region 当前
        public string temperature;
        public string humidity;
        public string airQuality;
        public string wind1;
        public string wind2;
        public WeatherStatus weatherStatus;
        #endregion

        #region 一天
        public string[] times;
        public string[] temperatures;
        public string[] wind1s;
        public string[] wind2s;
        public WeatherStatus[] weatherStatuses;
        #endregion

        private UnityAction onComplete;
        private string url
        {
            get
            {
                return "http://www.weather.com.cn/weather1d/" + GetUrlDress(provinceID, cityID, districtID);
            }

        }
        public WeatherDayAnalysis(string provinceID, string cityID, string districtID) : base(provinceID, cityID, districtID) { }

        public void UpdateAsync(UnityAction onComplete)
        {
            this.onComplete = onComplete;
            AsyncUtil.StartCoroutine(UpdateData());
        }
        IEnumerator UpdateData()
        {
            var www = new WWW(url);
            yield return www;
            AnalysisData(www.bytes);
        }

        private void AnalysisData(byte[] contentBytes)
        {
            using (MemoryStream stream = new MemoryStream(contentBytes))
            using (StreamReader reader = new StreamReader(stream,System.Text.Encoding.UTF8))
            {
                string content = reader.ReadToEnd();

               
            }

        }
    }
}

