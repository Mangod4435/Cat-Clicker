using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Scriptable Objects/Stats")]
public class Stats : ScriptableObject
{
    public static double cats = 0; // Initialize cats to 0. AND THIS WILL RESET TO 0 EVERY TIME YOU RESTART GAME.
}
