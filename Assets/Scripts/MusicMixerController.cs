using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicMixerController : MonoBehaviour
{
    public AudioMixer MasterMixer;
    public Button musicButton;
    // Start is called before the first frame update
    void Start()=>musicButton.onClick.AddListener(MusicMute);
    public void MusicMute()
    {
        MasterMixer.SetFloat("MusicVol", -80);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
