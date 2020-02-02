using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    public static event Action PlayerDisabled;
    public static event Action playerEnabled;
    public static event Action PlayerEnabled;

    [SerializeField] private int maxHealth;
    [SerializeField] private AudioClip damagedClip, repairedClip;

    [SerializeField] private float currentHealth;
    private PlayerController playerController;

    private void Awake()
    {
        currentHealth = maxHealth;
        playerController = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Player")
        {
            if (playerController.PlayerDisabled)
            {
                ReEnablePlayer();
            }
        }
    }

    private void ReEnablePlayer()
    {
        currentHealth = maxHealth;
        playerController.PlayerDisabled = false;
        AudioSource.PlayClipAtPoint(repairedClip, transform.position);
        playerEnabled?.Invoke();
    }

    private void DisablePlayer()
    {
        playerController.PlayerDisabled = true;
        PlayerDisabled?.Invoke();
    }

    public void TakeDamage(int amount)
    {
        currentHealth-= amount;
        AudioSource.PlayClipAtPoint(damagedClip, transform.position);
        PlayerEnabled?.Invoke();

        if (currentHealth <= 0)
            DisablePlayer();
    }
}
