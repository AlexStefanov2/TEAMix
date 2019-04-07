using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float leftX = -18;
    Transform character;

    Vector3 left;
    Vector3 center;

    void Start()
    {
        character = GetComponent<Transform>();
        center = character.position;
        left = center;
        left.x = leftX;
    }

    public float transitionLength = 1;
    float t = 0;
    void Update()
    {
        if (!character) {return;}
        t += Time.deltaTime / transitionLength;
        character.position = Vector3.Lerp(left, center, t);
    }
}
