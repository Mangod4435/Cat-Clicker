using TMPro;
using UnityEngine;

public class UpgradeDP : MonoBehaviour
{
    [SerializeField]
    string upgradeName;

    int lastAmount = -1;
    GameManager manager => GameManager.Instance;
    TextMeshProUGUI text;

    void DPUpgrade()
    {
        // Clicke Upgrades
        if (upgradeName == "Sharp Claw")
            text.text = manager.SharpClaw.ToString();
        // Lazy Upgrades
        if (upgradeName == "Cozy Spot")
            text.text = manager.CozySpot.ToString();
        if (upgradeName == "Fish Bowl")
            text.text = manager.FishBowl.ToString();
        if (upgradeName == "TV")
            text.text = manager.TV.ToString();
        if (upgradeName == "Laser")
            text.text = manager.Laser.ToString();
        if (upgradeName == "Factory")
            text.text = manager.Factory.ToString();
        if (upgradeName == "Satellite")
            text.text = manager.Satellite.ToString();
    }

    void Awake() => text = GetComponent<TextMeshProUGUI>();

    void Update()
    {
        if (upgradeName == "Sharp Claw" && manager.SharpClaw != lastAmount)
        {
            DPUpgrade();
            lastAmount = manager.SharpClaw;
        }

        if (upgradeName == "Cozy Spot" && manager.CozySpot != lastAmount)
        {
            DPUpgrade();
            lastAmount = manager.CozySpot;
        }
        if (upgradeName == "Fish Bowl" && manager.FishBowl != lastAmount)
        {
            DPUpgrade();
            lastAmount = manager.FishBowl;
        }
        if (upgradeName == "TV" && manager.TV != lastAmount)
        {
            DPUpgrade();
            lastAmount = manager.TV;
        }
        if (upgradeName == "Laser" && manager.Laser != lastAmount)
        {
            DPUpgrade();
            lastAmount = manager.Laser;
        }
        if (upgradeName == "Factory" && manager.Factory != lastAmount)
        {
            DPUpgrade();
            lastAmount = manager.Factory;
        }
        if (upgradeName == "Satellite" && manager.Satellite != lastAmount)
        {
            DPUpgrade();
            lastAmount = manager.Satellite;
        }
    }
}
