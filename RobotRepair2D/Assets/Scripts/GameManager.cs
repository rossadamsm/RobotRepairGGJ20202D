using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    int score = 0;
    float scoreMult = 1;
    float finalScore = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scrapCountText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PoolToHighScore(int scrapCount) {
        score += scrapCount;
        scoreMult += Mathf.Floor(score / 100);
        finalScore = score * scoreMult;
        scoreText.SetText(finalScore.ToString());
        scrapCountText.SetText($"Scrap: 0");
    }
}
