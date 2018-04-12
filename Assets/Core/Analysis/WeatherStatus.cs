using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Weather
{
    /// <summary>
    /// 天气状态
    /// </summary>
    public enum WeatherStatus
    {
        /// <summary>
        /// 未知
        /// </summary>
        Weizhi,
        /// <summary>
        /// 晴
        /// </summary>
        Qing,
        /// <summary>
        /// 多云
        /// </summary>
        Duoyun,
        /// <summary>
        /// 阴
        /// </summary>
        Yin,
        /// <summary>
        /// 阵雨
        /// </summary>
        Zhenyu,
        /// <summary>
        /// 雷阵雨
        /// </summary>
        Leizhenyu,
        /// <summary>
        /// 雷阵雨并伴有冰雹
        /// </summary>
        LeizhenyuBingpao,
        /// <summary>
        /// 雨夹雪
        /// </summary>
        Yujiaxue,
        /// <summary>
        /// 小雨
        /// </summary>
        Xiaoyu,
        /// <summary>
        /// 中雨
        /// </summary>
        Zhongyu,
        /// <summary>
        /// 大雨
        /// </summary>
        Dayu,
        /// <summary>
        /// 暴雨
        /// </summary>
        Baoyu,
        /// <summary>
        /// 大暴雨
        /// </summary>
        Dabaoyu,
        /// <summary>
        /// 特大暴雨
        /// </summary>
        Tedabaoyu,
        /// <summary>
        /// 阵雪
        /// </summary>
        Zhenxue,
        /// <summary>
        /// 小雪
        /// </summary>
        Xiaoxue,
        /// <summary>
        /// 中雪
        /// </summary>
        Zhongxue,
        /// <summary>
        /// 大雪
        /// </summary>
        Daxue,
        /// <summary>
        /// 暴雪
        /// </summary>
        Baoxue,
        /// <summary>
        /// 雾
        /// </summary>
        Wu,
        /// <summary>
        /// 冻雨
        /// </summary>
        Dongyu,
        /// <summary>
        /// 沙尘暴
        /// </summary>
        Shachenbao,
        /// <summary>
        /// 小雨-中雨
        /// </summary>
        XiaoyuZhuangZhongyu,
        /// <summary>
        /// 中雨-大雨
        /// </summary>
        ZhongyuZhuangDayu,
        /// <summary>
        /// 大雨-暴雨
        /// </summary>
        DayuZhuangBaoyu,
        /// <summary>
        /// 大雨-大暴雨
        /// </summary>
        BaoyuZhuangDabaoyu,
        /// <summary>
        /// 大暴雨-特大暴雨
        /// </summary>
        DabaoyuZhuangTedabaoyu,
        /// <summary>
        /// 小雪-中雪
        /// </summary>
        XiaoxueZhuangZhongxue,
        /// <summary>
        /// 中雪-大雪
        /// </summary>
        ZhongxueZhuangDaxue,
        /// <summary>
        /// 大雪-暴雪
        /// </summary>
        DaxueZhuangBaoxue,
        /// <summary>
        /// 浮尘
        /// </summary>
        Fuchen,
        /// <summary>
        /// 扬沙
        /// </summary>
        Yasha,
        /// <summary>
        /// 强沙尘暴
        /// </summary>
        Qiangshachenbao,
        /// <summary>
        /// 小到中雨
        /// </summary>
        Xiaodaozhongyu,
        /// <summary>
        /// 中到大雨
        /// </summary>
        Zhongdaodayu,
        /// <summary>
        /// 大到暴雨
        /// </summary>
        Dadaobaoyu,
        /// <summary>
        /// 小到中雪
        /// </summary>
        Xiaodaozhongxue,
        /// <summary>
        /// 中到大雪
        /// </summary>
        Zhongdaodaxue,
        /// <summary>
        /// 大到暴雪
        /// </summary>
        Dadaobaoxue,
        /// <summary>
        /// 小阵雨
        /// </summary>
        Xiaozhenyu,
        /// <summary>
        /// 阴天
        /// </summary>
        Yintian,
        /// <summary>
        /// 霾
        /// </summary>
        Mai,
        /// <summary>
        /// 雾霾
        /// </summary>
        Wumai
    }

}
