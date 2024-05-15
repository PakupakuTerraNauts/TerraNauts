using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takotuborun : Enemy
{
    #region // variables

    [Header("チェックで向き反転")] public bool isLeft = true;

    private CapsuleCollider2D capcol = null;
    #endregion

    protected override void Initialize(){
        capcol = GetComponent<CapsuleCollider2D>();
        capcol.enabled = true;
        if(!isLeft){
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected override void Moving(){
        anim.Play("tako_default");
        rb.velocity = new Vector2(0, -Data.gravity);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        anim.Play("tako_die");
    }
}
