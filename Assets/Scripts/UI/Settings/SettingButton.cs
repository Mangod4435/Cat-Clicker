using UnityEngine;

public class SettingButton : MonoBehaviour
{
    public bool isSettingOpen;

    [SerializeField]
    GameObject SettingUI;
    public static SettingButton instance;

    void Awake() => transform.rotation = Quaternion.Euler(0, 0, -45);

    public void OnClicked()
    {
        UIState.state =
            UIState.state == UIState.OpenedInterface.Setting
                ? UIState.OpenedInterface.None
                : UIState.OpenedInterface.Setting;
    }
}
