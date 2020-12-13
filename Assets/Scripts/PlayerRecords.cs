using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerRecords : MonoBehaviour
{
    public static PlayerRecords instance;
    uint totalScore;
    float farestHit;
    float closestHit;
    uint bestSilce;
    uint fastestDestruction;
    uint hitInRow;


    //file organization
    private void Awake()
    {
        instance = this;
        LoadData();
    }

    void LoadData()
    {
        string path = Application.persistentDataPath + "/Save.fun";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Open);

            PlayerData data = bf.Deserialize(file) as PlayerData;
            totalScore = data.GetTotalScore();
            farestHit = data.GetFarestHit(); 
            closestHit = data.GetClosestHit();
            bestSilce = data.GetBestSilce();
            fastestDestruction = data.GetFastestDestruction();
            hitInRow = data.GetHitInRow();
            file.Close();
        }
        else
        {
            totalScore = 0;
            farestHit = 10;
            closestHit = 5;
            bestSilce = 0;
            fastestDestruction = 0;
            hitInRow = 0;
            SaveData();
        }

    }

    public void SaveData()
    {
        string path = Application.persistentDataPath + "/Save.fun";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        PlayerData data = new PlayerData(totalScore, farestHit, closestHit, bestSilce, fastestDestruction,hitInRow);
        bf.Serialize(file, data);
        file.Close();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    //variable organization
    public void AddScore()
    {
        totalScore++;
        AchievementsMenager.instance.CheeckTotalScore(totalScore);
    }
    public void SetSilce(uint result)
    {
        if(result>bestSilce) 
        { 
            bestSilce = result;
            AchievementsMenager.instance.CheeckBestSilce(bestSilce);
        }
    }
    public void SetFarestOrClosestPoint(float distance)
    {
        if(distance>farestHit) 
        { 
            farestHit = distance;
            AchievementsMenager.instance.CheeckFarestHit(farestHit);
        }
        else if(distance<closestHit)
        { 
            closestHit = distance;
            AchievementsMenager.instance.CheeckClosestHit(closestHit);
        }
    }

    public void SetDestructionSpeed(uint value)
    {
        if (value > fastestDestruction) 
        { 
            fastestDestruction = value;
            AchievementsMenager.instance.CheeckFastestDestruction(fastestDestruction);
        }
    }

    public void SetHitInRow(uint value)
    {
        if (value > hitInRow)
        {
            hitInRow = value;
            AchievementsMenager.instance.CheeckHitInRow(hitInRow);
        }
    }

    public uint GetTotalScore()
    {
        return totalScore;
    }

    public float GetFarestHit()
    {
        return farestHit;
    }

    public float GetClosestHit()
    {
        return closestHit;
    }

    public uint GetBestSilce()
    {
        return bestSilce;
    }
    public uint GetFastestDestruction()
    {
        return fastestDestruction;
    }

    public uint GetHitInRow()
    {
        return hitInRow;
    }
}
