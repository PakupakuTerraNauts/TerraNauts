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

    // ���ɓ������Ƃ��{�p���Ń_���[�W
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
