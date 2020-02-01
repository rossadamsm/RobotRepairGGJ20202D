using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject target;
    public float moveSpeed = 5f;
    public float attackRange = 1f;
    public int damage = 1;
    public float attackSpeed = 1f;
    public float cooldown = 0f;

    // Update is called once per frame
    void Update()
    {
        FindPlayers();
        DealDamage();
    }

    void FixedUpdate()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        if (target != null)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }
    }

    void DealDamage()
    {
        if (cooldown <= 0 && target)
        {
            cooldown = attackSpeed;
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < attackRange)
            {
                target.GetComponent<IDamagable>().TakeDamage(damage);
            }
        }
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    void FindPlayers()
    {
        PlayerController[] gos = FindObjectsOfType<PlayerController>();
        float closestDistance = 999999f;
        int closestDistanceIndex = -1;
        for (int i = 0; i < gos.Length; i++)
        {
            float curDist = Vector3.Distance(transform.position, gos[i].transform.position);
            if (curDist < closestDistance)
            {
                closestDistance = curDist;
                closestDistanceIndex = i;
            }
        }
        if (closestDistanceIndex != -1)
        {
            target = gos[closestDistanceIndex].gameObject;
        }
    }
}
