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
        UIState.state = UIState.OpenedInterface.Upgrade;
        upgradesMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    void Update()
    {
        settingsMenu.SetActive(state == UIState.OpenedInterface.Setting);
        upgradesMenu.SetActive(state == UIState.OpenedInterface.Upgrade);
    }
}
