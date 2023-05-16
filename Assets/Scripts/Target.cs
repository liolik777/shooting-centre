using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Transform center;
	[SerializeField] private float intervalBtwRings;
	[SerializeField] private List<int> scores;
#if UNITY_EDITOR
	[SerializeField] private bool enableDebug = true;
	[SerializeField] private float debugAngle;
	[SerializeField] private Color debugColor;
#endif
	
	public int GetScore(Vector3 hitPoint)
	{
		float distance = Vector3.Distance(hitPoint, center.position);
		
		int ring = -1;
		if (distance / intervalBtwRings > Mathf.Floor(distance / intervalBtwRings))
			ring = (int)Mathf.Floor(distance / intervalBtwRings) + 1;
		else
			ring = (int)Mathf.Floor(distance / intervalBtwRings);
		
		int score = scores[ring];
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
			Vector3 previousPoint = center.position + Quaternion.Euler(0f, debugAngle, 0f) * Vector3.right * intervalBtwRings * i - center.right * 0.1f;
			for (int j = 1; j <= numSegments; j++)
			{
				float angle = j * angleStep;
				Vector3 nextPoint = center.position + Quaternion.Euler(0f, debugAngle, angle) * Vector3.right * intervalBtwRings * i - center.right * 0.1f;
				Debug.DrawLine(previousPoint, nextPoint, debugColor);
				previousPoint = nextPoint;
			}
		}
    }
}