using System;

namespace API
{
    public class UpgradeAPI
    {
        public static double PriceCalculator(double baseCost, double amount)
        {
            return Math.Round(Math.Pow(amount, Math.E) * 10) + baseCost;
        }
    }
}
