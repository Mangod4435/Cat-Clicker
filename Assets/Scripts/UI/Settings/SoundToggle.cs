using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    SettingManager manager => SettingManager.instance;

    void Start() => gameObject.GetComponent<Toggle>().isOn = manager.Sound;
}
