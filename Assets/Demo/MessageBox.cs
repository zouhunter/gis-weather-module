using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;
public class MessageBox : MonoBehaviour {
    [SerializeField]
    private Button m_close;
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text info;
    [SerializeField]
    private GameObject viewBody;
    private static MessageBox instence;
    private void Awake()
    {
        instence = this;
        m_close.onClick.AddListener(()=> { viewBody.gameObject.SetActive(false); });
        viewBody.gameObject.SetActive(false);
    }
    void ShowInternal(string title, string info)
    {
        viewBody.SetActive(true);
        this.title.text = title;
        this.info.text = info;
    }
    internal static void Show(string title, string info)
    {
        if(instence != null)
        {
            instence.ShowInternal(title, info);
        }
    }
}
