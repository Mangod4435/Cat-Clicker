using UnityEngine;
using UpgradeSystem;

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
                    if (Instance.Cats >= UpgradeCore.PriceCalculator(100, Instance.SharpClaw))
                        return true;
                    break;
                case "Cozy Spot":
                    if (Instance.Cats >= UpgradeCore.PriceCalculator(20, Instance.CozySpot))
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
                    Instance.AddCat(-UpgradeCore.PriceCalculator(100, Instance.SharpClaw));
                    break;
                case "Cozy Spot":
                    Instance.AddCat(-UpgradeCore.PriceCalculator(20, Instance.CozySpot));
                    break;
                default:
                    break;
            }
            Instance.AddUpgrade(upgradeName, 1);
        }
    }
}
