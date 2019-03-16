using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseMenuScript : MonoBehaviour
{
    public GameObject MenuPanel;
    public Button CloseButton;
    void Start()
    {
        CloseButton.onClick.AddListener(CloseMenu);
    }
    public void CloseMenu()
    {
        MenuPanel.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
