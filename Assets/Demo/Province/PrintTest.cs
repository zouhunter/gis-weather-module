using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Weather_com;

public class PrintTest : MonoBehaviour
{
    public bool async;
    private void Start()
    {
        if (async)
        {
            Async();
        }
        else
        {
            Sync();
        }
    }

    void Sync()
    {
        var provinces = PlaceUtility.GetProvinces();
        foreach (var province in provinces)
        {
            var citys = PlaceUtility.GetCitys(province);
            foreach (var city in citys)
            {
                var directs = PlaceUtility.GetDistricts(province, city);
                foreach (var direct in directs)
                {
                    Debug.Log(string.Format("{0}/{1}/{2}", province.Name, city.Name, direct.Name));
                }
            }
        }
    }
    void Async()
    {
        PlaceUtility.GetProvincesAsync((provinces) =>
        {
            foreach (var province in provinces)
            {
                PlaceUtility.GetCitysAsync(province.ID, (citys) =>
                {
                    foreach (var city in citys)
                    {
                        PlaceUtility.GetDistrictsAsync(province.ID, city.ID, (directs) =>
                        {
                            foreach (var direct in directs)
                            {
                                Debug.Log(string.Format("{0}/{1}/{2}", province.Name, city.Name, direct.Name));
                            }
                        });
                    }
                });
            }
        });
    }
}
