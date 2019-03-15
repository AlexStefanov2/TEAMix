using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyUnit : MonoBehaviour
{
    public int order;
 
    int health = 10;
    int attack = 0;
    int defense = 0;


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

        if (PlayerController.playerAttack > defense) {
            health -= (PlayerController.playerAttack - defense);
        }
        if (health <= 0) {
            Die();
        } else if (attack > PlayerController.playerDefense) {
            PlayerController.playerHealth -= (attack - PlayerController.playerDefense);
        }

        PlayerController.playerAttack = 0;
        PlayerController.playerDefense = 0;
        attack = 0;
        defense = 0;
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
