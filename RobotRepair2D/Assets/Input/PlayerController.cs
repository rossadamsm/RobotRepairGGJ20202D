using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    PlayerControls controls;
    Vector2 move;
    Vector2 rotate;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player1.Fire.performed += Fire_performed;

        controls.Player1.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Player1.Move.canceled += ctx => move = Vector2.zero;

        controls.Player1.Rotate.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.Player1.Rotate.canceled += ctx => rotate = Vector2.zero;

    }

    private void OnEnable()
    {
        controls.Player1.Enable();
    }

    private void OnDisable()
    {
        controls.Player1.Disable();
    }

    private void Fire_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Player1 Fired");
    }

    private void Update()
    {
        Vector2 m = new Vector2(move.x, move.y) * moveSpeed * Time.deltaTime;
        transform.Translate(m, Space.World);

        //Vector2 r = new Vector2(rotate.y, rotate.x) * 100f * Time.deltaTime;
        //transform.Rotate(r, Space.World);
        float heading = Mathf.Atan2(-rotate.x, rotate.y);
        transform.rotation = Quaternion.Euler(0f, 0f, heading * Mathf.Rad2Deg);
    }
}
