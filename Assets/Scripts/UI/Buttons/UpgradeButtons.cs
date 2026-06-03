using UnityEngine;

// This is for debugging only for phone i'll do the full one later.

public class UpgradeButtons : MonoBehaviour
{
    [SerializeField]
    string UpgradeName;
    GameManager instance => GameManager.instance;

    public void OnClicked() => instance.AddUpgrade(UpgradeName, 1);
}
