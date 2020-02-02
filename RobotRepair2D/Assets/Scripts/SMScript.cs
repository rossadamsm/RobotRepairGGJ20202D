using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMScript : MonoBehaviour
{
    public AudioSource efxSource;
    public AudioSource musicSource;
    public static SMScript Instance = null;

    public AudioClip[] EnemyDeathSounds;
    public AudioClip[] RobotPlayerHitSounds;
    public AudioClip[] CollectScrapSounds;
    public AudioClip[] ShootingSounds;


    public AudioClip[] RepairSounds;
    public AudioClip[] RobotDisabledSounds;


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        PlayLoopFromSource();
    }


    private void OnDisable()
    {
        Enemy.enemyDied -= Enemy_enemyDied;
        PlayerHealth.PlayerDisabled -= PlayerHealth_playerDisabled;
        PlayerHealth.PlayerDamaged -= PlayerHealth_playerHit;
        GameManager.ScrapCollected -= GameManager_ScrapCollected;
        GameManager.ShipPartiallyRepaired -= PlayRepairClip;
        PlayerHealth.PlayerEnabled -= PlayRepairClip;
    }


    private void OnEnable()
    {
        Enemy.enemyDied += Enemy_enemyDied;
        PlayerHealth.PlayerDisabled += PlayerHealth_playerDisabled;
        PlayerHealth.PlayerDamaged += PlayerHealth_playerHit;
        GameManager.ScrapCollected += GameManager_ScrapCollected;
        GameManager.ShipPartiallyRepaired += PlayRepairClip;
        PlayerHealth.PlayerEnabled += PlayRepairClip;

    }


    internal void PlayShootingClip()
    {
        PlaySingle(GetRandomClip(ShootingSounds));
    }

    private void PlayRepairClip()
    {
        PlaySingle(GetRandomClip(RepairSounds));
    }

    private void GameManager_ScrapCollected()
    {
        PlaySingle(GetRandomClip(CollectScrapSounds));
    }

    private void PlayerHealth_playerHit()
    {
        PlaySingle(GetRandomClip(RobotPlayerHitSounds));
    }

    private void PlayerHealth_playerDisabled()
    {
        PlaySingle(GetRandomClip(RobotDisabledSounds));
    }

    private void Enemy_enemyDied()
    {
        PlaySingle(GetRandomClip(EnemyDeathSounds));
    }



    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void PlayLoop(AudioClip clip)
    {
        if (musicSource)
            musicSource.Stop();
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayLoopFromSource()
    {
        if (musicSource)
            musicSource.Stop();
        else
            return;
        musicSource.loop = true;
        musicSource.Play();
    }

    private AudioClip GetRandomClip(AudioClip[] audioclips)
    {
        int index = UnityEngine.Random.Range(0, audioclips.Length - 1);
        return audioclips[index];
    }
}
