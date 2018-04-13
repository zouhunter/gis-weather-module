using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
namespace GentleWind
{
    /*https://www.heweather.com/documents/i18n
    zh zh-cn cn     简体中文
    zh-hk hk    繁体中文
    en  英文
    de  德语
    es  西班牙语
    fr  法语
    it  意大利语
    jp  日语
    kr  韩语
    ru  俄语
    in 	印度语
    th  泰语
    */

    public static class Lang
    {
        private const string keyword = "lang";
        public static string zh
        {
            get
            {
                return keyword + "=zh"; 
            }
        }
        public static string en
        {
            get
            {
                return keyword + "=en";
            }
        }
    }

}