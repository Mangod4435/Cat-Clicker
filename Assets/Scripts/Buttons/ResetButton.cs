using UnityEngine;

public class ResetButton : MonoBehaviour
{
    GameManager manager => GameManager.instance;
    GameObject cat;

    void Awake()
    {
        cat = transform.parent.parent.Find("Cat").gameObject;
    }

    public void OnClicked()
    {
        manager.ResetGame();
        Debug.Log($"[ResetButton.cs] isSettingOpen = {SettingButton.isSettingOpen}");
        cat.transform.rotation = Quaternion.Euler(0, 0, 0);
        SettingButton.isSettingOpen = false;
    }
}
