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
    public bool isMute=false;
    int isSwitched;
    void Start()
    {
        SpriteOriginal = musicButton.GetComponent<Image>().sprite;
        musicButton.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        isMute = !isMute;
        if (isMute)
        {
            audioMixer.SetFloat("MusicVol", -80);
            musicButton.image.overrideSprite = SpriteSwap;
            PlayerPrefs.SetInt("IsMute", isMute ? 1 : 0);
        }
        else
        {
            audioMixer.SetFloat("MusicVol", 0);
            musicButton.image.overrideSprite = SpriteOriginal;
            PlayerPrefs.SetInt("IsMute", isMute ? 1 : 0);
        }

    }
    void Update()
    {
        isSwitched = PlayerPrefs.GetInt("IsMute");
        if (isSwitched == 1)
        {
            isMute = true;
            musicButton.image.overrideSprite = SpriteSwap;
        }
    }
}