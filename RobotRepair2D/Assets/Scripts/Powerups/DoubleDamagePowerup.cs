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
        PlayerController p = target.GetComponent<PlayerController>();
        if (p != null)
        {
            applied = true;
            p.doubleDamageActive = true;
            //Move away from the game space, will be destroyed in 10s
            gameObject.transform.SetPositionAndRotation(new Vector3(1000, 1000), Quaternion.identity);

            FindObjectOfType<PostProcessingManager>().GetMad();
        }
        else
        {
            Debug.Log("Cant find bullet");
        }
    }

    void FixedUpdate()
    {
        if (applied)
        {
            lifetime -= Time.fixedDeltaTime;
            if (lifetime <= 0)
            {
                EndEffect();
            }
        }
    }

    void EndEffect()
    {
        PlayerController p = target.GetComponent<PlayerController>();
        p.doubleDamageActive = false;
        FindObjectOfType<PostProcessingManager>().ChillOut();
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            target = collision.gameObject;
            ApplyEffect();
        }
    }
}
