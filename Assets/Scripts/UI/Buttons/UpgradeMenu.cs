using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField]
    GameObject menu;

    [SerializeField]
    GameObject Cat;

    void Update()
    {
        if (menu.activeInHierarchy)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-1010, -50);
            Cat.GetComponent<RectTransform>().anchoredPosition = new Vector3(-410, 0);
        }
        else
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-50, -50);
            Cat.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0);
        }
    }

    public void OnClicked() => menu.SetActive(!menu.activeInHierarchy);
}
