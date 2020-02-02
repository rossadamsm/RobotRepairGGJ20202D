using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDamagePowerup : MonoBehaviour
{
    private int startDamage;
    private bool applied = false;
    private float lifetime = 10f;

    private GameObject target;
    public void ApplyEffect()
    {
        //Double the damage
        Bullet b = target.GetComponent<Bullet>();
        startDamage = b.damage;
        b.AddDamage(b.damage);
        applied = true;
    }

    void FixedUpdate() {
        if (applied) {
            lifetime -= Time.fixedDeltaTime;
            if (lifetime <= 0) {
                EndEffect();
            }
        }
    }

    void EndEffect() {
        Bullet b = target.GetComponent<Bullet>();
        //Return damage to normal
        b.AddDamage(-startDamage);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9) {
            target = collision.gameObject;
            ApplyEffect();
        }
    }
}
