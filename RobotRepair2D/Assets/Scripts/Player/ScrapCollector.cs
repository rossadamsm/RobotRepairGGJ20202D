using UnityEngine;
using TMPro;
using System;

public class ScrapCollector : MonoBehaviour
{
    public static int scrapCount = 50;

    public static int ScrapCount { get { return scrapCount; } set { scrapCount = value;  UpdateUI(); } }

    private static void UpdateUI()
    {
        UIManager.Instance.scrapTotal.SetText($"Scrap: {ScrapCount}");
    }

    private void Start()
    {
        UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Scrap scrap = collision.gameObject.GetComponent<Scrap>();
        if (scrap != null)
        {
            scrap.attract(gameObject);
        }
    }
}
