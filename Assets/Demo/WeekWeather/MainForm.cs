using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System.IO;
using System.Threading;
using Weather_com;

public class MainForm : MonoBehaviour
{
    [SerializeField]
    private Dropdown comboBoxProvince;
    [SerializeField]
    private Dropdown comboBoxCity;
    [SerializeField]
    private Dropdown comboBoxDistrict;
    [SerializeField]
    private Text lblVerson;
    [SerializeField]
    private Text lblStatus;
    [SerializeField]
    private Button buttonAbout;
    [SerializeField]
    private Button buttonSearch;
    [SerializeField]
    [Header("7个")]
    private WeatherDay[] weatherDays;//7 个
    [SerializeField]
    [Header("8个")]
    private WeatherDayMore[] weatherDaysMore;

    Queue<Action> actionQueue = new Queue<Action>();
    PlaceModel[] provinces;
    PlaceModel[] citys;
    PlaceModel[] districts;
    public void Awake()
    {
        InitializeComponent();
        MainForm_Load();
    }
    private void Update()
    {
        if (actionQueue.Count > 0)
        {
            var action = actionQueue.Dequeue();
            if (action != null)
            {
                action.Invoke();
            }
        }
    }

    private void InitializeComponent()
    {
        buttonAbout.onClick.AddListener(buttonAbout_Click);
        comboBoxProvince.onValueChanged.AddListener(ComboBoxProvince_SelectedIndexChanged);
        comboBoxCity.onValueChanged.AddListener(ComboBoxCity_SelectedIndexChanged);
        comboBoxDistrict.onValueChanged.AddListener(ComboBoxDistrict_SelectedIndexChanged);
        buttonSearch.onClick.AddListener(buttonSearch_Click);
    }
    public void InvokeToForm(Action action)
    {
        actionQueue.Enqueue(action);
    }

    private void BindProvince()
    {
        provinces = PlaceUtility.GetProvinces();

        if (provinces != null)
        {
            this.InvokeToForm(() =>
            {
                comboBoxProvince.ClearOptions();
                var list = new List<Dropdown.OptionData>();
                foreach (var item in provinces)
                {
                    var option = new Dropdown.OptionData();
                    option.text = item.Name;
                    list.Add(option);
                }
                comboBoxProvince.AddOptions(list);
                if (comboBoxProvince.value != 0)
                {
                    comboBoxProvince.value = 0;
                }
                else
                {
                    ComboBoxProvince_SelectedIndexChanged(0);
                }
            });
        }
    }

    private void BindCity()
    {
        if (provinces == null || provinces.Length == 0) return;

        PlaceModel province = provinces[comboBoxProvince.value];
        if (province != null)
        {
            citys = PlaceUtility.GetCitys(province);

            this.InvokeToForm(() =>
            {
                comboBoxCity.ClearOptions();

                var list = new List<Dropdown.OptionData>();
                foreach (var item in citys)
                {
                    var option = new Dropdown.OptionData();
                    option.text = item.Name;
                    list.Add(option);
                }
                comboBoxCity.AddOptions(list);
                if (comboBoxCity.value != 0)
                {
                    comboBoxCity.value = 0;
                }
                else
                {
                    ComboBoxCity_SelectedIndexChanged(0);
                }
            });
        }
    }

    private void BindDistrict()
    {
        if (provinces == null || provinces.Length == 0) return;
        if (citys == null || citys.Length == 0) return;

        PlaceModel province = provinces[comboBoxProvince.value];
        PlaceModel city = citys[comboBoxCity.value];

        if (province != null && city != null)
        {
            districts = PlaceUtility.GetDistricts(province, city);
            this.InvokeToForm(() =>
            {
                var list = new List<Dropdown.OptionData>();
                comboBoxDistrict.ClearOptions();
                foreach (var item in districts)
                {
                    var option = new Dropdown.OptionData();
                    option.text = item.Name;
                    list.Add(option);
                }
                comboBoxDistrict.AddOptions(list);
                if (comboBoxDistrict.value != 0)
                {
                    comboBoxDistrict.value = 0;
                }
                else
                {
                    ComboBoxDistrict_SelectedIndexChanged(0);
                }
            });
        }
        else
        {
            this.InvokeToForm(() => lblStatus.text = "地区加载错误，请确保联网正确");
        }
    }

    private void MainForm_Load()
    {
        lblVerson.text = "版本：" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        lblStatus.text = "数据加载中";

        ThreadPool.QueueUserWorkItem(new WaitCallback((x) =>
        {
            BindProvince();
        }), null);
    }

    private void ComboBoxProvince_SelectedIndexChanged(int id)
    {
        BindCity();
    }

    private void ComboBoxCity_SelectedIndexChanged(int id)
    {
        BindDistrict();
    }
    private void ComboBoxDistrict_SelectedIndexChanged(int id)
    {
        buttonSearch_Click();
    }

    private void buttonSearch_Click()
    {
        PlaceModel province = null, city = null, district = null;

        if (provinces != null && provinces.Length > comboBoxProvince.value)
        {
            province = provinces[comboBoxProvince.value];
        }

        if (citys != null && citys.Length > comboBoxCity.value)
        {
            city = citys[comboBoxCity.value];
        }

        if (districts != null && districts.Length > comboBoxDistrict.value)
        {
            district = districts[comboBoxDistrict.value];
        }

        if (province != null && city != null && district != null)
        {
            this.InvokeToForm(() =>
            {
                lblStatus.text = "查询中";
                province = provinces[comboBoxProvince.value];
                city = citys[comboBoxCity.value];
                district = districts[comboBoxDistrict.value];
                ThreadPool.QueueUserWorkItem((x) =>
                {
                    WeekWeatherAnalysis detail = this.Search(province, city, district);
                    this.InvokeToForm(new Action(() =>
                    {
                        if (detail != null)
                        {
                            this.SetWeather(detail);
                            lblStatus.text = "已完成";
                        }
                        else
                        {
                            lblStatus.text = "查询错误，请确保联网正确";
                        }

                    }));
                }, null);
            });
        }
        else
        {
            lblStatus.text = "地区加载错误，请确保联网正确";
        }
    }

    private void SetWeather(WeekWeatherAnalysis detail)
    {
        Debug.Assert(detail != null, "detail == null");
        for (int i = 0; i < weatherDays.Length; i++)
        {
            weatherDays[i].Day = detail.Day_1To7[i] ?? "";
            weatherDays[i].Info = detail.Info_1To7[i] ?? "";
            weatherDays[i].Temperature = detail.Temperature_1To7[i] ?? "";
            weatherDays[i].Wind = detail.Wind_1To7[i] ?? "";
            weatherDays[i].WeatherStatus = detail.WeatherStatus_1To7[i];
        }
        for (int i = 0; i < weatherDaysMore.Length; i++)
        {
            weatherDaysMore[i].Day = detail.Day_7To15[i] ?? "";
            weatherDaysMore[i].Info = detail.Info_7To15[i] ?? "";
            weatherDaysMore[i].Temperature = detail.Temperature_7To15[i] ?? "";
            weatherDaysMore[i].Wind1 = detail.Wind1_7To15[i] ?? "";
            weatherDaysMore[i].Wind2 = detail.Wind2_7To15[i] ?? "";
            weatherDaysMore[i].WeatherStatus = detail.WeatherStatus_7To15[i];
        }
    }

    private WeekWeatherAnalysis Search(PlaceModel province, PlaceModel city, PlaceModel district)
    {
        WeekWeatherAnalysis detail = new WeekWeatherAnalysis(province.ID, city.ID, district.ID);
        detail.HandleWeather();
        return detail;
    }

    private void buttonAbout_Click()
    {
        MessageBox.Show("关于", "天气预报\r\n版本：" + Assembly.GetExecutingAssembly().GetName().Version.ToString());
    }
}
