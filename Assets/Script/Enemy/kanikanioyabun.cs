using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kanikanioyabun : MonoBehaviour
{
    #region // variables
    public float gravity;
    public float speed;
    private float second = 0.0f;
    public float nowHP;

    public float ATK_player = Player.ATK;

    private bool isDead = false;
    private bool isLeft = false;

    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;

    private string swordTag = "Sword";
    #endregion

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate(){
        second += Time.deltaTime;
        if(sr.isVisible){
            if(!isDead){
                rb.WakeUp();
                anim.Play("kani_default");

                if(isLeft){
                    rb.velocity = new Vector2(-speed, -gravity);
                    if(second > 3.0f){
                        isLeft = false;
                        second = 0.0f;
                    }
                }
                else{
                    rb.velocity = new Vector2(speed, -gravity);
                    if(second > 3.0f){
                        isLeft = true;
                        second = 0.0f;
                    }
                }
            }
        }
        else{
            rb.Sleep();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.tag == swordTag && !isDead){
            nowHP = nowHP - ATK_player;
        }
        if(nowHP <= 0){
            anim.Play("kani_die");
            isDead = true;
            Destroy(gameObject, 3f);
        }
    }
}