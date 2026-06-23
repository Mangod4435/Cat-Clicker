using System;
using System.Collections.Generic;

[Serializable]
public class UpgradesData
{
    public int SharpClaw;
    public int CozySpot;
}

[Serializable]
public class SaveData
{
    public double cpc;
    public double cats;
    public UpgradesData upgrades;
}
