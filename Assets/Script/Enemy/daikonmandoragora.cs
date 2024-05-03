using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daikonmandoragora : Enemy
{
    #region //variables
    
    private bool isSteppedOn = false;
    private BoxCollider2D boxcol = null;
    private EdgeCollider2D edgcol = null;
    #endregion

    protected override void Initialize()
    {
        boxcol = GetComponent<BoxCollider2D>();
        edgcol = GetComponent<EdgeCollider2D>();
        boxcol.enabled = true;
        edgcol.enabled = true;
    }

    protected override void Moving(){
        rb.velocity = new Vector2(0, -gravity);
    }

    protected override void Sleeping(){
        if(!isDead){
            anim.Play("radissh_umari");     // âÊñ îÉÇ¢Ç…èoÇΩÇÁradissh_umariÇ…ñﬂÇÈ
            gameObject.tag = "EnemySleep";
            isSteppedOn = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player" && !isSteppedOn && !isDead){
            anim.Play("radissh_fumare");
            isSteppedOn = true;
            StartCoroutine(ChangeTag());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(!isDead)
            recievedDamage(collision);
    }

    protected override void dieAnimation(){
        if(!isDead){
            gameObject.tag = "EnemySleep";
            anim.Play("radissh_die");
        }
    }

    private IEnumerator ChangeTag(){
        yield return new WaitForSeconds(4.0f);
        gameObject.tag = "Enemy";
    }
    
}