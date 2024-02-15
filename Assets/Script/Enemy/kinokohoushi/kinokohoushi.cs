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
    public Random rand = new Random();
    
    protected override void Initialize(){
        capcol = GetComponent<CapsuleCollider2D>();
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
        capcol.tag = "DeadEnemy";
    }

    private void release(){
        hoshi1.Hoshi(rand);
        hoshi2.Hoshi(rand);
        hoshi3.Hoshi(rand);
    }
    private void deletehoshi(){
        hoshi1.HoshiDelete();
        hoshi2.HoshiDelete();
        hoshi3.HoshiDelete();
    }
}