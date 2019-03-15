using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class MusicControl : MonoBehaviour
{
    public bool isMusicPlaying = true;
    public Button button;
    public AudioMixer musicMixer;
    void Start()
    {
        button.OnClick.AddListener(TaskOnClick);
    }
    public void Mute()
    {
        AudioListener.pause = !AudioListener.pause;
    }
    // Update is called once per frame
    public void Update()
    {
        void TaskOnClick()
        {
              isMusicPlaying = false;
              musicMixer.SetFloat("Volume", -80);
        }
    }
}