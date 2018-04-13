using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Weather_com;

public class oneDay : MonoBehaviour {
    private void Start()
    {
        WeatherDayAnalysis analysis = new WeatherDayAnalysis("10102", "00", "01");
        analysis.UpdateAsync(() => {
            Debug.Log("温度：" + analysis.temperature);
        });
    }
}
