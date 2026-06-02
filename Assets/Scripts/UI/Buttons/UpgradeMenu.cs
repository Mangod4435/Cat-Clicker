using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField]
    GameObject menu;

    void Update()
    {
        if (menu.activeInHierarchy)
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-950, -150);
        else
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-150, -150);
    }

    public void OnClicked() => menu.SetActive(!menu.activeInHierarchy);
}
