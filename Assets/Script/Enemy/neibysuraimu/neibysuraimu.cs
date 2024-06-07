using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class neibysuraimu : Enemy
{
    #region // variables
    private bool allTuraraFallen = true;

    private BoxCollider2D boxcol = null;
    [SerializeField] private turara[] ice = new turara[5];
    #endregion

    protected override void Initialize(){
        boxcol = GetComponent<BoxCollider2D>();
        boxcol.enabled = true;

        foreach(var t in ice){
            t.InitializeCallBack(onTuraraFallenCheck);  // コールバック
        }
    }

    protected override void Moving(){
        if(allTuraraFallen){
            anim.Play("neiby_attack");
        }else{
            anim.Play("neiby_default");
        }
        rb.velocity = new Vector2(0.0f, -Data.gravity);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        anim.Play("neiby_die");
    }

/// <summary>
/// アニメーションから呼ばれる 氷柱を降らせる
/// </summary>
    private void whistle(){
        allTuraraFallen = false;    // 氷柱が降り始めたらアニメーションをdefaultに戻す

        foreach(var t in ice){
            t.Turara();
        }
    }

/// <summary>
/// 全ての攻撃が終了しているかチェック
/// </summary>
    private void onTuraraFallenCheck(){
        foreach(var t in ice){
            if(t.isFall){
                Debug.Log("return");
                return;
            }
            Debug.Log(t.name + t.isFall);
        }
            
        allTuraraFallen = true;
    }
}
