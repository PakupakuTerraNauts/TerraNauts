using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suraimu : Enemy
{
    #region //variables
    private CircleCollider2D circol = null;
    #endregion

    protected override void Initialize(){
        circol = GetComponent<CircleCollider2D>();
        circol.enabled = true;
    }

    protected override void Moving(){
        rb.velocity = new Vector2(0, -Data.gravity);
    }


    private void OnTriggerEnter2D(Collider2D collision){
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        anim.Play("suraimu_die");
    }
}
