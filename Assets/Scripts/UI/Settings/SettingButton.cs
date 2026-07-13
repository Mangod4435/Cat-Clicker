using UnityEngine;

public class SettingButton : MonoBehaviour
{
    public bool isSettingOpen;

    [SerializeField]
    GameObject SettingUI;
    public static SettingButton instance;

    void Update()
    {
        bool isOpen = UIState.state == UIState.OpenedInterface.Setting;
        transform.rotation = isOpen ? Quaternion.Euler(0, 0, -45) : Quaternion.Euler(0, 0, -15);
    }

    public void OnClicked()
    {
        UIState.state =
            UIState.state == UIState.OpenedInterface.Setting
                ? UIState.OpenedInterface.None
                : UIState.OpenedInterface.Setting;
    }
}
