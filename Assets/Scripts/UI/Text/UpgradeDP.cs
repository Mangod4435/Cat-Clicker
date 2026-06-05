using TMPro;
using UnityEngine;

public class UpgradeDP : MonoBehaviour
{
    [SerializeField]
    string upgradeName;

    int lastAmount = -1;
    GameManager instance => GameManager.instance;
    TextMeshProUGUI text;

    void DPUpgrade()
    {
        if (upgradeName == "Cat Food")
            text.text = instance.CatFood.ToString();
    }

    void OnEnable() => SaveSystem.OnNoSave += DPUpgrade;

    void OnDisable() => SaveSystem.OnNoSave -= DPUpgrade;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (upgradeName == "Cat Food" && instance.CatFood != lastAmount)
        {
            DPUpgrade();
            lastAmount = instance.CatFood;
        }
    }
}
