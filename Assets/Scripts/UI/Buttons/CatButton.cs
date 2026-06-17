using UnityEngine;

public class CatButton : MonoBehaviour
{
    GameManager manager => GameManager.Instance;
    SettingManager setting => SettingManager.instance;
    PressedEvent e;
    AudioSource meow;

    void Awake()
    {
        e = GetComponent<PressedEvent>();
        meow = GetComponent<AudioSource>();
    }

    void FixedUpdate() => transform.Rotate(0, 0, -22.5f * Time.fixedDeltaTime);

    void Update()
    {
        transform.localScale = e.holding ? Vector3.one * 0.8f : Vector3.one;
        if (setting.Sound == false)
            meow.volume = 0;
        else if (setting.Sound == true)
            meow.volume = 1;
    }

    public void OnClicked()
    {
        manager.AddCat(1 * manager.cpc);
        meow.Play();
    }
}
