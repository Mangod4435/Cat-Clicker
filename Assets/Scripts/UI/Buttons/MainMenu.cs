using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject setting;

    bool settingOpenning;

    public void StartGame() => SceneManager.LoadScene(1);

    public void openSetting()
    {
        setting.SetActive(!settingOpenning);
        settingOpenning = !settingOpenning;
    }

    public void quitGame() => Application.Quit();
}
