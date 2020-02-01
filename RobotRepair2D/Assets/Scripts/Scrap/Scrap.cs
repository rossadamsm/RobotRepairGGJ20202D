using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : MonoBehaviour
{

    public int scrapCount = 1;
    public float attractMinDistance = 5f;
    public bool isAttracting = false;
    public float attractSpeed = 12f;
    public GameObject attractTarget;

    public bool isExplodingFromEnemy = false;
    public Vector3 movingToLocation;

    // Update is called once per frame
    void Update()
    {
        if (isAttracting) {
            float step = attractSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, movingToLocation, step);
            float distance = Vector3.Distance(transform.position, movingToLocation);
            if (distance < attractMinDistance)
            {
                Player p = attractTarget.GetComponent<Player>();
                if (p != null) {
                    isAttracting = false;
                    p.scrapCount += scrapCount;
                    p.mesh.SetText($"Scrap: {p.scrapCount}");
                    Destroy(gameObject);
                }
            }
        }

        if (isExplodingFromEnemy) {
            float step = (attractSpeed * 2) * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, movingToLocation, step);
            float distance = Vector3.Distance(transform.position, movingToLocation);
            if (distance < 0.005f) {
                isExplodingFromEnemy = false;
            }
        }
    }

    public void attract(GameObject go) {
        isAttracting = true;
        attractTarget = go;
    }

    public void explodeFromEnemy(Vector3 explodeToLocation) {
        isExplodingFromEnemy = true;
        movingToLocation = explodeToLocation;
    }
}