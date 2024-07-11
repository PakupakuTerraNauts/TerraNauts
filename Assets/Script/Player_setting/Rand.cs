using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rand : MonoBehaviour
{
/// <summary>
/// ó‚¯æ‚Á‚½Šm—¦‚Å–Û‚ª‹N‚±‚Á‚½‚©‚Ç‚¤‚©Œˆ’è‚·‚é
/// </summary>
/// <param name="Persent">Šm—¦</param>
/// <returns></returns>
    public static bool RandomTF(float Persent){
        float Rate = UnityEngine.Random.value * 100.0f;

        if(Rate <= Persent)
            return true;
        return false;
    }
}