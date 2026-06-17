using TMPro;
using UnityEngine;

public class UpgradeDP : MonoBehaviour
{
    [SerializeField]
    string upgradeName;

    int lastAmount = -1;
    GameManager instance => GameManager.Instance;
    TextMeshProUGUI text;

    void DPUpgrade()
    {
        if (upgradeName == "Sharp Claw")
            text.text = instance.SharpClaw.ToString();
    }

    void OnEnable() => SaveSystem.OnNoSave += DPUpgrade;

    void OnDisable() => SaveSystem.OnNoSave -= DPUpgrade;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    //eduwfoifh
    void Update()
    {
        if (upgradeName == "Sharp Claw" && instance.SharpClaw != lastAmount)
        {
            DPUpgrade();
            lastAmount = instance.SharpClaw;
        }
    }
}
