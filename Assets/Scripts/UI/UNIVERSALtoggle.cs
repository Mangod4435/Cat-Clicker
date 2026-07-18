using UnityEngine;
using UnityEngine.UI;

public class UNIVERSALToggle : MonoBehaviour
{
    SettingManager manager => SettingManager.instance;

    [SerializeField]
    string toggleName;

    void Start()
    {
        Toggle t = gameObject.GetComponent<Toggle>();
        switch (toggleName)
        {
            case "sound":
                t.isOn = manager.Sound;
                break;
            case "notation":
                t.isOn = manager.Notation;
                break;
            default:
                Debug.LogError($"No Toggle named {toggleName}");
                break;
        }
    }
}
