using System.Collections.Generic;
using UnityEngine;

public class StaticTarget : Target
{
    [SerializeField] private Scoreboard scoreboard;
    
    public override int GetScore(Vector3 hitPoint, Transform centerPoint = null, List<int> scoresList = null)
    {
        int score = base.GetScore(hitPoint);
        if (scoreboard != null)
            scoreboard.SetScore(score);
        FindObjectOfType<ToysShop>().AddBalance(score);
        return score;
    }
}