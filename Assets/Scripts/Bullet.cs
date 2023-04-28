using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject dentPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 3)
            return;

        Debug.Log($"Пуля попала в объект {collision.gameObject.name}");
        Instantiate(dentPrefab, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
        Destroy(gameObject);
    }
}