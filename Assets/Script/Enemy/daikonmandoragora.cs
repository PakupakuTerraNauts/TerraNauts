using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daikonmandoragora : Enemy
{
    #region //variables
    
    private bool isSteppedOn = false;
    private CapsuleCollider2D capcol = null;
    private CircleCollider2D circol = null;

    #endregion

    protected override void Initialize()
    {
        capcol = GetComponent<CapsuleCollider2D>();
        circol = GetComponent<CircleCollider2D>();
    }

    protected override void Moving(){
        rb.velocity = new Vector2(0, -gravity);
    }

    protected override void Sleeping(){
        if(!isDead){
            anim.Play("radissh_umari");     // âÊñ îÉÇ¢Ç…èoÇΩÇÁradissh_umariÇ…ñﬂÇÈ
            gameObject.tag = "Untagged";
            isSteppedOn = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player" && !isSteppedOn && !isDead){
            anim.Play("radissh_fumare");
            isSteppedOn = true;
            StartCoroutine("ChangeTag");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(!isDead)
            recievedDamage(collision);
    }

    protected override void dieAnimation(){
        if(!isDead){
            anim.Play("radissh_die");
            capcol.tag = "DeadEnemy";
        }
    }

    private IEnumerator ChangeTag(){
        yield return new WaitForSeconds(4.0f);
        capcol.tag = "Enemy";
    }
}