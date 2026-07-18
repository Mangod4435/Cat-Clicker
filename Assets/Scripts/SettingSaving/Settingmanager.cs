using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager instance;
    public bool Sound { get; private set; }
    public bool Notation { get; private set; }

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
        Sound = data.isSoundOpen;
        Notation = data.scientificNotation;
    }

    // API
    public void SetSound(bool b) => Sound = b;

    public void SetNotation(bool b) => Notation = b;
}
