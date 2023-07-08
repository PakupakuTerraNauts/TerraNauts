using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daikonmandoragora : MonoBehaviour
{
    #region //variables
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
    public float speed;
>>>>>>> 75a25fae69896b08d2dadff28967600e838859a5
>>>>>>> 8654ea4f8de298b9a51b59631adc1a89103b448b
    public float gravity;

    private SpriteRenderer sr = null;
    private Rigidbody2D rb = null;
    private ObjectCollision oc = null;
    private Animator anim = null;
    private BoxCollider2D col = null;
    //private bool isDead = false;
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
<<<<<<< HEAD
    {
        
=======
<<<<<<< HEAD
    {
        
=======
    {/*
        if(fumaretatoki){

        }
        else{
            rb.Sleep();
        }
        */
>>>>>>> 75a25fae69896b08d2dadff28967600e838859a5
>>>>>>> 8654ea4f8de298b9a51b59631adc1a89103b448b
    }
}
