using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerupManager : MonoBehaviour
{
    public static PowerupManager Instance;
    public List<GameObject> powerups = new List<GameObject>();

    public int lowerBoundX = 100;
    public int upperBoundX = 565;
    public int lowerBoundY = 100;
    public int upperBoundY = 374;

    private void Awake()
    {
        //Ensures there is only ever one in the scene
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SpawnPowerups(int count)
    {
        int lower = 0;
        int upper = powerups.Count - 1;
        for (int i = 0; i < count; i++)
        {
            int quadX = Random.Range(0, 2);
            int quadY = Random.Range(0, 2);

            if (quadX == 0) quadX = -1;
            if (quadY == 0) quadY = -1;

            GameObject selectedPower = powerups[Random.Range(lower, upper)];
            Vector3 position = new Vector3(Random.Range(lowerBoundX, upperBoundX) * quadX / 2f, Random.Range(lowerBoundY, upperBoundY) * quadY / 2f, 0);
            Instantiate(selectedPower, position, Quaternion.identity);
            Debug.Log("Instantiate Power up @ " + position);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(upperBoundX, upperBoundY, 0));
        Gizmos.DrawWireCube(transform.position, new Vector3(lowerBoundX, lowerBoundY, 0));
    }
}
