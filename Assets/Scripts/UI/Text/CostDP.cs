using API;
using TMPro;
using UnityEngine;

public class CostDP : MonoBehaviour
{
    [SerializeField]
    string UpgradeName;

    GameManager manager => GameManager.Instance;
    TextMeshProUGUI text;

    void DPCost()
    {
        switch (UpgradeName)
        {
            case "Sharp Claw":
                text.text =
                    NumberFormatter.FormatDouble(UpgradeAPI.PriceCalculator(100, manager.SharpClaw))
                    + " Cats";
                break;
            case "Cozy Spot":
                text.text =
                    NumberFormatter.FormatDouble(UpgradeAPI.PriceCalculator(20, manager.CozySpot))
                    + " Cats";
                break;
            default:
                Debug.LogError($"There's no such upgrade named \"{UpgradeName}\"");
                break;
        }
    }

    void Awake() => text = GetComponent<TextMeshProUGUI>();

    void Update() => DPCost();
}
