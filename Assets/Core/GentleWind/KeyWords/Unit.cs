using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace GentleWind
{
    /*
     和风天气默认采用公制单位，例如：公里、摄氏度等，如需要采用英制单位，可以通过在接口中增加参数unit=i（英制）或unit=m（公制）进行单位的选择。
我们主要使用如下度量衡单位

    有一些数据项无论公制还是英制，都会采用公制单位

数据项 	公制单位 	英制单位
温度 	摄氏度 ℃ 	华氏度 ℉
风速 	公里/小时 km/h 	英里/小时 mile/h
能见度 	公里 km 	英里 mile
大气压强 	百帕 hPa 	百帕 hPa
降水量 	毫米 mm 	毫米 mm
PM2.5 	微克/立方米 μg/m3 	微克/立方米 μg/m3
PM10 	微克/立方米 μg/m3 	微克/立方米 μg/m3
O3 	微克/立方米 μg/m3 	微克/立方米 μg/m3
SO2 	微克/立方米 μg/m3 	微克/立方米 μg/m3
CO 	毫克/立方米 mg/m3 	毫克/立方米 mg/m3
NO2 	微克/立方米 μg/m3 	微克/立方米 μg/m3
     */
     /// <summary>
     /// 单位
     /// </summary>
    public static class Unit
    {
        private const string keyword = "unit";
        /// <summary>
        /// 公制
        /// </summary>
        public static string m
        {
            get
            {
                return keyword + "=m";
            }
        }
        /// <summary>
        /// 英制
        /// </summary>
        public static string i
        {
            get
            {
                return keyword + "=i";
            }
        }
    }

}