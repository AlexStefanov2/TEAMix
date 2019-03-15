using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static int playerHealth = 10;
    public static int playerAttack = 0;
    public static int playerDefense = 0;
    public static int playerAP = 1;
    public static int playerPassedAP = 0;

    public Button AttackButton;
    public Button DefendButton;
    public Button PassButton;

    void Start()
    {
        // subscribe to 3 button events
        AttackButton.onClick.AddListener(AttackPress);
        DefendButton.onClick.AddListener(DefendPress);
        PassButton.onClick.AddListener(PassPress);
    }

    static void AttackPress()
    {
        Debug.Log("Attack!");
        playerAP--;
        playerAttack++;
        CheckIfTurnEnds();
    }

    static void DefendPress()
    {
        Debug.Log("Defend!");
        playerAP--;
        playerDefense++;
        CheckIfTurnEnds();
    }

    static void PassPress()
    {
        Debug.Log("Pass!");
        playerAP--;
        playerPassedAP++;
        CheckIfTurnEnds();
    }

    static void CheckIfTurnEnds()
    {
        if (playerAP <= 0) {
            playerAP = playerPassedAP;
            TurnController.CycleTurns();
        }
    }

    public static void DebugPrint()
    {
        Debug.Log(string.Format("Player health: {0}", playerHealth));
        Debug.Log(string.Format("Player AP: {0}", playerAP));
    }
}
