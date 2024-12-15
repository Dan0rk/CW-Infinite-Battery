using HarmonyLib;
using Unity.Mathematics;
using UnityEngine;
using Zorro.Settings;
using System.Reflection;

// This Mod was made with the help of Landfalls ExampleCWPlugin. Thanks
// https://github.com/landfallgames/ExampleCWPlugin

namespace InfiniteBattery;

[ContentWarningPlugin("InfiniteBattery_by_Dan0rk", "1.1", false)]

[HarmonyPatch(typeof(BatteryEntry))]
[HarmonyPatch("GetPercentage")]
public class Patch
{
    
    static EnableMod? enableMod;

    static void Postfix(BatteryEntry __instance)
    {
        enableMod ??= GameHandler.Instance.SettingsHandler.GetSetting<EnableMod>();
        if(enableMod.Value == true)
        {
            __instance.m_charge = __instance.m_maxCharge;
        }
    }
}

[ContentWarningSetting]
public class EnableMod : BoolSetting, IExposedSetting {
    public override void ApplyValue() => Debug.Log($"Infinite Battery setting toggled to {(Value ? "Enabled" : "Disabled")}");

    protected override bool GetDefaultValue() => true;

    public SettingCategory GetSettingCategory() => SettingCategory.Mods;

    public string GetDisplayName() => "Enable Infinite Battery Mod";
}