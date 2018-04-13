using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GentleWind
{
    /*
    1. location=CN101010100
    2. location=116.40,39.9
    3. location=北京、 location=北京市、 location=beijing
    4. location=朝阳,北京、 location=chaoyang,beijing
    5. location=60.194.130.1
    6. location=auto_ip
    */
    public class Location
    {
        public static string key = "location";

        internal static string City(string city)
        {
            return key + "=" + city;
        }
    }
}