using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;
using UnityEngine.Networking;

namespace Weather_com
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

        //private UnityAction onComplete;
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
            //this.onComplete = onComplete;
            AsyncUtil.StartCoroutine(UpdateData());
        }
        IEnumerator UpdateData()
        {
            using (var request = UnityWebRequest.Get(url))
            {
                yield return request.Send();
                var headers = request.GetResponseHeaders();
                
                var newUrl = @"http://www.weather.com.cn/data/sk/101020100.html?_=" + GetCurrentTime();
                Debug.Log(newUrl);
                
                using (var newRequest = UnityWebRequest.Get(newUrl))
                {
                    foreach (var item in headers)
                    {
                        Debug.Log(item.Key + ":" + item.Value);
                    }
                    newRequest.SetRequestHeader("Cookie", "");
                    yield return newRequest.Send();
                    Debug.Log(newRequest.downloadHandler.text);
                }
            }
        }
        private long GetCurrentTime()
        {
            System.DateTime startTime = System.TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(System.DateTime.Now - startTime).TotalMilliseconds; // 相差毫秒数
            return timeStamp;
        }
        private void AnalysisData(byte[] contentBytes)
        {
            using (MemoryStream stream = new MemoryStream(contentBytes))
            using (StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8))
            {
               
                string content = reader.ReadToEnd();
                AnalysisDays(content);
            }

        }

      
        private void AnalysisDays(string content)
        {
            var matches = Regex.Matches(content, "<script>\nvar hour3data\\s*=\\s*(.*?)</script>", RegexOptions.Singleline);//"<script>\nvarobserve24h_data=(.*?)</script>"
            if (matches.Count > 0)
            {
                Debug.Log(matches[0].Groups[1].Value);
            }
        }

    }
}

