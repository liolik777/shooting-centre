using System.Collections.Generic;
using UnityEngine;

public class StaticTarget : Target
{
    [SerializeField] private Scoreboard scoreboard;
	[SerializeField] private int limitOfScore;
    private int _totalScore = 0;
	
    public override int GetScore(Vector3 hitPoint, Transform centerPoint = null, List<int> scoresList = null)
    {
        int score = base.GetScore(hitPoint);
		if (_totalScore >= limitOfScore)
		{
			scoreboard?.Limit();
			return 0;
		}
		
		_totalScore += score;
        scoreboard?.SetScore(score);
        FindObjectOfType<ToysShop>().AddBalance(score);
        return score;
    }
}