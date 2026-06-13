using UnityEngine;

public class UpgradeCore : MonoBehaviour
{
    private GameManager _manager => GameManager.Instance;
    private float t = 0;

    private int CalculateCps(UpgradesData data)
    {
        var catfood = data.catFood;
        var cps = 0;
        cps += catfood * 1;
        return cps;
    }

    private void FixedUpdate()
    {
        if (t < 1)
        {
            t += Time.fixedDeltaTime;
        }
        else if (t >= 1)
        {
            t = 0;
            _manager.AddCat(CalculateCps(_manager.Upgrades));
        }
    }
}
