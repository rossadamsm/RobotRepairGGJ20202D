using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamagable
{
    public static event Action enemyDied; 

    public int droppedScrapCount = 10;
    public GameObject scrapPrefab;
    public bool isDying = false;
    public float hp = 10;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            Die();
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
            Die();
    }

    public void Die()
    {
        isDying = true;
        for (int i = 0; i < droppedScrapCount; i++)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(transform.position.x + Random.Range(1, 5), transform.position.y + Random.Range(1, 5), transform.position.z);
            GameObject scrap = Instantiate(scrapPrefab, startPos, Quaternion.identity);
            scrap.GetComponent<Scrap>().explodeFromEnemy(endPos);
        }
        enemyDied?.Invoke();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScrapCollector p = collision.gameObject.GetComponent<ScrapCollector>();
        if (p != null)
        {
            Die();
        }
    }
}
