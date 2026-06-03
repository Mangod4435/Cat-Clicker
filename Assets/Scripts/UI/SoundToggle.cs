using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    SettingManager manager => SettingManager.instance;

    void Awake()
    {
        gameObject.GetComponent<Toggle>().isOn = manager.Sound;
    }
}
