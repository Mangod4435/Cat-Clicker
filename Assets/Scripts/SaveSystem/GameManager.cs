using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Lazy-upgrades Timer
    float NextAddTimer;
    #endregion

    #region Save - Load Timer
    private float _autoSaveTimer;
    private bool _saveLoaded = false;
    private const float AutoSaveInterval = 30f;
    #endregion

    #region public field
    public static GameManager Instance { get; private set; }
    public UpgradesData Upgrades { get; private set; }

    public double Cats { get; private set; }
    public double CPC { get; private set; }
    public double CPS { get; private set; }

    public int SharpClaw { get; private set; }
    public int CozySpot { get; private set; }
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

    void FixedUpdate()
    {
        NextAddTimer += Time.fixedDeltaTime;
        if (NextAddTimer >= 1)
        {
            AddCat(CPS);
            NextAddTimer = 0;
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

    public void SetCat(double amount) => Cats = amount;

    public void SetCat() => Cats = 0;

    public void AddUpgrade(string name, int amount)
    {
        switch (name)
        {
            case "Sharp Claw":
                SharpClaw += amount;
                break;
            case "Cozy Spot":
                CozySpot += amount;
                break;
            default:
                Debug.LogError($"That upgrade not found ({name})");
                return;
        }
        SyncUpgradesData();
        RecalculateCpc();
        RecalCulateCps();
    }
    #endregion

    #region Save - Load
    public void SaveGame()
    {
        var data = new SaveData
        {
            cpc = CPC,
            cats = Math.Round(Cats, 5),
            upgrades = new UpgradesData { SharpClaw = SharpClaw, CozySpot = CozySpot },
        };
        SaveSystem.Save(data);
    }

    private void LoadGame()
    {
        SaveData data = SaveSystem.Load();
        Cats = Math.Round(data.cats, 5);
        SharpClaw = data.upgrades?.SharpClaw ?? 0;
        CozySpot = data.upgrades?.CozySpot ?? 0;
        Upgrades = data.upgrades ?? new UpgradesData();
        RecalculateCpc();
        RecalCulateCps();
    }

    public void ResetGame()
    {
        SaveSystem.DESTROYtheSave();
        Cats = 0;
        SharpClaw = 0;
        CozySpot = 0;
        Upgrades = new UpgradesData();
        RecalculateCpc();
        RecalCulateCps();
    }
    #endregion

    #region Private helpers
    private void SyncUpgradesData()
    {
        if (Upgrades == null)
            Upgrades = new UpgradesData();
        Upgrades.SharpClaw = SharpClaw;
        Upgrades.CozySpot = CozySpot;
    }

    // Cpc logic lives here to avoid cross-assembly reference (SaveSystem -> Assembly-CSharp)
    // UpgradeCore.cs can stay as the source of truth for formula documentation
    private void RecalculateCpc()
    {
        int cpc = 1; // baseline
        cpc += SharpClaw * 1;
        CPC = cpc;
    }

    private void RecalCulateCps()
    {
        double cps = 0;
        cps += CozySpot * 0.1;
        CPS = cps;
    }
    #endregion
}
