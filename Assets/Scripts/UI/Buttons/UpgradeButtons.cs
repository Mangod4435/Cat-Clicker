using API;
using UnityEngine;

namespace UI.Buttons
{
    public class UpgradeButtons : MonoBehaviour
    {
        [SerializeField]
        string upgradeName;

        public enum ButtonState
        {
            Available,
            Shadow,
            Unrevealed,
        }

        ButtonState state = ButtonState.Unrevealed;
        private GameManager manager => GameManager.Instance;

        void Update()
        {
            if (manager.Cats > UpgradeAPI.getPrice(upgradeName))
            {
                state = ButtonState.Available;
                Debug.Log($"State = available on {upgradeName}");
            }
            if (manager.Cats < UpgradeAPI.getPrice(upgradeName))
            {
                state = ButtonState.Unrevealed;
                Debug.Log($"State = unrevealed on {upgradeName}");
            }

            if (state == ButtonState.Available)
            {
                gameObject.SetActive(true);
                Debug.Log($"Set active to true on {upgradeName}");
            }
            if (state == ButtonState.Shadow || state == ButtonState.Unrevealed)
            {
                gameObject.SetActive(false);
                Debug.Log($"Set active to false on {upgradeName}");
            }
        }

        private bool IsAffordable(string upgradeName)
        {
            switch (upgradeName)
            {
                case "Sharp Claw":
                    if (manager.Cats >= UpgradeAPI.PriceCalculator(100, manager.SharpClaw))
                        return true;
                    break;
                case "Cozy Spot":
                    if (manager.Cats >= UpgradeAPI.PriceCalculator(20, manager.CozySpot))
                        return true;
                    break;
                case "Fish Bowl":
                    if (manager.Cats >= UpgradeAPI.PriceCalculator(100, manager.FishBowl))
                        return true;
                    break;
                case "TV":
                    if (manager.Cats >= UpgradeAPI.PriceCalculator(1_000, manager.TV))
                        return true;
                    break;
                case "Laser":
                    if (manager.Cats >= UpgradeAPI.PriceCalculator(50_000, manager.Laser))
                        return true;
                    break;
                case "Factory":
                    if (manager.Cats >= UpgradeAPI.PriceCalculator(10_000_000, manager.Factory))
                        return true;
                    break;
                case "Satellite":
                    if (
                        manager.Cats >= UpgradeAPI.PriceCalculator(5_000_000_000, manager.Satellite)
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
            manager.AddCat(
                -UpgradeAPI.PriceCalculator(
                    UpgradeAPI.getPrice(upgradeName),
                    UpgradeAPI.getAmount(upgradeName)
                )
            );
            manager.AddUpgrade(upgradeName, 1);
        }
    }
}
