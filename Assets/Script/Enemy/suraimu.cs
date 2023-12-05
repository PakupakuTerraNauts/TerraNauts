using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suraimu : MonoBehaviour
{
    #region //variables
    public float gravity;
    //public HPBar HP;

    //private float hp = 0.0f;
    private bool isDead = false;
    private Animator anim = null;
    private CircleCollider2D circol = null;
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private string swordTag = "Sword";
    #endregion

    void Start(){
        anim = GetComponent<Animator>();
        circol = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        //hp = HPBar.instance.currentHealth;
    }

    void Update(){
        if(sr.isVisible){
            if(!isDead){
                rb.WakeUp();
                anim.Play("suraimu_furueru");
            }
        }
        else{
            rb.Sleep();
        }
    }

/*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == swordTag && !isDead){
            HP.UpdateHP(10.0f);     //player no kougekiryoku wo tukutte koko ni ireru.
            hp = hp - 10.0f;            //koko mo kougekiryoku.
        }
        
        if(hp <= 0.0f){
            anim.Play("suraimu_die");
            isDead = true;
            circol.enabled = false;
            Destroy(gameObject, 3f);
        }
    }
    */
}
