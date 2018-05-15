using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvaTime : MonoBehaviour
{
    Text value;
    string switchValueFan;
    string switchValueSop;
    string switchValueO2;
    string switchValueSS;
    string switchValueVehicle;
    string switchValueVent;
    string switchValueSuit;
    // Use this for initialization
    void Start()
    {
        value = transform.Find("value_position").transform.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (value == null)
            value = transform.Find("value_position").transform.GetComponentInChildren<Text>();
        value.text = TelemetryController.eva_time_hour + ":" + TelemetryController.eva_time_minute + ":" + TelemetryController.eva_time_second;
        if (TelemetryController.suitSwitch != null)
        {

            switchValueFan = TelemetryController.suitSwitch.fan_error;
            switchValueSop = TelemetryController.suitSwitch.sop_on;
            switchValueO2 = TelemetryController.suitSwitch.o2_off;
            switchValueSS = TelemetryController.suitSwitch.sspe;
            switchValueVehicle = TelemetryController.suitSwitch.vehicle_power;
            switchValueVent = TelemetryController.suitSwitch.vent_error;
            switchValueSuit = TelemetryController.suitSwitch.h2o_off;
            if (switchValueFan == "1")
            { transform.GetComponent<Image>().color = TelemetryController.CRITICAL_BACKGROUND_COLOR; }
            if (switchValueO2 == "1")
            { transform.GetComponent<Image>().color = TelemetryController.CRITICAL_BACKGROUND_COLOR; }
            if (switchValueVehicle == "0")
            { transform.GetComponent<Image>().color = TelemetryController.CRITICAL_BACKGROUND_COLOR; }
            if (switchValueSuit == "1")
            { transform.GetComponent<Image>().color = TelemetryController.CRITICAL_BACKGROUND_COLOR; }
            if (switchValueVent == "1")
            { transform.GetComponent<Image>().color = TelemetryController.CRITICAL_BACKGROUND_COLOR; }
            if (switchValueSS == "1")
            { transform.GetComponent<Image>().color = TelemetryController.CRITICAL_BACKGROUND_COLOR; }
            if (switchValueSop == "0")
            { transform.GetComponent<Image>().color = TelemetryController.CRITICAL_BACKGROUND_COLOR; }

        }

    }
    public void ChangeContrastMode(int contrast_idx)
    {

        value = transform.Find("value_position").transform.GetComponentInChildren<Text>();

        if (contrast_idx == TelemetryController.THEME_LIGHT)
        {
            transform.GetComponent<Image>().color = TelemetryController.THEME_LIGHT_BACKGROUND_COLOR;
            value.color = TelemetryController.THEME_LIGHT_TEXT_COLOR;
        }
        else if (contrast_idx == TelemetryController.THEME_DARK)
        {
            transform.GetComponent<Image>().color = TelemetryController.THEME_DARK_BACKGROUND_COLOR;
            value.color = TelemetryController.THEME_DARK_TEXT_COLOR;
        }
    }

}
