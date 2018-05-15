using System.Collections;
using System.Collections.Generic;


public class TelemetryLanguageController
{
    public const string ENG = "english";
    public const string VN = "vietnamese";
    public static Dictionary<string, string> eng_icon_names = new Dictionary<string, string>()
    {
        { TelemetryController.HEART_RATE, "Heart Rate" },
        { TelemetryController.INTERNAL_SUIT_PRESSURE, "Suit\nPressure" },
        { TelemetryController.EXTERNAL_ENVIRONMENT_PRESSURE, "External\nPressure" },
        { TelemetryController.EXTERNAL_ENVIRONMENT_TEMPERATURE, "External\nTemperature" },
        { TelemetryController.BATTERY_TIME_LIFE, "Time Life\nBattery" },
        { TelemetryController.BATTERY_CAPACITY, "Battery\nCapacity" },
        { TelemetryController.OXYGEN_TIME_LIFE, "Time Life\nOxygen" },
        { TelemetryController.OXYGEN_PRESSURE, "Oxygen\nPressure" },
        { TelemetryController.OXYGEN_RATE, "Oxygen\nRate" },
        { TelemetryController.SECONDARY_OXYGEN_PRESSURE, "Second O2\nPressure" },
        { TelemetryController.SECONDARY_OXYGEN_RATE, "Second O2\nRate" },
        { TelemetryController.H2O_GAS_PRESSURE, "H2O Gas\nPressure" },
        { TelemetryController.H2O_LIQUID_PRESSURE, "H2O Liquid\nPressure" },
        { TelemetryController.FAN_SPEED, "Fan\nSpeed" },
        { TelemetryController.WATER_TIME_LIFE, "Time Life\nWater" }
    };

    public static Dictionary<string, string> vn_icon_names = new Dictionary<string, string>()
    {
        { TelemetryController.HEART_RATE, "Nhịp Tim" },
        { TelemetryController.INTERNAL_SUIT_PRESSURE, "Áp Suất\nTrong" },
        { TelemetryController.EXTERNAL_ENVIRONMENT_PRESSURE, "Áp Suất\nNgoài" },
        { TelemetryController.EXTERNAL_ENVIRONMENT_TEMPERATURE, "Nhiệt Độ\nNgoài" },
        { TelemetryController.BATTERY_TIME_LIFE, "Thời Gian\nPin" },
        { TelemetryController.BATTERY_CAPACITY, "Dung Lượng\nPin" },
        { TelemetryController.OXYGEN_TIME_LIFE, "Thời Gian\nOxy" },
        { TelemetryController.OXYGEN_PRESSURE, "Áp Suất\nOxy" },
        { TelemetryController.OXYGEN_RATE, "Tốc Độ\nOxy" },
        { TelemetryController.SECONDARY_OXYGEN_PRESSURE, "Áp Suất\nOxy Phụ" },
        { TelemetryController.SECONDARY_OXYGEN_RATE, "Tốc Độ\nOxy Phụ" },
        { TelemetryController.H2O_GAS_PRESSURE, "Áp Suất\nH2O Khí" },
        { TelemetryController.H2O_LIQUID_PRESSURE, "Áp Suất\nH2O Lỏng" },
        { TelemetryController.FAN_SPEED, "Tốc Độ\nQuạt" },
        { TelemetryController.WATER_TIME_LIFE, "Thời Gian\nNước" }
    };
}
