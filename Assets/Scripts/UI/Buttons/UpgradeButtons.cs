using UnityEngine;

// This is for debugging only for phone I'll do the full one later.

namespace UI.Buttons
{
    public class UpgradeButtons : MonoBehaviour
    {
        [SerializeField]
        string upgradeName;

        private GameManager Instance => GameManager.instance;

        private bool IsAffordable(string localUpgradeName)
        {
            switch (localUpgradeName)
            {
                case "Cat Food":
                    switch (Instance.Cats)
                    {
                        case < 10:
                            break;
                        case >= 10:
                            return true;
                    }

                    break;
            }
            return false;
        }

        public void OnClicked()
        {
            if (IsAffordable(upgradeName))
            {
                Instance.AddUpgrade(upgradeName, 1);
                Instance.AddCat(-10);
            }
        }
    }
}
