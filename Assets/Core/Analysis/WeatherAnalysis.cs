using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace Weather
{
    public class WeatherAnalysis
    {
        protected string provinceID;
        protected string cityID;
        protected string districtID;
        public WeatherAnalysis(string provinceID,string cityID,string districtID)
        {
            this.provinceID = provinceID;
            this.cityID = cityID;
            this.districtID = districtID;
        }
        protected string GetUrlDress(string provinceID, string cityID, string districtID)
        {
            var url = "";
            if (cityID == "00")
            {
                url += string.Format("{0}{1}{2}.shtml", provinceID, districtID, cityID);
            }
            else if (districtID.Length > 2)
            {
                url += string.Format("{0}.shtml", districtID);
            }
            else
            {
                url += string.Format("{0}{1}{2}.shtml", provinceID, cityID, districtID);
            }
            return url;
        }
        protected string RemoveAngleBrackets(string input)
        {
            int firstStartIndex = input.IndexOf('<');
            int firstEndIndex = input.IndexOf('>');
            string result = input.Remove(firstStartIndex, firstEndIndex - firstStartIndex + 1);

            firstStartIndex = result.LastIndexOf('<');
            firstEndIndex = result.LastIndexOf('>');
            result = result.Remove(firstStartIndex, firstEndIndex - firstStartIndex + 1);
            return result;
        }
        protected WeatherStatus GetWeatherStatus(string weatherInfo)
        {
            switch (weatherInfo)
            {
                case "晴": return WeatherStatus.Qing;
                case "多云": return WeatherStatus.Duoyun;
                case "阴": return WeatherStatus.Yin;
                case "阵雨": return WeatherStatus.Zhenyu;
                case "雷阵雨": return WeatherStatus.Leizhenyu;
                case "雷阵雨并伴有冰雹": return WeatherStatus.LeizhenyuBingpao;
                case "雨夹雪": return WeatherStatus.Yujiaxue;
                case "小雨": return WeatherStatus.Xiaoyu;
                case "中雨": return WeatherStatus.Zhongyu;
                case "大雨": return WeatherStatus.Dayu;
                case "暴雨": return WeatherStatus.Baoyu;
                case "大暴雨": return WeatherStatus.Dabaoyu;
                case "特大暴雨": return WeatherStatus.Tedabaoyu;
                case "阵雪": return WeatherStatus.Zhenxue;
                case "小雪": return WeatherStatus.Xiaoyu;
                case "中雪": return WeatherStatus.Zhongxue;
                case "大雪": return WeatherStatus.Daxue;
                case "暴雪": return WeatherStatus.Baoxue;
                case "雾": return WeatherStatus.Wu;
                case "冻雨": return WeatherStatus.Dongyu;
                case "沙尘暴": return WeatherStatus.Shachenbao;
                case "小雨转中雨": return WeatherStatus.XiaoyuZhuangZhongyu;
                case "中雨转大雨": return WeatherStatus.ZhongyuZhuangDayu;
                case "大雨转暴雨": return WeatherStatus.DayuZhuangBaoyu;
                case "暴雨转大暴雨": return WeatherStatus.BaoyuZhuangDabaoyu;
                case "大暴雨转特大暴雨": return WeatherStatus.DabaoyuZhuangTedabaoyu;
                case "小雪转中雪": return WeatherStatus.XiaoxueZhuangZhongxue;
                case "中雪转大雪": return WeatherStatus.ZhongxueZhuangDaxue;
                case "大雪转暴雪": return WeatherStatus.DaxueZhuangBaoxue;
                case "浮尘": return WeatherStatus.Fuchen;
                case "扬沙": return WeatherStatus.Yasha;
                case "强沙尘暴": return WeatherStatus.Qiangshachenbao;
                case "小到中雨": return WeatherStatus.Xiaodaozhongyu;
                case "中到大雨": return WeatherStatus.Zhongdaodayu;
                case "大到暴雨": return WeatherStatus.Dadaobaoyu;
                case "小到中雪": return WeatherStatus.Xiaodaozhongxue;
                case "中到大雪": return WeatherStatus.Zhongdaodaxue;
                case "大到暴雪": return WeatherStatus.Dadaobaoxue;
                case "小阵雨": return WeatherStatus.Xiaozhenyu;
                case "阴天": return WeatherStatus.Yintian;
                case "霾": return WeatherStatus.Mai;
                case "雾霾": return WeatherStatus.Wumai;
                default: return WeatherStatus.Weizhi;
            }
        }
        protected WeatherStatus GetUnkownWeatherStatus(string weatherInfo)
        {
            string result = weatherInfo;

            int index = 0;
            if (result.Contains("间"))
            {
                index = result.IndexOf("间");
                result = result.Remove(index, result.Length - index);
            }
            if (result.Contains("转"))
            {
                index = result.IndexOf("转");
                result = result.Remove(index, result.Length - index);
            }

            if (result.Contains("雨")) return WeatherStatus.Xiaoyu;
            if (result.Contains("雪")) return WeatherStatus.Xiaoxue;
            if (result.Contains("雾")) return WeatherStatus.Wu;
            if (result.Contains("霾")) return WeatherStatus.Mai;

            return GetWeatherStatus(result);
        }
    }

}
