#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class GameManagerDebugWindow : EditorWindow
{
    private GameManager manager => GameManager.Instance;
    private Vector2 scrollPosition;

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

        // Currency Section
        EditorGUILayout.LabelField("Currency", EditorStyles.boldLabel);
        if (GUILayout.Button("Add 1000 Cats", GUILayout.Height(30)))
        {
            manager.AddCat(1000);
        }
        if (GUILayout.Button("Add 10000 Cats", GUILayout.Height(30)))
        {
            manager.AddCat(10000);
        }
        if (GUILayout.Button("Multiply Cats by 10", GUILayout.Height(30)))
        {
            manager.SetCat(manager.Cats * 10);
        }
        if (GUILayout.Button("Set Cats to 1 Million", GUILayout.Height(30)))
        {
            manager.SetCat(1_000_000);
        }

        EditorGUILayout.Space();

        // Upgrades Section
        EditorGUILayout.LabelField("Upgrades", EditorStyles.boldLabel);
        if (GUILayout.Button("Buy 10 Sharp Claws", GUILayout.Height(30)))
        {
            manager.AddUpgrade("Sharp Claw", 10);
        }
        if (GUILayout.Button("Buy 100 Sharp Claws", GUILayout.Height(30)))
        {
            manager.AddUpgrade("Sharp Claw", 100);
        }

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
