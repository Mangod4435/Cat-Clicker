using TMPro;
using UnityEngine;

public class CatsTextDP : MonoBehaviour
{
    GameManager manager => GameManager.instance;

    TextMeshProUGUI t;
    double lastCats = 0;

    void Awake()
    {
        t = GetComponent<TextMeshProUGUI>();
        if (t == null)
            Debug.LogError("No TMP component found on " + gameObject.name);
    }

    void Start()
    {
        t.text = $"{NumFormatr.frmtDob(manager.Cats)} cats";
    }

    void Update()
    {
        if (manager.Cats != lastCats)
        {
            t.text = $"{NumFormatr.frmtDob(manager.Cats)} cats";
            lastCats = manager.Cats;
        }
    }
}
