using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class painsisa : Enemy
{
    #region // variables
    private bool instantDeath = false;
    [HideInInspector] public bool isAttack = false;
    [Header("チェックで向き反転")] public bool isLeft = true;
    [SerializeField] private sisaRange range;

    private CircleCollider2D circol = null;
    #endregion

    protected override void Initialize(){
        range.InitializeCallBack(onSisaAttack);
        circol = GetComponent<CircleCollider2D>();
        circol.enabled = true;
        if(!isLeft){
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

/// <summary>
/// 攻撃
/// </summary>
    private void onSisaAttack(){
        // フラグ管理だとすり抜けるのでアニメーション名で判定
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("pinesisa_stand")){
            anim.Play("pinesisa_attack");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "FallingTree"){ // 他敵と違い 木で即死の専用アニメーションがあるのでフラグオンにしておく
            instantDeath = true;
        }
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        if(instantDeath){
            anim.Play("pinesisa_treedie");
        }
        else{
            anim.Play("pinesisa_defdie");
        }
    }

/// <summary>
/// アニメーションイベント 攻撃の終了を検知
/// </summary>
    private void endAnimation(){
        isAttack = false;
    }
}
