using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ติดกับ prefab แต่ละแถวใน leaderboard
/// </summary>
public class LeaderboardEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private TextMeshProUGUI usernameText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image background;

    [Header("Colors")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color myColor     = new Color(1f, 0.95f, 0.6f); // เหลืองอ่อน

    public void Setup(LeaderboardEntry entry, bool isMe)
    {
        rankText.text     = $"#{entry.rank}";
        usernameText.text = isMe ? $"{entry.username} (You)" : entry.username;
        scoreText.text    = entry.score.ToString("N0"); // 1,234,567

        background.color  = isMe ? myColor : normalColor;
    }
}
