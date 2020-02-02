using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static event Action ScrapCollected;
    public static event Action ShipPartiallyRepaired;

    public int score = 0;
    public int ShipScrapTotalRequired = 300;
    public float finalScore = 0;

    private int shipScrapCollected;
    public int currentScrapCount { get; private set; }

    [SerializeField] TextMeshProUGUI waveCompleteText;
    [SerializeField] LevelInfo[] levelInfos;

    private LevelInfo currentLevelInfo;
    private int currentLevelIndex = -1;
    private float scoreMult;
    private EnemySpawnController enemySpawnController;
    private int currentEnemyCount;
    private PlayerController[] playerControllers;

    public int powerupEveryXLevels = 2;
    public int spawnPer = 2;

    private void Awake()
    {
        //Ensures there is only ever one in the scene
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        enemySpawnController = FindObjectOfType<EnemySpawnController>();
        playerControllers = FindObjectsOfType<PlayerController>();
    }

    private void OnEnable()
    {
        Enemy.enemyDied += Enemy_enemyDied;
    }

    private void OnDisable()
    {
        Enemy.enemyDied -= Enemy_enemyDied;
    }

    private void Update()
    {
        if (playerControllers[0].PlayerDisabled && playerControllers[1].PlayerDisabled)
        {
            EndGame(false);
        }
    }

    private void Enemy_enemyDied()
    {
        currentEnemyCount--;

        Debug.Log("Enemy died");


        if (currentEnemyCount < 0)
            return;


        if (currentEnemyCount == 0)
        {
            Debug.Log("All Enemies Dead");
            StartCoroutine(LoadLevelAfterX());
        }

        Debug.Log("Enemies remaining:" + currentEnemyCount);
    }

    private IEnumerator LoadLevelAfterX()
    {
        waveCompleteText.GetComponent<Animator>().SetTrigger("FlyBy");
        yield return new WaitForSeconds(4f);
        MoveToNextLevel();
    }

    /// <summary>
    /// Manages all the things that need to change between levels.
    /// Note that the first level is started from the MyPlayersManager class, when both controllers have been detected.
    /// </summary>
    public void MoveToNextLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex % powerupEveryXLevels == 0) {
            PowerupManager.Instance.SpawnPowerups(spawnPer);
        }

        if (currentLevelIndex >= levelInfos.Length)
        {
            Debug.Log("All levels complete");
            return;
        }


        currentLevelInfo = levelInfos[currentLevelIndex];

        // Add Scrap to ShipTotal repair
        shipScrapCollected += currentScrapCount;
        currentScrapCount = 0;
        ShipPartiallyRepaired?.Invoke();

        if (shipScrapCollected >= ShipScrapTotalRequired)
        {
            EndGame(true);
            return;
        }

        //Update Game Progress UI
        UIManager.Instance.ShipRepairStatusText.SetText($"Ship Health : {shipScrapCollected}/{ShipScrapTotalRequired}");
        UIManager.Instance.ScrapTotalText.SetText($"Scrap: 0");
        UIManager.Instance.WaveInfoText.SetText($"Wave: {currentLevelInfo.LevelNo}");

        Debug.Log($"Wave {currentLevelInfo.LevelNo} spawing");

        //Spawn Next Wave of Enemies
        currentEnemyCount = currentLevelInfo.NoEnemiesToSpawn;
        enemySpawnController.SpawnEnemies(currentEnemyCount);
    }

    public void AddScrap(int scrapCount)
    {
        currentScrapCount += scrapCount;
        UIManager.Instance.ScrapTotalText.SetText($"Scrap: {currentScrapCount} ");
        PoolToHighScore(scrapCount);
        ScrapCollected?.Invoke();
    }


    private void PoolToHighScore(int scrapCount)
    {
        score += scrapCount;
        scoreMult += Mathf.Floor(scrapCount / 100);
        finalScore = score * scoreMult;
        UIManager.Instance.ScoreText.SetText(finalScore.ToString());
    }

    public void EndGame(bool completed)
    {
        if (completed)
        {
            Debug.Log("Game Completed!");
            SceneManager.LoadScene("Victory");
        }
        else
        {
            Debug.Log("Game Failed");
            SceneManager.LoadScene("GameOver");
        }
        //Load Final Scene (a bad version?)
    }
}
