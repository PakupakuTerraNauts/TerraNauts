using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class niwakokepittya : Enemy
{
    #region //variables
    public tama Tama;

    private float toriPosition_x = 0.0f;

    public bool isLeft = true;     // 初期状態 右向き
    private CapsuleCollider2D capcol = null;
    #endregion

    protected override void Initialize(){
        transform.localScale = new Vector3(1, 1, 1);
        capcol = GetComponent<CapsuleCollider2D>();
        capcol.enabled = true;
    }

    protected override void Moving(){
        toriPosition_x = transform.position.x;
        anim.Play("tori_pitch");
        rb.velocity = new Vector2(0, -gravity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        Tama.gameObject.SetActive(false);
        anim.Play("tori_die");
    }

    // playerの方向を判定して、そっちを向く tori_pitch終了時に呼ぶ
    // (animatorから同時に呼びたいのでdeletetamaを内蔵する) → 振り向いたときに球の当たり判定が残っていることがあるので非アクティブにする
    public void DirectJudge(){
        if(Player.playerPos.position.x > toriPosition_x && isLeft){
            transform.localScale = new Vector3(-1, 1, 1);
            isLeft = false;
        }
        else if(Player.playerPos.position.x < toriPosition_x && !isLeft){
            transform.localScale = new Vector3(1, 1, 1);
            isLeft = true;
        }
        // deletetama()
        Tama.gameObject.SetActive(false);
    }

    private void release(){
        Tama.gameObject.SetActive(true);
        Tama.TamaPitch(isLeft);
    }
}

