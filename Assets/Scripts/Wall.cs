using UnityEngine;
using Valve.VR.InteractionSystem;

public class Wall : MonoBehaviour
{
    private enum RepulsionDirection
    {
        X,
        Z
    }

    [SerializeField] private RepulsionDirection repulsionDirection;
    private const float moveCoef = 0.3f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerCamera"))
        {
            Transform player = FindObjectOfType<Player>().transform;
            Transform playerCamera = collision.transform;
            switch (repulsionDirection)
            {
                case RepulsionDirection.X:
                    if (playerCamera.transform.position.x > transform.position.x)
                        MovePlayer(transform.right);
                    else
                        MovePlayer(-transform.right);
                    break;
                case RepulsionDirection.Z:
                    if (playerCamera.transform.position.z < transform.position.z)
                        MovePlayer(transform.right);
                    else
                        MovePlayer(-transform.right);
                    break;
            }

            void MovePlayer(Vector3 direction)
            {
                Vector3 newPosition = player.transform.position + direction * moveCoef;
                newPosition.y = 0;
                player.GetComponent<PlayerMovement>().MovePlayer(newPosition);
            }
        }
    }
}