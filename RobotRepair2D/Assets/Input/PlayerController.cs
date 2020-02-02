using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private SpriteRenderer playerSprite, gunSprite;
    [SerializeField] private Transform gunTransform;
    [HideInInspector] public InputUser InputUser { get { return inputUser; } set { inputUser = value;} }

    [Tooltip("0 = player1, 1 = player 2")]
    [SerializeField] private int playerId = 0;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float firingDelay = 0.5f;
    [SerializeField] private Transform firingPivot;

    private InputUser inputUser;
    private InputUser otherInputUser;
    private Gamepad gamePad1;
    private Gamepad gamePad2;
    PlayerControls controls;
    Vector2 move;
    Vector2 rotate;
    bool canFire = true;
    float firingDelayTimer = 0;
    private Animator animator;

    [HideInInspector] public bool PlayerDisabled = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }


    public void RegisterControls(InputUser myUser, InputUser otherUser)
    {
        InputUser = myUser;
        otherInputUser = otherUser;

        gamePad1 = InputUser.pairedDevices[0] as Gamepad;
        gamePad2 = otherInputUser.pairedDevices[0] as Gamepad;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();

        if (gamePad1 == null || gamePad2 == null) return;

        if (!PlayerDisabled)
            move = gamePad1.leftStick.ReadValue();
        else
            move = Vector2.zero;

        rotate = gamePad2.rightStick.ReadValue();

        Vector2 m = new Vector2(move.x, move.y) * moveSpeed * Time.fixedDeltaTime;
        transform.Translate(m, Space.World);
        
        AnimateCharacter();

        //Vector2 r = new Vector2(rotate.y, rotate.x) * 100f * Time.deltaTime;
        //transform.Rotate(r, Space.World);
        float heading = Mathf.Atan2(-rotate.x, rotate.y);
        gunTransform.rotation = Quaternion.Euler(0f, 0f, heading * Mathf.Rad2Deg);

        if (gamePad2.rightShoulder.isPressed && canFire)
            Shoot();

        if (gamePad2.leftShoulder.isPressed)
            PlaceTower();

        if (canFire == false)
            firingDelayTimer += Time.fixedDeltaTime;

        if (firingDelayTimer >= firingDelay)
        {
            canFire = true;
            firingDelayTimer = 0;
        }

    }

    private void PlaceTower()
    {
        //TODO connect to tower system
    }

    internal void RumbleController()
    {
        StartCoroutine(ShortRumble());
    }

    private IEnumerator ShortRumble()
    {
        gamePad1.SetMotorSpeeds(0.25f, 0.75f);
        yield return new WaitForSeconds(0.5f);
        gamePad1.SetMotorSpeeds(0f, 0f);
    }

    private void AnimateCharacter()
    {
        if (move != Vector2.zero)
            animator.SetBool("Running", true);
        else
            animator.SetBool("Running", false);

        if (rotate.x > 0)
        {
            playerSprite.flipX = false;
            gunSprite.flipY = false;
            //gunSprite.sortingOrder = 11;
            //otherGunSprite.sortingOrder = 9;
        }
        else
        {
            playerSprite.flipX = true;
            gunSprite.flipY = true;
            //gunSprite.sortingOrder = 9;
            //otherGunSprite.sortingOrder = 11;
        }
    }

    private void Shoot()
    {
        Debug.Log(gameObject.name + " fired");
        Bullet bullet = Instantiate<Bullet>(bulletPrefab, firingPivot.position, Quaternion.identity);
        bullet.tag = gameObject.tag;
        bullet.Shoot(firingPivot.right);
        canFire = false;
    }
}
