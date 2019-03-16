using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPopUp : MonoBehaviour
{
    public GameObject Menupanel;
    public Button SettingsButton;
    void Start()
    {
        Menupanel.gameObject.SetActive(false);
        SettingsButton.onClick.AddListener(ShowPanel);
    }

    // Update is called once per frame
    public void ShowPanel()
    {
        Menupanel.gameObject.SetActive(true);
    }
}
