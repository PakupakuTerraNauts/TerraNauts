using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyubiRange : MonoBehaviour
{
    #region // variables
    public tyubidoragon tyubi;
    private BoxCollider2D boxcol = null;
    #endregion

    void Start(){
        boxcol = GetComponent<BoxCollider2D>();
    }

    // ‰Š‚É“ü‚Á‚½‚Æ‚«{Œp‘±‚Åƒ_ƒ[ƒW
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            tyubi.isAttack = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision){
        if(collision.tag == "Player"){
            tyubi.isAttack = true;
        }
    }
}
