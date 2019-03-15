using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitScript : MonoBehaviour
{
    public Button quitButton;
    void Start()
    {
        UnityEngine.UI.Button btn = quitButton.GetComponent<Button>();
        btn.onClick.AddListener(Quit);
    }

    public void Quit()=>Application.Quit();

    // Update is called once per frame
    void Update()
    { 
    }
}
