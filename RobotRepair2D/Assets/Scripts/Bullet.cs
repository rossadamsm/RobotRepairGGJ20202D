using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 1000;
    private Rigidbody2D rb;

    internal void Shoot(Vector3 forward)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(forward * bulletSpeed);
        Debug.Log("Added force to bullet" + forward * bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);    
    }
}
