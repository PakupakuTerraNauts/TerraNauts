using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree : MonoBehaviour
{
    #region // variables
    private float hp = 0.0f;
    private float ATK_player = 0.0f;
    public float gravity;

    private HPBar HP;
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    #endregion

    void Start(){
        HP = GetComponent<HPBar>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        ATK_player = Player.ATK;
        hp = HP.maxHealth;
    }

    void Update(){
        if(sr.isVisible){
            rb.WakeUp();
            rb.velocity = new Vector2(0, -gravity);
        }
        else{
            rb.Sleep();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Sword"){
            HP.UpdateHP(ATK_player);
            hp = hp - ATK_player;
        }
        if(hp <= 0.0f){
            anim.Play("pinesisa_tree_falling");
        }
    }
}
