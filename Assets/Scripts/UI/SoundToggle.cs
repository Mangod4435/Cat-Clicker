using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    SettingManager manager => SettingManager.instance;

    void Awake()
    {
        this.gameObject.GetComponent<Toggle>().isOn = manager.Sound;
    }
}
