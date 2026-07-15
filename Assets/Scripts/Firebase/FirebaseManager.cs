using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }

    private FirebaseFirestore db;
    private bool isInitialized = false;

    // Save interval = 5 minutes
    private const float SAVE_INTERVAL = 300f;
    private float saveTimer = 0f;

    public bool IsInitialized => isInitialized;

    // ── Events ──────────────────────────────────────────
    public event Action OnFirebaseReady;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitFirebase();
    }

    void Update()
    {
        if (!isInitialized) return;

        saveTimer += Time.deltaTime;
        if (saveTimer >= SAVE_INTERVAL)
        {
            saveTimer = 0f;
            SaveScore();
        }
    }

    // ── Init ─────────────────────────────────────────────
    void InitFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                db = FirebaseFirestore.DefaultInstance;
                isInitialized = true;
                Debug.Log("[Firebase] Ready");

                LoadScore();
                OnFirebaseReady?.Invoke();
            }
            else
            {
                Debug.LogError($"[Firebase] Failed: {task.Result}");
            }
        });
    }

    // ── Save ─────────────────────────────────────────────
    public void SaveScore()
    {
        if (!isInitialized) return;

        string userId = GetUserId();
        string weekKey = GetWeekKey();

        var data = new Dictionary<string, object>
        {
            { "userId",    userId },
            { "username",  PlayerPrefs.GetString("username", "Player") },
            { "score",     GameManager.Instance.Cats },
            { "weekKey",   weekKey },
            { "updatedAt", FieldValue.ServerTimestamp }
        };

        db.Collection("leaderboard")
          .Document($"{weekKey}_{userId}")
          .SetAsync(data)
          .ContinueWithOnMainThread(task =>
          {
              if (task.IsCompleted && !task.IsFaulted)
                  Debug.Log("[Firebase] Score saved");
              else
                  Debug.LogError($"[Firebase] Save failed: {task.Exception}");
          });
    }

    // ── Load ─────────────────────────────────────────────
    public void LoadScore()
    {
        if (!isInitialized) return;

        string userId = GetUserId();

        db.Collection("savedata")
          .Document(userId)
          .GetSnapshotAsync()
          .ContinueWithOnMainThread(task =>
          {
              if (task.IsFaulted)
              {
                  Debug.LogError($"[Firebase] Load failed: {task.Exception}");
                  return;
              }

              DocumentSnapshot snap = task.Result;
              if (snap.Exists)
              {
                  var data = snap.ToDictionary();
                  long score = (long)data["score"];
                  GameManager.Instance.Cats = score; // TODO: call UpdateUI() if needed;
                  Debug.Log($"[Firebase] Loaded score: {score}");
              }
              else
              {
                  Debug.Log("[Firebase] No save data found — fresh start");
              }
          });
    }

    // ── Leaderboard Fetch ─────────────────────────────────
    public void FetchLeaderboard(Action<List<LeaderboardEntry>> onComplete)
    {
        if (!isInitialized)
        {
            onComplete?.Invoke(null);
            return;
        }

        string weekKey = GetWeekKey();

        db.Collection("leaderboard")
          .WhereEqualTo("weekKey", weekKey)
          .OrderByDescending("score")
          .Limit(50)
          .GetSnapshotAsync()
          .ContinueWithOnMainThread(task =>
          {
              if (task.IsFaulted)
              {
                  Debug.LogError($"[Firebase] Leaderboard fetch failed: {task.Exception}");
                  onComplete?.Invoke(null);
                  return;
              }

              var entries = new List<LeaderboardEntry>();
              int rank = 1;
              foreach (var doc in task.Result.Documents)
              {
                  var data = doc.ToDictionary();
                  entries.Add(new LeaderboardEntry
                  {
                      rank     = rank++,
                      userId   = data["userId"].ToString(),
                      username = data["username"].ToString(),
                      score    = (long)data["score"]
                  });
              }

              onComplete?.Invoke(entries);
          });
    }

    // ── Helpers ───────────────────────────────────────────

    /// <summary>Returns a persistent unique ID for this player.</summary>
    public string GetUserId()
    {
        if (!PlayerPrefs.HasKey("userId"))
            PlayerPrefs.SetString("userId", Guid.NewGuid().ToString());
        return PlayerPrefs.GetString("userId");
    }

    /// <summary>Returns "2025-W03" style key so leaderboard resets weekly.</summary>
    public static string GetWeekKey()
    {
        DateTime now = DateTime.UtcNow;
        int week = System.Globalization.ISOWeek.GetWeekOfYear(now);
        return $"{now.Year}-W{week:D2}";
    }
}

// ── Data Model ────────────────────────────────────────────
[Serializable]
public class LeaderboardEntry
{
    public int    rank;
    public string userId;
    public string username;
    public long   score;
}
