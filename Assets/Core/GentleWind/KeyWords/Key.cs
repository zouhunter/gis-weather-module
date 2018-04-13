using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace GentleWind
{
    public class Key
    {
        private const string keyword = "key";
        /// <summary>
        /// 登录用户控制台可以有更新关键字
        /// </summary>
        public static string defult
        {
            get
            {
                return keyword + "=c1f6dd8c516940f48311373c85287c34";
            }
        }
    }
}