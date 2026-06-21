using System;

namespace UpgradeSystem
{
    // Formula reference — actual cpc calculation is mirrored in GameManager.RecalculateCpc()
    // because GameManager lives in SaveSystem assembly and cannot reference Assembly-CSharp
    public class UpgradeCore
    {
        public static int CalculateCpc(UpgradesData data)
        {
            if (data == null)
                return 1;
            int cpc = 1;
            cpc += data.SharpClaw * 1;
            return cpc;
        }

        public static double PriceCalculator(double baseCost, double amount)
        {
            return Math.Round(baseCost * Math.Pow(1.25, amount));
        }
    }
}
