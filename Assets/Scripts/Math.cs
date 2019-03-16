using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Math : MonoBehaviour
{
    public static int RoundUpTo10(int number) {
        while (number % 10 != 0) {
            number++;
        }
        return number;
    }

    public static int RoundDownTo10(int number) {
        while (number % 10 != 0) {
            number--;
        }
        return number;
    }
}
