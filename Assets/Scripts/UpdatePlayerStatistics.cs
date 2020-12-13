using UnityEngine;
using UnityEngine.UI;

public class UpdatePlayerStatistics : MonoBehaviour
{
    public Text data;

    public void UpdateDisplayedData()
    {
        data.text = "Total score: " + PlayerRecords.instance.GetTotalScore().ToString() +
        "\n Record silce: " + PlayerRecords.instance.GetBestSilce().ToString() +
        "\n Closest bubble: " + PlayerRecords.instance.GetClosestHit().ToString() +
        "\n Farest bubble: " + PlayerRecords.instance.GetFarestHit().ToString() +
        "\n Record bps: " + PlayerRecords.instance.GetFastestDestruction().ToString() +
        "\n Record hit in row: " + PlayerRecords.instance.GetHitInRow().ToString() +
        "\n Total achievements: " + ((int)((float)AchievementsMenager.instance.allAchievements.Length * AchievementsMenager.instance.completePercent)).ToString()+"/"+(AchievementsMenager.instance.allAchievements.Length-1).ToString();
    }
}
