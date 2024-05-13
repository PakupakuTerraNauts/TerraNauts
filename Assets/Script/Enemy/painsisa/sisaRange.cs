using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sisaRange : MonoBehaviour
{
    #region // variables
    public painsisa sisa;
    private BoxCollider2D boxcol = null;
    #endregion

    void Start(){
        boxcol = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            sisa.isAttack = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision){
        if(collision.tag == "Player"){
            sisa.isAttack = true;
        }
    }
}
