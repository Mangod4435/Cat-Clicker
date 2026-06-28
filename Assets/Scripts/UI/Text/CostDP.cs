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
            case "Fish Bowl":
                text.text =
                    NumberFormatter.FormatDouble(UpgradeAPI.PriceCalculator(100, manager.FishBowl))
                    + " Cats";
                break;
            case "TV":
                text.text =
                    NumberFormatter.FormatDouble(UpgradeAPI.PriceCalculator(1_000, manager.TV))
                    + " Cats";
                break;
            case "Laser":
                text.text =
                    NumberFormatter.FormatDouble(UpgradeAPI.PriceCalculator(50_000, manager.Laser))
                    + " Cats";
                break;
            case "Factory":
                text.text =
                    NumberFormatter.FormatDouble(
                        UpgradeAPI.PriceCalculator(10_000_000, manager.Factory)
                    ) + " Cats";
                break;
            case "Satellite":
                text.text =
                    NumberFormatter.FormatDouble(
                        UpgradeAPI.PriceCalculator(5_000_000_000, manager.Satellite)
                    ) + " Cats";
                break;
            default:
                Debug.LogError($"There's no such upgrade named \"{UpgradeName}\"");
                break;
        }
    }

    void Awake() => text = GetComponent<TextMeshProUGUI>();

    void Update() => DPCost();
}
