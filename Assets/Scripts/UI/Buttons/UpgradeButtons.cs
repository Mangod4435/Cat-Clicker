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
                    if (Instance.Cats >= 10)
                        return true;
                    break;
            }
            return false;
        }

        public void OnClicked()
        {
            if (!IsAffordable(upgradeName))
                return;
            Instance.AddUpgrade(upgradeName, 1);
            switch (upgradeName)
            {
                case "Sharp Claw":
                    Instance.AddCat(-10);
                    break;
                default:
                    break;
            }
        }
    }
}
