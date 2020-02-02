using UnityEngine;

public class TowerBuildController : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private KeyCode towerHotkey = KeyCode.B;
    [SerializeField] private int towerCost = 10;

    private float buildCooldown = 5f;
    private float currentCooldown = 0f;

    GameObject currentTower;

    // Update is called once per frame
    void Update()
    {
        HandleHotkeys();
        if (currentTower != null)
        {
            currentCooldown -= Time.deltaTime;
            //MoveObjectToMouse();
            //HandleClicks();
        }
    }

    void HandleHotkeys()
    {
        if (Input.GetKeyDown(towerHotkey))
        {
            if (GameManager.Instance.currentScrapCount >= 10)
            {
                if (currentTower != null)
                {
                    Destroy(currentTower);
                }
                else
                {
                    currentTower = Instantiate(towerPrefab);
                    currentTower.GetComponent<Tower>().isActivated = false;
                }
            }
        }
    }
    void MoveObjectToMouse()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hitInfo.collider != null)
        {
            currentTower.transform.position = hitInfo.point;
            currentTower.transform.rotation = Quaternion.identity;
        }
    }
    void HandleClicks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //The tower is being built here, subtract resource count then "place" the tower
            if (GameManager.Instance.currentScrapCount >= towerCost)
            {
                GameManager.Instance.AddScrap(-towerCost);
                currentTower.GetComponent<Tower>().isActivated = true;
                currentTower = null;
            }
            else
            {
                if (currentTower)
                {
                    Destroy(currentTower);
                    currentTower = null;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (currentTower != null)
            {
                Destroy(currentTower);
            }
        }
    }

    public void PlaceTower(Transform placeAt)
    {
        //This method is connecting to PlayerController.cs - specifically used for controller keybinds
        if (GameManager.Instance.currentScrapCount >= 10)
        {
            if (CanPlaceTower())
            {
                if (currentCooldown <= 0)
                {
                    currentTower = Instantiate(towerPrefab, new Vector3(placeAt.position.x, placeAt.position.y + 25), Quaternion.identity);
                    currentTower.GetComponent<Tower>().isActivated = true;
                    GameManager.Instance.AddScrap(-towerCost);

                    currentCooldown = buildCooldown;
                }
            }
        }
    }

    bool CanPlaceTower()
    {


        return true;
    }
}
