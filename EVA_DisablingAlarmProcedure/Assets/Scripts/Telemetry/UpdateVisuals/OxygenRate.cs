using UnityEngine;
using UnityEngine.UI;

public class OxygenRate : MonoBehaviour
{
    RawImage icon;
    Text icon_name, value;
    int contrast;
    string switchValueSOP;
    string switchValueO2;
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
        if (TelemetryController.oxygen_rate_warning)
            DisplayWarning(contrast);
        else if (TelemetryController.oxygen_rate_critical)
            DisplayCritical(contrast);
        else
            StopWarning(contrast);

        if (TelemetryController.suitSwitch != null)
        {

      
            switchValueSOP = TelemetryController.suitSwitch.sop_on;
            if (switchValueSOP == "0")
            {
                DisplayCritical(contrast);
            }

            switchValueO2 = TelemetryController.suitSwitch.o2_off;
            if (switchValueO2 == "1")
            {
                DisplayCritical(contrast);
            }
        }
    }

    public void UpdateValue(int contrast_idx)
    {
        contrast = contrast_idx;
        float oxygen_rate_float = float.Parse(TelemetryController.telemetryData.rate_o2);
        float delta = TelemetryController.rate_o2_warning_thr_up;

        value = transform.Find("value_position").transform.GetComponentInChildren<Text>();
        value.text = oxygen_rate_float.ToString("0.00") + " psi/min";

        icon_name = transform.Find("icon_position").transform.GetComponentInChildren<Text>();
        icon = transform.GetComponentInChildren<RawImage>();


        if (contrast_idx == TelemetryController.THEME_LIGHT)
        {
            transform.GetComponent<Image>().color = TelemetryController.THEME_LIGHT_BACKGROUND_COLOR;
            value.color = TelemetryController.THEME_LIGHT_TEXT_COLOR;
            icon_name.color = TelemetryController.THEME_LIGHT_TEXT_COLOR;

            if (oxygen_rate_float / delta >= 1)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "gauge_10") as Texture2D;
            }
            else if (oxygen_rate_float / delta >= 0.9f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "gauge_9") as Texture2D;
            }
            else if (oxygen_rate_float / delta >= 0.8f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "gauge_8") as Texture2D;
            }
            else if (oxygen_rate_float / delta >= 0.7f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "gauge_6") as Texture2D;
            }
            else if (oxygen_rate_float / delta >= 0.6f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "gauge_6") as Texture2D;
            }
            else if (oxygen_rate_float / delta >= 0.5f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "gauge_4") as Texture2D;
            }
            else if (oxygen_rate_float / delta >= 0.4f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "gauge_4") as Texture2D;
            }
            else if (oxygen_rate_float / delta >= 0.3f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "gauge_2") as Texture2D;
            }
            else if (oxygen_rate_float / delta >= 0.2f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "gauge_2") as Texture2D;
            }
            else if (oxygen_rate_float / delta >= 0.1f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "gauge_1") as Texture2D;
            }
            else
            {
                icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "gauge_0") as Texture2D;
            }
        }
        else if (contrast_idx == TelemetryController.THEME_DARK)
        {
            transform.GetComponent<Image>().color = TelemetryController.THEME_DARK_BACKGROUND_COLOR;
            value.color = TelemetryController.THEME_DARK_TEXT_COLOR;
            icon_name.color = TelemetryController.THEME_DARK_TEXT_COLOR;
            if (oxygen_rate_float / delta >= 1)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "gauge_10") as Texture2D;
            }
            else if (oxygen_rate_float / delta >= 0.9f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "gauge_9") as Texture2D;
            }
            else if (oxygen_rate_float / delta >= 0.8f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "gauge_7") as Texture2D;
            }
            else if (oxygen_rate_float / delta >= 0.7f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "gauge_7") as Texture2D;
            }
            else if (oxygen_rate_float / delta >= 0.6f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "gauge_5") as Texture2D;
            }
            else if (oxygen_rate_float / delta >= 0.5f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "gauge_5") as Texture2D;
            }
            else if (oxygen_rate_float / delta >= 0.4f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "gauge_3") as Texture2D;
            }
            else if (oxygen_rate_float / delta >= 0.3f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "gauge_3") as Texture2D;
            }
            else if (oxygen_rate_float / delta >= 0.2f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "gauge_1") as Texture2D;
            }
            else if (oxygen_rate_float / delta >= 0.1f)
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "gauge_1") as Texture2D;
            }
            else
            {
                icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "gauge_0") as Texture2D;
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
