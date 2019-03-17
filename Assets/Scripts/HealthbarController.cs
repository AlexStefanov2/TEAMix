using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarController : MonoBehaviour
{
    public Image barCover;
    public float percentage = 1.00f;

    void Update()
    {
        Vector3 scale = barCover.gameObject.transform.localScale;
        if (percentage < 0) {
             percentage = 0;
        }
        if (percentage > 1) {
            percentage = 1;
        }
        scale.x = 1-percentage;
        barCover.gameObject.transform.localScale = scale;
    }
}
