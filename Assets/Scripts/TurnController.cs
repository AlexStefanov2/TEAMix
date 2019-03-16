using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public static string bigLabelStatus = "";

    public static bool isPlayerTurn = true;
    public static int turnCount = 1;

    public EffectController swiper;

    void Start()
    {
        Debug.Log("Debug Console Active");
    }

    static bool toCycleTurns = false;
    void Update()
    {
        if (toCycleTurns) {
            StartCoroutine(Waitable());
            toCycleTurns = false;
        }
    }

    public static int stage = 0;
    public static void CycleTurns()
    {
        toCycleTurns = true;
    }

    public static bool hasWon;
    public static bool hasLost;
    public static bool toEnemySwipe = false;
    int temp;
    IEnumerator Waitable()
    {
        if (stage == 0) {
            isPlayerTurn = false;
            if (PlayerController.playerAttack > 0) {
                ChemistryController.GiveTask(0);
            } else {
                stage = 1;
            }
        }
        if (stage == 1) {
            if (PlayerController.playerAttack > 0) {
                swiper.EnableRightSwipe();
                yield return new WaitForSeconds(1);
                swiper.DisableRightSwipe();
            }
            if (PlayerController.playerDefence > 0) {
            ChemistryController.GiveTask(1);
            } else {
                stage = 2;
            }
        }
        if (stage == 2) {
            EnemyController.TakeDamage();
            yield return new WaitForSeconds(0.5f);
            EnemyController.ShiftEnemies();
            yield return new WaitForSeconds(0.5f);
            if (EnemyController.enemyCount == 0) {
                Debug.Log ("YOU WIN!");
                bigLabelStatus = "YOU WIN!";
                hasWon = true;
            } else {
                EnemyController.enemyAP += turnCount;
                yield return new WaitForSeconds(0.5f);
                EnemyController.UseAP();
                yield return new WaitForSeconds(0.5f);
                temp = PlayerController.playerHealth;
                EnemyController.DealDamage();
                if (PlayerController.playerHealth != temp) {
                    swiper.EnableLeftSwipe();
                    yield return new WaitForSeconds(1);
                    swiper.DisableLeftSwipe();
                }
                if (PlayerController.playerHealth <= 0) {
                    Debug.Log ("YOU LOSE.");
                    bigLabelStatus = "YOU LOSE.";
                    hasLost = true;
                
                } else {
                    turnCount++;
                    PlayerController.playerAP += turnCount;
                    PlayerController.playerDefence = 0;
                    PlayerController.playerAttack = 0;
                    DebugPrint();
                    isPlayerTurn = true;
                    
                    stage = 0;
                }
            }
        }
        yield return new WaitForSeconds(0);
    }
    static void DebugPrint()
    {
        PlayerController.DebugPrint();
        EnemyController.DebugPrint();
    }

}
