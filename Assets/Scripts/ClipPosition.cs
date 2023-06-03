using UnityEngine;
using Valve.VR.InteractionSystem;

public class ClipPosition : MonoBehaviour
{
	[SerializeField] private GameObject simulatedClip;
	private Clip injectedClip;
	
	private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Clip"))
		{
			other.gameObject.GetComponent<Interactable>().attachedToHand?.DetachObject(other.gameObject);
			simulatedClip.GetComponent<Interactable>().onAttachedToHand += EjectClip;
			InjectClip(other.gameObject);
		}
    }
	
	private void EjectClip(Hand hand)
	{
		hand.DetachObject(simulatedClip);
		simulatedClip.GetComponent<Interactable>().onAttachedToHand -= EjectClip;
		simulatedClip.SetActive(false);
		
		hand.AttachObject(injectedClip.gameObject, GrabTypes.Scripted);
		injectedClip.gameObject.SetActive(true);
		
		injectedClip = null;
	}
	
    public void InjectClip(GameObject clip)
	{
		injectedClip = clip.GetComponent<Clip>();
		simulatedClip.SetActive(true);
		clip.transform.SetParent(transform);
		clip.transform.localPosition = Vector3.zero;
		clip.transform.localRotation = new Quaternion(0, 0, 0, 0);
		clip.SetActive(false);
	}
}
