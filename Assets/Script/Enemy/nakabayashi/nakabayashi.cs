using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nakabayashi : Enemy
{
    #region // variables
    private CircleCollider2D circol = null;

    [SerializeField] private ivy[] venu = new ivy[8];
    #endregion

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
