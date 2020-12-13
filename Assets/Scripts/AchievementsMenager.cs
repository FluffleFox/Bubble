using UnityEngine;

public class AchievementsMenager : MonoBehaviour
{
    public static AchievementsMenager instance;
    public Achievement[] allAchievements;
    Achievement[] lockedTotalScoreAchievements;
    Achievement[] lockedFarestHitAchievements;
    Achievement[] lockedClosestHitAchievements;
    Achievement[] lockedBestSilceAchievements;
    Achievement[] lockedFastestDestructionAchievements;
    Achievement[] lockedHitInRowAchievements;



    public GameObject AchievementPrefab;
    public Transform AchievementParent;
    int unlocked = 0;

    public float completePercent;
    private void Start()
    {
        instance = this;
        Setup();
    }
    void Setup()
    {
        int lockedTotalScoreAchievementsLenght = 0;
        int lockedFarestHitAchievementsLenght =  0;
        int lockedClosestHitAchievementsLenght = 0;
        int lockedBestSilceAchievementsLenght =  0;
        int lockedFastestDestructionAchievementsLenght = 0;
        int lockedHitInRowAchievementsLenght  =  0;
        for (int i=0; i<allAchievements.Length; i++)
        {
            GameObject GO = (GameObject)Instantiate(AchievementPrefab);
            GO.transform.SetParent(AchievementParent);
            GO.GetComponent<AchievementDisplay>().Setup(allAchievements[i]);
            if (!allAchievements[i].CheeckUnlock()) 
            {
                switch (allAchievements[i].requiere)
                {
                    case Achievement.requiereType.TotalScore: { lockedTotalScoreAchievementsLenght++; break; }
                    case Achievement.requiereType.FarestHit:  { lockedFarestHitAchievementsLenght++; break; }
                    case Achievement.requiereType.ClosestHit: { lockedClosestHitAchievementsLenght++; break; }
                    case Achievement.requiereType.BestSilce:  { lockedBestSilceAchievementsLenght++; break; }
                    case Achievement.requiereType.FastestDestruction: { lockedFastestDestructionAchievementsLenght++; break; }
                    case Achievement.requiereType.HitInRow:   { lockedHitInRowAchievementsLenght++; break; }
                }
            }
            else
            {
                unlocked++;
            }
        }

        unlocked--; //because update interval always add one
        UpdateInterval();

        lockedTotalScoreAchievements = new Achievement[lockedTotalScoreAchievementsLenght];
        lockedFarestHitAchievements =  new Achievement[lockedFarestHitAchievementsLenght];
        lockedClosestHitAchievements = new Achievement[lockedClosestHitAchievementsLenght];
        lockedBestSilceAchievements =  new Achievement[lockedBestSilceAchievementsLenght];
        lockedFastestDestructionAchievements = new Achievement[lockedFastestDestructionAchievementsLenght];
        lockedHitInRowAchievements =   new Achievement[lockedHitInRowAchievementsLenght];

        lockedTotalScoreAchievementsLenght = 0;
        lockedFarestHitAchievementsLenght =  0;
        lockedClosestHitAchievementsLenght = 0;
        lockedBestSilceAchievementsLenght =  0;
        lockedFastestDestructionAchievementsLenght = 0;
        lockedHitInRowAchievementsLenght  =  0;

        for (int i = 0; i < allAchievements.Length; i++)
        {
            if (!allAchievements[i].CheeckUnlock())
            {
                switch (allAchievements[i].requiere)
                {
                    case Achievement.requiereType.TotalScore: { lockedTotalScoreAchievements[lockedTotalScoreAchievementsLenght] = allAchievements[i]; lockedTotalScoreAchievementsLenght++; break; }
                    case Achievement.requiereType.FarestHit:  { lockedFarestHitAchievements[lockedFarestHitAchievementsLenght] = allAchievements[i]; lockedFarestHitAchievementsLenght++; break; }
                    case Achievement.requiereType.ClosestHit: { lockedClosestHitAchievements[lockedClosestHitAchievementsLenght] = allAchievements[i]; lockedClosestHitAchievementsLenght++; break; }
                    case Achievement.requiereType.BestSilce:  { lockedBestSilceAchievements[lockedBestSilceAchievementsLenght] = allAchievements[i]; lockedBestSilceAchievementsLenght++; break; }
                    case Achievement.requiereType.FastestDestruction: { lockedFastestDestructionAchievements[lockedFastestDestructionAchievementsLenght] = allAchievements[i]; lockedFastestDestructionAchievementsLenght++; break; }
                    case Achievement.requiereType.HitInRow:   { lockedHitInRowAchievements[lockedHitInRowAchievementsLenght] = allAchievements[i]; lockedHitInRowAchievementsLenght++; break; }
                }
            }
        }
    }

    public void Refresh()
    {
        for (int i = 0; i < allAchievements.Length; i++)
        {
            AchievementParent.GetChild(i).gameObject.GetComponent<AchievementDisplay>().Setup(allAchievements[i]);
        }
    }

    //Sprawdzanie osiągnięć w runtimie

    public void CheeckTotalScore(uint value)
    {
        if (lockedTotalScoreAchievements.Length == 0) return;
        bool another = true;
        while (another)
        {
            if (value >= lockedTotalScoreAchievements[0].stepValue)
            {
                ShowPopup(lockedTotalScoreAchievements[0]);
                Achievement[] tmp = new Achievement[lockedTotalScoreAchievements.Length - 1];
                for (int i = 1; i < lockedTotalScoreAchievements.Length; i++)
                {
                    tmp[i - 1] = lockedTotalScoreAchievements[i];
                }
                lockedTotalScoreAchievements = tmp;
                UpdateInterval();
            }
            else { another = false; }
        }
    }

    public void CheeckFarestHit(float value)
    {
        bool another = true;
        while (another && lockedFarestHitAchievements.Length>0)
        {
            if (value >= lockedFarestHitAchievements[0].stepValue)
            {
                ShowPopup(lockedFarestHitAchievements[0]);
                Achievement[] tmp = new Achievement[lockedFarestHitAchievements.Length - 1];
                for (int i = 1; i < lockedFarestHitAchievements.Length; i++)
                {
                    tmp[i - 1] = lockedFarestHitAchievements[i];
                }
                lockedFarestHitAchievements = tmp;
                UpdateInterval();
            }
            else { another = false; }
        }
    }

    public void CheeckClosestHit(float value)
    {
        bool another = true;
        while (another && lockedClosestHitAchievements.Length > 0)
        {
            if (value <= lockedClosestHitAchievements[0].stepValue)
            {
                ShowPopup(lockedClosestHitAchievements[0]);
                Achievement[] tmp = new Achievement[lockedClosestHitAchievements.Length - 1];
                for (int i = 1; i < lockedClosestHitAchievements.Length; i++)
                {
                    tmp[i - 1] = lockedClosestHitAchievements[i];
                }
                lockedClosestHitAchievements = tmp;
                UpdateInterval();
            }
            else { another = false; }
        }
    }

    public void CheeckBestSilce(uint value)
    {
        bool another = true;
        while (another && lockedBestSilceAchievements.Length>0)
        {
            if (value >= lockedBestSilceAchievements[0].stepValue)
            {
                ShowPopup(lockedBestSilceAchievements[0]);
                Achievement[] tmp = new Achievement[lockedBestSilceAchievements.Length - 1];
                for (int i = 1; i < lockedBestSilceAchievements.Length; i++)
                {
                    tmp[i - 1] = lockedBestSilceAchievements[i];
                }
                lockedBestSilceAchievements = tmp;
                UpdateInterval();      
            }
            else { another = false; }
        }
    }

    public void CheeckFastestDestruction(uint value)
    {
        bool another = true;
        while (another && lockedFastestDestructionAchievements.Length > 0)
        {
            if (value >= lockedFastestDestructionAchievements[0].stepValue)
            {
                ShowPopup(lockedFastestDestructionAchievements[0]);
                Achievement[] tmp = new Achievement[lockedFastestDestructionAchievements.Length - 1];
                for (int i = 1; i < lockedFastestDestructionAchievements.Length; i++)
                {
                    tmp[i - 1] = lockedFastestDestructionAchievements[i];
                }
                lockedFastestDestructionAchievements = tmp;
                UpdateInterval();
            }
            else { another = false; }
        }
    }

    public void CheeckHitInRow(uint value)
    {
        bool another = true;
        while (another && lockedHitInRowAchievements.Length > 0)
        {
            if (value >= lockedHitInRowAchievements[0].stepValue)
            {
                ShowPopup(lockedHitInRowAchievements[0]);
                Achievement[] tmp = new Achievement[lockedHitInRowAchievements.Length - 1];
                for (int i = 1; i < lockedHitInRowAchievements.Length; i++)
                {
                    tmp[i - 1] = lockedHitInRowAchievements[i];
                }
                lockedHitInRowAchievements = tmp;
                UpdateInterval();
            }
            else { another = false; }
        }
    }

    void UpdateInterval()
    {
        unlocked++;
        completePercent = (float)unlocked / (float)allAchievements.Length;
        Spawner.instance.UpdateInterval(1.0f / (1.0f + (6.0f * completePercent)));
    }

    //Wywoływanie popupów
    void ShowPopup(Achievement data)
    {
        Popup.instance.DisplayPopup(data);
    }
}
