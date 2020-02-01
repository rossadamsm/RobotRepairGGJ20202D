using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuildController : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private KeyCode towerHotkey = KeyCode.B;
    private int towerCost = 10;

    GameObject currentTower;

    // Update is called once per frame
    void Update()
    {
        HandleHotkeys();
        if (currentTower != null)
        {
            MoveObjectToMouse();
            HandleClicks();
        }
    }

    void HandleHotkeys() {
        if (Input.GetKeyDown(towerHotkey))
        {
            Debug.Log("Key");
            ScrapCollector player = gameObject.GetComponent<ScrapCollector>();
            //if (player.ScrapCount >= 10) {
            //    if (currentTower != null)
            //    {
            //        Destroy(currentTower);
            //    }
            //    else
            //    {
            //        currentTower = Instantiate(towerPrefab);
            //        player.ScrapCount -= 10;
            //        player.mesh.SetText($"Scrap: {player.ScrapCount}");
            //    }
            //}
        }
    }
    void MoveObjectToMouse() {
        RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hitInfo.collider != null)
        {
            currentTower.transform.position = hitInfo.point;
            currentTower.transform.rotation = Quaternion.identity;
        }
    }
    void HandleClicks() {
        if (Input.GetMouseButtonDown(0))
        {
            currentTower = null;
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (currentTower != null)
            {
                Destroy(currentTower);
            }
        }
    }
}
