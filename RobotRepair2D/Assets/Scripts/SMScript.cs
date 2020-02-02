using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMScript : MonoBehaviour
{
    public AudioSource efxSource;
    public AudioSource musicSource;
    public static SMScript instance = null;

    public AudioClip[] EnemyDeathSounds;
    public AudioClip[] RobotPlayerHitSounds;
    public AudioClip[] CollectScrapSounds;
    public AudioClip[] RepairSounds;
    public AudioClip[] RobotDisabledSounds;

    private void OnDisable()
    {
        Enemy.enemyDied -= Enemy_enemyDied;
        PlayerHealth.PlayerDisabled -= PlayerHealth_playerDisabled;
        PlayerHealth.PlayerEnabled -= PlayerHealth_playerHit;
        GameManager.ScrapCollected -= GameManager_ScrapCollected;
        GameManager.ShipPartiallyRepaired -= PlayRepairClip;
        PlayerHealth.PlayerEnabled -= PlayRepairClip;
    }


    private void OnEnable()
    {
        Enemy.enemyDied += Enemy_enemyDied;
        PlayerHealth.PlayerDisabled += PlayerHealth_playerDisabled;
        PlayerHealth.PlayerEnabled += PlayerHealth_playerHit;
        GameManager.ScrapCollected += GameManager_ScrapCollected;
        GameManager.ShipPartiallyRepaired += PlayRepairClip;
        PlayerHealth.PlayerEnabled += PlayRepairClip;

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

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        PlayLoopFromSource();
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
        int index = Random.Range(0, audioclips.Length - 1);
        return audioclips[index];
    }
}
