using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kanikanioyabun : Enemy
{
    #region // variables
    public float speed;

    private float second = 0.0f;

    private bool isLeft = false;

    private BoxCollider2D boxcol = null;
    #endregion


    protected override void Initialize(){
        boxcol = GetComponent<BoxCollider2D>();
        boxcol.enabled = true;
    }

    protected override void MovingF(){
        second += Time.deltaTime;
        anim.Play("kani_default");

        if(isLeft){
            rb.velocity = new Vector2(-speed, -Data.gravity);
            if(second > 3.0f){
                isLeft = false;
                second = 0.0f;
            }
        }
        else{
            rb.velocity = new Vector2(speed, -Data.gravity);
            if(second > 3.0f){
                isLeft = true;
                second = 0.0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        anim.Play("kani_die");
    }
}