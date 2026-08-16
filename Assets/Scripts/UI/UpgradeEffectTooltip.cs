using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeEffectTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject panel;
    [SerializeField] Text title;
    [SerializeField] Text description;
    [SerializeField] Text cost;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // panel.transform.GetChild()
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
