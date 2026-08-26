using System.Collections.Generic;

public class TooltipTexts
{
	public static Dictionary<string, Tooltip> indexes = new Dictionary<string, Tooltip>
	{
		{"SharpClaw", new Tooltip("Sharper Claw", "SharpClaw", "Make your click sharper")}
	};
}

public class Tooltip
{
	public Tooltip(string name, string technicalName, string description)
	{
		this.name = name;
		this.technicalName = technicalName;
		this.description = description;
	}

	public string name;
	public string technicalName;
	public string description;
}