using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }

    // ── Firebase Config ───────────────────────────────────
    private const string PROJECT_ID = "cat-clicker-base-by-mangod";
    private const string API_KEY = "AIzaSyAoj8kSPAL72okjUbSWYZxjuucQFaXftBk";
    private const string BASE_URL =
        "https://firestore.googleapis.com/v1/projects/"
        + PROJECT_ID
        + "/databases/(default)/documents";

    private const float CLOUD_SAVE_INTERVAL = 300f;
    private float cloudSaveTimer = 0f;

    // ── Unity Lifecycle ───────────────────────────────────
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
        LoadCloudSave();
    }

    void Update()
    {
        cloudSaveTimer += Time.deltaTime;
        if (cloudSaveTimer >= CLOUD_SAVE_INTERVAL)
        {
            cloudSaveTimer = 0f;
            SaveCloudSave();
        }
    }

    public void SaveCloudSave()
    {
        StartCoroutine(SaveCloudSaveRoutine());
    }

    public void LoadCloudSave()
    {
        StartCoroutine(LoadCloudSaveRoutine());
    }

    IEnumerator SaveCloudSaveRoutine()
    {
        if (GameManager.Instance == null)
            yield break;

        string userId = GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogWarning("[Firebase] No userId available; skipping cloud save.");
            yield break;
        }

        string username = PlayerPrefs.GetString("username", string.Empty);
        string passHash = PlayerPrefs.GetString("passHash", string.Empty);
        string saveJson = JsonUtility.ToJson(GameManager.Instance.CreateSaveData());

        #region Requset Body Builder
        var bodyBuilder = new System.Text.StringBuilder();
        bodyBuilder.AppendLine("{");
        bodyBuilder.AppendLine("  \"fields\": {");
        bodyBuilder.Append("    \"userID\": {\"stringValue\": \"");
        bodyBuilder.Append(EscapeJson(userId));
        bodyBuilder.AppendLine("\"},");

        if (!string.IsNullOrEmpty(username))
        {
            bodyBuilder.Append("    \"username\": {\"stringValue\": \"");
            bodyBuilder.Append(EscapeJson(username));
            bodyBuilder.AppendLine("\"},");
        }

        if (!string.IsNullOrEmpty(passHash))
        {
            bodyBuilder.Append("    \"passHash\": {\"stringValue\": \"");
            bodyBuilder.Append(EscapeJson(passHash));
            bodyBuilder.AppendLine("\"},");
        }

        bodyBuilder.Append("    \"savedata\": {\"stringValue\": \"");
        bodyBuilder.Append(EscapeJson(saveJson));
        bodyBuilder.AppendLine("\"");
        bodyBuilder.AppendLine("  }");
        bodyBuilder.Append("}");

        string body = bodyBuilder.ToString();
        string url = $"{BASE_URL}/SAVE/{userId}?key={API_KEY}";
        #endregion

        using var req = new UnityWebRequest(url, "PATCH");
        req.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(body));
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
            Debug.Log("[Firebase] SAVE document saved");
        else
            Debug.LogError($"[Firebase] SAVE failed: {req.error}");
    }

    IEnumerator LoadCloudSaveRoutine()
    {
        if (GameManager.Instance == null)
            yield break;

        string userId = GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogWarning("[Firebase] No userId available; skipping cloud load.");
            yield break;
        }

        string url = $"{BASE_URL}/SAVE/{userId}?key={API_KEY}";

        using var req = UnityWebRequest.Get(url);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("[Firebase] No SAVE document found");
            yield break;
        }

        var json = SimpleJSON.JSON.Parse(req.downloadHandler.text);
        string username = json["fields"]["username"]?["stringValue"];
        string passHash = json["fields"]["passHash"]?["stringValue"];
        string saveJson = json["fields"]["savedata"]["stringValue"];

        if (!string.IsNullOrEmpty(username))
            PlayerPrefs.SetString("username", username);
        if (!string.IsNullOrEmpty(passHash))
            PlayerPrefs.SetString("passHash", passHash);

        SaveData saveData = JsonUtility.FromJson<SaveData>(saveJson);
        GameManager.Instance.ApplySaveData(saveData);
        GameManager.Instance.SaveGame();

        Debug.Log("[Firebase] SAVE document loaded ✧✦✧");
    }

    // ── Helpers ───────────────────────────────────────────
    public string GetUserId()
    {
        return PlayerPrefs.GetString("userId", string.Empty);
    }

    private static string EscapeJson(string value)
    {
        return value
            .Replace("\\", "\\\\")
            .Replace("\"", "\\\"")
            .Replace("\n", "\\n")
            .Replace("\r", "\\r")
            .Replace("\t", "\\t");
    }
}
