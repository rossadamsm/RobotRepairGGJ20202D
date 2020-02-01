using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
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
            if (playerController.playerDisabled)
            {
                ReEnablePlayer();
            }
        }
    }

    private void ReEnablePlayer()
    {
        currentHealth = maxHealth;
        playerController.playerDisabled = false;
        AudioSource.PlayClipAtPoint(repairedClip, transform.position);
    }

    private void DisablePlayer()
    {
        playerController.playerDisabled = true;
    }

    public void TakeDamage(int amount)
    {
        currentHealth-= amount;
        AudioSource.PlayClipAtPoint(damagedClip, transform.position);

        if (currentHealth <= 0)
            DisablePlayer();
    }
}
