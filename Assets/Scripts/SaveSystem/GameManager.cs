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

    public bool IsSharpClawRevealed { get; private set; }
    public bool IsCozySpotRevealed { get; private set; }
    public bool IsFishBowlRevealed { get; private set; }
    public bool IsTVRevealed { get; private set; }
    public bool IsLaserRevealed { get; private set; }
    public bool IsFactoryRevealed { get; private set; }
    public bool IsSatelliteRevealed { get; private set; }
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

    public void SetCat(double amount = 0) => Cats = amount;

    public void AddUpgrade(UNIVERSALButton.UpgradeType name, int amount = 1)
    {
        switch (name)
        {
            case UNIVERSALButton.UpgradeType.SharpClaw:
                SharpClaw += amount;
                break;
            case UNIVERSALButton.UpgradeType.CozySpot:
                CozySpot += amount;
                break;
            case UNIVERSALButton.UpgradeType.FishBowl:
                FishBowl += amount;
                break;
            case UNIVERSALButton.UpgradeType.TV:
                TV += amount;
                break;
            case UNIVERSALButton.UpgradeType.Laser:
                Laser += amount;
                break;
            case UNIVERSALButton.UpgradeType.Factory:
                Factory += amount;
                break;
            case UNIVERSALButton.UpgradeType.Satellite:
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

    public void setRevealed(UNIVERSALButton.UpgradeType name, bool value)
    {
        switch (name)
        {
            case UNIVERSALButton.UpgradeType.SharpClaw:
                IsSharpClawRevealed = value;
                break;
            case UNIVERSALButton.UpgradeType.CozySpot:
                IsCozySpotRevealed = value;
                break;
            case UNIVERSALButton.UpgradeType.FishBowl:
                IsFishBowlRevealed = value;
                break;
            case UNIVERSALButton.UpgradeType.TV:
                IsTVRevealed = value;
                break;
            case UNIVERSALButton.UpgradeType.Laser:
                IsLaserRevealed = value;
                break;
            case UNIVERSALButton.UpgradeType.Factory:
                IsFactoryRevealed = value;
                break;
            case UNIVERSALButton.UpgradeType.Satellite:
                IsSatelliteRevealed = value;
                break;
            default:
                Debug.LogError($"That upgrade not found ({name})");
                return;
        }
        SyncUpgradesData();
    }

    public bool getRevealed(UNIVERSALButton.UpgradeType name)
    {
        switch (name)
        {
            case UNIVERSALButton.UpgradeType.SharpClaw:
                return IsSharpClawRevealed;
            case UNIVERSALButton.UpgradeType.CozySpot:
                return IsCozySpotRevealed;
            case UNIVERSALButton.UpgradeType.FishBowl:
                return IsFishBowlRevealed;
            case UNIVERSALButton.UpgradeType.TV:
                return IsTVRevealed;
            case UNIVERSALButton.UpgradeType.Laser:
                return IsLaserRevealed;
            case UNIVERSALButton.UpgradeType.Factory:
                return IsFactoryRevealed;
            case UNIVERSALButton.UpgradeType.Satellite:
                return IsSatelliteRevealed;
            default:
                Debug.LogError($"That upgrade not found ({name})");
                return false;
        }
    }
    #endregion

    #region Save - Load
    public SaveData CreateSaveData()
    {
        SyncUpgradesData();

        return new SaveData
        {
            cpc = CPC,
            cps = CPS,
            cats = Math.Round(Cats, 5),

            upgrades = new UpgradesData
            {
                // click
                SharpClaw = SharpClaw,
                // lazy
                CozySpot = CozySpot,
                FishBowl = FishBowl,
                TV = TV,
                Laser = Laser,
                Factory = Factory,
                Satellite = Satellite,
                IsSharpClawReveal = IsSharpClawRevealed,
                IsCozySpotReveal = IsCozySpotRevealed,
                IsFishBowlReveal = IsFishBowlRevealed,
                IsTVReveal = IsTVRevealed,
                IsLaserReveal = IsLaserRevealed,
                IsFactoryReveal = IsFactoryRevealed,
                IsSatelliteReveal = IsSatelliteRevealed,
            },
        };
    }

    public void ApplySaveData(SaveData data)
    {
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
        IsSharpClawRevealed = Upgrades.IsSharpClawReveal;
        IsCozySpotRevealed = Upgrades.IsCozySpotReveal;
        IsFishBowlRevealed = Upgrades.IsFishBowlReveal;
        IsTVRevealed = Upgrades.IsTVReveal;
        IsLaserRevealed = Upgrades.IsLaserReveal;
        IsFactoryRevealed = Upgrades.IsFactoryReveal;
        IsSatelliteRevealed = Upgrades.IsSatelliteReveal;

        SyncUpgradesData();
        RecalculateCpc();
        RecalCulateCps();
    }

    public void SaveGame()
    {
        var data = CreateSaveData();
        SaveSystem.Save(data);
    }

    private void LoadGame()
    {
        SaveData data = SaveSystem.Load();
        ApplySaveData(data);
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
        Upgrades.FishBowl = FishBowl;
        Upgrades.TV = TV;
        Upgrades.Laser = Laser;
        Upgrades.Factory = Factory;
        Upgrades.Satellite = Satellite;
        Upgrades.IsSharpClawReveal = IsSharpClawRevealed;
        Upgrades.IsCozySpotReveal = IsCozySpotRevealed;
        Upgrades.IsFishBowlReveal = IsFishBowlRevealed;
        Upgrades.IsTVReveal = IsTVRevealed;
        Upgrades.IsLaserReveal = IsLaserRevealed;
        Upgrades.IsFactoryReveal = IsFactoryRevealed;
        Upgrades.IsSatelliteReveal = IsSatelliteRevealed;
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
