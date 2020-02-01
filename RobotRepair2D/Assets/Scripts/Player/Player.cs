using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public TextMeshProUGUI mesh;
    public int scrapCount;
    public float hp = 100f;

    // Update is called once per frame
    void Update()
    {
        
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
