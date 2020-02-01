using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;

    [HideInInspector] public InputUser InputUser { get { return inputUser; } set { inputUser = value;} }

    [Tooltip("0 = player1, 1 = player 2")]
    [SerializeField] private int playerId = 0;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float firingDelay = 0.5f;
    [SerializeField] private Transform firingPivotPosition;

    private InputUser inputUser;
    private InputUser otherInputUser;
    private Gamepad gamePad1;
    private Gamepad gamePad2;
    PlayerControls controls;
    Vector2 move;
    Vector2 rotate;
    bool canFire = true;
    float firingDelayTimer = 0;

    public void RegisterControls(InputUser myUser, InputUser otherUser)
    {
        InputUser = myUser;
        otherInputUser = otherUser;

        gamePad1 = InputUser.pairedDevices[0] as Gamepad;
        gamePad2 = otherInputUser.pairedDevices[0] as Gamepad;
    }

    private void Update()
    {
        if (gamePad1 == null || gamePad2 == null) return;

        move = gamePad1.leftStick.ReadValue();
        rotate = gamePad2.rightStick.ReadValue();

        Vector2 m = new Vector2(move.x, move.y) * moveSpeed * Time.deltaTime;
        transform.Translate(m, Space.World);

        //Vector2 r = new Vector2(rotate.y, rotate.x) * 100f * Time.deltaTime;
        //transform.Rotate(r, Space.World);
        float heading = Mathf.Atan2(-rotate.x, rotate.y);
        transform.rotation = Quaternion.Euler(0f, 0f, heading * Mathf.Rad2Deg);

        Shoot();

        if (canFire == false)
            firingDelayTimer += Time.deltaTime;

        if (firingDelayTimer >= firingDelay)
        {
            canFire = true;
            firingDelayTimer = 0;
        }
        
    }

    private void Shoot()
    {
        if (gamePad2.rightShoulder.isPressed && canFire)
        {
            Debug.Log(gameObject.name + " fired");
            Bullet bullet = Instantiate<Bullet>(bulletPrefab, firingPivotPosition.position, Quaternion.identity);
            bullet.Shoot(transform.up);
            canFire = false;
        }
    }


    //private void OnDisable()
    //{
    //    controls.Player1.Disable();
    //}

    //private void Fire_performed(InputAction.CallbackContext context)
    //{
    //    Debug.Log("Player1 Fired");
    //}

    //private void Update()
    //{
    //    Vector2 m = new Vector2(move.x, move.y) * moveSpeed * Time.deltaTime;
    //    transform.Translate(m, Space.World);

    //    //Vector2 r = new Vector2(rotate.y, rotate.x) * 100f * Time.deltaTime;
    //    //transform.Rotate(r, Space.World);
    //    float heading = Mathf.Atan2(-rotate.x, rotate.y);
    //    transform.rotation = Quaternion.Euler(0f, 0f, heading * Mathf.Rad2Deg);
    //}
}
