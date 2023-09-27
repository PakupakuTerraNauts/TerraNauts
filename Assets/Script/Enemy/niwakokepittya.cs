using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class niwakokepittya : MonoBehaviour
{
    #region //variables
    public float gravity;
    public HPBar HP;

    private float hp = 0.0f;
    private bool isDead = false;
    private Animator anim = null;
    private CapsuleCollider2D capcol = null;
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private string swordTag = "Sword";
    #endregion

    void Start(){
        anim = GetComponent<Animator>();
        capcol = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        hp = HPBar.instance.currentHealth;
    }

    void Update(){
        if(sr.isVisible){
            rb.WakeUp();
            anim.Play("tori_pitch");    //player no houkou wo mukaseru.
        }
        else{
            rb.Sleep();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == swordTag && !isDead){
            HP.UpdateHP(10.0f);     //player no kougekiryoku wo tukutte koko ni ireru.
            hp -= 10.0f;            //koko mo kougekiryoku.
        }
        
        if(hp <= 0.0f){
            anim.Play("tori_die");
            isDead = true;
            capcol.enabled = false;
            Destroy(gameObject, 3f);
        }
    }
}

