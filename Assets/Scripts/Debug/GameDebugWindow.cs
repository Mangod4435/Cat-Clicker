#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

public class GameManagerDebugWindow : EditorWindow
{
    private GameManager manager => GameManager.Instance;
    private Vector2 scrollPosition;
    double Cats = 0;
    string Upgrades = "";
    int amount = 0;

    [MenuItem("Window/Cat Clicker/Game Manager Debug")]
    public static void ShowWindow()
    {
        GetWindow<GameManagerDebugWindow>("Game Debug");
    }

    private void OnGUI()
    {
        if (manager == null)
        {
            EditorGUILayout.HelpBox("GameManager not found in scene!", MessageType.Warning);
            return;
        }

        EditorGUILayout.LabelField("Game Manager Debug API", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        EditorGUILayout.LabelField("Currency", EditorStyles.boldLabel);
        // Currency Section

        Cats = EditorGUILayout.DoubleField("Cats", Cats);
        if (GUILayout.Button("Add cat", GUILayout.Height(30)))
            manager.AddCat(Cats);
        if (GUILayout.Button("Set cat", GUILayout.Height(30)))
            manager.SetCat(Cats);

        EditorGUILayout.Space();

        // Upgrades Section
        EditorGUILayout.LabelField("Upgrades", EditorStyles.boldLabel);
        Upgrades = EditorGUILayout.TextField(label: "Upgrade", text: Upgrades);
        amount = EditorGUILayout.IntField(label: "Amount", value: amount);

        if (GUILayout.Button("Add Upgrades", GUILayout.Height(30)))
            manager.AddUpgrade(Upgrades, amount);

        EditorGUILayout.Space();

        // Game State Section
        EditorGUILayout.LabelField("Game State", EditorStyles.boldLabel);
        if (GUILayout.Button("Print Game State", GUILayout.Height(30)))
        {
            Debug.Log(
                $"=== GAME STATE ===\n"
                    + $"Cats: {manager.Cats}\n"
                    + $"CPC: {manager.cpc}\n"
                    + $"Sharp Claw: {manager.SharpClaw}\n"
                    + $"Cozy Spot: {manager.CozySpot}"
            );
        }
        if (GUILayout.Button("Reset Game", GUILayout.Height(30)))
        {
            if (EditorUtility.DisplayDialog("Reset Game", "Are you sure?", "Yes", "Cancel"))
            {
                manager.ResetGame();
                Debug.Log("Game reset!");
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // Current Stats Display
        EditorGUILayout.LabelField("Current Stats", EditorStyles.boldLabel);
        EditorGUILayout.LabelField($"Cats: {manager.Cats}");
        EditorGUILayout.LabelField($"CPC: {manager.cpc}");
        EditorGUILayout.LabelField($"Sharp Claw: {manager.SharpClaw}");
        EditorGUILayout.LabelField($"Cozy Spot: {manager.CozySpot}");

        EditorGUILayout.EndScrollView();
    }
}
#endif
