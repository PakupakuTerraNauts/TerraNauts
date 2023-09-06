using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suraimu : MonoBehaviour
{
    private float HP = 70;
    private bool isDead = false;
    private Animator anim = null;
    private CircleCollider2D col = null;
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;

    void Start(){
        anim = GetComponent<Animator>();
        col = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update(){
        if(sr.isVisible){   //‰æ–ÊŠO‚Å‚Í“®‚©‚È‚¢

        } else {
            rb.Sleep();
        }
    } 
}
