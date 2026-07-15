using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardPanel : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform entryContainer;   // ScrollView Content
    [SerializeField] private GameObject entryPrefab;     // prefab สำหรับแต่ละแถว
    [SerializeField] private TextMeshProUGUI weekLabel;  // "Week 3 · 2025"
    [SerializeField] private TextMeshProUGUI statusText; // "Updating..." / "Updated X min ago"
    [SerializeField] private Button refreshButton;

    // ── Cache ─────────────────────────────────────────────
    private List<LeaderboardEntry> cachedEntries;
    private float lastFetchTime = -999f;
    private const float CACHE_DURATION = 300f; // 5 นาที

    // ── Unity Events ──────────────────────────────────────
    void OnEnable()
    {
        // เรียกทุกครั้งที่เปิด panel
        TryFetch();
    }

    void Start()
    {
        refreshButton.onClick.AddListener(OnRefreshClicked);
        UpdateWeekLabel();
    }

    // ── Fetch Logic ───────────────────────────────────────
    void TryFetch()
    {
        bool hasCache   = cachedEntries != null;
        bool cacheValid = Time.time - lastFetchTime < CACHE_DURATION;

        if (hasCache && cacheValid)
        {
            // แสดง cache เดิม ไม่ดึง Firebase
            float minsAgo = (Time.time - lastFetchTime) / 60f;
            SetStatus($"Updated {Mathf.FloorToInt(minsAgo)} min ago");
            DisplayEntries(cachedEntries);
            return;
        }

        // ดึงใหม่
        FetchFromFirebase();
    }

    void FetchFromFirebase()
    {
        SetStatus("Updating...");
        refreshButton.interactable = false;

        FirebaseManager.Instance.FetchLeaderboard(entries =>
        {
            refreshButton.interactable = true;

            if (entries == null)
            {
                SetStatus("Failed to load. Try again.");
                return;
            }

            cachedEntries = entries;
            lastFetchTime = Time.time;

            SetStatus("Just updated");
            DisplayEntries(cachedEntries);
        });
    }

    // ── Display ───────────────────────────────────────────
    void DisplayEntries(List<LeaderboardEntry> entries)
    {
        // Clear เดิม
        foreach (Transform child in entryContainer)
            Destroy(child.gameObject);

        string myId = FirebaseManager.Instance.GetUserId();

        foreach (var entry in entries)
        {
            GameObject row = Instantiate(entryPrefab, entryContainer);
            var ui = row.GetComponent<LeaderboardEntryUI>();
            ui.Setup(entry, isMe: entry.userId == myId);
        }
    }

    // ── Refresh Button ────────────────────────────────────
    void OnRefreshClicked()
    {
        float timeSinceFetch = Time.time - lastFetchTime;

        if (timeSinceFetch < CACHE_DURATION)
        {
            // ยังไม่ถึง 5 นาที — บอกผู้เล่น
            float minsLeft = (CACHE_DURATION - timeSinceFetch) / 60f;
            SetStatus($"Refresh in {Mathf.CeilToInt(minsLeft)} min");
            return;
        }

        FetchFromFirebase();
    }

    // ── Helpers ───────────────────────────────────────────
    void SetStatus(string msg)
    {
        if (statusText != null)
            statusText.text = msg;
    }

    void UpdateWeekLabel()
    {
        if (weekLabel != null)
        {
            string weekKey = FirebaseManager.GetWeekKey(); // e.g. "2025-W03"
            weekLabel.text = $"Leaderboard · {weekKey}";
        }
    }
}
