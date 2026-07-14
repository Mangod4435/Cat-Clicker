using System;

[Serializable]
public class UpgradesData
{
    #region Upgrades Data Fields
    public int SharpClaw;
    public int CozySpot;
    public int FishBowl;
    public int TV;
    public int Laser;
    public int Factory;
    public int Satellite;
    public bool IsSharpClawReveal;
    public bool IsCozySpotReveal;
    public bool IsFishBowlReveal;
    public bool IsTVReveal;
    public bool IsLaserReveal;
    public bool IsFactoryReveal;
    public bool IsSatelliteReveal;
    #endregion

    #region UpgradesData constructor
    public UpgradesData(
        int sc = 0,
        int cs = 0,
        int fb = 0,
        int tv = 0,
        int l = 0,
        int f = 0,
        int s = 0,
        bool isScReveal = false,
        bool isCsReveal = false,
        bool isFbReveal = false,
        bool isTvReveal = false,
        bool isLReveal = false,
        bool isFReveal = false,
        bool isSReveal = false
    )
    {
        SharpClaw = sc;
        CozySpot = cs;
        FishBowl = fb;
        TV = tv;
        Laser = l;
        Factory = f;
        Satellite = s;
        IsSharpClawReveal = isScReveal;
        IsCozySpotReveal = isCsReveal;
        IsFishBowlReveal = isFbReveal;
        IsTVReveal = isTvReveal;
        IsLaserReveal = isLReveal;
        IsFactoryReveal = isFReveal;
        IsSatelliteReveal = isSReveal;
    }
    #endregion
}

[Serializable]
public class SaveData
{
    #region Save Data Fields
    public double cpc;
    public double cps;
    public double cats;
    public UpgradesData upgrades;
    #endregion

    #region Save Data constructor
    public SaveData(double cpc = 1, double cps = 0, double cats = 0, UpgradesData upgrades = null)
    {
        this.cpc = cpc;
        this.cps = cps;
        this.cats = cats;
        this.upgrades = upgrades ?? new UpgradesData();
    }
    #endregion
}
