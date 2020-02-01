using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyIn : MonoBehaviour
{
    public Vector3 offset;
    public float delay;
    public float lerpPercentage;

    // Start is called before the first frame update
    private Vector3 startPos;
    private bool flying;
    void Start()
    {
        flying = false;
        startPos = this.transform.position;
        transform.position += offset;
        Invoke("FlyBack", delay);
    }

    // Update is called once per frame
    void Update()
    {
        if (flying)
		{
            transform.position = Vector3.Lerp(this.transform.position, startPos, lerpPercentage);
		}
    }

    void FlyBack()
	{
        flying = true;
	}
}
