using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace Weather
{

    [DefaultProperty("Day")]
    public partial class WeatherDay : UserControl
    {
        public WeatherDay()
        {
            InitializeComponent();
        }

        string day;
        [Category("设置")]
        [Description("设置或获得日期")]
        [DefaultValue("")]
        public string Day
        {
            set
            {
                this.day = value;
                this.labelDay.Text = this.day;
            }
            get
            {
                return this.day;
            }
        }
        string info;
        [Category("设置")]
        [Description("设置或获得天气信息")]
        [DefaultValue("")]
        public string Info
        {
            set
            {
                this.info = value;
                this.labelInfo.Text = this.info;
            }
            get
            {
                return this.info;
            }
        }

        string temperature;
        [Category("设置")]
        [Description("设置或获得温度")]
        public string Temperature
        {
            set
            {
                this.temperature = value;
                this.labelTemp.Text = this.temperature;
            }
            get
            {
                return this.temperature;
            }
        }


        string wind;
        [Category("设置")]
        [Description("设置或获得风级")]
        [DefaultValue("")]
        public string Wind
        {
            set
            {
                this.wind = value;
                this.labelWind.Text = this.wind;
            }
            get
            {
                return this.wind;
            }
        }

        WeatherStatus status = WeatherStatus.Weizhi;
        [Category("设置")]
        [Description("设置或获得天气图标")]
        [DefaultValue(WeatherStatus.Weizhi)]
        public WeatherStatus WeatherStatus
        {
            set
            {
                this.status = value;
                this.pictureBoxWeather.ImageLocation = this.GetBigWeatherStatusString(this.status);
            }
            get
            {
                return this.status;
            }
        }

        string GetBigWeatherStatusString(WeatherStatus weatherStatus)
        {
            string url = "http://www.webxml.com.cn/images/weather/";
            switch (weatherStatus)
            {
                case WeatherStatus.Weizhi: url += "a_nothing.gif"; break;
                case WeatherStatus.Qing: url += "a_0.gif"; break;
                case WeatherStatus.Duoyun: url += "a_1.gif"; break;
                case WeatherStatus.Yin: url += "a_2.gif"; break;
                case WeatherStatus.Zhenyu: url += "a_3.gif"; break;
                case WeatherStatus.Leizhenyu: url += "a_4.gif"; break;
                case WeatherStatus.LeizhenyuBingpao: url += "a_5.gif"; break;
                case WeatherStatus.Yujiaxue: url += "a_6.gif"; break;
                case WeatherStatus.Xiaoyu: url += "a_7.gif"; break;
                case WeatherStatus.Zhongyu: url += "a_8.gif"; break;
                case WeatherStatus.Dayu: url += "a_9.gif"; break;
                case WeatherStatus.Baoyu: url += "a_10.gif"; break;
                case WeatherStatus.Dabaoyu: url += "a_11.gif"; break;
                case WeatherStatus.Tedabaoyu: url += "a_12.gif"; break;
                case WeatherStatus.Zhenxue: url += "a_13.gif"; break;
                case WeatherStatus.Xiaoxue: url += "a_14.gif"; break;
                case WeatherStatus.Zhongxue: url += "a_15.gif"; break;
                case WeatherStatus.Daxue: url += "a_16.gif"; break;
                case WeatherStatus.Baoxue: url += "a_17.gif"; break;
                case WeatherStatus.Wu: url += "a_18.gif"; break;
                case WeatherStatus.Dongyu: url += "a_19.gif"; break;
                case WeatherStatus.Shachenbao: url += "a_20.gif"; break;
                case WeatherStatus.XiaoyuZhuangZhongyu: url += "a_21.gif"; break;
                case WeatherStatus.ZhongyuZhuangDayu: url += "a_22.gif"; break;
                case WeatherStatus.DayuZhuangBaoyu: url += "a_23.gif"; break;
                case WeatherStatus.BaoyuZhuangDabaoyu: url += "a_24.gif"; break;
                case WeatherStatus.DabaoyuZhuangTedabaoyu: url += "a_25.gif"; break;
                case WeatherStatus.XiaoxueZhuangZhongxue: url += "a_26.gif"; break;
                case WeatherStatus.ZhongxueZhuangDaxue: url += "a_27.gif"; break;
                case WeatherStatus.DaxueZhuangBaoxue: url += "a_28.gif"; break;
                case WeatherStatus.Fuchen: url += "a_29.gif"; break;
                case WeatherStatus.Yasha: url += "a_30.gif"; break;
                case WeatherStatus.Qiangshachenbao: url += "a_31.gif"; break;
                case WeatherStatus.Xiaodaozhongyu: url += "a_21.gif"; break;
                case WeatherStatus.Zhongdaodayu: url += "a_22.gif"; break;
                case WeatherStatus.Dadaobaoyu: url += "a_23.gif"; break;
                case WeatherStatus.Xiaodaozhongxue: url += "a_26.gif"; break;
                case WeatherStatus.Zhongdaodaxue: url += "a_27.gif"; break;
                case WeatherStatus.Dadaobaoxue: url += "a_28.gif"; break;
                case WeatherStatus.Xiaozhenyu: url += "a_3.gif"; break;
                case WeatherStatus.Yintian: url += "a_2.gif"; break;
                case WeatherStatus.Mai: url += "a_18.gif"; break;
                case WeatherStatus.Wumai: url += "a_18.gif"; break;
                default: url += "a_nothing.gif"; break;
            }
            return url;
        }


    }

}