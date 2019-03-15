using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicControl : MonoBehaviour
{
    public bool isMusicPlaying = true;
    public Button musicButton;
    void Start()
    {
        UnityEngine.UI.Button btn = musicButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
    // Update is called once per frame
    void TaskOnClick()
    {
        AudioListener.pause = !AudioListener.pause;
    }
   
}