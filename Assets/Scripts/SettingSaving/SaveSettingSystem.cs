using System;
using UnityEngine;

public static class SaveSettingSystem
{
    private const string KEY = "pref";

    public static void Save(SettingData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(KEY, json);
        PlayerPrefs.Save();

        Debug.Log($"[SaveSettingSystem.cs] Saved : {json}");
    }

    public static SettingData Load()
    {
        if (!HasSave())
        {
            Debug.Log("[SaveSettingSystem.cs] No preference found");
            return new SettingData { isSoundOpen = false };
        }

        string json = PlayerPrefs.GetString(KEY);
        SettingData data = JsonUtility.FromJson<SettingData>(json);
        Debug.Log($"[SaveSettingSystem.cs] Loaded: {json}");
        return data;
    }

    private static bool HasSave() => PlayerPrefs.HasKey(KEY);
}
