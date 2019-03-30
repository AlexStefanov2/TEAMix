using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicControl : MonoBehaviour
{
    public Button musicButton;
    public Sprite SpriteOriginal;
    public Sprite SpriteSwap;
    public AudioMixer audioMixer;
    bool isMute;
    void Start()
    {
        isMute = 1==PlayerPrefs.GetInt("IsMute");
        SpriteOriginal = musicButton.GetComponent<Image>().sprite;
        musicButton.onClick.AddListener(TaskOnClick);
        UpdateMusic();
    }

    public void TaskOnClick()
    {
        
        isMute = !isMute;
        PlayerPrefs.SetInt("IsMute", isMute ? 1 : 0);
        UpdateMusic();
    }

    void UpdateMusic()
    {
        if (isMute) {
            audioMixer.SetFloat("MusicVol", -80);
            musicButton.image.overrideSprite = SpriteSwap;
        } else {
            audioMixer.SetFloat("MusicVol", 0);
            musicButton.image.overrideSprite = SpriteOriginal;
            
        }
    }
}