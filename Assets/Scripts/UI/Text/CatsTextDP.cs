using API;
using TMPro;
using UnityEngine;

public class CatsTextDP : MonoBehaviour
{
    GameManager manager => GameManager.Instance;

    TextMeshProUGUI t;
    double lastCats = -1;

    void DPCat() => t.text = $"{NumberFormatter.FormatDouble(manager.Cats)} cats";

    void OnEnable()
    {
        SaveSystem.OnNoSave += DPCat;
    }

    void OnDisable()
    {
        SaveSystem.OnNoSave -= DPCat;
    }

    void Awake()
    {
        t = GetComponent<TextMeshProUGUI>();
        if (t == null)
            Debug.LogError("No TMP component found on " + gameObject.name);
    }

    void Start()
    {
        lastCats = -1;
    }

    void Update()
    {
        if (manager.Cats != lastCats)
        {
            DPCat();
            lastCats = manager.Cats;
        }
    }
}
