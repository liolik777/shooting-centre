using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Interactable))]
public class Shield : MonoBehaviour
{
	private Interactable interactable;

	private void Start()
	{
		interactable = this.GetComponent<Interactable>();
	}

	private void OnHandHoverBegin(Hand hand)
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}