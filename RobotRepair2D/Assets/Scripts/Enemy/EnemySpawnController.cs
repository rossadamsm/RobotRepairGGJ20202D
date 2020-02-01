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
        }

        if (currentWaveTime > 0) {
            currentWaveTime -= Time.deltaTime;
        }

        waveInfo.SetText($"Wave: {currentWaveCount}\n Time: {currentWaveTime.ToString("0.00")}s");
    }

    void Spawn() {
        float spawnTotal = 10;
        for (int i = 0; i < spawnTotal; i++) {
            GameObject enemy = Instantiate(enemyPrefab, new Vector3(transform.position.x + Random.Range(1,5), transform.position.y + Random.Range(1,5), transform.position.z),Quaternion.identity);
        }
    }
}
