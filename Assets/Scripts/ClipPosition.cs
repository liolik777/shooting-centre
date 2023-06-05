using UnityEngine;
using Valve.VR.InteractionSystem;

public class ClipPosition : MonoBehaviour
{
	[SerializeField] private Weapon weapon;
	[SerializeField] private GameObject simulatedClip;
	private Clip injectedClip;
	
	private void Start()
	{
		simulatedClip.GetComponent<Interactable>().onAttachedToHand += EjectClip;
	}
	
	private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Clip"))
		{
			other.gameObject.GetComponent<Interactable>().attachedToHand?.DetachObject(other.gameObject);
			InjectClip(other.gameObject);
		}
    }
	
	public void OnDetachedFromHand()
	{
		simulatedClip.SetActive(false);
	}
	
    public void InjectClip(GameObject clip)
	{
		injectedClip = clip.GetComponent<Clip>();
		simulatedClip.SetActive(true);
		clip.transform.SetParent(transform);
		clip.transform.localPosition = Vector3.zero;
		clip.transform.localRotation = new Quaternion(0, 0, 0, 0);
		clip.SetActive(false);
		
		weapon.InjectClip(clip.GetComponent<Clip>());
	}
	
	private void EjectClip(Hand hand)
	{
		hand.DetachObject(simulatedClip);
		
		injectedClip.gameObject.SetActive(true);
		hand.AttachObject(injectedClip.gameObject, GrabTypes.Trigger);
		injectedClip.transform.rotation = simulatedClip.transform.rotation;
		
		injectedClip = null;
		weapon.EjectClip();
	}
}
