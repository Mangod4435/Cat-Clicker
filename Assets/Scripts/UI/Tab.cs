using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
    [SerializeField]
    int FrameIndex;

    [SerializeField]
    GameObject FrameObject;

    static class TabData
    {
        internal static int TabOpenedIndex = 0;
    }

    void OnEnable() => gameObject.GetComponent<Button>().onClick.AddListener(OnClicked);

    void OnDisable() => gameObject.GetComponent<Button>().onClick.RemoveListener(OnClicked);

    void Update()
    {
        if (FrameIndex == TabData.TabOpenedIndex)
        {
            FrameObject.SetActive(true);
            GetComponent<Image>().color = new Color32(100, 100, 100, 255);
        }
        else
        {
            FrameObject.SetActive(false);
            GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    void OnClicked() => TabData.TabOpenedIndex = FrameIndex;
}
