using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

namespace GentleWind
{
    public class GentleWindRequest : IEnumerator
    {
        string urlbase = "https://free-api.heweather.com/s6/weather/now";
        UnityWebRequest request;
        UnityAction<HeWeatherItem> onGet;
        string city;
        public GentleWindRequest(string city, UnityAction<HeWeatherItem> onGet,string key = null)
        {
            this.city = city;
            this.onGet = onGet;
            if (string.IsNullOrEmpty(key))
                key = Key.defult;
            var url = string.Format("{0}?{1}&{2}",urlbase,Location.City(city),Key.defult);
            Debug.Log(url);
            request = UnityWebRequest.Get(url);
            request.Send();
        }

        public object Current {
            get
            {
                return null;
            }
        }

        public bool MoveNext()
        {
            bool isDone = false;
            if (request != null && request.error != null)
            {
                UnityEngine.Debug.LogError(request.error);
                isDone = true;
            }
            else if (request == null)
            {
                isDone = true;
            }
            else if (request.isDone)
            {
                isDone = true;
                if (onGet != null) {
                    var weather = GetWeather(request.downloadHandler.text);
                    onGet.Invoke(weather);
                }
                request.Dispose();
            }
            return !isDone;
        }

        public void Reset()
        {
            
        }

        private HeWeatherItem GetWeather(string text)
        {
            if (string.IsNullOrEmpty(text)){
                UnityEngine.Debug.LogError("empty data of " + city);
                return null;
            }
            var newtext = text.Substring(15, text.Length - 17);
            Debug.Log(newtext);
            return JsonUtility.FromJson<HeWeatherItem>(newtext);
        }
    }
}