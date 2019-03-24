using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowText : MonoBehaviour
{
    public float transitionLength = 3;
    float t = 0;

    public Text textbox;

    void Update()
    {
        t += Time.deltaTime / transitionLength;
        textbox.color = Color.HSVToRGB(t%1, 1f, 1f);
    }
}
