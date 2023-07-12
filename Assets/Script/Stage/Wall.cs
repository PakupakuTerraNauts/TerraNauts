using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wall : MonoBehaviour
{
    //public WallAnimation Script;

    public WallAnimation targetObj;


    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            targetObj.SendMessage("Start");
        }
    }
}
