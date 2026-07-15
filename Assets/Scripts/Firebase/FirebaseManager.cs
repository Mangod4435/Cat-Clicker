using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }

    // ── Firebase Config ───────────────────────────────────
    private const string PROJECT_ID = "cat-clicker-base-by-mangod";
    private const string API_KEY    = "AIzaSyAoj8kSPAL72okjUbSWYZxjuucQFaXftBk";
    private const string BASE_URL   = "https://firestore.googleapis.com/v1/projects/"
                                    + PROJECT_ID + "/databases/(default)/documents";

    // ── Save Timer ────────────────────────────────────────
    private const float SAVE_INTERVAL = 300f; // 5 นาที
    private float saveTimer = 0f;

    // ── Unity Lifecycle ───────────────────────────────────
    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        LoadScore();
    }

    void Update()
    {
        saveTimer += Time.deltaTime;
        if (saveTimer >= SAVE_INTERVAL)
        {
            saveTimer = 0f;
            SaveScore();
        }
    }

    // ── Save Score ────────────────────────────────────────
    public void SaveScore()
    {
        StartCoroutine(SaveScoreRoutine());
    }

    IEnumerator SaveScoreRoutine()
    {
        string userId  = GetUserId();
        string weekKey = GetWeekKey();
        string username = PlayerPrefs.GetString("username", "Player");
        double score   = GameManager.Instance.Cats;

        string url  = $"{BASE_URL}/leaderboard/{weekKey}_{userId}?key={API_KEY}";
        string body = $@"{{
            ""fields"": {{
                ""userId"":   {{""stringValue"": ""{userId}""}},
                ""username"": {{""stringValue"": ""{username}""}},
                ""score"":    {{""doubleValue"": {score}}},
                ""weekKey"":  {{""stringValue"": ""{weekKey}""}}
            }}
        }}";

        using var req = new UnityWebRequest(url, "PATCH");
        req.uploadHandler   = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(body));
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
            Debug.Log("[Firebase] Score saved ✅");
        else
            Debug.LogError($"[Firebase] Save failed: {req.error}");
    }

    // ── Load Score ────────────────────────────────────────
    public void LoadScore()
    {
        StartCoroutine(LoadScoreRoutine());
    }

    IEnumerator LoadScoreRoutine()
    {
        string userId = GetUserId();
        // โหลดจาก savedata collection แยกต่างหาก
        string url = $"{BASE_URL}/savedata/{userId}?key={API_KEY}";

        using var req = UnityWebRequest.Get(url);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("[Firebase] No save data found — fresh start");
            yield break;
        }

        var json = SimpleJSON.JSON.Parse(req.downloadHandler.text);
        double score = json["fields"]["score"]["doubleValue"].AsDouble;
        GameManager.Instance.Cats = score;
        Debug.Log($"[Firebase] Loaded score: {score} ✅");
    }

    // ── Fetch Leaderboard ─────────────────────────────────
    public void FetchLeaderboard(Action<List<LeaderboardEntry>> onComplete)
    {
        StartCoroutine(FetchLeaderboardRoutine(onComplete));
    }

    IEnumerator FetchLeaderboardRoutine(Action<List<LeaderboardEntry>> onComplete)
    {
        string weekKey = GetWeekKey();

        // Firestore REST query
        string url  = $"https://firestore.googleapis.com/v1/projects/{PROJECT_ID}/databases/(default)/documents:runQuery?key={API_KEY}";
        string body = $@"{{
            ""structuredQuery"": {{
                ""from"": [{{""collectionId"": ""leaderboard""}}],
                ""where"": {{
                    ""fieldFilter"": {{
                        ""field"": {{""fieldPath"": ""weekKey""}},
                        ""op"": ""EQUAL"",
                        ""value"": {{""stringValue"": ""{weekKey}""}}
                    }}
                }},
                ""orderBy"": [{{
                    ""field"": {{""fieldPath"": ""score""}},
                    ""direction"": ""DESCENDING""
                }}],
                ""limit"": 50
            }}
        }}";

        using var req = new UnityWebRequest(url, "POST");
        req.uploadHandler   = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(body));
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"[Firebase] Fetch failed: {req.error}");
            onComplete?.Invoke(null);
            yield break;
        }

        var entries = new List<LeaderboardEntry>();
        var json    = SimpleJSON.JSON.Parse(req.downloadHandler.text);
        int rank    = 1;

        foreach (var item in json.AsArray)
        {
            var fields = item.Value["document"]["fields"];
            if (fields == null) continue;

            entries.Add(new LeaderboardEntry
            {
                rank     = rank++,
                userId   = fields["userId"]["stringValue"],
                username = fields["username"]["stringValue"],
                score    = (long)fields["score"]["doubleValue"].AsDouble
            });
        }

        onComplete?.Invoke(entries);
    }

    // ── Helpers ───────────────────────────────────────────
    public string GetUserId()
    {
        if (!PlayerPrefs.HasKey("userId"))
            PlayerPrefs.SetString("userId", Guid.NewGuid().ToString());
        return PlayerPrefs.GetString("userId");
    }

    public static string GetWeekKey()
    {
        DateTime now  = DateTime.UtcNow;
        int week      = System.Globalization.ISOWeek.GetWeekOfYear(now);
        return $"{now.Year}-W{week:D2}";
    }
}

[Serializable]
public class LeaderboardEntry
{
    public int    rank;
    public string userId;
    public string username;
    public long   score;
}
