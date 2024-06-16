using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class obakekabotya : Enemy
{
    #region // variables
    private float second = 0.0f;

    private bool isUp = true;
    private bool isLeft = true;  // 初期状態 右向き
    
    private CapsuleCollider2D capcol = null;

    [SerializeField] private kabotya[] kabo = new kabotya[5];
    #endregion

    protected override void Initialize(){
        capcol = GetComponent<CapsuleCollider2D>();
        capcol.enabled = true;

        foreach(var k in kabo)
            k.InitializeCallBack(onThrowKabotya);
    }
    
    protected override void Moving(){
        bool allKReady = false;
        foreach(var k in kabo){
            if(!k.Ready)
                break;
            allKReady = true;   // 全てのカボチャが手元に帰ってきたら攻撃可能になる
        }

        if(allKReady){
            foreach(var k in kabo)
                k.gameObject.SetActive(true);
        }
    }

    /// fixedUpdate 上下にフワフワしている
    protected override void MovingF(){
        second += Time.deltaTime;
        
        if(isUp){
            rb.velocity = new Vector2(0.0f, Data.gravity);
            if(second > 1.0f){
                isUp = false;
                second = 0.0f;
            }
        }
        else{
            rb.velocity = new Vector2(0.0f, -Data.gravity);
            if(second > 1.0f){
                isUp = true;
                second = 0.0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        anim.Play("kabotya_die");
        foreach(var k in kabo)
            k.ObakeDead();
    }

/// <summary>
/// おばけの攻撃アニメーションと向き
/// </summary>
    private void onThrowKabotya(){
        if(!isDead){
            anim.Play("kabotya_attack");
            
            float kaboPosition_x = transform.position.x;
            float playerPosition_x = Player.playerPos.position.x;

            if(playerPosition_x > kaboPosition_x && isLeft){
                transform.localScale = new Vector3(-1, 1, 1);
                isLeft = false;
            }
            else if(playerPosition_x < kaboPosition_x && !isLeft){
                transform.localScale = new Vector3(1, 1, 1);
                isLeft = true;
            }
        }
    }
}
