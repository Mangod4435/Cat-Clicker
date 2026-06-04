using TMPro;
using UnityEngine;

public class UpgradeDP : MonoBehaviour
{
    [SerializeField]
    string upgradeName;

    int lastAmount;
    GameManager instance => GameManager.instance;
    TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (upgradeName == "Cat Food" && instance.CatFood != lastAmount)
        {
            text.text = instance.CatFood.ToString();
            lastAmount = instance.CatFood;
        }
    }
}
