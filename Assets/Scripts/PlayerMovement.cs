using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField, Range(0.05f, 1f)] private float minDistanceToStop;

    private Vector3 _newPosition;
    private bool _moveing;

    public void MovePlayer(Vector3 newPosition)
    {
        _newPosition = newPosition;
        _moveing = true;
    }

    private void Update()
    {
        if (!_moveing)
            return;

        transform.position = Vector3.Lerp(transform.position, _newPosition, speed);
        if (Vector3.Distance(transform.position, _newPosition) <= minDistanceToStop)
            _moveing = false;
    }
}
