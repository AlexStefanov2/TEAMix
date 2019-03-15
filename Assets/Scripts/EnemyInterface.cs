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

    public delegate void FightAction();
    public static event FightAction ToFight;
    public static void CalculateDamage()
    {
        if (ToFight != null) {
            ToFight();
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
