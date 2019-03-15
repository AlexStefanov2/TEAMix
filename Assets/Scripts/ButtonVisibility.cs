using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonVisibility : MonoBehaviour
{
    public string buttonName;

    void Update()
    {
        GameObject.Find(buttonName).SetActive(TurnController.isPlayerTurn);
    }
}
 