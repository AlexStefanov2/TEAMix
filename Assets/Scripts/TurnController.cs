using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public static void ResetVars()
    {
        bigLabelStatus = "";
        hasWon = false;
        hasLost = false;
        isPlayerTurn = true;
        turnCount = 1;
        toCycleTurns = false;
        stage = 0;
        toEnemySwipe = false;
        enemyHasDefended = 0;
        enemyHasAttacked = 0;
    }
    public static string bigLabelStatus;

    public static bool isPlayerTurn;
    public static int turnCount;

    public Button retryButton;

    public EffectController swiper;

    void Start()
    {
        ResetVars();
        retryButton.gameObject.SetActive(false); 
        Debug.Log("Debug Console Active");
    }

    static bool toCycleTurns;
    void Update()
    {
        if (toCycleTurns) {
            StartCoroutine(Waitable());
            toCycleTurns = false;
        }
    }

    public static int stage;
    public static void CycleTurns()
    {
        toCycleTurns = true;
    }

    public static bool hasWon;
    public static bool hasLost;
    public static bool toEnemySwipe;
    public static int enemyHasAttacked;
    public static int enemyHasDefended;
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
            if (PlayerController.playerDefence > 0) {
                swiper.EnableLeftShield();
                yield return new WaitForSeconds(1.5f);
            }
            EnemyController.TakeDamage();
            yield return new WaitForSeconds(0.5f);
            EnemyController.ShiftEnemies();
            yield return new WaitForSeconds(1.0f);
            if (EnemyController.enemyCount == 0) {
                Debug.Log ("YOU WIN!");
                bigLabelStatus = "YOU WIN!";
                hasWon = true;
                retryButton.gameObject.SetActive(true);
            } else {
                EnemyController.enemyAP += turnCount;
                yield return new WaitForSeconds(0.5f);
                EnemyController.UseAP();
                if (enemyHasDefended > 0) {
                    swiper.EnableRightShield();
                    yield return new WaitForSeconds(1.5f);
                }
                yield return new WaitForSeconds(0.5f);
                EnemyController.DealDamage();
                if (enemyHasAttacked > 0) {
                    swiper.EnableLeftSwipe();
                    yield return new WaitForSeconds(1);
                    swiper.DisableLeftSwipe();
                }
                if (PlayerController.playerHealth <= 0) {
                    Debug.Log ("YOU LOSE.");
                    bigLabelStatus = "YOU LOSE.";
                    hasLost = true;
                    retryButton.gameObject.SetActive(true);
                }
                else {
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
        ChemistryController.DebugPrint();
    }

}
