using System;
using System.Collections.Generic;

[Serializable]
public class UpgradesData
{
    public int SharpClaw;
}

[Serializable]
public class SaveData
{
    public double cpc;
    public double cats;
    public UpgradesData upgrades;
}
