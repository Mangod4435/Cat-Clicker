using API;
using UnityEngine;

namespace UI.Buttons
{
    public class UpgradeButtons : MonoBehaviour
    {
        [SerializeField]
        string upgradeName;

        private GameManager Instance => GameManager.Instance;

        private bool IsAffordable(string upgradeName)
        {
            switch (upgradeName)
            {
                case "Sharp Claw":
                    if (Instance.Cats >= UpgradeAPI.PriceCalculator(100, Instance.SharpClaw))
                        return true;
                    break;
                case "Cozy Spot":
                    if (Instance.Cats >= UpgradeAPI.PriceCalculator(20, Instance.CozySpot))
                        return true;
                    break;
                case "Fish Bowl":
                    if (Instance.Cats >= UpgradeAPI.PriceCalculator(100, Instance.FishBowl))
                        return true;
                    break;
                case "TV":
                    if (Instance.Cats >= UpgradeAPI.PriceCalculator(1_000, Instance.TV))
                        return true;
                    break;
                case "Laser":
                    if (Instance.Cats >= UpgradeAPI.PriceCalculator(50_000, Instance.Laser))
                        return true;
                    break;
                case "Factory":
                    if (Instance.Cats >= UpgradeAPI.PriceCalculator(10_000_000, Instance.Factory))
                        return true;
                    break;
                case "Satellite":
                    if (
                        Instance.Cats
                        >= UpgradeAPI.PriceCalculator(5_000_000_000, Instance.Satellite)
                    )
                        return true;
                    break;
            }
            return false;
        }

        public void OnClicked()
        {
            if (!IsAffordable(upgradeName))
                return;
            switch (upgradeName)
            {
                case "Sharp Claw":
                    Instance.AddCat(-UpgradeAPI.PriceCalculator(100, Instance.SharpClaw));
                    break;
                case "Cozy Spot":
                    Instance.AddCat(-UpgradeAPI.PriceCalculator(20, Instance.CozySpot));
                    break;
                case "Fish Bowl":
                    Instance.AddCat(-UpgradeAPI.PriceCalculator(100, Instance.FishBowl));
                    break;
                case "TV":
                    Instance.AddCat(-UpgradeAPI.PriceCalculator(1_000, Instance.TV));
                    break;
                case "Laser":
                    Instance.AddCat(-UpgradeAPI.PriceCalculator(50_000, Instance.Laser));
                    break;
                case "Factory":
                    Instance.AddCat(-UpgradeAPI.PriceCalculator(10_000_000, Instance.Factory));
                    break;
                case "Satellite":
                    Instance.AddCat(-UpgradeAPI.PriceCalculator(5_000_000_000, Instance.Satellite));
                    break;

                default:
                    break;
            }
            Instance.AddUpgrade(upgradeName, 1);
        }
    }
}
