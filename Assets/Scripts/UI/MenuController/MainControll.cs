using Unity.VisualScripting;
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
        upgradesMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    void Update()
    {
        if (state == UIState.OpenedInterface.Setting)
            show("setting");
        else if (state == UIState.OpenedInterface.Upgrade)
            show("upgrade");
        else
            show("null");
    }

    private void show(string input)
    {
        if (input == "null")
        {
            settingsMenu.SetActive(false);
            settingsMenu.SetActive(false);
        }
        settingsMenu.SetActive(input == "setting");
        upgradesMenu.SetActive(input == "upgrade");
    }
}
