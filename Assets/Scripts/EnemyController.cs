using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static void ResetVars()
    {
        enemyCount = 3;
        enemyAP = 0;
    }
    void Start()
    {
        ResetVars();
    }
    
    public static int enemyCount;
    public static int enemyAP;

    public delegate void ShiftAction();
    public static event ShiftAction ToShift;
    public static void ShiftEnemies()
    {
        if (ToShift != null) {
            ToShift();
        }
    }

    public delegate void DealDamageAction();
    public static event DealDamageAction ToDealDamage;
    public static void DealDamage()
    {
        if (ToDealDamage != null) {
            ToDealDamage();
        }
    }

    public delegate void UseAPAction();
    public static event UseAPAction ToUseAP;
    public static void UseAP()
    {
        if (ToUseAP != null) {
            ToUseAP();
        }
    }
    
    public delegate void TakeDamageAction();
    public static event TakeDamageAction ToTakeDamage;
    public static void TakeDamage()
    {
        if (ToTakeDamage != null) {
            ToTakeDamage();
        }
    }

    public delegate void DebugAction();
    public static event DebugAction ToDebug;
    public static void DebugPrint()
    {
        if (ToDebug != null) {
            ToDebug();
            Debug.Log(string.Format("Enemy AP: {0}", enemyAP));
        }
    }

}
