using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvaTime : MonoBehaviour
{
    Text value;
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
