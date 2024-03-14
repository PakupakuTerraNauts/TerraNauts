using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class painsisa : Enemy
{
    #region // variables
    private bool instantDeath = false;
    public bool isAttack = false;

    private CircleCollider2D circol = null;
    #endregion

    protected override void Initialize(){
        circol = GetComponent<CircleCollider2D>();
        circol.enabled = true;
    }

    protected override void Moving(){
        rb.velocity = new Vector2(0, -gravity);
        if(isAttack){
            anim.Play("pinesisa_attack");
        }
        else{
            anim.Play("pinesisa_stand");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "FallingTree"){
            instantDeath = true;
        }
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        if(instantDeath){
            anim.Play("pinesisa_treedie");
        }
        else{
            anim.Play("pinesisa_defdie");
        }
    }

    private void endAnimation(){
        isAttack = false;
    }
}
