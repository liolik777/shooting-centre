using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Transform center;
	[SerializeField] private int intervalBtwRings;
	[SerializeField] private List<int> scores;
	
	public int GetScore(Vector3 hitPoint)
	{
		float distance = Vector3.Distance(hitPoint, center.position);
		
		int ring = -1;
		if (distance / intervalBtwRings > Mathf.Floor(distance / intervalBtwRings))
			ring = Mathf.Floor(distance / intervalBtwRings) + 1;
		else
			ring = Mathf.Floor(distance / intervalBtwRings);
		
		int score = scores[^ring];
		return score;
	}
	
	public void OnDrawGizmosSelected()
    {
		Gizmos.DrawWireSphere(center.position, intervalBtwRings);
    }
}