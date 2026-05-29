using UnityEngine;

public class SettingButton : MonoBehaviour
{
    public bool isSettingOpen;
    GameObject SettingUI;
    public static SettingButton instance;

    void Awake()
    {
        instance = this;
        SettingUI = transform.parent.Find("SettingUI").gameObject;
        //Setup first call
        isSettingOpen = false;
        SettingUI.SetActive(false);
        transform.rotation = Quaternion.Euler(0, 0, -15);
    }

    public void OnClicked()
    {
        isSettingOpen = !isSettingOpen;
        UpdateSetting();
    }

    public void UpdateSetting()
    {
        SettingUI.SetActive(isSettingOpen);
        transform.rotation = isSettingOpen
            ? Quaternion.Euler(0, 0, -45)
            : Quaternion.Euler(0, 0, -15);
    }
}
