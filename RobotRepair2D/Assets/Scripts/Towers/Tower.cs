using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{

    private float damage = 2f;
    private float attackspeed = 1f;
    private float cooldown = 0;

    public Enemy targettedEnemy;
    public GameObject targettedEnemyGameObject;

    public bool findingNewTarget = false;

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0 && targettedEnemy != null)
        {
            cooldown = attackspeed;
            targettedEnemy.hp -= damage;
            if (targettedEnemy.hp <= 0)
            {
                targettedEnemy.Die();
            }
        }

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        if (cooldown <= 0 && targettedEnemy == null)
        {
            findingNewTarget = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go != null)
        {
            targettedEnemyGameObject = go;
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                targettedEnemy = enemy;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject enemyGo = collision.gameObject;
        if (enemyGo == targettedEnemyGameObject)
        {
            targettedEnemy = null;
        }
    }

    void FixedUpdate()
    {
        if (findingNewTarget)
        {
            Collider2D[] results = new Collider2D[100];
            Physics2D.OverlapCircleNonAlloc(transform.position, 4, results);

            if (results.Length > 0)
            {
                for (int i = 0; i < results.Length; i++)
                {
                    targettedEnemyGameObject = results[i].gameObject;
                    targettedEnemy = targettedEnemyGameObject.GetComponent<Enemy>();
                    if (targettedEnemy == null)
                    {
                        targettedEnemy = null;
                        targettedEnemyGameObject = null;
                    }
                    else
                    {
                        findingNewTarget = false;
                        return;
                    }
                }
            }
        }
    }
}
