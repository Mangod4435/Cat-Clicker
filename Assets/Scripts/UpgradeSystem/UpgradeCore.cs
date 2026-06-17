using UnityEngine;

namespace UpgradeSystem
{
    public class UpgradeCore : MonoBehaviour
    {
        private GameManager _manager => GameManager.Instance;
        private float t = 0;

        internal int CalculateCps(UpgradesData data)
        {
            var sharpClaw = data.SharpClaw;
            var cpc = 1;
            cpc += sharpClaw * 1;
            return cpc;
        }
    }
}
