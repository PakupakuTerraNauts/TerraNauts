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
    public turara turara5;  // Å‘å”5

    protected override void Initialize(){
        boxcol = GetComponent<BoxCollider2D>();
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
        boxcol.tag = "DeadEnemy";
    }

    private void whistle(){
        turara1.Turara();
        // 1~5‚Å•X’Œ‚Ì”‚ğ’²ß‰Â”\
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
