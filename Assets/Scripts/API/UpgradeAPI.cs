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

        public static double getPrice(UNIVERSALButton.UpgradeType name)
        {
            switch (name)
            {
                //click upgrades
                case UNIVERSALButton.UpgradeType.SharpClaw:
                    return 100;

                //lazy upgrades
                case UNIVERSALButton.UpgradeType.CozySpot:
                    return 20;
                case UNIVERSALButton.UpgradeType.FishBowl:
                    return 100;
                case UNIVERSALButton.UpgradeType.TV:
                    return 1_000;
                case UNIVERSALButton.UpgradeType.Laser:
                    return 50_000;
                case UNIVERSALButton.UpgradeType.Factory:
                    return 10_000_000;
                case UNIVERSALButton.UpgradeType.Satellite:
                    return 5_000_000_000;
                default:
                    Debug.LogError($"No upgrade with name \"{name}\" found");
                    return 0;
            }
        }

        public static double getAmount(UNIVERSALButton.UpgradeType name)
        {
            switch (name)
            {
                //click upgrades
                case UNIVERSALButton.UpgradeType.SharpClaw:
                    return manager.SharpClaw;

                //lazy upgrades
                case UNIVERSALButton.UpgradeType.CozySpot:
                    return manager.CozySpot;
                case UNIVERSALButton.UpgradeType.FishBowl:
                    return manager.FishBowl;
                case UNIVERSALButton.UpgradeType.TV:
                    return manager.TV;
                case UNIVERSALButton.UpgradeType.Laser:
                    return manager.Laser;
                case UNIVERSALButton.UpgradeType.Factory:
                    return manager.Factory;
                case UNIVERSALButton.UpgradeType.Satellite:
                    return manager.Satellite;
                default:
                    Debug.LogError($"No upgrade with name \"{name}\" found");
                    return 0;
            }
        }

        public static double getCalculatedPrice(UNIVERSALButton.UpgradeType name)
        {
            return PriceCalculator(getPrice(name), getAmount(name));
        }
    }
}
