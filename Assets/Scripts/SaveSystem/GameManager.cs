using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public double Cats { get; private set; }

    private float _autoSaveTimer;
    private bool _saveLoaded = false;
    private const float AUTO_SAVE_INTERVAL = 30f;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        LoadGame();
        _saveLoaded = true;
    }

    void Update()
    {
        _autoSaveTimer += Time.deltaTime;
        if (_autoSaveTimer >= AUTO_SAVE_INTERVAL)
        {
            SaveGame();
            _autoSaveTimer = 0f;
        }
    }

    void OnApplicationFocus(bool focus)
    {
        if (!focus && _saveLoaded)
            SaveGame();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus && _saveLoaded)
            SaveGame();
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }

    // ━━ Public API ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

    public void AddCats(double amount) => Cats += amount;

    public void AddCat() => Cats++;

    // ━━ Save / Load ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

    public void SaveGame()
    {
        var data = new SaveData { cats = Cats };
        SaveSystem.Save(data);
    }

    public void LoadGame()
    {
        SaveData data = SaveSystem.Load();
        Cats = data.cats;
    }

    public void ResetGame()
    {
        SaveSystem.DESTROYtheSave();
        Cats = 0;
    }
}
