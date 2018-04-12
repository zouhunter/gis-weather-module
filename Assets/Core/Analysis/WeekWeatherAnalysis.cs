using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Weather
{
    class WeekWeatherAnalysis : WeatherAnalysis
    {
        private string weekUrl
        {
            get
            {
                string url = "http://www.weather.com.cn/weather/" + GetUrlDress(provinceID, cityID, districtID);
                return url;
            }
           
        }
        private string weekMoreUrl
        {
            get
            {
                var url = "http://www.weather.com.cn/weather15d/" + GetUrlDress(provinceID, cityID, districtID);
                return url;
            }
        }
        public WeekWeatherAnalysis(string provinceID, string cityID, string districtID) : base(provinceID, cityID, districtID) { }

        public string[] Day_1To7 { private set; get; }
        public string[] Day_7To15 { private set; get; }
        public string[] Info_1To7 { private set; get; }
        public string[] Info_7To15 { private set; get; }
        public string[] Temperature_1To7 { private set; get; }
        public string[] Temperature_7To15 { private set; get; }
        public string[] Wind_1To7 { private set; get; }
        public string[] Wind1_7To15 { private set; get; }
        public string[] Wind2_7To15 { private set; get; }
        public WeatherStatus[] WeatherStatus_1To7 { private set; get; }
        public WeatherStatus[] WeatherStatus_7To15 { private set; get; }

        public void HandleWeather()
        {
            using (WebClient webClient = new WebClient() { Encoding = Encoding.UTF8 })
            {
                UnityEngine.Debug.Log(weekUrl);
                byte[] contentBytes = webClient.DownloadData(weekUrl);
                Analysis7DayData(contentBytes);

                //More
                contentBytes = webClient.DownloadData(weekMoreUrl);
                AnalysisMoreData(contentBytes);
            }
        }
       
        private void Analysis7DayData(byte[] contentBytes)
        {
            using (MemoryStream stream = new MemoryStream(contentBytes))
            using (StreamReader reader = new StreamReader(stream))
            {
                string content = reader.ReadToEnd();
                MatchCollection matches = Regex.Matches(content, "<ul class=\"t clearfix\">.*?</ul>", RegexOptions.Singleline);
                if (matches.Count > 0)
                {
                    matches = Regex.Matches(matches[0].Value, "<li class=\"sky skyid.*?</li>", RegexOptions.Singleline);
                    if (matches.Count == 7)
                    {
                        List<string> dayList = new List<string>();
                        List<string> infoList = new List<string>();
                        List<string> tempList = new List<string>();
                        List<string> windList = new List<string>();
                        List<WeatherStatus> statusList = new List<WeatherStatus>();
                        MatchCollection matches_temp;

                        foreach (Match match in matches)
                        {
                            //日期：31日（今日）
                            matches_temp = Regex.Matches(match.Value, "<h1>.*?</h1>");
                            if (matches_temp.Count > 0) dayList.Add(RemoveAngleBrackets(matches_temp[0].Value));
                            else dayList.Add("");
                            //天气信息：晴
                            matches_temp = Regex.Matches(match.Value, "<p.*class=\"wea\".*?</p>");
                            if (matches_temp.Count > 0)
                            {
                                infoList.Add(RemoveAngleBrackets(matches_temp[0].Value));
                                WeatherStatus status = GetWeatherStatus(RemoveAngleBrackets(matches_temp[0].Value));
                                if (status == WeatherStatus.Weizhi)
                                {
                                    status = GetUnkownWeatherStatus(RemoveAngleBrackets(matches_temp[0].Value));
                                }
                                statusList.Add(status);
                            }
                            else
                            {
                                dayList.Add("");
                                statusList.Add(WeatherStatus.Weizhi);
                            }
                            //温度信息：4/-1℃
                            //风力信息：3-4级转3级
                            matches_temp = Regex.Matches(match.Value, "<span>.*?</span>");
                            string temperature = "";
                            string wind = "";
                            if (matches_temp.Count > 0)
                            {
                                temperature = RemoveAngleBrackets(matches_temp[0].Value) + "℃";
                            }
                            matches_temp = Regex.Matches(match.Value, "<i>.*?</i>");
                            if (matches_temp.Count >= 1)
                            {
                                temperature = string.IsNullOrEmpty(temperature) ? RemoveAngleBrackets(matches_temp[0].Value) : string.Format("{0}/{1}", temperature, RemoveAngleBrackets(matches_temp[0].Value));
                            }
                            if (matches_temp.Count >= 2)
                            {
                                wind = RemoveAngleBrackets(matches_temp[1].Value);
                            }
                            tempList.Add(temperature);
                            windList.Add(wind);
                        }
                        this.Day_1To7 = dayList.ToArray();
                        this.Info_1To7 = infoList.ToArray();
                        this.Temperature_1To7 = tempList.ToArray();
                        this.Wind_1To7 = windList.ToArray();
                        this.WeatherStatus_1To7 = statusList.ToArray();
                    }
                }
            }

        }
        private void AnalysisMoreData(byte[] contentBytes)
        {
            using (MemoryStream stream = new MemoryStream(contentBytes))
            using (StreamReader reader = new StreamReader(stream))
            //----
            {
                string content = reader.ReadToEnd();
                MatchCollection matches = Regex.Matches(content, "<ul class=\"t clearfix\">.*?</ul>", RegexOptions.Singleline);
                if (matches.Count > 0)
                {
                    matches = Regex.Matches(matches[0].Value, "<li.*?</li>", RegexOptions.Singleline);
                    if (matches.Count == 8)
                    {
                        List<string> dayList = new List<string>();
                        List<string> infoList = new List<string>();
                        List<string> tempList = new List<string>();
                        List<string> wind1List = new List<string>();
                        List<string> wind2List = new List<string>();
                        List<WeatherStatus> statusList = new List<WeatherStatus>();
                        MatchCollection matches_temp;
                        foreach (Match match in matches)
                        {
                            //日期：31日（今日）
                            matches_temp = Regex.Matches(match.Value, "<span class=\"time\">.*?</span>");
                            if (matches_temp.Count > 0) dayList.Add(RemoveAngleBrackets(matches_temp[0].Value));
                            else dayList.Add("");
                            //天气信息：晴
                            matches_temp = Regex.Matches(match.Value, "<span class=\"wea\">.*?</span>");
                            if (matches_temp.Count > 0)
                            {
                                infoList.Add(RemoveAngleBrackets(matches_temp[0].Value));
                                WeatherStatus status = GetWeatherStatus(RemoveAngleBrackets(matches_temp[0].Value));
                                if (status == WeatherStatus.Weizhi)
                                {
                                    status = GetUnkownWeatherStatus(RemoveAngleBrackets(matches_temp[0].Value));
                                }
                                statusList.Add(status);
                            }
                            else
                            {
                                dayList.Add("");
                                statusList.Add(WeatherStatus.Weizhi);
                            }
                            //温度信息：4/-1℃
                            //风力信息：3-4级转3级
                            matches_temp = Regex.Matches(match.Value, "<span class=\"tem\".*?</span>");
                            if (matches_temp.Count > 0) tempList.Add(RemoveAngleBrackets(RemoveAngleBrackets(matches_temp[0].Value)));
                            else tempList.Add("");

                            matches_temp = Regex.Matches(match.Value, "<span class=\"wind\">.*?</span>");
                            if (matches_temp.Count > 0) wind1List.Add(RemoveAngleBrackets(matches_temp[0].Value));
                            else wind1List.Add("");

                            matches_temp = Regex.Matches(match.Value, "<span class=\"wind1\">.*?</span>");
                            if (matches_temp.Count > 0) wind2List.Add(RemoveAngleBrackets(matches_temp[0].Value));
                            else wind2List.Add("");
                        }
                        this.Day_7To15 = dayList.ToArray();
                        this.Info_7To15 = infoList.ToArray();
                        this.Temperature_7To15 = tempList.ToArray();
                        this.Wind1_7To15 = wind1List.ToArray();
                        this.Wind2_7To15 = wind2List.ToArray();
                        this.WeatherStatus_7To15 = statusList.ToArray();
                    }
                }
            }
            //----
        }
    }
}
