﻿using UnityEngine;

public class StaticTarget : Target
{
    [SerializeField] private Scoreboard scoreboard;
    
    public override int GetScore(Vector3 hitPoint, Transform centerPoint = null)
    {
        int score = base.GetScore(hitPoint);
        if (scoreboard != null)
            scoreboard.AddScore(score);
        return score;
    }
}