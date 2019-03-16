using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundEffectsMixerController : MonoBehaviour
{
    public AudioMixer MasterMixer; 
    public bool isSoundEffectPlaying;
    public Button soundeffectsButton;
    public Sprite SpriteOriginal;
    public Sprite SpriteSwap;
    private int i = 0;
    // Start is called before the first frame update
    void Start()=>soundeffectsButton.onClick.AddListener(SoundEffectsMute);
    public void SoundEffectsMute()
    {
        MasterMixer.SetFloat("SfxVol", -80);
        if (i % 2 == 0)
        {
            soundeffectsButton.image.overrideSprite = SpriteSwap;
        }
        else soundeffectsButton.image.overrideSprite = SpriteOriginal;
        i++;

    }

}
