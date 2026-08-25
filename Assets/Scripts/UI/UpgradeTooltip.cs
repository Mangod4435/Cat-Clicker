using System;
using System.Linq;
using API;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeEffectTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler // this class uses with the button
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
		title.text = TooltipTextIndexes.indexes["Sharp Claw"].name;
		description.text = TooltipTextIndexes.indexes["Sharp Claw"].description;
		cost.text = UpgradeAPI.getCalculatedPrice(ParseEnum<UNIVERSALButton.UpgradeType>(TooltipTextIndexes.indexes["Sharper Claw"].technicalName)).ToString();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		throw new System.NotImplementedException();
	}

	T ParseEnum<T>(String target) => (T)Enum.Parse(typeof(T), target);
}
