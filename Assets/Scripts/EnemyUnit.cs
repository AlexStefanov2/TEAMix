using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyUnit : MonoBehaviour
{
    public int order;
    public bool isDead = false;
 
    public int health = 10;
    int attack = 0;
    int defence = 0;

    public Transform enemy;


    void Start()
    {
        EnemyController.ToShift += Shift;
        EnemyController.ToDealDamage += DealDamage;
        EnemyController.ToTakeDamage += TakeDamage;
        EnemyController.ToUseAP += UseAP;
        EnemyController.ToDebug += DebugPrint;
        start = enemy.position;
        end = start;
    }

    float t = 0;
    Vector2 start;
    Vector2 end;
    float transitionLength;
    void UpdateScreenPosition()
    {
        start = enemy.position;
        end = start;
        end.y = (-2*order)+2;
        t = 0;
    }

    void Update()
    {
        t += Time.deltaTime;
        enemy.position = Vector2.Lerp(start, end, t);
    }

    bool hasDefended = false;
    void UseAP()
    {
        if (order != 0) {
            return;
        }
        defence = 0;
        attack = 0;
        /* // Primitive AI (always attacks with all of its AP):
        attack = EnemyController.enemyAP;
        EnemyController.enemyAP = 0;
        */

        // Not So primitive AI (alternates full defence and full attack)
        if (hasDefended) {
            attack = EnemyController.enemyAP;
        } else {
            defence = EnemyController.enemyAP;
        }
        hasDefended = !hasDefended;
        EnemyController.enemyAP = 0;
    }

    void Shift()
    {
        order--;
        if (order < 0) {
            order = EnemyController.enemyCount - 1;
        }
        UpdateScreenPosition();
    }

    void DealDamage()
    {
        if (order != 0) {
            return;
        }
        if (attack > PlayerController.playerDefence) {
            PlayerController.playerHealth -= (attack - PlayerController.playerDefence);
        }
    }

    void TakeDamage()
    {
        if (order != 0) {
            return;
        }
        if (PlayerController.playerAttack > defence) {
            health -= (PlayerController.playerAttack - defence);
        }
        if (health <= 0) {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        EnemyController.enemyCount--;
        EnemyController.ToShift -= Shift;
        EnemyController.ToDealDamage -= DealDamage;
        EnemyController.ToTakeDamage -= TakeDamage;
        EnemyController.ToUseAP -= UseAP;
        EnemyController.ToDebug -= DebugPrint;
        order = -9;
        UpdateScreenPosition();
    }


    void DebugPrint()
    {
        Debug.Log(string.Format("Unit {0} health: {1}", order, health));
    }
}
