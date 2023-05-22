using UnityEngine;

public class MoveingTarget : Target
{
    [SerializeField] private LeanTweenData<Transform> knockedAnimation;
    [SerializeField] private float moveingDuration;
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    private Vector3 targetPosition;
    private bool _isKnocked;
	
    public override int GetScore(Vector3 hitPoint)
    {
        if (_isKnocked)
            return 0;

        _isKnocked = true;
        LeanTween.value(knockedAnimation.targetGameObject.gameObject, knockedAnimation.startValue, knockedAnimation.targetValue, knockedAnimation.duration).setEase(knockedAnimation.curve)
            .setOnUpdate((float value) =>
            {
                knockedAnimation.targetGameObject.rotation = Quaternion.Euler(knockedAnimation.targetGameObject.rotation.x, knockedAnimation.targetGameObject.rotation.y, value);
			});
        return base.GetScore(hitPoint);
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
}