﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Math;

public class EnemyUnit : MonoBehaviour
{
    public int order;
    public int AIType;
    public bool isDead = false;
 
    public int health = 3;
    public int maxHealth;
    int attack = 0;
    int defence = 0;

    public Transform enemy;
    public HealthbarController healthbar;
    /*
    public Transform enemyHPTag;
    public Transform enemyHPBar;
    */

    

    void Start()
    {
        maxHealth = health;
        EnemyController.ToShift += Shift;
        EnemyController.ToDealDamage += DealDamage;
        EnemyController.ToTakeDamage += TakeDamage;
        EnemyController.ToUseAP += UseAP;
        EnemyController.ToDebug += DebugPrint;
        enemyStart = enemy.position;
        enemyEnd = enemyStart;
        /* 
        tagStart = enemyHPTag.position;
        tagEnd = tagStart;
        barStart = enemyHPBar.position;
        barEnd = barStart;
        */
    }

    float t = 0;
    Vector2 enemyStart;
    Vector2 enemyEnd;
    /* 
    Vector2 tagStart;
    Vector2 tagEnd;
    Vector2 barStart;
    Vector2 barEnd;
    */
    float transitionLength = 0.5f;

    void UpdateScreenPosition()
    {
        if (!enemy) {return;}
        enemyStart = enemy.position;
        enemyEnd = enemyStart;
        enemyEnd.y = ((-150f*order)+220f)/60f; // divide by 60 because idk why
        /*
        tagStart = enemyHPTag.position;
        tagEnd = tagStart;
        tagEnd.y = (-2*order)+2.5f;
        barStart = enemyHPBar.position;
        barEnd = barStart;
        barEnd.y = (-2*order)+1.5f;
         */
        t = 0;
    }

    void Update()
    {
        t += Time.deltaTime / transitionLength;
        enemy.position = Vector2.Lerp(enemyStart, enemyEnd, t);
        /* 
        enemyHPTag.position = Vector2.Lerp(tagStart, tagEnd, t);
        enemyHPBar.position = Vector2.Lerp(barStart, barEnd, t);
        */

        healthbar.percentage = (float)health / maxHealth;
    }

    bool hasDefended = false;
    bool skipConditional = false;

    void UseAP()
    {
        if (order != 0) {
            return;
        }
        ComputeAP();
        ShowChoices();
    }
    void ComputeAP()
    {
        defence = 0;
        attack = 0;

        if (AIType == 0) {
            // Primitive AI: alternates full defence and full attack)
            if (hasDefended) {
                attack = 10*EnemyController.enemyAP;
            } else {
                defence = 10*EnemyController.enemyAP;
            }
            hasDefended = !hasDefended;
            EnemyController.enemyAP = 0;
        } else if (AIType == 1) {
            // Simple AI:
            if (EnemyController.enemyAP >= (PlayerController.playerHealth + PlayerController.playerDefence)) {
                attack = 10*EnemyController.enemyAP;
                EnemyController.enemyAP = 0;
                return;
            }
            if (TurnController.turnCount <= 3) {
                return;
            }
            if (PlayerController.playerDefence < EnemyController.enemyAP) {
                attack = RoundUpTo10(PlayerController.playerDefence) + 10;
                defence = 10*EnemyController.enemyAP - attack;
                EnemyController.enemyAP = 0;
                return;
            }
            defence = RoundDownTo10(EnemyController.enemyAP * 10 / 2);
            EnemyController.enemyAP -= defence / 10;
        } else if (AIType == 2) {
            // Complex AI:
            if (EnemyController.enemyAP*10 >= (PlayerController.playerHealth + PlayerController.playerDefence)) {
                attack = EnemyController.enemyAP*10;
                EnemyController.enemyAP = 0;
                return;
            }
            if (TurnController.turnCount <= 3) {
                return;
            }
            if (EnemyController.enemyAP % 3 == 0) {
                attack = EnemyController.enemyAP * 10 / 3;
                defence = EnemyController.enemyAP * 10 / 3;
                EnemyController.enemyAP /= 3;
                return;
            }
            if (EnemyController.enemyAP < (PlayerController.playerAP + TurnController.turnCount + 1)) {
                defence = RoundDownTo10(EnemyController.enemyAP * 10 / 2) + 10;
                EnemyController.enemyAP -= defence / 10;
                return;
            }
            if (!skipConditional && EnemyController.enemyAP * 10 > PlayerController.playerDefence) {
                attack = RoundUpTo10(PlayerController.playerDefence) + 10;
                EnemyController.enemyAP -= RoundUpTo10(PlayerController.playerDefence) + 10;
                skipConditional = true;
                UseAP();
            }
            skipConditional = false;
            if (EnemyController.enemyAP % 2 == 0) {
                defence = EnemyController.enemyAP * 10 / 2;
                attack = EnemyController.enemyAP * 10 / 2;
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
                    attack += 10;
                } else if (randomPick == 2) {
                    defence += 10;
                } else if (randomPick == 3) {
                    APToPass++;
                }
                EnemyController.enemyAP--;
            }
            EnemyController.enemyAP = APToPass;
        }
    }

    public Text attackTag;
    public Text defenceTag;
    void ShowChoices()
    {
        if (!attackTag) {return;}
        if (!defenceTag) {return;}
        Debug.Log("ShowChoices was called.");
        TurnController.enemyHasAttacked = attack;
        TurnController.enemyHasDefended = defence;
        attackTag.text = string.Format("Att: {0}", attack.ToString());
        defenceTag.text = string.Format("Def: {0}", defence.ToString());
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
