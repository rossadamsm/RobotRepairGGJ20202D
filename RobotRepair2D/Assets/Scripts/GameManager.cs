using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public float scoreMult = 1;
    public float finalScore = 0;

    private void Awake()
    {
        //Ensures there is only ever one in the scene
        if (Instance != null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    public void PoolToHighScore(int scrapCount)
    {
        score += scrapCount;
        scoreMult += Mathf.Floor(scrapCount / 100);
        finalScore = score * scoreMult;
        UIManager.Instance.score.SetText(finalScore.ToString());
        UIManager.Instance.scrapTotal.SetText($"Scrap: 0");
    }
}
