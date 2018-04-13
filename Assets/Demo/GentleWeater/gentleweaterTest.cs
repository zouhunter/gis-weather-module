using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class gentleweaterTest : MonoBehaviour {
    [SerializeField]
    public string city = "CN101080402";
    private void Start()
    {
        StartCoroutine(new GentleWind.GentleWindRequest(city, ((weater) =>
        {
            if (weater != null)
            {

                Debug.Log(weater.status);
                Debug.Log(weater.update.loc);
                Debug.Log("湿度" + weater.now.hum);
                Debug.Log("温度" + weater.now.tmp);
            }
            else
            {
                Debug.Log("下载失败");
            }
        })));
    }
}
