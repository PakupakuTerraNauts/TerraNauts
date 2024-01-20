using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class niwakokepittya : Enemy
{
    #region //variables
    public GameObject PlayerObject;

    private float toriPosition_x = 0.0f;
    private float playerPosition_x = 0.0f;

    private bool isLeft = true;     // ‰Šúó‘Ô ‰EŒü‚«
    private CapsuleCollider2D capcol = null;
    #endregion

    protected override void Initialize(){
        capcol = GetComponent<CapsuleCollider2D>();
        PlayerObject = GameObject.Find("neko-default");
    }

    protected override void Moving(){
        toriPosition_x = transform.position.x;
        playerPosition_x = PlayerObject.transform.position.x;
        anim.Play("tori_pitch");
        rb.velocity = new Vector2(0, -gravity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        anim.Play("tori_die");
        capcol.tag = "DeadEnemy";
    }

    // player‚Ì•ûŒü‚ğ”»’è‚µ‚ÄA‚»‚Á‚¿‚ğŒü‚­ tori_pitchI—¹‚ÉŒÄ‚Ô
    public void DirectJudge(){
        if(playerPosition_x > toriPosition_x && isLeft){
            transform.localScale = new Vector3(-1, 1, 1);
            isLeft = false;
        }
        else if(playerPosition_x < toriPosition_x && !isLeft){
            transform.localScale = new Vector3(1, 1, 1);
            isLeft = true;
        }
    }
}

