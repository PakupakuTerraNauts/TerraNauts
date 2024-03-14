using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class neibysuraimu : Enemy
{
    private bool isFall = false;

    private BoxCollider2D boxcol = null;
    public turara turara1;
    public turara turara2;
    public turara turara3;
    public turara turara4;
    public turara turara5;  // 最大数5

    protected override void Initialize(){
        boxcol = GetComponent<BoxCollider2D>();
        boxcol.enabled = true;
    }

    protected override void Moving(){
        if(!isFall){
            anim.Play("neiby_attack");
        }else{
            anim.Play("neiby_default");
        }
        rb.velocity = new Vector2(0.0f, -gravity);
        isFall = turara1.FallTF();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        anim.Play("neiby_die");
    }

    private void whistle(){
        turara1.Turara();
        // 1~5で氷柱の数を調節可能
        try{
            turara2.Turara();
            turara3.Turara();
            turara4.Turara();
            turara5.Turara();
        }
        catch(Exception){
            return;
        }
    }
}
