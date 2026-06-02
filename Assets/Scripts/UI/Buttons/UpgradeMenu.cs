using UnityEditor;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField]
    GameObject menu;

    public void OnClicked() => menu.SetActive(!menu.activeInHierarchy);
}
