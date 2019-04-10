using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public ParticleSystem rightSwipe1;
    public ParticleSystem rightSwipe2;
    public ParticleSystem leftSwipe1;
    public ParticleSystem leftSwipe2;
    public ParticleSystem rightShield;
    public ParticleSystem leftShield;
    public ParticleSystem DeathSmoke;

    public void EnableRightSwipe()
    {
        rightSwipe1.Play();
        rightSwipe2.Play();
    }
    public void DisableRightSwipe()
    {
        rightSwipe1.Pause();
        rightSwipe2.Pause();
    }
    public void EnableLeftSwipe()
    {
        leftSwipe1.Play();
        leftSwipe2.Play();
    }
    public void DisableLeftSwipe()
    {
        leftSwipe1.Pause();
        leftSwipe2.Pause();
    }
    public void EnableRightShield()
    {
        rightShield.Play();
    }
    public void EnableLeftShield()
    {
        leftShield.Play();
    }
    public void EnableDeathSmoke()
    {
        DeathSmoke.Play();
    }
}
