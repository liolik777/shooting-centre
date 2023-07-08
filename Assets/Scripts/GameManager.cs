using UnityEngine;
using Valve.VR.InteractionSystem;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject playerPrefab;
	[SerializeField] private Transform spawnPosition;

	private void Start()
	{
		bool hasPlayer = FindObjectOfType<Player>() != null;
		if (!hasPlayer)
			Instantiate(playerPrefab, spawnPosition.position, spawnPosition.rotation);
	}
}