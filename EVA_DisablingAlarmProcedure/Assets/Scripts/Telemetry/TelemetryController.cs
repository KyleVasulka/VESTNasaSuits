using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class TelemetryController : MonoBehaviour
{
    string language = TelemetryLanguageController.ENG;

    public static Telemetry telemetryData;
    public static SuitSwitch suitSwitch = null;

    public GameObject worldSpace, evaSphereSpace;
    Transform mainCamera;
    Transform panel_top, panel_bottom, panel_midle, panel_left, panel_center, panel_right;
    GameObject sound_effect;

    float start_time, eva_time;

    int topTelemetrySize = 0;
    GameObject heart_rate_obj,
        battery_time_life_obj, battery_capacity_obj,
        internal_suit_pressure_obj,
        external_environment_pressure_obj, external_environment_temperature_obj,
        oxygen_time_life_obj, oxygen_pressure_obj, oxygen_rate_obj,
        secondary_oxygen_rate_obj, secondary_oxygen_pressure_obj,
        H2O_gas_pressure_obj, H2O_liquid_pressure_obj,
        water_time_life_obj, fan_speed_obj;

    public GameObject heart_rate_prefab,
        battery_time_life_prefab, battery_capacity_prefab,
        internal_suit_pressure_prefab,
        external_environment_pressure_prefab, external_environment_temperature_prefab,
        oxygen_time_life_prefab, oxygen_pressure_prefab, oxygen_rate_prefab,
        secondary_oxygen_rate_prefab, secondary_oxygen_pressure_prefab,
        H2O_gas_pressure_prefab, H2O_liquid_pressure_prefab,
        water_time_life_prefab, fan_speed_prefab,
        empty_telemetry_prefab;


    public GameObject eva_time_prefab;

    public static bool heart_rate_warning,
        battery_time_life_warning, battery_capacity_warning,
        internal_suit_pressure_warning,
        external_environment_pressure_warning, external_environment_temperature_warning,
        oxygen_time_life_warning, oxygen_pressure_warning, oxygen_rate_warning,
        secondary_oxygen_rate_warning, secondary_oxygen_pressure_warning,
        H2O_gas_pressure_warning, H2O_liquid_pressure_warning,
        water_time_life_warning, fan_speed_warning;

    public static bool heart_rate_critical,
        battery_time_life_critical, battery_capacity_critical,
        internal_suit_pressure_critical,
        external_environment_pressure_critical, external_environment_temperature_critical,
        oxygen_time_life_critical, oxygen_pressure_critical, oxygen_rate_critical,
        secondary_oxygen_rate_critical, secondary_oxygen_pressure_critical,
        H2O_gas_pressure_critical, H2O_liquid_pressure_critical,
        water_time_life_critical, fan_speed_critical;

    public static float heart_bpm_warning_thr_up = 0;
    public static float heart_bpm_warning_thr_low = 0;

    public static float p_suit_warning_thr_up = 0;
    public static float p_suit_warning_thr_low = 0;

    public static float t_battery_warning_thr_up = 0;
    public static float t_battery_warning_thr_low = 0;
    public static float cap_battery_warning_thr_up = 0;
    public static float cap_battery_warning_thr_low = 0;

    public static float t_oxygen_warning_thr_up = 0;
    public static float t_oxygen_warning_thr_low = 0;
    public static float p_o2_warning_thr_up = 0;
    public static float p_o2_warning_thr_low = 0;
    public static float rate_o2_warning_thr_up = 0;
    public static float rate_o2_warning_thr_low = 0;

    public static float p_sop_warning_thr_up = 0;
    public static float p_sop_warning_thr_low = 0;
    public static float rate_sop_warning_thr_up = 0;
    public static float rate_sop_warning_thr_low = 0;

    public static float t_water_warning_thr_up = 0;
    public static float t_water_warning_thr_low = 0;

    public static float p_sub_warning_thr_up = 0;
    public static float p_sub_warning_thr_low = 0;
    public static float t_sub_warning_thr_up = 0;
    public static float t_sub_warning_thr_low = 0;

    public static float v_fan_warning_thr_up = 0;
    public static float v_fan_warning_thr_low = 0;

    public static float p_h2o_g_warning_thr_up = 0;
    public static float p_h2o_g_warning_thr_low = 0;
    public static float p_h2o_l_warning_thr_up = 0;
    public static float p_h2o_l_warning_thr_low = 0;

    /************************************************************
     */
    public static float heart_bpm_critical_thr_up = 0;
    public static float heart_bpm_critical_thr_low = 0;

    public static float p_suit_critical_thr_up = 0;
    public static float p_suit_critical_thr_low = 0;

    public static float t_battery_critical_thr_up = 0;
    public static float t_battery_critical_thr_low = 0;
    public static float cap_battery_critical_thr_up = 0;
    public static float cap_battery_critical_thr_low = 0;

    public static float t_oxygen_critical_thr_up = 0;
    public static float t_oxygen_critical_thr_low = 0;
    public static float p_o2_critical_thr_up = 0;
    public static float p_o2_critical_thr_low = 0;
    public static float rate_o2_critical_thr_up = 0;
    public static float rate_o2_critical_thr_low = 0;

    public static float p_sop_critical_thr_up = 0;
    public static float p_sop_critical_thr_low = 0;
    public static float rate_sop_critical_thr_up = 0;
    public static float rate_sop_critical_thr_low = 0;

    public static float t_water_critical_thr_up = 0;
    public static float t_water_critical_thr_low = 0;

    public static float p_sub_critical_thr_up = 0;
    public static float p_sub_critical_thr_low = 0;
    public static float t_sub_critical_thr_up = 0;
    public static float t_sub_critical_thr_low = 0;

    public static float v_fan_critical_thr_up = 0;
    public static float v_fan_critical_thr_low = 0;

    public static float p_h2o_g_critical_thr_up = 0;
    public static float p_h2o_g_critical_thr_low = 0;
    public static float p_h2o_l_critical_thr_up = 0;
    public static float p_h2o_l_critical_thr_low = 0;

    /************************************************************
     */


    public static int eva_time_hour, eva_time_minute, eva_time_second;

    public const string HEART_RATE = "heart_rate";
    public const string INTERNAL_SUIT_PRESSURE = "internal_suit_pressure";
    public const string EXTERNAL_ENVIRONMENT_TEMPERATURE = "external_environment_temperature";
    public const string EXTERNAL_ENVIRONMENT_PRESSURE = "external_environment_pressure";
    public const string BATTERY_TIME_LIFE = "battery_time_life";
    public const string BATTERY_CAPACITY = "battery_capacity";
    public const string OXYGEN_TIME_LIFE = "oxygen_time_life";
    public const string OXYGEN_PRESSURE = "oxygen_pressure";
    public const string OXYGEN_RATE = "oxygen_rate";
    public const string SECONDARY_OXYGEN_RATE = "secondary_oxygen_rate";
    public const string SECONDARY_OXYGEN_PRESSURE = "secondary_oxygen_pressure";
    public const string H2O_GAS_PRESSURE = "H2O_gas_pressure";
    public const string H2O_LIQUID_PRESSURE = "H2O_liquid_pressure";
    public const string FAN_SPEED = "fan_speed";
    public const string WATER_TIME_LIFE = "water_time_life";


    public GameObject switch_sop_on_prefab, switch_sspe_prefab, switch_fan_error_prefab,
       switch_vent_error_prefab, switch_vehicle_power_prefab,
       switch_h2o_off_prefab, switch_o2_off_prefab, switch_empty_prefab;

    private GameObject switch_sop_on_obj, switch_sspe_obj, switch_fan_error_obj,
      switch_vent_error_obj, switch_vehicle_power_obj,
      switch_h2o_off_obj, switch_o2_off_obj;

    public const string SWITCH_SOP_ON = "switch_sop_on";
    public const string SWITCH_SSPE = "switch_sspe";
    public const string SWITCH_FAN_ERROR = "switch_fan_error";
    public const string SWITCH_VENT_ERROR = "switch_vent_error";
    public const string SWITCH_VEHICLE_POWER = "switch_vehicle_power";
    public const string SWITCH_H2O_OFF = "switch_h2o_off";
    public const string SWITCH_O2_OFF = "switch_o2_off";


    public const int THEME_LIGHT = 1;
    public static Color THEME_LIGHT_TEXT_COLOR = new Color(50 / 255, 50 / 255, 50 / 255, 255 / 255);
    public static Color THEME_LIGHT_BACKGROUND_COLOR = new Color(255 / 255, 255 / 255, 255 / 255, 255 / 255);
    public const string THEME_LIGHT_ICON_FOLDER = "TelemetryIcons/theme_light/";
    public const int THEME_DARK = 2;
    public static Color THEME_DARK_TEXT_COLOR = new Color(255 / 255, 255 / 255, 255 / 255, 255 / 255);
    public static Color THEME_DARK_BACKGROUND_COLOR = new Color(0 / 255, 0 / 255, 0 / 255, 255 / 255);
    public const string THEME_DARK_ICON_FOLDER = "TelemetryIcons/theme_dark/";
    public static Color WARNING_BACKGROUND_COLOR = Color.yellow;
    public static Color CRITICAL_BACKGROUND_COLOR = Color.red;
    public static Color SWITCH_ON_BACKGROUND_COLOR = new Color(80 / 255, 185 / 255, 80 / 255, 255 / 255);
    public static Color SWITCH_OFF_BACKGROUND_COLOR = new Color(135 / 255, 135 / 255, 135 / 255, 255 / 255);
    int theme_idx = 2;

    public const string SOUND_EFFECT_COMPLETE = "TelemetrySounds/complete";
    public const string SOUND_EFFECT_WARNING = "TelemetrySounds/warning";

    Dictionary<int, string> rank = new Dictionary<int, string>();
    // Use this for initialization
    void Start()
    {
        start_time = Time.time;

        mainCamera = Camera.main.transform;
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);

        panel_top = transform.Find("panel_top");
        panel_midle = transform.Find("panel_midle");
        panel_bottom = transform.Find("panel_bottom");

        panel_left = panel_midle.Find("panel_left");
        panel_center = panel_midle.Find("panel_center");
        panel_right = panel_midle.Find("panel_right");
        sound_effect = transform.Find("sound_effect").gameObject;

        StartCoroutine(GetTelemetryMeta());
        StartCoroutine(UpdateTelemetrySwitch());
    }
    //voice categories
    public void TelemetryPressure()
    { //uninstantiated everything
        GameObject[] teleArray;
        teleArray = GameObject.FindGameObjectsWithTag("telemetry");
        foreach (GameObject teleObjects in teleArray)
        {
            Debug.Log(teleObjects);
            Destroy(teleObjects);
        }
        //instantiated what we want
        GameObject objInt = null;
        GameObject objExt = null;
        GameObject objOxy = null;
        GameObject objHG = null;
        GameObject objHL = null;
        GameObject objSec = null;

     
        objInt = Instantiate(internal_suit_pressure_prefab, panel_left);
        objInt.name = INTERNAL_SUIT_PRESSURE;
        objExt = Instantiate(external_environment_pressure_prefab,panel_center);
        objExt.name = EXTERNAL_ENVIRONMENT_PRESSURE;
        objOxy = Instantiate(oxygen_pressure_prefab,panel_right);
        objOxy.name = OXYGEN_PRESSURE;
        objHG = Instantiate(H2O_liquid_pressure_prefab,panel_left);
        objHG.name = H2O_LIQUID_PRESSURE;
        objHL = Instantiate(H2O_gas_pressure_prefab,panel_center);
        objHL.name = H2O_GAS_PRESSURE;
        objSec = Instantiate(secondary_oxygen_pressure_prefab,panel_right);
        objSec.name = SECONDARY_OXYGEN_PRESSURE;




        PlaySoundEffect(SOUND_EFFECT_COMPLETE, false);

    }
    public void TelemetryEnergy()
    {
        //uninstantiated everything
        GameObject[] teleArray;
        teleArray = GameObject.FindGameObjectsWithTag("telemetry");
        foreach (GameObject teleObjects in teleArray)
        {
            Debug.Log(teleObjects);
            Destroy(teleObjects);
        }
        GameObject objExtTemp = null;
        GameObject objBatCap = null;
        GameObject objFan = null;

        objExtTemp = Instantiate(external_environment_temperature_prefab,panel_left);
        objExtTemp.name = EXTERNAL_ENVIRONMENT_TEMPERATURE;
        objBatCap = Instantiate(battery_capacity_prefab,panel_center);
        objBatCap.name = BATTERY_CAPACITY;
        objFan = Instantiate(fan_speed_prefab,panel_right);
        objFan.name = FAN_SPEED;
        PlaySoundEffect(SOUND_EFFECT_COMPLETE, false);
    }
    public void TelemetryRate()
    {
        //uninstantiated everything
        GameObject[] teleArray;
        teleArray = GameObject.FindGameObjectsWithTag("telemetry");
        foreach (GameObject teleObjects in teleArray)
        {
            Debug.Log(teleObjects);
            Destroy(teleObjects);
        }
        GameObject objOxygenRate = null;
        GameObject objHeart = null;
        GameObject objSecOxygenRate = null;

        objHeart = Instantiate(heart_rate_prefab,panel_left);
        objHeart.name = HEART_RATE;
        objOxygenRate = Instantiate(oxygen_rate_prefab,panel_center);
        objOxygenRate.name = OXYGEN_RATE;
        objSecOxygenRate = Instantiate(secondary_oxygen_rate_prefab,panel_right);
        objSecOxygenRate.name = SECONDARY_OXYGEN_RATE;
        PlaySoundEffect(SOUND_EFFECT_COMPLETE, false);
    }
    public void TelemetryTimeLife()
    {//uninstantiated everything
        GameObject[] teleArray;
        teleArray = GameObject.FindGameObjectsWithTag("telemetry");
        foreach (GameObject teleObjects in teleArray)
        {
            Debug.Log(teleObjects);
            Destroy(teleObjects);
        }
        GameObject objBatteryTime = null;
        GameObject objOxygenTime = null;
        GameObject objWaterTime = null;

        objBatteryTime = Instantiate(battery_time_life_prefab,panel_left);
        objBatteryTime.name = BATTERY_TIME_LIFE;
        objOxygenTime = Instantiate(oxygen_time_life_prefab,panel_center);
        objOxygenTime.name = OXYGEN_TIME_LIFE;
        objWaterTime = Instantiate(water_time_life_prefab,panel_right);
        objWaterTime.name = WATER_TIME_LIFE;
        PlaySoundEffect(SOUND_EFFECT_COMPLETE, false);
    }

    // Update is called once per frame
    void Update()
    {
        eva_time = Time.time - start_time;
        eva_time_hour = (int)eva_time / 3600;
        eva_time_minute = (int)eva_time / 60;
        eva_time_second = (int)eva_time % 60;
        


    }

    private IEnumerator GetTelemetryMeta()
    {
        string hostName = ConnectionController.HOST_NAME;
        if (!hostName.Equals(ConnectionController.HOST_NAME_UNRECHABLE))
        {
            WWW httpResponse = new WWW("http://" + hostName + "/NasaSuits/api/Tele/GetTeleMeta.php");
            yield return httpResponse;
            Debug.Log("Get Telemetry ranking value");

            List<TelemetryMeta> telemetryMetaList = null;
            try
            {
                telemetryMetaList = JsonConvert.DeserializeObject<List<TelemetryMeta>>(httpResponse.text);
                for (int i = 0; i < telemetryMetaList.Count; i++)
                {
                    TelemetryMeta telemetryMeta = telemetryMetaList[i];
                    rank[Int32.Parse(telemetryMeta.rank)] = telemetryMeta.telemetry_data;

                    switch (telemetryMeta.telemetry_data)
                    {

                        case HEART_RATE:
                            heart_bpm_warning_thr_up = float.Parse(telemetryMeta.warning_thr_up);
                            heart_bpm_warning_thr_low = float.Parse(telemetryMeta.warning_thr_low);
                            heart_bpm_critical_thr_up = float.Parse(telemetryMeta.critical_thr_up);
                            heart_bpm_critical_thr_low = float.Parse(telemetryMeta.critical_thr_low);
                            break;

                        case INTERNAL_SUIT_PRESSURE:
                            p_suit_warning_thr_up = float.Parse(telemetryMeta.warning_thr_up);
                            p_suit_warning_thr_low = float.Parse(telemetryMeta.warning_thr_low);
                            p_suit_critical_thr_up = float.Parse(telemetryMeta.critical_thr_up);
                            p_suit_critical_thr_low = float.Parse(telemetryMeta.critical_thr_low);
                            break;
                       
                        case EXTERNAL_ENVIRONMENT_PRESSURE:
                            p_sub_warning_thr_up = float.Parse(telemetryMeta.warning_thr_up);
                            p_sub_warning_thr_low = float.Parse(telemetryMeta.warning_thr_low);
                            p_sub_critical_thr_up = float.Parse(telemetryMeta.critical_thr_up);
                            p_sub_critical_thr_low = float.Parse(telemetryMeta.critical_thr_low);
                            break;
                        case EXTERNAL_ENVIRONMENT_TEMPERATURE:
                            t_sub_warning_thr_up = float.Parse(telemetryMeta.warning_thr_up);
                            t_sub_warning_thr_low = float.Parse(telemetryMeta.warning_thr_low);
                            t_sub_critical_thr_up = float.Parse(telemetryMeta.critical_thr_up);
                            t_sub_critical_thr_low = float.Parse(telemetryMeta.critical_thr_low);
                            break;
                        case BATTERY_TIME_LIFE:
                            t_battery_warning_thr_up = float.Parse(telemetryMeta.warning_thr_up);
                            t_battery_warning_thr_low = float.Parse(telemetryMeta.warning_thr_low);
                            t_battery_critical_thr_up = float.Parse(telemetryMeta.critical_thr_up);
                            t_battery_critical_thr_low = float.Parse(telemetryMeta.critical_thr_low);
                            break;
                        case BATTERY_CAPACITY:
                            cap_battery_warning_thr_up = float.Parse(telemetryMeta.warning_thr_up);
                            cap_battery_warning_thr_low = float.Parse(telemetryMeta.warning_thr_low);
                            cap_battery_critical_thr_up = float.Parse(telemetryMeta.critical_thr_up);
                            cap_battery_critical_thr_low = float.Parse(telemetryMeta.critical_thr_low);
                            break;
                        case OXYGEN_TIME_LIFE:
                            t_oxygen_warning_thr_up = float.Parse(telemetryMeta.warning_thr_up);
                            t_oxygen_warning_thr_low = float.Parse(telemetryMeta.warning_thr_low);
                            t_oxygen_critical_thr_up = float.Parse(telemetryMeta.critical_thr_up);
                            t_oxygen_critical_thr_low = float.Parse(telemetryMeta.critical_thr_low);
                            break;
                        case OXYGEN_PRESSURE:
                            p_o2_warning_thr_up = float.Parse(telemetryMeta.warning_thr_up);
                            p_o2_warning_thr_low = float.Parse(telemetryMeta.warning_thr_low);
                            p_o2_critical_thr_up = float.Parse(telemetryMeta.critical_thr_up);
                            p_o2_critical_thr_low = float.Parse(telemetryMeta.critical_thr_low);
                            break;
                        case OXYGEN_RATE:
                            rate_o2_warning_thr_up = float.Parse(telemetryMeta.warning_thr_up);
                            rate_o2_warning_thr_low = float.Parse(telemetryMeta.warning_thr_low);
                            rate_o2_critical_thr_up = float.Parse(telemetryMeta.critical_thr_up);
                            rate_o2_critical_thr_low = float.Parse(telemetryMeta.critical_thr_low);
                            break;
                        case SECONDARY_OXYGEN_PRESSURE:
                            p_sop_warning_thr_up = float.Parse(telemetryMeta.warning_thr_up);
                            p_sop_warning_thr_low = float.Parse(telemetryMeta.warning_thr_low);
                            p_sop_critical_thr_up = float.Parse(telemetryMeta.critical_thr_up);
                            p_sop_critical_thr_low = float.Parse(telemetryMeta.critical_thr_low);
                            break;
                        case SECONDARY_OXYGEN_RATE:
                            rate_sop_warning_thr_up = float.Parse(telemetryMeta.warning_thr_up);
                            rate_sop_warning_thr_low = float.Parse(telemetryMeta.warning_thr_low);
                            rate_sop_critical_thr_up = float.Parse(telemetryMeta.critical_thr_up);
                            rate_sop_critical_thr_low = float.Parse(telemetryMeta.critical_thr_low);
                            break;
                        case H2O_GAS_PRESSURE:
                            p_h2o_g_warning_thr_up = float.Parse(telemetryMeta.warning_thr_up);
                            p_h2o_g_warning_thr_low = float.Parse(telemetryMeta.warning_thr_low);
                            p_h2o_g_critical_thr_up = float.Parse(telemetryMeta.critical_thr_up);
                            p_h2o_g_critical_thr_low = float.Parse(telemetryMeta.critical_thr_low);
                            break;
                        case H2O_LIQUID_PRESSURE:
                            p_h2o_l_warning_thr_up = float.Parse(telemetryMeta.warning_thr_up);
                            p_h2o_l_warning_thr_low = float.Parse(telemetryMeta.warning_thr_low);
                            p_h2o_l_critical_thr_up = float.Parse(telemetryMeta.critical_thr_up);
                            p_h2o_l_critical_thr_low = float.Parse(telemetryMeta.critical_thr_low);
                            break;
                        case FAN_SPEED:
                            v_fan_warning_thr_up = float.Parse(telemetryMeta.warning_thr_up);
                            v_fan_warning_thr_low = float.Parse(telemetryMeta.warning_thr_low);
                            v_fan_critical_thr_up = float.Parse(telemetryMeta.critical_thr_up);
                            v_fan_critical_thr_low = float.Parse(telemetryMeta.critical_thr_low);
                            break;
                      
                        case WATER_TIME_LIFE:
                            t_water_warning_thr_up = float.Parse(telemetryMeta.warning_thr_up);
                            t_water_warning_thr_low = float.Parse(telemetryMeta.warning_thr_low);
                            t_water_critical_thr_up = float.Parse(telemetryMeta.critical_thr_up);
                            t_water_critical_thr_low = float.Parse(telemetryMeta.critical_thr_low);
                            break;
                        default:
                            break;

                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(httpResponse.text);
                Debug.Log(e.ToString());
            }
        }
        ShowTopTelemetryData(rank.Count);
        ShowAllSwitches();
        yield return null;
    }

    private IEnumerator UpdateTelemetrySwitch()
    {
        while (true)
        {
            string hostName = ConnectionController.HOST_NAME;
            if (!hostName.Equals(ConnectionController.HOST_NAME_UNRECHABLE))
            {
                WWW httpResponse = new WWW("http://" + hostName + "/NasaSuits/api/Tele/GetTele.php?RespSize=1");
                yield return httpResponse;

                List<Telemetry> telemetryDataList = null;
                try
                {
                    telemetryDataList = JsonConvert.DeserializeObject<List<Telemetry>>(httpResponse.text);
                    telemetryData = telemetryDataList[0];
                }
                catch (Exception e)
                {
                    Debug.Log(httpResponse.text);
                    Debug.Log(e.ToString());
                }

                if (CheckWarning())
                {
                    PlaySoundEffect(SOUND_EFFECT_WARNING, true);
                }
                else
                {
                    StopSoundEffect();
                }

                UpdateTelemetry();

                httpResponse = new WWW("http://" + hostName + "/NasaSuits/api/Tele/GetSwitches.php?RespSize=1");
                yield return httpResponse;

                List<SuitSwitch> suitSwitchList = null;
                try
                {
                    suitSwitchList = JsonConvert.DeserializeObject<List<SuitSwitch>>(httpResponse.text);
                    suitSwitch = suitSwitchList[0];
                }
                catch (Exception e)
                {
                    Debug.Log(httpResponse.text);
                    Debug.Log(e.ToString());
                }

                UpdateSwitch();

            }

            yield return new WaitForSeconds(1);
        }
    }

    private void UpdateTelemetry()
    {
        heart_rate_obj = GameObject.Find(HEART_RATE);
        internal_suit_pressure_obj = GameObject.Find(INTERNAL_SUIT_PRESSURE);
        external_environment_pressure_obj = GameObject.Find(EXTERNAL_ENVIRONMENT_PRESSURE);
        external_environment_temperature_obj = GameObject.Find(EXTERNAL_ENVIRONMENT_TEMPERATURE);
        battery_time_life_obj = GameObject.Find(BATTERY_TIME_LIFE);
        battery_capacity_obj = GameObject.Find(BATTERY_CAPACITY);
        oxygen_time_life_obj = GameObject.Find(OXYGEN_TIME_LIFE);
        oxygen_pressure_obj = GameObject.Find(OXYGEN_PRESSURE);
        oxygen_rate_obj = GameObject.Find(OXYGEN_RATE);
        secondary_oxygen_pressure_obj = GameObject.Find(SECONDARY_OXYGEN_PRESSURE);
        secondary_oxygen_rate_obj = GameObject.Find(SECONDARY_OXYGEN_RATE);
        H2O_gas_pressure_obj = GameObject.Find(H2O_GAS_PRESSURE);
        H2O_liquid_pressure_obj = GameObject.Find(H2O_LIQUID_PRESSURE);
        fan_speed_obj = GameObject.Find(FAN_SPEED);
        water_time_life_obj = GameObject.Find(WATER_TIME_LIFE);
        

        if (heart_rate_obj != null)
            heart_rate_obj.GetComponent<HeartRate>().UpdateValue(theme_idx);

        if (internal_suit_pressure_obj != null)
            internal_suit_pressure_obj.GetComponent<InternalSuitPressure>().UpdateValue(theme_idx);

        if (external_environment_temperature_obj != null)
            external_environment_temperature_obj.GetComponent<ExternalEnvironmentTemperature>().UpdateValue(theme_idx);
        if (external_environment_pressure_obj != null)
            external_environment_pressure_obj.GetComponent<ExternalEnvironmentPressure>().UpdateValue(theme_idx);

        if (secondary_oxygen_pressure_obj != null)
            secondary_oxygen_pressure_obj.GetComponent<SecondaryOxygenPressure>().UpdateValue(theme_idx);
        if (secondary_oxygen_rate_obj != null)
            secondary_oxygen_rate_obj.GetComponent<SecondaryOxygenRate>().UpdateValue(theme_idx);

        if (battery_time_life_obj != null)
            battery_time_life_obj.GetComponent<BatteryTimeLife>().UpdateValue(theme_idx);
        if (battery_capacity_obj != null)
            battery_capacity_obj.GetComponent<BatteryCapacity>().UpdateValue(theme_idx);

        if (oxygen_rate_obj != null)
            oxygen_rate_obj.GetComponent<OxygenRate>().UpdateValue(theme_idx);
        if (oxygen_time_life_obj != null)
            oxygen_time_life_obj.GetComponent<OxygenTimeLife>().UpdateValue(theme_idx);
        if (oxygen_pressure_obj != null)
            oxygen_pressure_obj.GetComponent<OxygenPressure>().UpdateValue(theme_idx);


        if (H2O_gas_pressure_obj != null)
            H2O_gas_pressure_obj.GetComponent<H2OGasPressure>().UpdateValue(theme_idx);
        if (H2O_liquid_pressure_obj != null)
            H2O_liquid_pressure_obj.GetComponent<H2OLiquidPressure>().UpdateValue(theme_idx);

        if (fan_speed_obj != null)
            fan_speed_obj.GetComponent<FanSpeed>().UpdateValue(theme_idx);
        if (water_time_life_obj != null)
            water_time_life_obj.GetComponent<WaterTimeLife>().UpdateValue(theme_idx);
    }

    private void UpdateSwitch()
    {
        switch_sop_on_obj = GameObject.Find(SWITCH_SOP_ON);
        switch_sspe_obj = GameObject.Find(SWITCH_SSPE);
        switch_fan_error_obj = GameObject.Find(SWITCH_FAN_ERROR);
        switch_vent_error_obj = GameObject.Find(SWITCH_VENT_ERROR);
        switch_vehicle_power_obj = GameObject.Find(SWITCH_VEHICLE_POWER);
        switch_h2o_off_obj = GameObject.Find(SWITCH_H2O_OFF);
        switch_o2_off_obj = GameObject.Find(SWITCH_O2_OFF);

        if (switch_sop_on_obj != null)
            switch_sop_on_obj.GetComponent<SopSwitch>().UpdateValue(theme_idx);
        if (switch_sspe_obj != null)
            switch_sspe_obj.GetComponent<SspeSwitch>().UpdateValue(theme_idx);
        if (switch_fan_error_obj != null)
            switch_fan_error_obj.GetComponent<FanErrorSwitch>().UpdateValue(theme_idx);
        if (switch_vent_error_obj != null)
            switch_vent_error_obj.GetComponent<VentErrorSwitch>().UpdateValue(theme_idx);
        if (switch_vehicle_power_obj != null)
            switch_vehicle_power_obj.GetComponent<VehiclePowerSwitch>().UpdateValue(theme_idx);
        if (switch_h2o_off_obj != null)
            switch_h2o_off_obj.GetComponent<WaterOffSwitch>().UpdateValue(theme_idx);
        if (switch_o2_off_obj != null)
            switch_o2_off_obj.GetComponent<OxygenOffSwitch>().UpdateValue(theme_idx);
    }

    private bool CheckWarning()
    {
        bool warning = false;
        if (telemetryData == null)
            return warning;

        // Heart rate
        float heart_bpm_float = float.Parse(telemetryData.heart_bpm);
        if ((heart_bpm_float > heart_bpm_critical_thr_low && heart_bpm_float < heart_bpm_warning_thr_low) || (heart_bpm_float > heart_bpm_warning_thr_up && heart_bpm_float < heart_bpm_critical_thr_up))
        {
            Debug.Log("heart rate is at warning level");
            warning = true;
            heart_rate_warning = true;
            heart_rate_critical = false;
        }
        else if (heart_bpm_float <= heart_bpm_critical_thr_low || heart_bpm_float >= heart_bpm_critical_thr_up)
        {
            Debug.Log("heart rate is at critical level");
            warning = true;
            heart_rate_warning = false;
            heart_rate_critical = true;
        }
        else
        {
            heart_rate_warning = false;
            heart_rate_critical = false;
        }

        // External Temperature
        float external_environment_temperature_float = float.Parse(telemetryData.t_sub);
        if ((external_environment_temperature_float > t_sub_critical_thr_low && external_environment_temperature_float < t_sub_warning_thr_low) || (external_environment_temperature_float > t_sub_warning_thr_up && external_environment_temperature_float < t_sub_critical_thr_up))
        {
            Debug.Log("external temperature is at warning level");
            warning = true;
            external_environment_temperature_warning = true;
            external_environment_temperature_critical = false;
        }
        else if (external_environment_temperature_float <= t_sub_critical_thr_low || external_environment_temperature_float >= t_sub_critical_thr_up)
        {
            Debug.Log("external temperature is at critical level");
            warning = true;
            external_environment_temperature_warning = false;
            external_environment_temperature_critical = true;
        }
        else
        {
            external_environment_temperature_warning = false;
            external_environment_temperature_critical = false;
        }

        // Battery Capacity
        float battery_capacity_float = float.Parse(telemetryData.cap_battery);
        if ((battery_capacity_float > cap_battery_critical_thr_low && battery_capacity_float < cap_battery_warning_thr_low) || (battery_capacity_float > cap_battery_warning_thr_up && battery_capacity_float < cap_battery_critical_thr_up))
        {
            Debug.Log("battery capacity is at warning level");
            warning = true;
            battery_capacity_warning = true;
            battery_capacity_critical = false;
        }
        else if (battery_capacity_float <= cap_battery_critical_thr_low || battery_capacity_float >= cap_battery_critical_thr_up)
        {
            Debug.Log("battery capacity is at critical level");
            warning = true;
            battery_capacity_warning = false;
            battery_capacity_critical = true;
        }
        else
        {
            battery_capacity_warning = false;
            battery_capacity_critical = false;
        }



        // Battery time life
        float battery_time_life_float = float.Parse(telemetryData.t_battery.Substring(0, telemetryData.t_battery.IndexOf(":")));

        if (battery_time_life_float > t_battery_critical_thr_low && battery_time_life_float < t_battery_warning_thr_low)
        {
            Debug.Log("battery time life is at warning level");
            warning = true;
            battery_time_life_warning = true;
            battery_time_life_critical = false;
        }
        else if (battery_time_life_float <= t_battery_critical_thr_low)
        {
            Debug.Log("battery time life is at critical level");
            warning = true;
            battery_time_life_critical = true;
            battery_time_life_warning = false;
        }
        else
        {
            battery_time_life_critical = false;
            battery_time_life_warning = false;
        }

        // Oxygen time life
        float oxygen_time_life_float = float.Parse(telemetryData.t_battery.Substring(0, telemetryData.t_oxygen.IndexOf(":")));
        if (oxygen_time_life_float > t_oxygen_critical_thr_low && oxygen_time_life_float < t_oxygen_warning_thr_low)
        {
            Debug.Log("oxygen time life is at warning level");
            warning = true;
            oxygen_time_life_warning = true;
            oxygen_time_life_critical = false;
        }
        else if (oxygen_time_life_float <= t_oxygen_critical_thr_low)
        {
            Debug.Log("oxygen time life is at critical level");
            warning = true;
            oxygen_time_life_warning = false;
            oxygen_time_life_critical = true;
        }
        else
        {
            oxygen_time_life_warning = false;
            oxygen_time_life_critical = false;
        }

        // Water time life
        float water_time_life_float = float.Parse(telemetryData.t_battery.Substring(0, telemetryData.t_water.IndexOf(":")));
        if (water_time_life_float > t_water_critical_thr_low && water_time_life_float < t_water_warning_thr_low)
        {
            Debug.Log("water time life is at warning level");
            warning = true;
            water_time_life_critical = false;
            water_time_life_warning = true;
        }
        else if (water_time_life_float <= t_water_critical_thr_low)
        {
            Debug.Log("water time life is at critical level");
            warning = true;
            water_time_life_critical = true;
            water_time_life_warning = false;
        }
        else
        {
            water_time_life_critical = false;
            water_time_life_warning = false;
        }

        // Internal suit presessure
        float internal_suit_pressure_float = float.Parse(telemetryData.p_suit);
        if ((internal_suit_pressure_float > p_suit_critical_thr_low && internal_suit_pressure_float < p_suit_warning_thr_low) || (internal_suit_pressure_float > p_suit_warning_thr_up && internal_suit_pressure_float < p_suit_critical_thr_up))
        {
            Debug.Log("suit pressure is at warning level");
            warning = true;
            internal_suit_pressure_warning = true;
            internal_suit_pressure_critical = false;
        }
        else if (internal_suit_pressure_float <= p_suit_critical_thr_low || internal_suit_pressure_float >= p_suit_critical_thr_up)
        {
            Debug.Log("suit pressure is at critical level, " + internal_suit_pressure_float + " out of (" + p_suit_critical_thr_low + "," + p_suit_critical_thr_up + ")");
            warning = true;
            internal_suit_pressure_warning = false;
            internal_suit_pressure_critical = true;
        }
        else
        {
            internal_suit_pressure_critical = false;
            internal_suit_pressure_warning = false;
        }

        // external environment pressure
        float external_environment_pressure_float = float.Parse(telemetryData.p_sub);
        if ((external_environment_pressure_float > p_sub_critical_thr_low && external_environment_pressure_float < p_sub_warning_thr_low) || (external_environment_pressure_float > p_sub_warning_thr_up && external_environment_pressure_float < p_sub_critical_thr_up))
        {
            warning = true;
            external_environment_pressure_warning = true;
            external_environment_pressure_critical = false;
        }
        else if (external_environment_pressure_float <= p_sub_critical_thr_low || external_environment_pressure_float >= p_sub_critical_thr_up)
        {
            warning = true;
            external_environment_pressure_warning = false;
            external_environment_pressure_critical = true;
        }
        else
        {
            external_environment_pressure_warning = false;
            external_environment_pressure_critical = false;
        }

        // oxygen pressure
        float oxygen_pressure_float = float.Parse(telemetryData.p_o2);
        if ((oxygen_pressure_float > p_o2_critical_thr_low && oxygen_pressure_float < p_o2_warning_thr_low) || (oxygen_pressure_float > p_o2_warning_thr_up && oxygen_pressure_float < p_o2_critical_thr_up))
        {
            warning = true;
            oxygen_pressure_warning = true;
            oxygen_pressure_critical = false;
        }
        else if (oxygen_pressure_float <= p_o2_critical_thr_low || oxygen_pressure_float >= p_o2_critical_thr_up)
        {
            warning = true;
            oxygen_pressure_warning = false;
            oxygen_pressure_critical = true;
        }
        else
        {
            oxygen_pressure_warning = false;
            oxygen_pressure_critical = false;
        }

        // oxygen rate
        float oxygen_rate_float = float.Parse(telemetryData.rate_o2);
        if ((oxygen_rate_float > rate_o2_critical_thr_low && oxygen_rate_float < rate_o2_warning_thr_low) || (oxygen_rate_float > rate_o2_warning_thr_up && oxygen_rate_float < rate_o2_critical_thr_up))
        {
            warning = true;
            oxygen_rate_warning = true;
            oxygen_rate_critical = false;
        }
        else if (oxygen_rate_float <= rate_o2_critical_thr_low || oxygen_rate_float >= rate_o2_critical_thr_up)
        {
            warning = true;
            oxygen_rate_warning = false;
            oxygen_rate_critical = true;
        }
        else
        {
            oxygen_rate_warning = false;
            oxygen_rate_critical = false;
        }

        // sop pressure
        float secondary_oxygen_pressure_float = float.Parse(telemetryData.p_sop);
        if ((secondary_oxygen_pressure_float > p_sop_critical_thr_low && secondary_oxygen_pressure_float < p_sop_warning_thr_low) || (secondary_oxygen_pressure_float > p_sop_warning_thr_up && secondary_oxygen_pressure_float < p_sop_critical_thr_up))
        {
            warning = true;
            secondary_oxygen_pressure_warning = true;
            secondary_oxygen_pressure_critical = false;
        }
        else if (secondary_oxygen_pressure_float <= p_sop_critical_thr_low || secondary_oxygen_pressure_float >= p_sop_critical_thr_up)
        {
            warning = true;
            secondary_oxygen_pressure_warning = false;
            secondary_oxygen_pressure_critical = true;
        }
        else
        {
            secondary_oxygen_pressure_warning = false;
            secondary_oxygen_pressure_critical = false;
        }

        // sop rate 
        float secondary_oxygen_rate_float = float.Parse(telemetryData.rate_sop);
        if ((secondary_oxygen_rate_float > rate_sop_critical_thr_low && secondary_oxygen_rate_float < rate_sop_warning_thr_low) || (secondary_oxygen_rate_float > rate_sop_warning_thr_up && secondary_oxygen_rate_float < rate_sop_critical_thr_up))
        {
            warning = true;
            secondary_oxygen_rate_warning = true;
            secondary_oxygen_rate_critical = false;
        }
        else if (secondary_oxygen_rate_float <= rate_sop_critical_thr_low || secondary_oxygen_rate_float >= rate_sop_critical_thr_up)
        {
            warning = true;
            secondary_oxygen_rate_warning = false;
            secondary_oxygen_rate_critical = true;
        }
        else
        {
            secondary_oxygen_rate_warning = false;
            secondary_oxygen_rate_critical = false;
        }


        // fan speed
        float fan_speed_float = float.Parse(telemetryData.v_fan);
        if ((fan_speed_float > v_fan_critical_thr_low && fan_speed_float < v_fan_warning_thr_low) || (fan_speed_float > v_fan_warning_thr_up && fan_speed_float < v_fan_critical_thr_up))
        {
            warning = true;
            fan_speed_warning = true;
            fan_speed_critical = false;
        }
        else if (fan_speed_float <= v_fan_critical_thr_low || fan_speed_float >= v_fan_critical_thr_up)
        {
            warning = true;
            fan_speed_warning = false;
            fan_speed_critical = true;
        }
        else
        {
            fan_speed_warning = false;
            fan_speed_critical = false;
        }


        // h2o gas pressure
        float H2O_gas_pressure_float = float.Parse(telemetryData.p_h2o_g);
        if ((H2O_gas_pressure_float > p_h2o_g_critical_thr_low && H2O_gas_pressure_float < p_h2o_g_warning_thr_low) || (H2O_gas_pressure_float > p_h2o_g_warning_thr_up && H2O_gas_pressure_float < p_h2o_g_critical_thr_up))
        {
            warning = true;
            H2O_gas_pressure_warning = true;
            H2O_gas_pressure_critical = false;
        }
        else if (H2O_gas_pressure_float <= p_h2o_g_critical_thr_low || H2O_gas_pressure_float >= p_h2o_g_critical_thr_up)
        {
            warning = true;
            H2O_gas_pressure_warning = false;
            H2O_gas_pressure_critical = true;
        }
        else
        {
            H2O_gas_pressure_warning = false;
            H2O_gas_pressure_critical = false;
        }

        float H2O_liquid_pressure_float = float.Parse(telemetryData.p_h2o_l);
        if ((H2O_liquid_pressure_float > p_h2o_l_critical_thr_low && H2O_liquid_pressure_float < p_h2o_l_warning_thr_low) || (H2O_liquid_pressure_float > p_h2o_l_warning_thr_up && H2O_liquid_pressure_float < p_h2o_l_critical_thr_up))
        {
            warning = true;
            H2O_liquid_pressure_warning = true;
            H2O_liquid_pressure_critical = false;
        }
        else if (H2O_liquid_pressure_float <= p_h2o_l_critical_thr_low || H2O_liquid_pressure_float >= p_h2o_l_critical_thr_up)
        {
            warning = true;
            H2O_liquid_pressure_warning = false;
            H2O_liquid_pressure_critical = true;
        }
        else
        {
            H2O_liquid_pressure_warning = false;
            H2O_liquid_pressure_critical = false;
        }

        return warning;
    }

    public void ShowAllTelemetry()
    {
        ShowTopTelemetryData(rank.Count);
        PlaySoundEffect(SOUND_EFFECT_COMPLETE, false);
        topTelemetrySize = rank.Count;
    }

    public void HideAllTelemetry()
    {
        ShowTopTelemetryData(0);
        PlaySoundEffect(SOUND_EFFECT_COMPLETE, false);
        topTelemetrySize = 0;
    }

    public void ShowTopTelemetryData(int size)
    {
        Debug.Log("Display top " + size + " telemetry values");
        GameObject[] objs = GameObject.FindGameObjectsWithTag("telemetry");
        for (int i = 0; i < objs.Length; i++)
        {
            Destroy(objs[i]);
        }

        string value;
        for (int i = 1; i <= size; i++)
        {
            rank.TryGetValue(i, out value);
            if (i % 3 == 1)
                AddTelemetryDataToGrid(value, panel_left);
            else if (i % 3 == 2)
                AddTelemetryDataToGrid(value, panel_center);
            else
                AddTelemetryDataToGrid(value, panel_right);
        }

        if (size % 3 == 1)
        {
            AddTelemetryDataToGrid("empty", panel_center);
            AddTelemetryDataToGrid("empty", panel_right);
        }
        else if (size % 3 == 2)
        {
            AddTelemetryDataToGrid("empty", panel_right);
        }

 
       
       internal_suit_pressure_obj = GameObject.Find(INTERNAL_SUIT_PRESSURE);
        external_environment_pressure_obj = GameObject.Find(EXTERNAL_ENVIRONMENT_PRESSURE);
        external_environment_temperature_obj = GameObject.Find(EXTERNAL_ENVIRONMENT_TEMPERATURE);
        battery_time_life_obj = GameObject.Find(BATTERY_TIME_LIFE);
        battery_capacity_obj = GameObject.Find(BATTERY_CAPACITY);
        oxygen_time_life_obj = GameObject.Find(OXYGEN_TIME_LIFE);
        oxygen_pressure_obj = GameObject.Find(OXYGEN_PRESSURE);
        oxygen_rate_obj = GameObject.Find(OXYGEN_RATE);
        secondary_oxygen_pressure_obj = GameObject.Find(SECONDARY_OXYGEN_PRESSURE);
        secondary_oxygen_rate_obj = GameObject.Find(SECONDARY_OXYGEN_RATE);
        H2O_gas_pressure_obj = GameObject.Find(H2O_GAS_PRESSURE);
        H2O_liquid_pressure_obj = GameObject.Find(H2O_LIQUID_PRESSURE);
        fan_speed_obj = GameObject.Find(FAN_SPEED);
        heart_rate_obj = GameObject.Find(HEART_RATE);
        water_time_life_obj = GameObject.Find(WATER_TIME_LIFE);
        

        //ChangeAllIconNames();
        StartCoroutine(WaitAndChangeAllIconNames(1));
        PlaySoundEffect(SOUND_EFFECT_COMPLETE, false);
        topTelemetrySize = size;
    }

    private GameObject AddTelemetryDataToGrid(string telemetryDataName, Transform parent)
    {
        GameObject obj = null;
        Vector3 orgPos;
        switch (telemetryDataName)
        {
            //---------------------------------------------------
            case HEART_RATE:
                obj = Instantiate(heart_rate_prefab);
                break;
            case INTERNAL_SUIT_PRESSURE:
                obj = Instantiate(internal_suit_pressure_prefab);
                break;
            //---------------------------------------------------
            case EXTERNAL_ENVIRONMENT_TEMPERATURE:
                obj = Instantiate(external_environment_temperature_prefab);
                break;
            case EXTERNAL_ENVIRONMENT_PRESSURE:
                obj = Instantiate(external_environment_pressure_prefab);
                break;
            //---------------------------------------------------
            case BATTERY_TIME_LIFE:
                obj = Instantiate(battery_time_life_prefab);
                break;
            case BATTERY_CAPACITY:
                obj = Instantiate(battery_capacity_prefab);
                break;
            //---------------------------------------------------
            case OXYGEN_PRESSURE:
                obj = Instantiate(oxygen_pressure_prefab);
                break;
            case OXYGEN_RATE:
                obj = Instantiate(oxygen_rate_prefab);
                break;
            case OXYGEN_TIME_LIFE:
                obj = Instantiate(oxygen_time_life_prefab);
                break;
            //---------------------------------------------------
            case SECONDARY_OXYGEN_PRESSURE:
                obj = Instantiate(secondary_oxygen_pressure_prefab);
                break;
            case SECONDARY_OXYGEN_RATE:
                obj = Instantiate(secondary_oxygen_rate_prefab);
                break;
            //---------------------------------------------------       
            case H2O_GAS_PRESSURE:
                obj = Instantiate(H2O_gas_pressure_prefab);
                break;
            case H2O_LIQUID_PRESSURE:
                obj = Instantiate(H2O_liquid_pressure_prefab);
                break;
            //--------------------------------------------------- 
            case WATER_TIME_LIFE:
                obj = Instantiate(water_time_life_prefab);
                break;
            case FAN_SPEED:
                obj = Instantiate(fan_speed_prefab);
                break;
           
            //--------------------------------------------------- 
            default:
                obj = Instantiate(empty_telemetry_prefab);
                break;
        }
        obj.name = telemetryDataName;
        obj.transform.SetParent(parent);
        orgPos = obj.transform.localPosition;
        obj.transform.localPosition = new Vector3(orgPos.x, orgPos.y, 0);
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.transform.localRotation = Quaternion.identity;
        return obj;
    }

    public void HideAllSwitches()
    {
        Debug.Log("Hide Switches");
        GameObject[] objs = GameObject.FindGameObjectsWithTag("switch");
        for (int i = 0; i < objs.Length; i++)
        {
            Destroy(objs[i]);
        }
        PlaySoundEffect(SOUND_EFFECT_COMPLETE, false);
    }

    public void ShowAllSwitches()
    {
        Debug.Log("Show Switches");
        GameObject[] objs = GameObject.FindGameObjectsWithTag("switch");
        for (int i = 0; i < objs.Length; i++)
        {
            Destroy(objs[i]);
        }
        
        AddSwitchToGrid(SWITCH_SSPE, panel_bottom);
        AddSwitchToGrid(SWITCH_SOP_ON, panel_bottom);
        AddSwitchToGrid(SWITCH_FAN_ERROR, panel_bottom);
        AddSwitchToGrid(SWITCH_VENT_ERROR, panel_bottom);
        AddSwitchToGrid(SWITCH_VEHICLE_POWER, panel_bottom);
        AddSwitchToGrid(SWITCH_H2O_OFF, panel_bottom);
        AddSwitchToGrid(SWITCH_O2_OFF, panel_bottom);


        PlaySoundEffect(SOUND_EFFECT_COMPLETE, false);
    }

    private GameObject AddSwitchToGrid(string switchName, Transform parent)
    {
        GameObject obj = null;
        Vector3 orgPos;
        switch (switchName)
        {
            //---------------------------------------------------
            case SWITCH_SOP_ON:
                obj = Instantiate(switch_sop_on_prefab);
                break;
            case SWITCH_SSPE:
                obj = Instantiate(switch_sspe_prefab);
                break;
            case SWITCH_FAN_ERROR:
                obj = Instantiate(switch_fan_error_prefab);
                break;
            case SWITCH_VENT_ERROR:
                obj = Instantiate(switch_vent_error_prefab);
                break;
            case SWITCH_VEHICLE_POWER:
                obj = Instantiate(switch_vehicle_power_prefab);
                break;
            case SWITCH_H2O_OFF:
                obj = Instantiate(switch_h2o_off_prefab);
                break;
            case SWITCH_O2_OFF:
                obj = Instantiate(switch_o2_off_prefab);
                break;
            //--------------------------------------------------- 
            default:
                obj = Instantiate(switch_empty_prefab);
                break;
        }
        obj.name = switchName;
        obj.transform.SetParent(parent);
        orgPos = obj.transform.localPosition;
        obj.transform.localPosition = new Vector3(orgPos.x, orgPos.y, 0);
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.transform.localRotation = Quaternion.identity;
        return obj;
    }

    public void Pin()
    {
        Vector3 position = transform.position;
        transform.SetParent(worldSpace.transform);
        transform.position = mainCamera.transform.position + mainCamera.transform.forward * 2;
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position, mainCamera.transform.up);
        Debug.Log("Pin Telemetry to position (" + transform.position.x + ", " + transform.position.y + ", " + transform.position.z + ")");
        //float rotate_z = transform.eulerAngles.z;
        //transform.Rotate(Vector3.forward, Camera.main.transform.eulerAngles.z - rotate_z);
        //Debug.Log(Camera.main.transform.eulerAngles.z - rotate_z);
        //gameObject.GetComponent<LookAtCamera>().enabled = true;
        PlaySoundEffect(SOUND_EFFECT_COMPLETE, false);
    }

    public void UnPin()
    {
        Debug.Log("Unpin Telemetry");
        transform.SetParent(evaSphereSpace.transform);
        transform.localPosition = new Vector3(1.2f, 0.125f, 1.85f);
        //gameObject.GetComponent<LookAtCamera>().enabled = false;
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
        PlaySoundEffect(SOUND_EFFECT_COMPLETE, false);
    }

    public void ChangeLanguage(int langIdx)
    {
        if (langIdx == 1)
        {
            Debug.Log("Change Language to English");
            language = TelemetryLanguageController.ENG;
            ChangeAllIconNames();
        }
        else if (langIdx == 2)
        {
            Debug.Log("Change Language to Vietnamese");
            language = TelemetryLanguageController.VN;
            ChangeAllIconNames();
        }
        PlaySoundEffect(SOUND_EFFECT_COMPLETE, false);
    }

    private void ChangeAllIconNames()
    {
        Debug.Log("Change all icons names by language " + language);

        if (heart_rate_obj != null)
            heart_rate_obj.GetComponent<HeartRate>().ChangeIconName(GetIconName(HEART_RATE));

        if (internal_suit_pressure_obj != null)
            internal_suit_pressure_obj.GetComponent<InternalSuitPressure>().ChangeIconName(GetIconName(INTERNAL_SUIT_PRESSURE));

        if (external_environment_temperature_obj != null)
            external_environment_temperature_obj.GetComponent<ExternalEnvironmentTemperature>().ChangeIconName(GetIconName(EXTERNAL_ENVIRONMENT_TEMPERATURE));
        if (external_environment_pressure_obj != null)
            external_environment_pressure_obj.GetComponent<ExternalEnvironmentPressure>().ChangeIconName(GetIconName(EXTERNAL_ENVIRONMENT_PRESSURE));

        if (secondary_oxygen_pressure_obj != null)
            secondary_oxygen_pressure_obj.GetComponent<SecondaryOxygenPressure>().ChangeIconName(GetIconName(SECONDARY_OXYGEN_PRESSURE));
        if (secondary_oxygen_rate_obj != null)
            secondary_oxygen_rate_obj.GetComponent<SecondaryOxygenRate>().ChangeIconName(GetIconName(SECONDARY_OXYGEN_RATE));

        if (battery_time_life_obj != null)
            battery_time_life_obj.GetComponent<BatteryTimeLife>().ChangeIconName(GetIconName(BATTERY_TIME_LIFE));
        if (battery_capacity_obj != null)
            battery_capacity_obj.GetComponent<BatteryCapacity>().ChangeIconName(GetIconName(BATTERY_CAPACITY));

        if (oxygen_rate_obj != null)
            oxygen_rate_obj.GetComponent<OxygenRate>().ChangeIconName(GetIconName(OXYGEN_RATE));
        if (oxygen_time_life_obj != null)
            oxygen_time_life_obj.GetComponent<OxygenTimeLife>().ChangeIconName(GetIconName(OXYGEN_TIME_LIFE));
        if (oxygen_pressure_obj != null)
            oxygen_pressure_obj.GetComponent<OxygenPressure>().ChangeIconName(GetIconName(OXYGEN_PRESSURE));


        if (H2O_gas_pressure_obj != null)
            H2O_gas_pressure_obj.GetComponent<H2OGasPressure>().ChangeIconName(GetIconName(H2O_GAS_PRESSURE));
        if (H2O_liquid_pressure_obj != null)
            H2O_liquid_pressure_obj.GetComponent<H2OLiquidPressure>().ChangeIconName(GetIconName(H2O_LIQUID_PRESSURE));

        if (fan_speed_obj != null)
            fan_speed_obj.GetComponent<FanSpeed>().ChangeIconName(GetIconName(FAN_SPEED));
        if (water_time_life_obj != null)
            water_time_life_obj.GetComponent<WaterTimeLife>().ChangeIconName(GetIconName(WATER_TIME_LIFE));

    }

    private string GetIconName(string telemetryDataname)
    {
        string icon_name = "";
        if (language.Equals(TelemetryLanguageController.ENG))
            TelemetryLanguageController.eng_icon_names.TryGetValue(telemetryDataname, out icon_name);
        else if (language.Equals(TelemetryLanguageController.VN))
            TelemetryLanguageController.vn_icon_names.TryGetValue(telemetryDataname, out icon_name);
        //Debug.Log(icon_name);
        return icon_name;
    }

    IEnumerator WaitAndChangeAllIconNames(float sec)
    {
        yield return new WaitForSeconds(sec);
        ChangeAllIconNames();
        ChangeContrastAll(theme_idx);
    }

    public void ChangeContrast(int _theme_idx)
    {
        theme_idx = _theme_idx;
        ChangeContrastAll(theme_idx);
        PlaySoundEffect(SOUND_EFFECT_COMPLETE, false);
    }

    private void ChangeContrastAll(int theme_idx)
    {
        if (theme_idx == THEME_LIGHT)
        {
            Debug.Log("Change Contrast to Light Mode");
        }
        else if (theme_idx == THEME_DARK)
        {
            Debug.Log("Change Contrast to Dark Mode");
        }

        if (heart_rate_obj != null)
            heart_rate_obj.GetComponent<HeartRate>().ChangeContrastMode(theme_idx);

        if (internal_suit_pressure_obj != null)
            internal_suit_pressure_obj.GetComponent<InternalSuitPressure>().ChangeContrastMode(theme_idx);

        if (external_environment_temperature_obj != null)
            external_environment_temperature_obj.GetComponent<ExternalEnvironmentTemperature>().ChangeContrastMode(theme_idx);
        if (external_environment_pressure_obj != null)
            external_environment_pressure_obj.GetComponent<ExternalEnvironmentPressure>().ChangeContrastMode(theme_idx);

        if (secondary_oxygen_pressure_obj != null)
            secondary_oxygen_pressure_obj.GetComponent<SecondaryOxygenPressure>().ChangeContrastMode(theme_idx);
        if (secondary_oxygen_rate_obj != null)
            secondary_oxygen_rate_obj.GetComponent<SecondaryOxygenRate>().ChangeContrastMode(theme_idx);

        if (battery_time_life_obj != null)
            battery_time_life_obj.GetComponent<BatteryTimeLife>().ChangeContrastMode(theme_idx);
        if (battery_capacity_obj != null)
            battery_capacity_obj.GetComponent<BatteryCapacity>().ChangeContrastMode(theme_idx);

        if (oxygen_rate_obj != null)
            oxygen_rate_obj.GetComponent<OxygenRate>().ChangeContrastMode(theme_idx);
        if (oxygen_time_life_obj != null)
            oxygen_time_life_obj.GetComponent<OxygenTimeLife>().ChangeContrastMode(theme_idx);
        if (oxygen_pressure_obj != null)
            oxygen_pressure_obj.GetComponent<OxygenPressure>().ChangeContrastMode(theme_idx);


        if (H2O_gas_pressure_obj != null)
            H2O_gas_pressure_obj.GetComponent<H2OGasPressure>().ChangeContrastMode(theme_idx);
        if (H2O_liquid_pressure_obj != null)
            H2O_liquid_pressure_obj.GetComponent<H2OLiquidPressure>().ChangeContrastMode(theme_idx);

        if (fan_speed_obj != null)
            fan_speed_obj.GetComponent<FanSpeed>().ChangeContrastMode(theme_idx);
        if (water_time_life_obj != null)
            water_time_life_obj.GetComponent<WaterTimeLife>().ChangeContrastMode(theme_idx);

    }

    private void PlaySoundEffect(string sound_file, bool loop)
    {
        AudioSource audioSource = sound_effect.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.loop = loop;
            audioSource.clip = Resources.Load(sound_file) as AudioClip;
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
    }

    private void StopSoundEffect()
    {
        AudioSource audioSource = sound_effect.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
    }
}
