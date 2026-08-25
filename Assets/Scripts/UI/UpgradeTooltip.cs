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
		title.text = TooltipTexts.indexes["Sharp Claw"].name;
		description.text = TooltipTexts.indexes["Sharp Claw"].description;
		cost.text = UpgradeAPI.getCalcedPrice(
			ParseEnum<UNIVERSALButton.UpgradeType>(TooltipTexts.indexes["Sharper Claw"].technicalName)
		).ToString();

		GetComponent<CanvasGroup>().alpha = 1;
	}

	public void OnPointerExit(PointerEventData eventData) => GetComponent<CanvasGroup>().alpha = 0;

	T ParseEnum<T>(string target) => (T)System.Enum.Parse(typeof(T), target);
}
