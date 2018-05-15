using UnityEngine;
using UnityEngine.UI;

public class HeartRate : MonoBehaviour
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
        if (TelemetryController.heart_rate_warning)
            DisplayWarning(contrast);
        else if (TelemetryController.heart_rate_critical)
            DisplayCritical(contrast);
        else
            StopWarning(contrast);
    }

    public void UpdateValue(int contrast_idx)
    {
        contrast = contrast_idx;
        value = transform.Find("value_position").transform.GetComponentInChildren<Text>();
        value.text = TelemetryController.telemetryData.heart_bpm + " bpm";

        icon_name = transform.Find("icon_position").transform.GetComponentInChildren<Text>();
        icon = transform.GetComponentInChildren<RawImage>();
        if (contrast_idx == TelemetryController.THEME_LIGHT)
        {
            transform.GetComponent<Image>().color = TelemetryController.THEME_LIGHT_BACKGROUND_COLOR;
            value.color = TelemetryController.THEME_LIGHT_TEXT_COLOR;
            icon_name.color = TelemetryController.THEME_LIGHT_TEXT_COLOR;
            icon.texture = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "heart_rate") as Texture2D;
        }
        else if (contrast_idx == TelemetryController.THEME_DARK)
        {
            transform.GetComponent<Image>().color = TelemetryController.THEME_DARK_BACKGROUND_COLOR;
            value.color = TelemetryController.THEME_DARK_TEXT_COLOR;
            icon_name.color = TelemetryController.THEME_DARK_TEXT_COLOR;
            icon.texture = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "heart_rate") as Texture2D;
        }

    }

    public void ChangeIconName(string name)
    {
        icon_name = transform.Find("icon_position").transform.GetComponentInChildren<Text>();
        //Debug.Log("Changing name from '" + icon_name.text + "' to '" + name + "'");
        icon_name.text = name;
    }

    public void ChangeContrastMode(int contrast_idx)
    {
        //Texture2D texture2D = null;
        //icon = transform.GetComponentInChildren<RawImage>();
        //value = transform.Find("value_position").transform.GetComponentInChildren<Text>();
        //icon_name = transform.Find("icon_position").transform.GetComponentInChildren<Text>();
        //if (contrast_idx == TelemetryController.THEME_LIGHT)
        //{
        //    transform.GetComponent<Image>().color = TelemetryController.THEME_LIGHT_BACKGROUND_COLOR;
        //    texture2D = Resources.Load(TelemetryController.THEME_LIGHT_ICON_FOLDER + "heart_rate") as Texture2D;
        //    icon.texture = texture2D;
        //    value.color = TelemetryController.THEME_LIGHT_TEXT_COLOR;
        //    icon_name.color = TelemetryController.THEME_LIGHT_TEXT_COLOR;
        //}
        //else if (contrast_idx == TelemetryController.THEME_DARK)
        //{
        //    transform.GetComponent<Image>().color = TelemetryController.THEME_DARK_BACKGROUND_COLOR;
        //    texture2D = Resources.Load(TelemetryController.THEME_DARK_ICON_FOLDER + "heart_rate") as Texture2D;
        //    icon.texture = texture2D;
        //    value.color = TelemetryController.THEME_DARK_TEXT_COLOR;
        //    icon_name.color = TelemetryController.THEME_DARK_TEXT_COLOR;
        //}
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
