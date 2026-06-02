using UnityEngine;

public class ResetButton : MonoBehaviour
{
    GameManager manager => GameManager.instance;
    [SerializeField]
    GameObject cat;
    SettingButton settingButton => SettingButton.instance;

    public void OnClicked()
    {
        manager.ResetGame();
        cat.transform.rotation = Quaternion.Euler(0, 0, 0);
        settingButton.isSettingOpen = false;
        settingButton.UpdateSetting();
    }
}
