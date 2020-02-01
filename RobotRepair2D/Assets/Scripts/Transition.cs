using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public bool fadeIn;
    public string destinationScene;

    private SpriteRenderer sr;

    private float scale;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (fadeIn)
		{
            scale = 0f;
		}
        else
		{
            scale = 10f;
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
		{
            if (scale < 10f)
			{
                scale += 0.1f;
			}
            else
			{
                SceneManager.LoadScene(destinationScene);
			}
		}
        else
		{
            if (scale > 0f)
			{
                scale -= 0.1f;
			}
            else
			{
                Destroy(gameObject);
			}
		}

        transform.localScale = new Vector3(scale, scale, 1f);
    }
}
