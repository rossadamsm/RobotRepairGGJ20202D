using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject target;
    public float moveSpeed = 5f;
    public float attackRange = 1f;
    public int damage = 1;
    public float attackSpeed = 1f;
    public float cooldown = 0f;
    private PlayerController[] players;

    private void Awake()
    {
        players = FindObjectsOfType<PlayerController>();
    }


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
        //Made it so it only considers nondisabled players as valid targets (prevents it just sitting on an already disabled player)
        List<PlayerController> validPlayers = new List<PlayerController>();
        validPlayers = players.Where(x => !x.PlayerDisabled).ToList();

        float closestDistance = 999999f;
        int closestDistanceIndex = -1;
        for (int i = 0; i < validPlayers.Count; i++)
        {
            float curDist = Vector3.Distance(transform.position, validPlayers[i].transform.position);
            if (curDist < closestDistance)
            {
                closestDistance = curDist;
                closestDistanceIndex = i;
            }
        }
        if (closestDistanceIndex != -1)
        {
            target = validPlayers[closestDistanceIndex].gameObject;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
