using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static int enemyCount = 3;
    public static int enemyAP = 0;

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
