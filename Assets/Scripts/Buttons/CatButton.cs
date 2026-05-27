using UnityEngine;

public class CatButton : MonoBehaviour
{
    GameManager manager => GameManager.instance;

    PressedEvent e;

    void Awake()
    {
        e = gameObject.GetComponent<PressedEvent>();
    }

    void FixedUpdate() => transform.Rotate(0, 0, -22.5f * Time.fixedDeltaTime);

    void Update()
    {
        transform.localScale = e.holding ? Vector3.one * 0.8f : Vector3.one;
    }

    public void OnButtonClicked()
    {
        manager.AddCats(1);
    }
}
