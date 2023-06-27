using UnityEngine;

public class ShootSimulation : MonoBehaviour
{
    [SerializeField] private Target target;
	
	[ContextMenu("Simulate")]
	public void Simulate()
	{
		Vector3 hitPoint = target.center.position;
		target.GetScore(hitPoint, null, null);
	}
}
