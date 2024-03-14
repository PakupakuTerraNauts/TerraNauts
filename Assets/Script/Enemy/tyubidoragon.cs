using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tyubidoragon : Enemy
{
    #region // variables
    public float speed;
    private float second = 0.0f;

    private bool isLeft = false;
    private bool isAttack = false;

    private CapsuleCollider2D capcol = null;
    #endregion

    protected override void Initialize(){
        capcol = GetComponent<CapsuleCollider2D>();
        capcol.enabled = true;
    }

    protected override void MovingF(){
        if(!isAttack){
            second += Time.deltaTime;
            anim.Play("tyuubi_walk");

            if(isLeft){
                transform.localScale = new Vector3(1, 1, 1);
                rb.velocity = new Vector2(-speed, -gravity);
                if(second > 4.0f){
                    isLeft = false;
                    isAttack = true;
                }
            }
            else{
                transform.localScale = new Vector3(-1, 1, 1);
                rb.velocity = new Vector2(speed, -gravity);
                if(second > 4.0f){
                    isLeft = true;
                    isAttack = true;
                }
            }
        }
        else{
            rb.velocity = new Vector2(0, -gravity);
            anim.Play("tyuubi_fire");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        anim.Play("tyuubi_die");
    }

    private void EndAnimation(){
        isAttack = false;
        second = 0.0f;
    }
}
