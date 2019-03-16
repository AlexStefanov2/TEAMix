using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyUnit : MonoBehaviour
{
    public int order;
    public int AIType;
    public bool isDead = false;
 
    public float health = 10;
    float attack = 0;
    float defence = 0;

    public Transform enemy;
    public Transform enemyHPTag;



    void Start()
    {
        EnemyController.ToShift += Shift;
        EnemyController.ToDealDamage += DealDamage;
        EnemyController.ToTakeDamage += TakeDamage;
        EnemyController.ToUseAP += UseAP;
        EnemyController.ToDebug += DebugPrint;
        enemyStart = enemy.position;
        enemyEnd = enemyStart;
        tagStart = enemyHPTag.position;
        tagEnd = tagStart;
    }

    float t = 0;
    Vector2 enemyStart;
    Vector2 enemyEnd;
    Vector2 tagStart;
    Vector2 tagEnd;
    float transitionLength = 0.5f;
    void UpdateScreenPosition()
    {
        enemyStart = enemy.position;
        enemyEnd = enemyStart;
        enemyEnd.y = (-2*order)+2;
        tagStart = enemyHPTag.position;
        tagEnd = tagStart;
        tagEnd.y = (-2*order)+2;
        t = 0;
    }

    void Update()
    {
        t += Time.deltaTime / transitionLength;
        enemy.position = Vector2.Lerp(enemyStart, enemyEnd, t);
        enemyHPTag.position = Vector2.Lerp(tagStart, tagEnd, t);
    }

    bool hasDefended = false;
    bool skipConditional = false;
    void UseAP()
    {
        if (order != 0) {
            return;
        }
        defence = 0;
        attack = 0;

        if (AIType == 0) {
            // Primitive AI: alternates full defence and full attack)
            if (hasDefended) {
                attack = EnemyController.enemyAP;
            } else {
                defence = EnemyController.enemyAP;
            }
            hasDefended = !hasDefended;
            EnemyController.enemyAP = 0;
        } else if (AIType == 1) {
            // Simple AI:
            if (EnemyController.enemyAP >= (PlayerController.playerHealth + PlayerController.playerDefence)) {
                attack = EnemyController.enemyAP;
                EnemyController.enemyAP = 0;
                return;
            }
            if (TurnController.turnCount <= 3) {
                return;
            }
            if (PlayerController.playerDefence < EnemyController.enemyAP) {
                attack = PlayerController.playerDefence + 1;
                defence = EnemyController.enemyAP - (PlayerController.playerDefence + 1);
                EnemyController.enemyAP = 0;
                return;
            }
            defence = EnemyController.enemyAP / 2;
            EnemyController.enemyAP = EnemyController.enemyAP - (EnemyController.enemyAP/2);
        } else if (AIType == 2) {
            // Complex AI:
            if (EnemyController.enemyAP >= (PlayerController.playerHealth + PlayerController.playerDefence)) {
                attack = EnemyController.enemyAP;
                EnemyController.enemyAP = 0;
                return;
            }
            if (TurnController.turnCount <= 3) {
                return;
            }
            if (EnemyController.enemyAP % 3 == 0) {
                attack = EnemyController.enemyAP / 3;
                defence = EnemyController.enemyAP / 3;
                EnemyController.enemyAP /= 3;
                return;
            }
            if (EnemyController.enemyAP < (PlayerController.playerAP + TurnController.turnCount + 1)) {
                defence = (EnemyController.enemyAP / 2) + 1;
                EnemyController.enemyAP -= (EnemyController.enemyAP / 2) + 1;
                return;
            }
            if (!skipConditional && EnemyController.enemyAP > PlayerController.playerDefence) {
                attack = Mathf.Ceil(PlayerController.playerDefence) + 1f;
                EnemyController.enemyAP -= Mathf.CeilToInt(PlayerController.playerDefence) + 1;
                skipConditional = true;
                UseAP();
            }
            skipConditional = false;
            if (EnemyController.enemyAP % 2 == 0) {
                defence = EnemyController.enemyAP / 2;
                attack = EnemyController.enemyAP / 2;
                EnemyController.enemyAP = 0;
                return;
            }
        } else if (AIType == 3) {
            // Random AI:
            int randomPick;
            int APToPass = 0;
            while (EnemyController.enemyAP > 0) {
                randomPick = Random.Range(1, 3);
                if (randomPick == 1) {
                    attack++;
                } else if (randomPick == 2) {
                    defence++;
                } else if (randomPick == 3) {
                    APToPass++;
                }
                EnemyController.enemyAP--;
            }
            EnemyController.enemyAP = APToPass;
        }

        showChoices();
    }

    void showChoices()
    {
        // display enemy attack and defence
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
