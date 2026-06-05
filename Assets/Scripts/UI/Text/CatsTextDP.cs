using TMPro;
using UnityEngine;

public class CatsTextDP : MonoBehaviour
{
    GameManager manager => GameManager.instance;

    TextMeshProUGUI t;
    double lastCats = -1;

    void DPCat()
    {
        Debug.Log($"[DPCat] called, manager.Cats = {manager.Cats}, t = {t}");
        t.text = $"{NumberFormatter.formatDouble(manager.Cats)} cats";
    }

    void OnEnable() => SaveSystem.OnNoSave += DPCat;

    void OnDisable() => SaveSystem.OnNoSave -= DPCat;

    void Awake()
    {
        t = GetComponent<TextMeshProUGUI>();
        if (t == null)
        {
            Debug.LogError("No TMP component found on " + gameObject.name);
            return;
        }
    }

    void Start()
    {
        lastCats = -1;
    }

    void Update()
    {
        Debug.Log($"[CatsTextDP.cs]: cats = {manager.Cats} and lastCats = {lastCats}");
        if (manager.Cats != lastCats)
        {
            DPCat();
            Debug.Log($"[CatsTextDP.cs]: change cat from {t.text} -> {manager.Cats}");
            lastCats = manager.Cats;
        }
    }
}
