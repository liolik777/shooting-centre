using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Target : MonoBehaviour
{
    [SerializeField] private Transform center;
	[SerializeField] private float intervalBtwRings;
	[SerializeField] private List<int> scores;
	[SerializeField] private TMP_Text scoreText;
	private int _totalScore;
#if UNITY_EDITOR
	[SerializeField] private bool enableDebug = true;
	[SerializeField] private float debugAngle;
#endif
	
	private void Start()
	{
		UpdateScoreText();
	}
	
	public virtual int GetScore(Vector3 hitPoint)
	{
		float distance = Vector3.Distance(hitPoint, center.position);
		int ring = (int)Mathf.Floor(distance / intervalBtwRings);
		int score = scores[ring];
		AddScore(score);
		return score;
	}
	
	public void AddScore(int score)
	{
		_totalScore += score;
		UpdateScoreText();
	}
	
	public void ResetScore()
	{
		_totalScore = 0;
		UpdateScoreText();
	}
	
	private void UpdateScoreText()
	{
		if (scoreText != null)
			scoreText.text = _totalScore.ToString();
	}
	
	public void OnDrawGizmos()
    {
		if (center == null || !enableDebug)
			return;
		
		int numSegments = 32;
		float angleStep = 360f / numSegments;
		for (int i = 0; i < scores.Count; i++)
        {
			Vector3 previousPoint = center.position + Quaternion.Euler(0f, debugAngle, 0f) * Vector3.right * intervalBtwRings * (i + 1) - center.right * 0.1f;
			for (int j = 1; j <= numSegments; j++)
			{
				float angle = j * angleStep;
				Vector3 nextPoint = center.position + Quaternion.Euler(0f, debugAngle, angle) * Vector3.right * intervalBtwRings * (i + 1) - center.right * 0.1f;
				Debug.DrawLine(previousPoint, nextPoint, Color.red);
				previousPoint = nextPoint;
			}
		}
    }
}