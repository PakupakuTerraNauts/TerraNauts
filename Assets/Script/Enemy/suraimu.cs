using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suraimu : MonoBehaviour
{
    #region //variables
    public float gravity;
    private float hp = 0.0f;
    private float ATK_player = 0.0f;

    private bool isDead = false;
    
    private HPBar HP;
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
        HP = GetComponent<HPBar>();

        ATK_player = Player.ATK;
        hp = HPBar.instance.currentHealth;
    }

    void Update(){
        if(sr.isVisible){
            if(!isDead){
                rb.WakeUp();
                anim.Play("suraimu_furueru");
                rb.velocity = new Vector2(0, -gravity);
            }
        }
        else{
            rb.Sleep();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == swordTag && !isDead){
            HP.UpdateHP(ATK_player);
            hp = hp - ATK_player;
        }
        
        if(hp <= 0.0f){
            anim.Play("suraimu_die");
            isDead = true;
            circol.enabled = false;
            Destroy(gameObject, 3f);
        }
    }
}
