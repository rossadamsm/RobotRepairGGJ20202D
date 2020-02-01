using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMScript : MonoBehaviour
{
    public AudioSource efxSource;
    public AudioSource musicSource;
    public static SMScript instance = null;

    public AudioClip[] Sounds;

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
