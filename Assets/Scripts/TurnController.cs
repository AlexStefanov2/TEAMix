using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public static string bigLabelStatus = "";

    public static bool isPlayerTurn = true;
    static int turnCount = 1;

    void Start()
    {
        Debug.Log("Debug Console Active");
    }

    public static void CycleTurns()
    {
        isPlayerTurn = false;
        EnemyController.TakeDamage();
        EnemyController.ShiftEnemies();
        EnemyController.enemyAP += turnCount;
        EnemyController.UseAP();
        EnemyController.DealDamage();
        if (EnemyController.enemyCount == 0) {
            Debug.Log ("YOU WIN!");
            bigLabelStatus = "YOU WIN!";
            return;
        }

        if (PlayerController.playerHealth <= 0) {
            Debug.Log ("YOU LOSE.");
            bigLabelStatus = "YOU LOSE.";
            return;
        }
        turnCount++;
        PlayerController.playerAP += turnCount;
        PlayerController.playerDefence = 0;
        PlayerController.playerAttack = 0;
        DebugPrint();
        isPlayerTurn = true;
    }

    static void DebugPrint()
    {
        PlayerController.DebugPrint();
        EnemyController.DebugPrint();
    }

}
