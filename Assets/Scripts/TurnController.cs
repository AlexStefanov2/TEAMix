﻿using System.Collections;
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

    public static int stage = 0;
    public static void CycleTurns()
    {
        Debug.Log("Activated on stage " + stage.ToString());
        if (stage == 0) {
            isPlayerTurn = false;
            if (PlayerController.playerAttack > 0) {
                ChemistryController.GiveTask(0);
            } else {
                stage = 1;
            }
        }
        if (stage == 1) {
            if (PlayerController.playerDefence > 0) {
               ChemistryController.GiveTask(1);
            } else {
                stage = 2;
            }
        }
        if (stage == 2) {
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
            Debug.Log("will enable buttons!");
            isPlayerTurn = true;
            
            stage = 0;
        }
    }

    static void DebugPrint()
    {
        PlayerController.DebugPrint();
        EnemyController.DebugPrint();
    }

}
