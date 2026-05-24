using UnityEngine;

public static class SaveSystem
{
    private const string KEY = "CatClickerSave";

    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(KEY, json);
        PlayerPrefs.Save();

        Debug.Log($"[SaveSystem.cs] Saved: {json}");
    }

    public static SaveData Load()
    {
        if (!HasSave())
        {
            Debug.Log("[SaveSystem.cs] No save found");
            return new SaveData { cats = 0 };
        }

        string json = PlayerPrefs.GetString(KEY);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        Debug.Log($"[SaveSystem.cs] Loaded: {json}");
        return data;
    }

    public static void DESTROYtheSave()
    {
        PlayerPrefs.DeleteKey(KEY);
        PlayerPrefs.Save();
        Debug.Log("[SaveSystem.cs] Save destroyed");
    }

    public static bool HasSave() => PlayerPrefs.HasKey(KEY);
}
