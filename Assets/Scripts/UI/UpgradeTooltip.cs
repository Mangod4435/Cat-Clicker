using API;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeEffectTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler // this class uses with the button
{
	[SerializeField] Transform panel;
	TextMeshProUGUI title;
	TextMeshProUGUI description;
	TextMeshProUGUI cost;

	void Awake()
	{
		title = panel.GetChild(0).GetComponent<TextMeshProUGUI>();
		description = panel.GetChild(1).GetComponent<TextMeshProUGUI>();
		cost = panel.GetChild(2).GetComponent<TextMeshProUGUI>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		title.text = TooltipTexts.indexes[GetComponent<UNIVERSALButton>().upgradeName.ToString()].name;
		description.text = TooltipTexts.indexes[GetComponent<UNIVERSALButton>().upgradeName.ToString()].description;
		cost.text = UpgradeAPI.getCalcedPrice(
			ParseEnum<UNIVERSALButton.UpgradeType>(TooltipTexts.indexes[GetComponent<UNIVERSALButton>().upgradeName.ToString()].technicalName)
		).ToString() + " cats";

		panel.GetComponent<CanvasGroup>().alpha = 1;
	}

	public void OnPointerExit(PointerEventData eventData) => panel.GetComponent<CanvasGroup>().alpha = 0;

	T ParseEnum<T>(string target) => (T)System.Enum.Parse(typeof(T), target);
}
