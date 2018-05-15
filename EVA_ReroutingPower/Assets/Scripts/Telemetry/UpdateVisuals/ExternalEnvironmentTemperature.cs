using UnityEngine;
using UnityEngine.UI;

public class ExternalEnvironmentTemperature : MonoBehaviour
{
    RawImage icon;
    Text icon_name, value;
    int contrast;
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
        if (TelemetryController.external_environment_temperature_warning)
            DisplayWarning(contrast);
        else if (TelemetryController.external_environment_temperature_critical)
            DisplayCritical(contrast);
        else
            StopWarning(contrast);
    }

    public void UpdateValue(int contrast_idx)
    {
        contrast = contrast_idx;
        float sub_temperature_float = float.Parse(TelemetryController.telemetryData.t_sub);
        float delta = 300;

        value = transform.Find("value_position").transform.GetComponentInChildren<Text>();
        value.text = sub_temperature_float.ToString("0.00") + " deg F";

        icon_name = transform.Find("icon_position").transform.GetComponentInChildren<Text>();
        icon = transform.GetComponentInChildren<RawImage>();


        if (contrast_idx == TelemetryController.THEME_LIGHT)
        {
            transform.GetComponent<Image>().color = TelemetryController.THEME_LIGHT_BACKGROUND_COLOR;
            value.color = TelemetryController.THEME_LIGHT_TEXT_COLOR;
            icon_name.color = TelemetryController.THEME_LIGHT_TEXT_COLOR;

            if (sub_temperature_float / delta >= 1)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "temp_7") as Texture2D;
            }
            else if (sub_temperature_float / delta >= 0.8f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "temp_6") as Texture2D;
            }
            else if (sub_temperature_float / delta >= 0.7f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "temp_5") as Texture2D;
            }
            else if (sub_temperature_float / delta >= 0.5f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "temp_4") as Texture2D;
            }
            else if (sub_temperature_float / delta >= 0.3f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "temp_3") as Texture2D;
            }
            else if (sub_temperature_float / delta >= 0.2f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "temp_2") as Texture2D;
            }
            else if (sub_temperature_float / delta >= 0.1f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "temp_1") as Texture2D;
            }
            else
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "temp_0") as Texture2D;
            }
        }
        else if (contrast_idx == TelemetryController.THEME_DARK)
        {
            transform.GetComponent<Image>().color = TelemetryController.THEME_DARK_BACKGROUND_COLOR;
            value.color = TelemetryController.THEME_DARK_TEXT_COLOR;
            icon_name.color = TelemetryController.THEME_DARK_TEXT_COLOR;
            if (sub_temperature_float / delta >= 1)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "temp_7") as Texture2D;
            }
            else if (sub_temperature_float / delta >= 0.8f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "temp_6") as Texture2D;
            }
            else if (sub_temperature_float / delta >= 0.7f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "temp_5") as Texture2D;
            }
            else if (sub_temperature_float / delta >= 0.5f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "temp_4") as Texture2D;
            }
            else if (sub_temperature_float / delta >= 0.3f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "temp_3") as Texture2D;
            }
            else if (sub_temperature_float / delta >= 0.2f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "temp_2") as Texture2D;
            }
            else if (sub_temperature_float / delta >= 0.1f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "temp_1") as Texture2D;
            }
            else
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "temp_0") as Texture2D;
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
