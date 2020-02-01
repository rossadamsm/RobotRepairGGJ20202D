using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToContinue : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject transition;
    public string destinationScene;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
            if (GameObject.FindGameObjectsWithTag("Transition").Length == 0)
			{
                GameObject newTrans = Instantiate(transition, Vector3.zero, Quaternion.identity);
                Transition newTransTransition = newTrans.GetComponent<Transition>();
                newTransTransition.fadeIn = true;
                newTransTransition.destinationScene = destinationScene;
			}
        }
        
    }
}
