using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suraimu : Enemy
{
    private CircleCollider2D circol = null;

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
    
/// <summary>
/// クリティカルエフェクト表示
/// </summary>
    protected override void onCriticalEffect(){
        Vector3 critPos = new Vector3(transform.position.x, transform.position.y - 1.5f, 0f); // デフォルトだとエフェクトの位置がずれる敵はオーバーライドして調整する
        CritEffect.instance.CriticalHit(critPos, Data.critRatio);
    }
}
