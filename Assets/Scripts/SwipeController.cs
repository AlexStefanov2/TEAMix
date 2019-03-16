using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public ParticleSystem enemySwipe1;
    public ParticleSystem enemySwipe2;

    public void EnableEnemySwipe()
    {
        enemySwipe1.Play();
        enemySwipe2.Play();
    }
    public void DisableEnemySwipe()
    {
        enemySwipe1.Pause();
        enemySwipe2.Pause();
    }
}
