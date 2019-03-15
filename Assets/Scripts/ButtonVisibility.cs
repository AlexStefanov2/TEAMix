using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonVisibility : MonoBehaviour
{
    public Button button;

    void Update()
    {
        Debug.Log("is updating!");
        button.gameObject.SetActive(TurnController.isPlayerTurn);
    }
}
 