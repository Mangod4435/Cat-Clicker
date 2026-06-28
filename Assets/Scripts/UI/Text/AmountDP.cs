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
        if (upgradeName == "Sharp Claw")
            text.text = manager.SharpClaw.ToString();
        if (upgradeName == "Cozy Spot")
            text.text = manager.CozySpot.ToString();
    }

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

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
    }
}
