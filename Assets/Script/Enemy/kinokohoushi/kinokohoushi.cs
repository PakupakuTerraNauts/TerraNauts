using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class kinokohoushi : Enemy
{
    private CapsuleCollider2D capcol = null;
    public houshi hoshi1;
    public houshi hoshi2;
    public houshi hoshi3;
    public houshi hoshi4;   // ç≈ëÂêî4
    public Random rand = new Random();
    
    protected override void Initialize(){
        capcol = GetComponent<CapsuleCollider2D>();
        capcol.enabled = true;
    }

    protected override void Moving(){
        anim.Play("kinoko_hoshi");
        rb.velocity = new Vector2(0, -gravity);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        anim.Play("kinoko_die");
    }

    private void release(){
        hoshi1.Hoshi(rand);
        // 1~4Ç≈ñEéqÇÃêîÇí≤êÆâ¬î\
        try{
            hoshi2.Hoshi(rand);
            hoshi3.Hoshi(rand);
            hoshi4.Hoshi(rand);
        }
        catch(Exception){
            return;
        }
    }
    private void deletehoshi(){    
        hoshi1.HoshiDelete();
        try{
            hoshi2.HoshiDelete();
            hoshi3.HoshiDelete();
            hoshi4.HoshiDelete();
        }
        catch(Exception){
            return;
        }
    }
}