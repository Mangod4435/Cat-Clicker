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
    }
}
