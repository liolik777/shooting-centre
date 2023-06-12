using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Transform center;
	[SerializeField] private float intervalBtwRings;
	[SerializeField] private List<int> scores;

	[SerializeField] private bool enableDebug = true;
	[SerializeField] private float debugAngle;

	protected Transform Center => center;
	protected bool EnableDebug => enableDebug;
	protected float DebugAngle => debugAngle;
	
	public virtual int GetScore(Vector3 hitPoint, Transform centerPoint = null, List<int> scoresList = null)
	{
		centerPoint ??= center;
		scoresList ??= scores;
		float distance = Vector3.Distance(hitPoint, centerPoint.position);
		Debug.Log(distance);
		int ring = (int)Mathf.Floor(distance / intervalBtwRings);
		int score = scoresList[ring];
		return score;
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