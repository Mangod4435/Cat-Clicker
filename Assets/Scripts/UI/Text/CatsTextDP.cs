using API;
using TMPro;
using UnityEngine;

public class CatsTextDP : MonoBehaviour
{
    private GameManager manager => GameManager.Instance;

    private TextMeshProUGUI t;
    private double lastCats = -1;

    private void DPCat()
    {
        Debug.Log($"[CatsTextDP.cs] called, manager.Cats = {manager.Cats}");
        t.text = $"{NumberFormatter.FormatDouble(manager.Cats)} cats";
    }

    private void OnEnable()
    {
        SaveSystem.OnNoSave += DPCat;
    }

    private void OnDisable()
    {
        SaveSystem.OnNoSave -= DPCat;
    }

    private void Awake()
    {
        t = GetComponent<TextMeshProUGUI>();
        if (t == null) Debug.LogError("No TMP component found on " + gameObject.name);
    }

    private void Start()
    {
        lastCats = -1;
    }

    private void Update()
    {
        if (manager.Cats != lastCats)
        {
            DPCat();
            lastCats = manager.Cats;
        }
    }
}