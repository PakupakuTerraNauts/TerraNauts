using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daikonmandoragora : MonoBehaviour
{
    #region //variables
    public float gravity;
    private float hp = 0.0f;
    
    private HPBar HP;
    private float ATK_player = 0.0f;
    
    private bool isDead = false;
    private bool isSteppedOn = false;
    private bool isLooked = false;
    private SpriteRenderer sr = null;
    private Rigidbody2D rb = null;
    private Animator anim = null;
    private CapsuleCollider2D capcol = null;
    private string playerTag = "Player";
    private string swordTag = "Sword";

    #endregion

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capcol = GetComponent<CapsuleCollider2D>();
        HP = GetComponent<HPBar>();

        ATK_player = Player.ATK;
        hp = HPBar.instance.currentHealth;
    }

// Switching true/false of islooked
    void Update(){
        if(sr.isVisible){
            if(!isDead && !isLooked){
                isLooked = true;
                rb.WakeUp();
                anim.Play("radissh_umari");
                rb.velocity = new Vector2(0, -gravity);
            }
        }
        else{
            // STOP when isn't looked.
            isLooked = false;
            rb.Sleep();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == playerTag && !isSteppedOn){
            anim.Play("radissh_fumare");
            isSteppedOn = true;
            StartCoroutine("ChangeTag");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == swordTag && !isDead){
            HP.UpdateHP(ATK_player);
            hp = hp - ATK_player;
        }

        if(hp <= 0.0f){
            anim.Play("radissh_die");
            isDead = true;
            capcol.enabled = false;
            Destroy(gameObject, 3f);
        }
    }

    private IEnumerator ChangeTag(){
        yield return new WaitForSeconds(5.0f);
        capcol.tag = "Enemy";
    }
}