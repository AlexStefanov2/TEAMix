using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyUnit : MonoBehaviour
{
    public int order;
 
    int health = 10;
    int attack = 0;
    int defence = 0;


    void Start()
    {
        EnemyController.ToShift += Shift;
        EnemyController.ToFight += Fight;
        EnemyController.ToUseAP += UseAP;
        EnemyController.ToDebug += DebugPrint;
    }

    void UseAP()
    {
        // Primitive AI (always attacks with all of its AP):
        if (order != 0) {
            return;
        }
        attack = EnemyController.enemyAP;
        EnemyController.enemyAP = 0;
    }

    void Shift()
    {
        order--;
        if (order < 0) {
            order = EnemyController.enemyCount - 1;
        }
        // shift enemy appearance on screen (along with its HP tag)
    }

// Calculations done here for both the player and the unit.
    void Fight()
    {
        if (order != 0) {
            return;
        }

        if (PlayerController.playerAttack > defence) {
            health -= (PlayerController.playerAttack - defence);
        }
        if (health <= 0) {
            Die();
        } else if (attack > PlayerController.playerDefence) {
            PlayerController.playerHealth -= (attack - PlayerController.playerDefence);
        }

        PlayerController.playerAttack = 0;
        PlayerController.playerDefence = 0;
        attack = 0;
        defence = 0;
    }

    void Die()
    {
        EnemyController.enemyCount--;
        EnemyController.ToShift -= Shift;
        EnemyController.ToFight -= Fight;
        EnemyController.ToUseAP -= UseAP;
        EnemyController.ToDebug -= DebugPrint;
        order = -10;
    }

    void DebugPrint()
    {
        Debug.Log(string.Format("Unit {0} health: {1}", order, health));
    }
}
