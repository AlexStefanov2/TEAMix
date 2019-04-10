using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RetryScript : MonoBehaviour
{
    public Button button;
    private Scene scene = SceneManager.GetActiveScene();

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(Retry);
    }

    void Retry()
    {
        SceneManager.LoadScene(scene.name);
    }
}

