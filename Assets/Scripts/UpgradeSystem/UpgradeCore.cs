using UnityEngine;

namespace UpgradeSystem
{
    // Formula reference — actual cpc calculation is mirrored in GameManager.RecalculateCpc()
    // because GameManager lives in SaveSystem assembly and cannot reference Assembly-CSharp
    public class UpgradeCore : MonoBehaviour
    {
        internal int CalculateCpc(UpgradesData data)
        {
            if (data == null) return 1;
            int cpc = 1;
            cpc += data.SharpClaw * 1;
            return cpc;
        }
    }
}
