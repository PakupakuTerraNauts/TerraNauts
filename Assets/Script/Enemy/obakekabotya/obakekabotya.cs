using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obakekabotya : Enemy
{
    #region // variables
    private bool isLeft = true;  // ‰Šúó‘Ô ‰EŒü‚«
    private CapsuleCollider2D capcol = null;
    #endregion

    protected override void Initialize(){
        capcol = GetComponent<CapsuleCollider2D>();
        capcol.enabled = true;
    }

    public void ThrowKabotya(){
        anim.Play("kabotya_attack");
        
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        float kaboPosition_x = transform.position.x;
        float playerPosition_x = Player.transform.position.x;

        if(playerPosition_x > kaboPosition_x && isLeft){
            transform.localScale = new Vector3(-1, 1, 1);
            isLeft = false;
        }
        else if(playerPosition_x < kaboPosition_x && !isLeft){
            transform.localScale = new Vector3(1, 1, 1);
            isLeft = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        anim.Play("kabotya_die");
    }
}