using UnityEngine;

namespace UpgradeSystem
{
    public class UpgradeCore : MonoBehaviour
    {
        private GameManager _manager => GameManager.Instance;

        // Renamed from CalculateCps → CalculateCpc (clicks per click, not cats per second)
        internal int CalculateCpc(UpgradesData data)
        {
            if (data == null) return 1;

            int cpc = 1; // baseline: always at least 1
            cpc += data.SharpClaw * 1;
            return cpc;
        }
    }
}
