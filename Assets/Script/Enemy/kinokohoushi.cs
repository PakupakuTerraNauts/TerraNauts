using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kinokohoushi : MonoBehaviour
{
    # region // variables
    // 動かない　float重力のみ
    public float gravity;
    public float nowHP;

    private bool isDead = false;

    private Rigidbody2D rb = null;
    private Animator anim = null;
    private SpriteRenderer sr = null;


    public float ATK_player = Player.ATK;

    private string swordTag = "Sword";
    # endregion

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // アニメーション流すだけなのでUpdate
    void Update(){
        if(sr.isVisible){
            if(!isDead){
                rb.WakeUp();
            }
        }
        else{
            rb.Sleep();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.tag == swordTag){
            nowHP = nowHP - ATK_player;
        }

        if(nowHP <= 0){
            anim.Play("kinoko_die");
            isDead = true;
            //Destroy(GameObject, 2f);
        }
    }
}