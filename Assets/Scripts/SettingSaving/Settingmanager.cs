using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager instance;
    public bool Sound { get; private set; }

    void Awake()
    {
        instance = this;
        Load();
    }

    // save - load
    public void Save()
    {
        var data = new SettingData { isSoundOpen = Sound };
        SaveSettingSystem.Save(data);
    }

    public void Load()
    {
        SettingData data = SaveSettingSystem.Load();
        this.Sound = data.isSoundOpen;
    }

    // API
    public void SetSound(bool b)
    {
        Sound = b;
    }
}
