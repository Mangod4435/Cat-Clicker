using System;
using UnityEngine;

public static class SaveSystem
{
    private const string KEY = "CatClickerSave";
    private const string HASH_KEY = "SaveHash";
    public static event Action OnNoSave;

    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        string hash = MangodHasher.Hash(json);
        PlayerPrefs.SetString(HASH_KEY, hash);
        PlayerPrefs.SetString(KEY, json);
        PlayerPrefs.Save();

#if UNITY_EDITOR
        Debug.Log($"[SaveSysytem.cs] Saved hash: {hash}");
#endif

        Debug.Log($"[SaveSystem.cs] Saved: {json}");
    }

    public static SaveData Load()
    {
        if (!HasSave())
        {
            Debug.Log("[SaveSystem.cs] No save found");
            OnNoSave?.Invoke();
            return new SaveData
            {
                cpc = 1,
                cats = 0,
                upgrades = new UpgradesData { SharpClaw = 0 },
            };
        }

        string json = PlayerPrefs.GetString(KEY);

        //protect the fake injected save
        if (MangodHasher.Hash(json) != PlayerPrefs.GetString(HASH_KEY))
        {
            return new SaveData
            {
                cpc = 1,
                cats = 0,
                upgrades = new UpgradesData { SharpClaw = 0 },
            };
        }

        SaveData data = JsonUtility.FromJson<SaveData>(json);
        Debug.Log($"[SaveSystem.cs] Loaded: {json}");
        return data;
    }

    public static void DESTROYtheSave()
    {
        PlayerPrefs.DeleteKey(KEY);
        PlayerPrefs.DeleteKey(HASH_KEY);
        PlayerPrefs.Save();
        Debug.Log("[SaveSystem.cs] Save destroyed");
    }

    public static bool HasSave() => PlayerPrefs.HasKey(KEY);
}
