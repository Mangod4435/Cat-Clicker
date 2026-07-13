using API;
using UnityEngine;

namespace UI.Buttons
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UpgradeButtons : MonoBehaviour
    {
        [SerializeField]
        string upgradeName;

        [SerializeField]
        int index;

        CanvasGroup cg;

        public enum ButtonState
        {
            Available,
            NotAffordable,
            Shadow,
            Unrevealed,
        }

        ButtonState state = ButtonState.Unrevealed;
        private GameManager manager => GameManager.Instance;

        void Awake() => cg = gameObject.GetComponent<CanvasGroup>();

        void Update()
        {
            if (manager.Cats >= UpgradeAPI.getPrice(upgradeName))
                state = ButtonState.Available;
            else if (manager.Cats < UpgradeAPI.getPrice(upgradeName))
                state = ButtonState.Unrevealed;

            if (state == ButtonState.Unrevealed)
            {
                cg.alpha = 0;
                cg.blocksRaycasts = false;
                transform.SetSiblingIndex(-1);
            }
        }

        void LateUpdate()
        {
            if (state == ButtonState.Available)
            {
                cg.alpha = 1;
                cg.blocksRaycasts = true;
                transform.SetSiblingIndex(index);
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
