﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawnController : MonoBehaviour
{
    public int spawnPerWave = 3;

    public float spawnIncreaseRate;
    //Seconds
    public float waveTime = 60f;

    public GameObject enemyPrefab;

    public int currentWaveCount = 1;
    public float currentWaveTime = 0;



    public TextMeshProUGUI waveInfo;

    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWaveTime <= 0) {
            currentWaveTime = waveTime;
            Spawn();
            currentWaveCount++;
        }

        if (currentWaveTime > 0) {
            currentWaveTime -= Time.deltaTime;
        }

        waveInfo.SetText($"Wave: {currentWaveCount}\n Time: {currentWaveTime.ToString("0.00")}s");
    }

    void Spawn() {
        GameObject[] obs = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject go in obs) {
            Player p = go.GetComponent<Player>();
            if (p != null) {
                manager.PoolToHighScore(p.scrapCount);
                p.scrapCount = 0;
            }
        }
        int toSpawn = spawnPerWave * currentWaveCount;
        for (int i = 0; i < (spawnPerWave * currentWaveCount); i++) {
            foreach (GameObject spawner in GameObject.FindGameObjectsWithTag("Spawner")) {
                if (toSpawn > 0) {
                    Instantiate(enemyPrefab, new Vector3(spawner.transform.position.x + Random.Range(1, 5), spawner.transform.position.y + Random.Range(1, 5), 0), Quaternion.identity);
                    toSpawn--;
                }
            }
        }
    }
}
