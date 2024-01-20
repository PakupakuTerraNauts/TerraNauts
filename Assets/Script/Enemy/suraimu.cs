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
    }

    protected override void Moving(){
        anim.Play("suraimu_furueru");
        rb.velocity = new Vector2(0, -gravity);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        anim.Play("suraimu_die");
        circol.tag = "DeadEnemy";
    }

}
