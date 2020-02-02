using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : MonoBehaviour
{

    public int scrapCount = 1;
    public float attractMinDistance = 0.005f;
    public bool isAttracting = false;
    public float attractSpeed = 7f;
    public GameObject attractTarget;

    public bool isExplodingFromEnemy = false;
    public Vector3 movingToLocation;

    // Update is called once per frame
    void Update()
    {
        if (isAttracting) {
            float step = attractSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, attractTarget.transform.position, step);
            float distance = Vector3.Distance(transform.position, attractTarget.transform.position);
            if (distance < attractMinDistance)
            {
                ScrapCollector p = attractTarget.GetComponent<ScrapCollector>();
                if (p != null) {
                    isAttracting = false;
                    GameManager.Instance.AddScrap(scrapCount);
                    //ScrapCollector.ScrapCount += scrapCount;
                    //p.mesh.SetText($"Scrap: {p.scrapCount}");
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