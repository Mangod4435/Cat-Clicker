using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Timer
    float CPSTimer;
    float _autoSaveTimer;
    bool _saveLoaded = false;
    const float AutoSaveInterval = 30f;
    #endregion

    #region public field
    public static GameManager Instance { get; private set; }
    public UpgradesData Upgrades { get; private set; }

    public double Cats { get; private set; }
    public double CPC { get; private set; }
    public double CPS { get; private set; }

    // Click upgrades
    public int SharpClaw { get; private set; }

    // Lazy upgrades
    public int CozySpot { get; private set; }
    public int FishBowl { get; private set; }
    public int TV { get; private set; }
    public int Laser { get; private set; }
    public int Factory { get; private set; }
    public int Satellite { get; private set; }
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
        CPSTimer += Time.fixedDeltaTime;
        if (CPSTimer >= 1)
        {
            AddCat(CPS);
            CPSTimer = 0;
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
            case "Fish Bowl":
                FishBowl += amount;
                break;
            case "TV":
                TV += amount;
                break;
            case "Laser":
                Laser += amount;
                break;
            case "Factory":
                Factory += amount;
                break;
            case "Satellite":
                Satellite += amount;
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
        SyncUpgradesData();

        var data = new SaveData
        {
            cpc = CPC,
            cps = CPS,
            cats = Math.Round(Cats, 5),

            upgrades = new UpgradesData
            {
                SharpClaw = SharpClaw,
                CozySpot = CozySpot,
                FishBowl = FishBowl,
                TV = TV,
                Laser = Laser,
                Factory = Factory,
                Satellite = Satellite,
            },
        };
        SaveSystem.Save(data);
    }

    private void LoadGame()
    {
        SaveData data = SaveSystem.Load();
        if (data == null)
            data = new SaveData();

        Upgrades = data.upgrades ?? new UpgradesData();
        Cats = Math.Round(data.cats, 5);

        SharpClaw = Upgrades.SharpClaw;
        CozySpot = Upgrades.CozySpot;
        FishBowl = Upgrades.FishBowl;
        TV = Upgrades.TV;
        Laser = Upgrades.Laser;
        Factory = Upgrades.Factory;
        Satellite = Upgrades.Satellite;

        SyncUpgradesData();
        RecalculateCpc();
        RecalCulateCps();
    }

    public void ResetGame()
    {
        SaveSystem.DESTROYtheSave();
        LoadGame();
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

    private void RecalculateCpc()
    {
        int cpc = 1;
        cpc += SharpClaw * 1;
        CPC = cpc;
    }

    private void RecalCulateCps()
    {
        double cps = 0;
        cps += CozySpot * 0.1;
        cps += FishBowl * 1;
        cps += TV * 3;
        cps += Laser * 50;
        cps += Factory * 200;
        cps += Satellite * 3000;
        CPS = cps;
    }
    #endregion
}
