using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static int playerHealth = 100;
    public static int playerMaxHealth = playerHealth;
    public static int playerAttack = 0;
    public static int playerDefence = 0;
    public static int playerAP = 1;
    public static int playerPassedAP = 0;

    public Text attackTag;
    public Text defenceTag;
    public HealthbarController healthbar;

    void Update()
    {
        attackTag.text = playerAttack.ToString();
        defenceTag.text = playerDefence.ToString();
        healthbar.percentage = (float)playerHealth / playerMaxHealth;
    }

    public void AttackPress()
    {
        Debug.Log("Attack!");
        playerAP--;
        playerAttack += 10 + ChemistryController.multiplier;
        CheckIfTurnEnds();
    }

    public void DefendPress()
    {
        Debug.Log("Defend!");
        playerAP--;
        playerDefence += 10 + ChemistryController.multiplier;
        CheckIfTurnEnds();
    }

    public void PassPress()
    {
        Debug.Log("Pass!");
        playerPassedAP = playerAP;
        playerAP = 0;
        CheckIfTurnEnds();
    }

    static void CheckIfTurnEnds()
    {
        if (playerAP <= 0) {
            playerAP = playerPassedAP;
            playerPassedAP = 0;
            TurnController.CycleTurns();
        }
    }


    public static void DebugPrint()
    {
        Debug.Log(string.Format("Player health: {0}", playerHealth));
        Debug.Log(string.Format("Player AP: {0}", playerAP));
    }
}
