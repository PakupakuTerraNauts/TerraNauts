using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sisaRange : MonoBehaviour
{
    #region // variables
    private BoxCollider2D boxcol = null;

    public delegate void sisaAttack();
    private sisaAttack sisaAttackCallBack;
    #endregion

    void Start(){
        boxcol = GetComponent<BoxCollider2D>();
    }

/// <summary>
/// onSisaAttack コールバックをセット
/// </summary>
/// <param name="onSisaAttack">パインシーサー 攻撃</param>
    public void InitializeCallBack(sisaAttack onSisaAttack){
        sisaAttackCallBack = onSisaAttack;
    }

    // 入ったとき 居続けるときに攻撃をする
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            sisaAttackCallBack();
        }
    }
    private void OnTriggerStay2D(Collider2D collision){
        if(collision.tag == "Player"){
            sisaAttackCallBack();
        }
    }
}
