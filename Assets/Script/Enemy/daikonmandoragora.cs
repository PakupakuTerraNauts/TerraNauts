using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daikonmandoragora : MonoBehaviour
{
    #region //variables
    //public float speed;
    public float gravity;

    private bool isDead = false;
    private SpriteRenderer sr = null;
    private Rigidbody2D rb = null;
    private ObjectCollision oc = null;
    private Animator anim = null;
    private BoxCollider2D col = null;
    private string playerTag = "Player";
    #endregion

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        oc = GetComponent<ObjectCollision>();
        col = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == playerTag){
            anim.Play("radissh_fumare");
        }
    }
}