using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private AudioClip bulletClip;
    [SerializeField] private float bulletSpeed = 1000;
    [SerializeField] private int damage = 1;
    private Rigidbody2D rb;

    internal void Shoot(Vector3 forward)
    {
        SMScript.Instance.PlayShootingClip();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(forward * bulletSpeed);
        Debug.Log("Added force to bullet" + forward * bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == gameObject.tag) { Debug.Log("ignored myself bullet"); return; }

        TryDamage(collision);

        Debug.Log("Bullet hit " + collision.gameObject.name);
        Destroy(gameObject);    
    }

    private void TryDamage(Collider2D collision)
    {
        IDamagable damagable = collision.GetComponent<IDamagable>();

        if (damagable != null)
        {
            damagable.TakeDamage(damage);
        }
    }
}
