using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField]
    GameObject menu;

    [SerializeField]
    GameObject Cat;

    void Update()
    {
        bool isOpen = UIState.state == UIState.OpenedInterface.Upgrade;
        gameObject.GetComponent<RectTransform>().anchoredPosition = isOpen
            ? new Vector3(-1010, -50)
            : new Vector3(-50, -50);
        Cat.GetComponent<RectTransform>().anchoredPosition = isOpen
            ? new Vector3(-410, 0)
            : new Vector3(0, 0);
    }

    public void OnClicked()
    {
        UIState.state =
            UIState.state == UIState.OpenedInterface.Upgrade
                ? UIState.OpenedInterface.None
                : UIState.OpenedInterface.Upgrade;
    }
}
