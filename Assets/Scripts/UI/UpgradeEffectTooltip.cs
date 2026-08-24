using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeEffectTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler // this class 
{
	[SerializeField] GameObject panel;
	TextMeshProUGUI title;
	TextMeshProUGUI description;
	TextMeshProUGUI cost;

	void Awake()
	{
		title = panel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
		description = panel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
		cost = panel.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		throw new System.NotImplementedException();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		throw new System.NotImplementedException();
	}
}
