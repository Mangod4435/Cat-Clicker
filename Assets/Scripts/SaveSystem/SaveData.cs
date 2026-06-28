using System;

[Serializable]
public class UpgradesData
{
    public int SharpClaw;
    public int CozySpot;
    public int FishBowl;
    public int TV;
    public int Laser;
    public int Factory;
    public int Satellite;
}

[Serializable]
public class SaveData
{
    public double cpc;
    public double cps;
    public double cats;
    public UpgradesData upgrades;
}
