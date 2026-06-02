using UnityEngine;
using UnityEngine.UI;

public class CatButton : MonoBehaviour
{
    GameManager manager => GameManager.instance;
    PressedEvent e;
    AudioSource meow;
    [SerializeField]
    Toggle SoundBtn;

    void Awake()
    {
        e = GetComponent<PressedEvent>();
        meow = GetComponent<AudioSource>();
    }

    void FixedUpdate() => transform.Rotate(0, 0, -22.5f * Time.fixedDeltaTime);

    void Update()
    {
        transform.localScale = e.holding ? Vector3.one * 0.8f : Vector3.one;
        if (SoundBtn.isOn == false)
            meow.volume = 0;
        else if (SoundBtn.isOn == true)
            meow.volume = 1;
    }

    public void OnClicked()
    {
        manager.AddCats(1);
        meow.Play();
    }
}
