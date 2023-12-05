using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daikonmandoragora : MonoBehaviour
{
    #region //variables
    public float gravity;
    public float nowHP;

    public float ATK_player = Player.ATK;

    private bool isDead = false;
    private bool isSteppedOn = false;
    private bool isLooked = false;
    private SpriteRenderer sr = null;
    private Rigidbody2D rb = null;
    private Animator anim = null;
    private CapsuleCollider2D capcol = null;
    private string playerTag = "Player";
    private string swordTag = "Sword";

    [Header("プレイヤー")] public Player player;
    #endregion

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capcol = GetComponent<CapsuleCollider2D>();
    }

// Switching true/false of islooked
    void Update(){
        if(sr.isVisible){
            if(!isDead && !isLooked){
                isLooked = true;
                rb.WakeUp();
                anim.Play("radissh_umari");
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
            nowHP = nowHP - ATK_player;
        }

        if(nowHP <= 0.0f){
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