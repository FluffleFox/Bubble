using UnityEngine;

[CreateAssetMenu(fileName = "AchievementData", menuName = "Achievement", order = 1)]
public class Achievement : ScriptableObject
{
    public Sprite lockedIcon;
    public Sprite unlockedIcon;

    public string tittle;
    public string smallDescriptionWhenLocked;
    public string smallDescriptionWhenUnlocked;
    public string fullDescriptionWhenLocked;
    public string fullDescriptionWhenUnlocked;

    public enum requiereType{TotalScore, FarestHit, ClosestHit, BestSilce, FastestDestruction, HitInRow};
    public requiereType requiere;
    public float stepValue;

    public bool CheeckUnlock()
    {
        switch (requiere)
        {
            case requiereType.TotalScore: { if (PlayerRecords.instance.GetTotalScore() >= stepValue) return true; else return false; }
            case requiereType.FarestHit: { if (PlayerRecords.instance.GetFarestHit() >= stepValue) return true; else return false; }
            case requiereType.ClosestHit: { if (PlayerRecords.instance.GetClosestHit() <= stepValue) return true; else return false; }
            case requiereType.BestSilce: { if (PlayerRecords.instance.GetBestSilce() >= stepValue) return true; else return false; }
            case requiereType.FastestDestruction: { if (PlayerRecords.instance.GetFastestDestruction() >= stepValue) return true; else return false; }
            case requiereType.HitInRow: { if (PlayerRecords.instance.GetHitInRow() >= stepValue) return true; else return false; }
            default: return false;
        }
    }
}
