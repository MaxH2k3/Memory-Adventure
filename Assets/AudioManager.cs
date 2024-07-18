using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    [Header("---------- Audio Sources ----------")]
    [SerializeField]
    AudioSource musicSource;
    [SerializeField]
    AudioSource sfxSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip backgroundStoryGame;
    public AudioClip backgroundSurvivalGame;
    public AudioClip SwordSlash;
    public AudioClip MagicFire;
    public AudioClip BoomerangThrow;

    public void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (SceneManager.GetActiveScene().name.Equals("SurvivalScene"))
        {
            musicSource.clip = backgroundSurvivalGame;
        } else
        {
            musicSource.clip = backgroundStoryGame;
        }

        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void StopSFX()
    {
        sfxSource.Stop();
    }

    public void StopBackgroundMusic()
    {
        musicSource.Stop();
    }

}
