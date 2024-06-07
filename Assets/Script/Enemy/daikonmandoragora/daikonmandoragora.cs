using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daikonmandoragora : Enemy
{
    #region //variables
    
    private bool isSteppedOn = false;
    private BoxCollider2D boxcol = null;
    private EdgeCollider2D edgcol = null;

    [SerializeField] private radisshRange range;
    #endregion

    protected override void Initialize()
    {
        range.InitializeCallBack(onRadisshStapped); // コールバック
        boxcol = GetComponent<BoxCollider2D>();
        edgcol = GetComponent<EdgeCollider2D>();
        boxcol.enabled = true;
        edgcol.enabled = true;
    }

    protected override void Moving(){
        rb.velocity = new Vector2(0, -Data.gravity);
    }

    protected override void Sleeping(){
        if(!isDead){
            anim.Play("radissh_umari");     // 画面外に出たら埋まった状態に戻る
            gameObject.tag = "EnemySleep";
            isSteppedOn = false;
        }
    }

/// <summary>
/// プレイヤーが近づいたとき 大根が地面から出現
/// </summary>
    private void onRadisshStapped()
    {
        if(!isSteppedOn && !isDead){
            anim.Play("radissh_fumare");
            isSteppedOn = true;
            StartCoroutine(ChangeTag());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(!isDead)
            recievedDamage(collision);
    }

    protected override void dieAnimation(){
        if(!isDead){
            gameObject.tag = "EnemySleep";
            anim.Play("radissh_die");
        }
    }

    private IEnumerator ChangeTag(){
        yield return new WaitForSeconds(4.0f);
        if(!isDead)     // コルーチン経過前に倒された場合を除くため
            gameObject.tag = "Enemy";
    }
}