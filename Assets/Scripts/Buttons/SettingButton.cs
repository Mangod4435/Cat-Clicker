using UnityEngine;

public class SettingButton : MonoBehaviour
{
    public static bool isSettingOpen = false;
    GameObject SettingUI;

    void Awake()
    {
        SettingUI = transform.parent.Find("SettingUI").gameObject;
        isSettingOpen = false;
        SettingUI.SetActive(false);
        transform.rotation = Quaternion.Euler(0, 0, -15);
    }

    public void OnClicked()
    {
        isSettingOpen = !isSettingOpen;
        SettingUI.SetActive(isSettingOpen);
        transform.rotation = isSettingOpen
            ? Quaternion.Euler(0, 0, -45)
            : Quaternion.Euler(0, 0, -15);
    }
}
