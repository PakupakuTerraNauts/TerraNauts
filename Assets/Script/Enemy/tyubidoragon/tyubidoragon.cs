using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyubidoragon : Enemy
{
    #region // variables
    public float speed;
    private float leftlim;
    private float rightlim;

    private bool isLeft = true;
    [HideInInspector] public bool isAttack = false;

    private CapsuleCollider2D capcol = null;
    #endregion

    protected override void Initialize(){
        capcol = GetComponent<CapsuleCollider2D>();
        capcol.enabled = true;

        // 左右に移動できる限界の距離を決めておく
        // 任意の位置（プレイヤーがrangeに入ったところ）で攻撃が始まるから
        leftlim = transform.position.x - 5.0f;
        rightlim = transform.position.x + 5.0f;
    }

    // fixedUpdate 左右に動く
    protected override void MovingF(){
        if(!isAttack){
            if(isLeft){
                rb.velocity = new Vector2(-speed, -Data.gravity);

                if(transform.position.x < leftlim){
                    transform.localScale = new Vector3(-1, 1, 1);
                    isLeft = false;
                }
            }
            else{   // 右向き
                rb.velocity = new Vector2(speed, -Data.gravity);

                if(transform.position.x > rightlim){
                    transform.localScale = new Vector3(1, 1, 1);
                    isLeft = true;
                }
            }
        }
        else{   // 攻撃フラグ ON
            rb.velocity = new Vector2(0, -Data.gravity);
            anim.Play("tyuubi_fire");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        recievedDamage(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.tag == "Player"){
            isLeft = !isLeft;
            if(isLeft)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected override void dieAnimation(){
        anim.Play("tyuubi_die");
    }

/// <summary>
/// アニメーションイベントから終了検知
/// </summary>
    private void EndAnimation(){
        isAttack = false;
        anim.Play("tyuubi_walk");
    }
}
