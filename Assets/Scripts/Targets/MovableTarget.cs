using UnityEngine;
using Valve.VR.InteractionSystem;

public class MovableTarget : Target
{
    [SerializeField] private LinearMapping linearMapping;
    [SerializeField] private float speed;
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    private Vector3 targetPosition;

    private void Update()
    {
        SmoothMove();
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