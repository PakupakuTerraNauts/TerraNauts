using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kanikanioyabun : MonoBehaviour
{
    #region // variables
    public float gravity;
    public float speed;

    private float second;
    private float hp = 0.0f;

    private bool isDead = false;
    private bool isLeft = false;
    private float ATK_player = 0.0f;

    private HPBar HP;

    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;
    
    private string swordTag = "Sword";
    #endregion


    void Start(){
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        HP = GetComponent<HPBar>();

        ATK_player = Player.ATK;
        hp = HPBar.instance.currentHealth;
    }

    void FixedUpdate(){
        second += Time.deltaTime;
        if(sr.isVisible){
            rb.velocity = new Vector2(0, -gravity);
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

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == swordTag && !isDead){
            HP.UpdateHP(ATK_player);
            hp = hp - ATK_player;
        }
        if(hp <= 0){
            anim.Play("kani_die");
            isDead = true;
            Destroy(gameObject, 3f);
        }
    }
}