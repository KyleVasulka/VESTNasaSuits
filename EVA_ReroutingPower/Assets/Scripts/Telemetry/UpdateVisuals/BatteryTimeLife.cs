using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BatteryTimeLife : MonoBehaviour
{
    RawImage icon;
    Text icon_name, value;
    int contrast;
    string switchValue;
    // Use this for initialization
    void Start()
    {
        //value = transform.Find("value_position").transform.GetComponentInChildren<Text>();
        //icon = transform.GetComponentInChildren<RawImage>();
        //icon_name = transform.Find("icon_position").transform.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TelemetryController.battery_time_life_warning)
        {
            DisplayWarning(contrast);
        }
        else if (TelemetryController.battery_time_life_critical)
            DisplayCritical(contrast);
        else
        {
            StopWarning(contrast);
        }

        if (TelemetryController.suitSwitch != null)
        {
            switchValue = TelemetryController.suitSwitch.vehicle_power;
            if (switchValue == "0")
            {
                DisplayCritical(contrast);
            }
            else
            {
                StopWarning(contrast);
            }
        }

    }

    public void UpdateValue(int contrast_idx)
    {
        contrast = contrast_idx;
        value = transform.Find("value_position").transform.GetComponentInChildren<Text>();
        value.text = TelemetryController.telemetryData.t_battery;

        icon_name = transform.Find("icon_position").transform.GetComponentInChildren<Text>();
        icon = transform.GetComponentInChildren<RawImage>();

        float time_life_battery_float = float.Parse(TelemetryController.telemetryData.t_battery.Substring(0, TelemetryController.telemetryData.t_battery.IndexOf(":")));
        float delta = TelemetryController.t_battery_warning_thr_up;

        if (contrast_idx == TelemetryController.THEME_LIGHT)
        {
            // if there is no warning flag for this value, update background normally
            // when there is a warning flag, the background color is updated inside the Blink() function
            if (!TelemetryController.battery_capacity_warning)
            {
                transform.GetComponent<Image>().color = TelemetryController.THEME_LIGHT_BACKGROUND_COLOR;
            }
            value.color = TelemetryController.THEME_LIGHT_TEXT_COLOR;
            icon_name.color = TelemetryController.THEME_LIGHT_TEXT_COLOR;

            if (time_life_battery_float / delta >= 1)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "battery_7") as Texture2D;
            }
            else if (time_life_battery_float / delta >= 0.8f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "battery_6") as Texture2D;
            }
            else if (time_life_battery_float / delta >= 0.7f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "battery_5") as Texture2D;
            }
            else if (time_life_battery_float / delta >= 0.5f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "battery_4") as Texture2D;
            }
            else if (time_life_battery_float / delta >= 0.3f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "battery_3") as Texture2D;
            }
            else if (time_life_battery_float / delta >= 0.2f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "battery_2") as Texture2D;
            }
            else if (time_life_battery_float / delta >= 0.1f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "battery_1") as Texture2D;
            }
            else
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "battery_0") as Texture2D;
            }
        }
        else if (contrast_idx == TelemetryController.THEME_DARK)
        {
            if (!TelemetryController.battery_capacity_warning)
            {
                transform.GetComponent<Image>().color = TelemetryController.THEME_DARK_BACKGROUND_COLOR;
            }
            value.color = TelemetryController.THEME_DARK_TEXT_COLOR;
            icon_name.color = TelemetryController.THEME_DARK_TEXT_COLOR;
            if (time_life_battery_float / delta >= 1)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "battery_7") as Texture2D;
            }
            else if (time_life_battery_float / delta >= 0.8f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "battery_6") as Texture2D;
            }
            else if (time_life_battery_float / delta >= 0.7f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "battery_5") as Texture2D;
            }
            else if (time_life_battery_float / delta >= 0.5f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "battery_4") as Texture2D;
            }
            else if (time_life_battery_float / delta >= 0.3f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "battery_3") as Texture2D;
            }
            else if (time_life_battery_float / delta >= 0.2f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "battery_2") as Texture2D;
            }
            else if (time_life_battery_float / delta >= 0.1f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "battery_1") as Texture2D;
            }
            else
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "battery_0") as Texture2D;
            }
        }

    }

    public void ChangeIconName(string name)
    {
        icon_name = transform.Find("icon_position").transform.GetComponentInChildren<Text>();
        icon_name.text = name;
    }
    public void ChangeContrastMode(int contrast_idx)
    {
        UpdateValue(contrast_idx);
    }
    public void DisplayWarning(int contrast_idx)
    {
        transform.GetComponent<Image>().color = TelemetryController.WARNING_BACKGROUND_COLOR;
        //value = transform.Find("value_position").transform.GetComponentInChildren<Text>();
        //value.color = TelemetryController.THEME_LIGHT_TEXT_COLOR;
    }
    public void DisplayCritical(int contrast_idx)
    {
        transform.GetComponent<Image>().color = TelemetryController.CRITICAL_BACKGROUND_COLOR;
        //value = transform.Find("value_position").transform.GetComponentInChildren<Text>();
        //value.color = Color.white;
    }
    public void StopWarning(int contrast_idx)
    {
        if (contrast_idx == TelemetryController.THEME_LIGHT)
        {
            transform.GetComponent<Image>().color = TelemetryController.THEME_LIGHT_BACKGROUND_COLOR;
            value = transform.Find("value_position").transform.GetComponentInChildren<Text>();
            value.color = TelemetryController.THEME_LIGHT_TEXT_COLOR;
        }
        if (contrast_idx == TelemetryController.THEME_DARK)
        {
            transform.GetComponent<Image>().color = TelemetryController.THEME_DARK_BACKGROUND_COLOR;
            value = transform.Find("value_position").transform.GetComponentInChildren<Text>();
            value.color = TelemetryController.THEME_DARK_TEXT_COLOR;
        }
    }
}
