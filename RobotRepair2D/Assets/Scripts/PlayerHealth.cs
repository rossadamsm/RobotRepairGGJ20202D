using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    public static event Action PlayerDisabled;
    public static event Action PlayerDamaged;
    public static event Action PlayerEnabled;

    [SerializeField] private int maxHealth;
    [SerializeField] private AudioClip damagedClip, repairedClip;

    [SerializeField] private float currentHealth;
    [SerializeField] private SpriteRenderer playerSprite;
    private PlayerController playerController;

    [SerializeField] private Slider healthBar;
    [SerializeField] private Sprite damagedSprite;
    [SerializeField] private Sprite originalSprite;

    [SerializeField] private ParticleSystem[] disabledParticlesEffects;
    [SerializeField] private ParticleSystem[] repairEffects;


    private void Awake()
    {
        currentHealth = maxHealth;
        playerController = GetComponent<PlayerController>();

        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
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
        if (GameManager.Instance.currentScrapCount >= 3)
        {
            GameManager.Instance.AddScrap(-3);

            currentHealth = maxHealth;
            healthBar.value = currentHealth;
            playerController.PlayerDisabled = false;
            AudioSource.PlayClipAtPoint(repairedClip, transform.position);
            PlayerEnabled?.Invoke();
            playerSprite.sprite = originalSprite;
            playerSprite.GetComponent<Animator>().enabled = true;

            for (int i = 0; i < disabledParticlesEffects.Length; i++)
                disabledParticlesEffects[i].Stop();

            for (int i = 0; i < repairEffects.Length; i++)
                repairEffects[i].Play();
        }
    }

    private void DisablePlayer()
    {
        playerController.PlayerDisabled = true;
        PlayerDisabled?.Invoke();
        playerSprite.GetComponent<Animator>().enabled = false;
        playerSprite.sprite = damagedSprite;

        for (int i = 0; i < disabledParticlesEffects.Length; i++)
            disabledParticlesEffects[i].Play();
    }

    public void TakeDamage(int amount)
    {
        currentHealth-= amount;
        AudioSource.PlayClipAtPoint(damagedClip, transform.position);
        PlayerDamaged?.Invoke();

        if (currentHealth <= 0)
            DisablePlayer();

        healthBar.value = currentHealth;

        StartCoroutine(FlashPlayerRed());

        playerController.RumbleController();
    }

    private IEnumerator FlashPlayerRed()
    {
        playerSprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        playerSprite.color = Color.white;
    }
}
