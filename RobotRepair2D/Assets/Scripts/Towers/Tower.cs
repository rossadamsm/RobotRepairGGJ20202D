using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    private float damage = 2f;
    private float attackspeed = 1f;
    private float cooldown = 0;

    public Enemy targettedEnemy;
    public GameObject targettedEnemyGameObject;

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0 && targettedEnemy != null) {
            cooldown = attackspeed;
            targettedEnemy.hp -= damage;
            if (targettedEnemy.hp <= 0) {
                targettedEnemy.Die();
            }
        }   

        if (cooldown > 0) {
            cooldown -= Time.deltaTime;
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
}
