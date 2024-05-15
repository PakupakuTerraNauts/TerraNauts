using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class neibysuraimu : Enemy
{
    private bool isFall = false;

    private BoxCollider2D boxcol = null;
    [SerializeField] private turara turara1;
    [SerializeField] private turara turara2;
    [SerializeField] private turara turara3;
    [SerializeField] private turara turara4;
    [SerializeField] private turara turara5;  // Å‘å”5

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
        rb.velocity = new Vector2(0.0f, -Data.gravity);
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
