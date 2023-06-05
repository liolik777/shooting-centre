using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class SimulatedClipInteractable : Interactable
{
	[SerializeField] private UnityEvent onDetachedFromHand;
	
	protected override void OnAttachedToHand(Hand hand)
	{
		base.OnAttachedToHand(hand);
		attachedToHand = null;
	}
	
    protected override void OnDetachedFromHand(Hand hand)
	{
		base.OnDetachedFromHand(hand);
		onDetachedFromHand?.Invoke();
	}
}
