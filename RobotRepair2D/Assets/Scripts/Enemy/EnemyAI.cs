using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    GameObject target;
    float moveSpeed = 5f;
    float attackRange = 1f;
    float damage = 1f;
    float attackSpeed = 1f;
    float cooldown = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FindPlayers();
        DealDamage();
    }

    void FixedUpdate() {
        MoveToPlayer();
    }

    void MoveToPlayer() {
        if (target != null) {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }
    }

    void DealDamage() {
        if (cooldown <= 0 && target) {
            cooldown = attackSpeed;
            float distance = Vector3.Distance(transform.position, target.transform.position);
            //if (distance < attackRange) {
            //    target.GetComponent<ScrapCollector>().hp -= damage;
            //}
        }
        if (cooldown > 0) {
            cooldown -= Time.deltaTime;
        }
    }

    void FindPlayers() {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
        float closestDistance = 999999f;
        int closestDistanceIndex = -1;
        for (int i = 0; i < gos.Length; i++) {
            float curDist = Vector3.Distance(transform.position, gos[i].transform.position);
            if (curDist < closestDistance) {
                closestDistance = curDist;
                closestDistanceIndex = i;
            }
        }

        target = gos[closestDistanceIndex];
    }
}
