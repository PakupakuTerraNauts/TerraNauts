using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteredBossRoom : MonoBehaviour
{
    public bool isEnter = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            isEnter = true;
        }
    }
}
