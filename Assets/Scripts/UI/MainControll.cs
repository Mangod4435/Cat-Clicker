using UnityEngine;

public class MainControl : MonoBehaviour
{
    [SerializeField]
    GameObject upgradesMenu;

    [SerializeField]
    GameObject settingsMenu;

    UIState.OpenedInterface state => UIState.state;

    void Start()
    {
        UIState.state = UIState.OpenedInterface.Upgrade;
        if (upgradesMenu != null)
            upgradesMenu?.SetActive(false);
        if (settingsMenu != null)
            settingsMenu?.SetActive(false);
    }

    void Update()
    {
        if (settingsMenu != null)
            settingsMenu.SetActive(state == UIState.OpenedInterface.Setting);
        if (upgradesMenu != null)
            upgradesMenu.SetActive(state == UIState.OpenedInterface.Upgrade);
    }
}
