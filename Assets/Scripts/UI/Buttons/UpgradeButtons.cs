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
                    if (Instance.Cats >= UpgradeCore.PriceCalculator(10, Instance.SharpClaw))
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
                    Instance.AddCat(UpgradeCore.PriceCalculator(10, Instance.SharpClaw) * -1);
                    break;
                default:
                    break;
            }
            Instance.AddUpgrade(upgradeName, 1);
        }
    }
}
