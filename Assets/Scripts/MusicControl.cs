using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicControl : MonoBehaviour
{
    public bool isMusicPlaying = true;
    public Button musicButton;
    public Sprite SpriteOriginal;
    public Sprite SpriteSwap;
    private int i = 0;
    void Start()
    {
        SpriteOriginal = musicButton.GetComponent<Image>().sprite;
        musicButton.onClick.AddListener(TaskOnClick);
    }
    // Update is called once per frame
    public void TaskOnClick()
    {
        AudioListener.pause = !AudioListener.pause;
        if (i % 2 == 0)
        {
            musicButton.image.overrideSprite = SpriteSwap;
        }
        else musicButton.image.overrideSprite = SpriteOriginal;
        i++;
    }
   
}