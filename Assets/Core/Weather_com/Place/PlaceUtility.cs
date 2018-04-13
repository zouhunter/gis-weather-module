using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.Events;
using System.Net;

namespace Weather_com
{
    public static class PlaceUtility
    {
        private class DownLander : IEnumerator
        {
            public UnityAction<PlaceModel[]> onGet;
            private UnityWebRequest request;
            public DownLander(UnityWebRequest request, UnityAction<PlaceModel[]> onGet)
            {
                this.request = request;
                this.onGet = onGet;
                request.Send();
            }

            public object Current
            {
                get { return null; }
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
                    if (onGet != null)
                    {
                        //var data = JsonToArray(request.downloadHandler.text);
                        //if(data != null)
                        //{
                        //    onGet.Invoke(data.ToArray());
                        //}
                        //else
                        //{
                        //    UnityEngine.Debug.Log( "Error:" + request.url);
                        //}
                        using (Stream stream = new MemoryStream(request.downloadHandler.data))
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string content = reader.ReadToEnd();
                            var data = JsonToArray(content);
                            if(data != null)
                            {
                                onGet.Invoke(data.ToArray());
                            }
                            else
                            {
                                UnityEngine.Debug.Log("Error:" + request.url);
                            }
                            
                        }
                    }
                    request.Dispose();
                }
                return !isDone;
            }

            public void Reset()
            {
            }
        }

        public static PlaceModel[] GetProvinces()
        {
            return GetPlaceList("http://www.weather.com.cn/data/city3jdata/china.html");
        }

        public static PlaceModel[] GetCitys(PlaceModel province)
        {
            return GetPlaceList(string.Format("http://www.weather.com.cn/data/city3jdata/provshi/{0}.html", province.ID));
        }

        public static PlaceModel[] GetDistricts(PlaceModel province, PlaceModel city)
        {
            return GetPlaceList(string.Format("http://www.weather.com.cn/data/city3jdata/station/{0}{1}.html", province.ID, city.ID));
        }

        public static void GetProvincesAsync(UnityAction<PlaceModel[]> onGet)
        {
            var url = "http://www.weather.com.cn/data/city3jdata/china.html";
            GetPlaceListAsync(url, onGet);
        }

        public static void GetCitysAsync(string provinceID, UnityAction<PlaceModel[]> onGet)
        {
            var url = string.Format("http://www.weather.com.cn/data/city3jdata/provshi/{0}.html", provinceID);
            GetPlaceListAsync(url, onGet);
        }

        public static void GetDistrictsAsync(string provinceID, string cityID, UnityAction<PlaceModel[]> onGet)
        {
            var url = string.Format("http://www.weather.com.cn/data/city3jdata/station/{0}{1}.html", provinceID, cityID);
            GetPlaceListAsync(url, onGet);
        }

        static PlaceModel[] GetPlaceList(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //request.Timeout = 3000;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string content = reader.ReadToEnd();
                    UnityEngine.Debug.Log(content);

                    response.Close();
                    return JsonToArray(content).ToArray();
                }
            }
            catch
            {
                return new List<PlaceModel>().ToArray();
            }
        }

        static void GetPlaceListAsync(string url, UnityAction<PlaceModel[]> onGet)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            AsyncUtil.StartCoroutine(new DownLander(request, onGet));
        }

        static PlaceModel[] JsonToArray(string json)
        {
            if (json.StartsWith("<!DOCTYPE html>", StringComparison.CurrentCulture)) return null;

            try
            {
                List<PlaceModel> list = new List<PlaceModel>();
                string[] array1 = json.Replace("{", "").Replace("}", "").Split(',');
                string[] array2;
                for (int i = 0; i < array1.Length; i++)
                {
                    array2 = array1[i].Split(':');
                    for (int j = 0; j < array2.Length; j = j + 2)
                    {
                        list.Add(new PlaceModel { ID = array2[j].Replace("\"", ""), Name = array2[j + 1].Replace("\"", "") });
                    }
                }
                return list.ToArray();
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e);
                return null;
            }

        }
    }


}
