using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayerMovement : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.position += new Vector3(horizontal, vertical, 0) * Time.deltaTime * 5;
    }
}
