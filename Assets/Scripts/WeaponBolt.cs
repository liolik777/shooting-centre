using UnityEngine;
using Valve.VR.InteractionSystem;

public class WeaponBolt : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
	[SerializeField] private LinearMapping linearMapping;
	[SerializeField] private Interactable body;
	[SerializeField] private Transform startPosition;
	[SerializeField] private Transform endPosition;
	[SerializeField] private float duration;
	[SerializeField] private AnimationCurve curve;
	private bool _shutterIsDistorted;
	
	private void Start()
	{
		body.onDetachedFromHand += OnDetachedFromHand;
		body.onAttachedToHand += OnAttachedToHand;
	}
	
	private void OnDetachedFromHand(Hand hand)
	{
		if (linearMapping.value == 1)
			weapon.DistorteShutter();
		
		LeanTween.value(body.gameObject, body.transform.position, startPosition.position, duration).setEase(curve)
			.setOnUpdate(
				(Vector3 value) =>
				{
					Transform bodyTransform = body.transform;
					bodyTransform.position = value;
					linearMapping.value = CalculateLinearMapping(bodyTransform);
				});
	}
	
	private void OnAttachedToHand(Hand hand)
	{
		
	}
	
	private float CalculateLinearMapping(Transform updateTransform)
	{
		Vector3 direction = endPosition.position - startPosition.position;
		float length = direction.magnitude;
		direction.Normalize();

		Vector3 displacement = updateTransform.position - startPosition.position;

		return Vector3.Dot( displacement, direction ) / length;
	}
}
