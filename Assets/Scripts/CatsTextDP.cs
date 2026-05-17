using TMPro;
using UnityEngine;

public class CatsTextDP : MonoBehaviour
{
    [SerializeField]
    PlayerStats stat;

    TextMeshProUGUI t;
    double lastCats;

    void Awake() => t = GetComponent<TextMeshProUGUI>();

    void Start()
    {
        t.text = $"{NumFormatr.frmtDob(stat.cats)} cats";
        lastCats = stat.cats;
    }

    void Update()
    {
        if (stat.cats != lastCats)
        {
            t.text = $"{NumFormatr.frmtDob(stat.cats)} cats";
            lastCats = stat.cats;
        }
    }
}
