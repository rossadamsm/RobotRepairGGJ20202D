using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public int ShipScrapTotalRequired = 300;
    public float finalScore = 0;

    private int shipScrapCollected;
    public int currentScrapCount { get; private set; }

    [SerializeField] LevelInfo[] levelInfos;

    private LevelInfo currentLevelInfo;
    private int currentLevelIndex = -1;
    private float scoreMult;
    private EnemySpawnController enemySpawnController;

    private void Awake()
    {
        //Ensures there is only ever one in the scene
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        enemySpawnController = FindObjectOfType<EnemySpawnController>();
    }

    /// <summary>
    /// Manages all the things that need to change between levels.
    /// Note that the first level is started from the MyPlayersManager class, when both controllers have been detected.
    /// </summary>
    public void MoveToNextLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex >= levelInfos.Length)
        {
            Debug.Log("All levels complete");
            return;
        }

        currentLevelInfo = levelInfos[currentLevelIndex];

        // Add Scrap to ShipTotal repair
        shipScrapCollected += currentScrapCount;
        currentScrapCount = 0;

        //Update Game Progress UI
        UIManager.Instance.ShipRepairStatusText.SetText($"Ship Health : {shipScrapCollected}/{ShipScrapTotalRequired}");
        UIManager.Instance.ScrapTotalText.SetText($"Scrap: 0");

        //Spawn Next Wave of Enemies
        enemySpawnController.SpawnEnemies(currentLevelInfo.NoEnemiesToSpawn);
    }

    public void AddScrap(int scrapCount)
    {
        currentScrapCount += scrapCount;
        UIManager.Instance.ScrapTotalText.SetText($"Scrap: {currentScrapCount} ");
        PoolToHighScore(scrapCount);
    }


    private void PoolToHighScore(int scrapCount)
    {
        score += scrapCount;
        scoreMult += Mathf.Floor(scrapCount / 100);
        finalScore = score * scoreMult;
        UIManager.Instance.ScoreText.SetText(finalScore.ToString());
    }
}
