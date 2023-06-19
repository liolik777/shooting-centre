using System.Collections.Generic;
using UnityEngine;

public class MoveingTarget : Target
{
    [SerializeField] private Transform centerHead;
    [SerializeField] private float headIntervalBtwRings;
    [SerializeField] private List<int> headScores;
    [SerializeField] private LeanTweenData<Transform> knockedAnimation;
    [SerializeField] private float moveingDuration;
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    private Vector3 targetPosition;
    private bool _isKnocked;
	
    public override int GetScore(Vector3 hitPoint, Transform centerPoint = null, List<int> scoresList = null)
    {
        if (_isKnocked)
            return 0;

        _isKnocked = true;
        LeanTween.value(knockedAnimation.targetGameObject.gameObject, knockedAnimation.startValue, knockedAnimation.targetValue, knockedAnimation.duration).setEase(knockedAnimation.curve)
            .setOnUpdate((float value) =>
            {
                knockedAnimation.targetGameObject.rotation = Quaternion.Euler(knockedAnimation.targetGameObject.rotation.x, knockedAnimation.targetGameObject.rotation.y, value);
			});
        
        float distanceHead = Vector3.Distance(hitPoint, centerHead.position);
        float distanceBody = Vector3.Distance(hitPoint, Center.position);

        int score = 0;
        
        if (distanceHead < distanceBody)
            score = base.GetScore(hitPoint, centerHead, headScores);
        else
            score = base.GetScore(hitPoint, Center);
        FindObjectOfType<ToysShop>().AddBalance(score);
        return score;
    }
    
    private void Start()
    {
        MoveToEnd();
    }

    private void MoveToEnd()
    {
        LeanTween.value(gameObject, startPosition.position, endPosition.position, moveingDuration).setOnUpdate(
            (Vector3 value) =>
            {
				Vector3 newPosition = new Vector3(value.x, transform.position.y, value.z);
				transform.position = newPosition;
            }).setOnComplete(MoveToStart);
    }
    
    private void MoveToStart()
    {
        LeanTween.value(gameObject, endPosition.position, startPosition.position, moveingDuration).setOnUpdate(
            (Vector3 value) =>
            {
                Vector3 newPosition = new Vector3(value.x, transform.position.y, value.z);
				transform.position = newPosition;
            }).setOnComplete(MoveToEnd);
    }
    
    private void OnDrawGizmosSelected()
    {
		if (startPosition == null || endPosition == null)
			return;
		
        Debug.DrawLine(startPosition.position, endPosition.position, Color.green);
    }
    
    public void OnDrawGizmos()
    {
        if (centerHead == null || !EnableDebug)
            return;
		
        int numSegments = 32;
        float angleStep = 360f / numSegments;
        for (int i = 0; i < headScores.Count; i++)
        {
            Vector3 previousPoint = centerHead.position + Quaternion.Euler(0f, DebugAngle, 0f) * Vector3.right * headIntervalBtwRings * (i + 1) - centerHead.right * 0.1f;
            for (int j = 1; j <= numSegments; j++)
            {
                float angle = j * angleStep;
                Vector3 nextPoint = centerHead.position + Quaternion.Euler(0f, DebugAngle, angle) * Vector3.right * headIntervalBtwRings * (i + 1) - centerHead.right * 0.1f;
                Debug.DrawLine(previousPoint, nextPoint, Color.red);
                previousPoint = nextPoint;
            }
        }
    }
}