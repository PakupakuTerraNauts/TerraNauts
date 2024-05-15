using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sabochan : Enemy
{
    #region // variables
    private bool isAttack = true;

    public togeboru Togeboru;

    [Header ("チェックで向き反転")] public bool isLeft = true;
    private CapsuleCollider2D capcol = null;
    #endregion

    protected override void Initialize(){
        if(!isLeft){
            transform.localScale = new Vector3(-1, 1, 1);
        }
        capcol = GetComponent<CapsuleCollider2D>();
        capcol.enabled = true;
    }

    protected override void Moving(){
        Debug.Log(isAttack);
        if(isAttack){
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("saboten_stand")){
                anim.Play("saboten_attack");
                isAttack = false;
            }
        }
        rb.velocity = new Vector2(0, -Data.gravity);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        anim.Play("saboten_die");
    }

    private void release(){
        Togeboru.gameObject.SetActive(true);
        isAttack = Togeboru.ThrowBall();
    }
}
