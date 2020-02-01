using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class EnemySpawnController : MonoBehaviour
{
    /// <summary>
    /// No longer using a timebased approach to spawning
    /// </summary>
    //public int spawnPerWave = 3;
    //public float spawnIncreaseRate;
    //public float waveTime = 60f;
    //public float currentWaveTime = 0;

    public GameObject enemyPrefab;
    public int currentWaveCount = 1;


    //// Update is called once per frame
    //void Update()
    //{
    //    if (currentWaveTime <= 0)
    //    {
    //        currentWaveTime = waveTime;
    //        Spawn();
    //        currentWaveCount++;
    //    }

    //    if (currentWaveTime > 0)
    //    {
    //        currentWaveTime -= Time.deltaTime;
    //    }

    //    UIManager.Instance.WaveInfoText.SetText($"Wave: {currentWaveCount}\n Time: {currentWaveTime.ToString("0.00")}s");
    //}

    void Spawn()
    {
        //GameObject[] obs = GameObject.FindGameObjectsWithTag("Player");
        //foreach(GameObject go in obs) {
        //    ScrapCollector p = go.GetComponent<ScrapCollector>();
        //    if (p != null) {
        //        GameManager.Instance.PoolToHighScore(p.ScrapCount);
        //        p.ScrapCount = 0;
        //    }
        //}
        //int toSpawn = Mathf.FloorToInt(spawnPerWave * (spawnIncreaseRate * currentWaveCount));
        //// for (int i = 0; i < (spawnPerWave * currentWaveCount); i++) {
        //for (int i = 0; i < 1; i++)
        //{
        //    foreach (GameObject spawner in GameObject.FindGameObjectsWithTag("Spawner"))
        //    {
        //        if (toSpawn > 0)
        //        {
        //            Instantiate(enemyPrefab, new Vector3(spawner.transform.position.x + Random.Range(1, 5), spawner.transform.position.y + Random.Range(1, 5), 0), Quaternion.identity);
        //            toSpawn--;
        //        }
        //    }
        //}
    }

    public void SpawnEnemies(int noOfEnemies)
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");

        for (int i = 0; i < noOfEnemies; i++)
        {
            int spawnerIndex = Random.Range(0, spawners.Length - 1);
            GameObject spawner = spawners[spawnerIndex];

            Instantiate(enemyPrefab, new Vector3(spawner.transform.position.x + Random.Range(1, 5), spawner.transform.position.y + Random.Range(1, 5), 0), Quaternion.identity);
        }
    }
}
