using System;
using UnityEngine;

namespace API
{
    public class UpgradeAPI
    {
        static GameManager manager => GameManager.Instance;

        public static double PriceCalculator(double baseCost, double amount)
        {
            return baseCost * Math.Pow(1.15, amount);
        }

        public static double getPrice(string name)
        {
            switch (name)
            {
                //click upgrades
                case "Sharp Claw":
                    return 100;

                //lazy upgrades
                case "Cozy Spot":
                    return 20;
                case "Fish Bowl":
                    return 100;
                case "TV":
                    return 1_000;
                case "Laser":
                    return 50_000;
                case "Factory":
                    return 10_000_000;
                case "Satellite":
                    return 5_000_000_000;
                default:
                    Debug.LogError($"No upgrade with name \"{name}\" found");
                    return 0;
            }
        }

        public static double getAmount(string name)
        {
            switch (name)
            {
                //click upgrades
                case "Sharp Claw":
                    return manager.SharpClaw;

                //lazy upgrades
                case "Cozy Spot":
                    return manager.CozySpot;
                case "Fish Bowl":
                    return manager.FishBowl;
                case "TV":
                    return manager.TV;
                case "Laser":
                    return manager.Laser;
                case "Factory":
                    return manager.Factory;
                case "Satellite":
                    return manager.Satellite;
                default:
                    Debug.LogError($"No upgrade with name \"{name}\" found");
                    return 0;
            }
        }

        public static double getCalculatedPrice(string name)
        {
            return PriceCalculator(getPrice(name), getAmount(name));
        }
    }
}
