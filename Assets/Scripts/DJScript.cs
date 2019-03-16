using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class DJScript : MonoBehaviour
{
    public AudioSource musicPlayer;
    public AudioClip winTheme;
    public AudioClip loseTheme;
    AudioSource audioSource;

    void Start()
    {
        audioSource = musicPlayer.GetComponent<AudioSource>();
    }
    void Update()
    {
        if (TurnController.hasWon) {
            audioSource.clip = winTheme;
            audioSource.Play();
            audioSource.loop = false;
            TurnController.hasWon = false;
        }
        if (TurnController.hasLost) {
            audioSource.clip = loseTheme;
            audioSource.Play();
            audioSource.loop = false;
            TurnController.hasLost = false;
        }
    }
}
