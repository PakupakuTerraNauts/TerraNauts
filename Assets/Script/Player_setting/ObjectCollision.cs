using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    private string enemyTag = "Enemy";

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.tag == enemyTag){
            Debug.Log("“G‚ÆÚG‚µ‚½");
        }
    }
}