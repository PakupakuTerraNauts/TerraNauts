using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daikonmandoragora : MonoBehaviour
{
    #region //variables
    //public float speed;
    public float gravity;
    public HPBar HP;

    private float hp = 0.0f;
    private bool isDead = false;
    private bool isFumareta = false;
    private SpriteRenderer sr = null;
    private Rigidbody2D rb = null;
    private ObjectCollision oc = null;
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
        oc = GetComponent<ObjectCollision>();
        capcol = GetComponent<CapsuleCollider2D>();

        hp = HPBar.instance.currentHealth;
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == playerTag && !isFumareta){
            anim.Play("radissh_fumare");
            isFumareta = true;
            capcol.tag = "Enemy";  //enemy ja naku sitoite.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == swordTag && !isDead){
            HP.UpdateHP(10.0f); // player kara kougekiryoku wo syutoku suru.
            hp -= 10.0f;
        }

        if(hp <= 0.0f){
            anim.Play("radissh_die");
            isDead = true;
            capcol.enabled = false;
            Destroy(gameObject, 3f);
        }
    }
}