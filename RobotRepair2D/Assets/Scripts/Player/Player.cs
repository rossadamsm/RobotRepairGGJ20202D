using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public TextMeshProUGUI mesh;
    public int scrapCount;

    // Update is called once per frame
    void Update()
    {
        
    }

    void pickupAndDestroyScrap(Scrap scrap) {
        if (scrap != null) {
            scrap.attract(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Scrap scrap = collision.gameObject.GetComponent<Scrap>();
        pickupAndDestroyScrap(scrap);
    }
}
