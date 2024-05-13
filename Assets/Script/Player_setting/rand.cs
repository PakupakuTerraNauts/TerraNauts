using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rand : MonoBehaviour
{
    public static bool Random(float Persent){
        float Rate = UnityEngine.Random.value * 100.0f;

        if(Rate <= Persent){
            return true;
        }
        else{
            return false;
        }
    }
}
