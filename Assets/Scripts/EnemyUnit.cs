using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Math;

public class EnemyUnit : MonoBehaviour
{
    public int order;
    public int AIType = 0;
    public bool isDead = false;

    public EffectController smoker;


    public int health = 3;
    public int maxHealth;
    int mouseElement;
    int attack = 0;
    int defence = 0;

    public Transform enemy;
    public EnemyCharacter character;
    public HealthbarController healthbar;
    

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

        mouseElement = ChemistryController.possibleElements[Random.Range(0, ChemistryController.possibleElements.Length-1)];
        if (AIType == 0) {
            AIType = Random.Range(1, 3);
        }
        if (order == 0) {
            ChemistryController.currentElement = mouseElement;
        }
        if (order == 0) {
            ShowChoices();
        }

        UpdateScreenPosition();
        if (order == 0) {
            StartCoroutine(character.Show());
        }
    }

    float t = 0;
    Vector2 enemyStart;
    Vector2 enemyEnd;

    float transitionLength = 0.5f;

    void UpdateScreenPosition()
    {
        if (!enemy) {return;}
        if (!character) {return;}
        enemyStart = enemy.position;
        enemyEnd = enemyStart;
        enemyEnd.y = ((-115f*order)+240f)/60f; // divide by 60 because idk why
        t = 0;
        if (order == 0) {
            StartCoroutine(character.Show());
        } else {
            StartCoroutine(character.Hide());
        }
    }

    void Update()
    {
        t += Time.deltaTime / transitionLength;
        enemy.position = Vector2.Lerp(enemyStart, enemyEnd, t);

        healthbar.percentage = (float)health / maxHealth;
    }

   // bool hasDefended = false;
    bool skipConditional = false;

    void UseAP()
    {
        if (order != 0) {
            return;
        }
        Debug.Log("UseAP was called.");
        defence = 0;
        attack = 0;
        ComputeAP();
        ShowChoices();
    }
    void ComputeAP()
    {
        Debug.Log("Computing ap...");
        if (AIType == 1) {
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
        Debug.Log("Enemy attack: " + attack.ToString());
        Debug.Log("Enemy defence: " + defence.ToString());
        TurnController.enemyHasAttacked = attack;
        TurnController.enemyHasDefended = defence;
        attackTag.text = attack.ToString();
        defenceTag.text = defence.ToString();
    }

    void Shift()
    {
        order--;
        if (order < 0) {
            order = EnemyController.enemyCount - 1;
        }
        if (order == 0) {
            ChemistryController.currentElement = mouseElement;
        }
        UpdateScreenPosition();
    }

    void DealDamage()
    {
        if (order != 0) {
            return;
        }
        Debug.Log(string.Format("Enemy will deal {0} damage!", attack));
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

    IEnumerator Dying()
    {
        smoker.EnableDeathSmoke();
        yield return new WaitForSeconds(1.5f);
    }

    void OnDestroy()
    {
        Unsubscribe();
    }
    void Unsubscribe()
    {
        EnemyController.ToShift -= Shift;
        EnemyController.ToDealDamage -= DealDamage;
        EnemyController.ToTakeDamage -= TakeDamage;
        EnemyController.ToUseAP -= UseAP;
        EnemyController.ToDebug -= DebugPrint;
    }
    void Die()
    {
        isDead = true;
        StartCoroutine(Dying());
        character.Die();
        EnemyController.enemyCount--;
        Unsubscribe();
        order = -9;
        UpdateScreenPosition();
    }


    void DebugPrint()
    {
        Debug.Log(string.Format("Unit {0} health: {1}", order, health));
    }
}
