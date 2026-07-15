public class SettingData
{
    public SettingData(bool sound = true, bool notation = false)
    {
        isSoundOpen = sound;
        scientificNotation = notation;
    }

    public bool isSoundOpen;
    public bool scientificNotation;
}
