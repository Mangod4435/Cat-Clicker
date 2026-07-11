using UnityEngine;

public class ResetButton : MonoBehaviour
{
    GameManager manager => GameManager.Instance;

    [SerializeField]
    GameObject cat;
    SettingButton settingButton => SettingButton.instance;

    public void OnClicked()
    {
        manager.ResetGame();
        cat.transform.rotation = Quaternion.Euler(0, 0, 0);
        UIState.state = UIState.OpenedInterface.None;
    }
}
