using UnityEngine;
using System.Collections.Generic;
using Valve.VR.InteractionSystem;

public class MovableTarget : Target
{
    [SerializeField] private Scoreboard scoreboard;
    [SerializeField] private LinearMapping linearMapping;
    [SerializeField] private float speed;
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
	[SerializeField] private int limitOfScore;
    private Vector3 targetPosition;

	private int _totalScore;

    private void Update()
    {
        SmoothMove();
    }

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

    private void SmoothMove()
    { 
        targetPosition = ((endPosition.position - startPosition.position) * linearMapping.value) + startPosition.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
		if (startPosition == null || endPosition == null)
			return;
		
        Debug.DrawLine(startPosition.position, endPosition.position, Color.red);
    }
}