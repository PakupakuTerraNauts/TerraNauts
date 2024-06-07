using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nakabayashi : Enemy
{
    // variable
    private CircleCollider2D circol = null;

    // 攻撃はツタが行う 本体は特にすることがない
    protected override void Initialize(){
        circol = GetComponent<CircleCollider2D>();
        circol.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        anim.Play("nakabayashi_die");
    }
}
