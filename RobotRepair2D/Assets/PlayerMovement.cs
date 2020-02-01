using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, InputMaster.IPlayer1Actions
{
    InputMaster controls;

    private void Awake()
    {
        controls = new InputMaster();
        controls.Player1.SetCallbacks(this);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 delta = context.ReadValue<Vector2>();
        transform.position += new Vector3(delta.x, delta.y, 0) * Time.deltaTime;
    }

    public void OnLooking(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        //transform.Rotate(new Vector3(delta.x, delta.y, 0) * Time.deltaTime);

        float heading = Mathf.Atan2(input.x, input.y);
        Debug.Log(input.x);
        Debug.Log(input.y);
        transform.rotation = Quaternion.Euler(0f, 0f, heading * Mathf.Rad2Deg);
    }


    //void Update()
    //{
    //    float moveHorizontal = Input.GetAxis("Horizontal1");
    //    float moveVertical = Input.GetAxis("Vertical1"); 

    //    transform.position += new Vector3(moveHorizontal, moveVertical, 0) * Time.deltaTime;

    //    float lookHorizontal = Input.GetAxis("LookHorizontal1");
    //    float lookVertical = Input.GetAxis("LookVertical1");

    //    transform.Rotate(new Vector3(lookHorizontal, lookVertical, 0) * Time.deltaTime);
    //}
}
