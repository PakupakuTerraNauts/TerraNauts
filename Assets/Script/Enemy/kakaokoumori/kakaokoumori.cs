using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kakaokoumori : Enemy
{
    #region // variables
    private BoxCollider2D boxcol = null;
    #endregion

    protected override void Initialize(){
        boxcol = GetComponent<BoxCollider2D>();
        boxcol.enabled = true;
    }

    protected override void Moving(){
        rb.velocity = new Vector2(0, gravity);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        anim.Play("kakao_die");
    }
}
