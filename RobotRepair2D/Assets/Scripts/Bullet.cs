using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private AudioClip bulletClip;
    [SerializeField] private float bulletSpeed = 1000;
    private Rigidbody2D rb;

    internal void Shoot(Vector3 forward)
    {
        AudioSource.PlayClipAtPoint(bulletClip, transform.position);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(forward * bulletSpeed);
        Debug.Log("Added force to bullet" + forward * bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bullet hit " + collision.gameObject.name);
        Destroy(gameObject);    
    }
}
