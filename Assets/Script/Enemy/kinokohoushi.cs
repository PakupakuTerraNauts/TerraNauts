using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kinokohoushi : MonoBehaviour
{
    # region // variables
    // 動かない　float重力のみ
    public float gravity;
    private float hp = 0.0f;
    private float ATK_player = 0.0f;

    private bool isDead = false;

    private HPBar HP;
    private Rigidbody2D rb = null;
    private Animator anim = null;
    private SpriteRenderer sr = null;

    private string swordTag = "Sword";
    # endregion

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        hp = HPBar.instance.currentHealth;
        ATK_player = Player.ATK;
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
            HP.UpdateHP(ATK_player);
            hp = hp - ATK_player;
        }

        if(hp <= 0){
            anim.Play("kinoko_die");
            isDead = true;
            //Destroy(GameObject, 2f);
        }
    }
}