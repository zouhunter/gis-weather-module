using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GentleWind
{
    [System.Serializable]
    public class HeWeatherItem
    {
        public string status;//ok
        public Basic basic;
        public Update update;
        public Now now;
    }
    [System.Serializable]
    public class Basic
    {
        public string location;// 地区／城市名称 海淀
        public string cid;//地区／城市ID CN101080402
        public string lon;// 地区／城市纬度 	39.956074
        public string lat;//地区／城市经度 	116.310316
        public string parent_city;//该地区／城市的上级城市 北京
        public string admin_area;// 该地区／城市所属行政区域 北京
        public string cnty;// 该地区／城市所属国家名称 中国
        public string tz;//该地区／城市所在时区 	+8.0
    }
    [System.Serializable]
    public class Update
    {
        public string loc;// 当地时间，24小时制，格式yyyy-MM-dd HH:mm 	2017-10-25 12:34
        public string utc;//UTC时间，24小时制，格式yyyy-MM-dd HH:mm 	2017-10-25 04:34
    }
    [System.Serializable]
    public class Now
    {
        public string fl;// 体感温度，默认单位：摄氏度 	23
        public string tmp;// 温度，默认单位：摄氏度 	21
        public string cond_code;// 实况天气状况代码 	100
        public string cond_txt;//实况天气状况代码    晴
        public string wind_deg;//    风向360角度 	305
        public string wind_dir;// 风向  西北
        public string wind_sc;//   风力 	3
        public string wind_spd;//风速，公里/小时 	15
        public string hum;// 相对湿度 	40
        public string pcpn;//降水量 	0
        public string pres;//大气压强 	1020
        public string vis;// 能见度，默认单位：公里 	10
        public string cloud;// 云量 	23
    }
}