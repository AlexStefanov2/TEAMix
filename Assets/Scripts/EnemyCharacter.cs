﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour
{
    public float rightX = 13;
    public float underworldY = -15;
    Transform character;

    Vector3 right;
    Vector3 center;

    Vector3 start;
    Vector3 end;

    bool loaded = false;
    void Start()
    {
        character = GetComponent<Transform>();
        center = character.position;
        right = center;
        right.x = rightX;

        character.position = right;
        end = right;
        start = right;
        loaded = true;
    }

    public float transitionLength = 0.5f;
    float t = 0;
    void Update()
    {
        if (!character) {return;}
        t += Time.deltaTime / transitionLength;
        character.position = Vector3.Lerp(start, end, t);
    }

    bool isShown = false;
    public IEnumerator Show()
    {
        while (!loaded) {yield return null;}
        if (isShown) {yield break;}
        start = right;
        end = center;
        t = 0;
        isShown = true;
    }

    public IEnumerator Hide()
    {
        while (!loaded) {yield return null;}
        if (!isShown) {yield break;}
        start = center;
        end = right;
        t = 0;
        isShown = false;
    }

    public void Die()
    {
        start = center;
        end = center;
        end.y = underworldY;
        t = 0;
    }
}
