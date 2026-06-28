using System;

namespace API
{
    public class UpgradeAPI
    {
        public static double PriceCalculator(double baseCost, double amount)
        {
            return baseCost * Math.Pow(1.15, amount);
        }
    }
}
