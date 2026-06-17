using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float _autoSaveTimer;
    private bool _saveLoaded = false;
    private const float AutoSaveInterval = 30f;

    #region public field
    public static GameManager Instance { get; private set; }

    public UpgradesData Upgrades { get; private set; }
    public double cpc { get; private set; }
    public double Cats { get; private set; }
    public int SharpClaw { get; private set; }
    #endregion

    #region unity lifetime
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
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
        if (_autoSaveTimer >= AutoSaveInterval)
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
    #endregion

    #region API
    public void AddCat(double amount) => Cats += amount;

    public void AddCat() => Cats++;

    public void AddUpgrade(string name, int amount)
    {
        switch (name)
        {
            case "Sharp Claw":
                SharpClaw += amount;
                break;
            default:
                Debug.LogError($"That upgrade not found ({name})");
                return;
        }
        SyncUpgradesData();
        RecalculateCpc();
    }
    #endregion

    #region Save - Load
    public void SaveGame()
    {
        var data = new SaveData
        {
            cpc = cpc,
            cats = Cats,
            upgrades = new UpgradesData { SharpClaw = SharpClaw },
        };
        SaveSystem.Save(data);
    }

    private void LoadGame()
    {
        SaveData data = SaveSystem.Load();
        Cats = data.cats;
        SharpClaw = data.upgrades?.SharpClaw ?? 0;
        Upgrades = data.upgrades ?? new UpgradesData();
        RecalculateCpc();
    }

    public void ResetGame()
    {
        SaveSystem.DESTROYtheSave();
        Cats = 0;
        SharpClaw = 0;
        Upgrades = new UpgradesData();
        RecalculateCpc();
    }
    #endregion

    #region Private helpers
    private void SyncUpgradesData()
    {
        if (Upgrades == null) Upgrades = new UpgradesData();
        Upgrades.SharpClaw = SharpClaw;
    }

    // Cpc logic lives here to avoid cross-assembly reference (SaveSystem -> Assembly-CSharp)
    // UpgradeCore.cs can stay as the source of truth for formula documentation
    private void RecalculateCpc()
    {
        int cpc = 1; // baseline
        cpc += SharpClaw * 1;
        this.cpc = cpc;
    }
    #endregion
}
