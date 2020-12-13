using System;

[Serializable]
public class PlayerData
{
    uint totalScore;
    float farestHit;
    float closestHit;
    uint bestSilce;
    uint fastestDestruction;
    uint hitInRow;

    public PlayerData(uint _totalScore, float _farestHit, float _closestHit, uint _bestSilce, uint _fastestDestruction, uint _hitInRow)
    {
        totalScore = _totalScore;
        farestHit = _farestHit;
        closestHit = _closestHit;
        bestSilce = _bestSilce;
        fastestDestruction = _fastestDestruction;
        hitInRow = _hitInRow;
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