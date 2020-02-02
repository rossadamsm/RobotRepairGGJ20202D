using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCamera : MonoBehaviour
{
    public List<GameObject> photos;

    private AudioSource[] audioSources;

    public GameObject transition;

    private int target;
    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        target = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(photos[target].transform.position.x, photos[target].transform.position.y, -10f), 0.05f);

        if (Input.GetMouseButtonDown(0))
		{
            if (target < photos.Count - 1)
			{
                target += 1;
                audioSources[1].Play();
            }
            else
			{
                if (GameObject.FindGameObjectsWithTag("Transition").Length == 0)
				{
                    //Instantiate Transition;
                    GameObject newTrans = Instantiate(transition, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity);
                    Transition newTransTrans = newTrans.GetComponent<Transition>();
                    newTransTrans.destinationScene = "Menu";
                    newTransTrans.fadeIn = true;
                    audioSources[1].Play();
				}
			}
		}
    }
}
