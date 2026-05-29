using UnityEngine;

public class ResetButton : MonoBehaviour
{
    GameManager manager => GameManager.instance;
    GameObject cat;
    SettingButton settingButton => SettingButton.instance;

    void Awake()
    {
        cat = transform.parent.parent.Find("Cat").gameObject;
    }

    public void OnClicked()
    {
        manager.ResetGame();
        cat.transform.rotation = Quaternion.Euler(0, 0, 0);
        settingButton.isSettingOpen = false;
        settingButton.UpdateSetting();
    }
}
