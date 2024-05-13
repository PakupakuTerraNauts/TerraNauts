using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ivyRange : MonoBehaviour
{
    private BoxCollider2D boxcol = null;

    public delegate void ivyAttack();
    private ivyAttack ivyAttackCallBack;

    void Awake(){
        boxcol = GetComponent<BoxCollider2D>(); //ivy.Start ‚©‚ç InitializeRange ‚ðŒÄ‚Ô‚½‚ß Awake
    }

    public void InitializeCallBack(ivyAttack onIvyAttack){
        ivyAttackCallBack = onIvyAttack;
    }

    public void InitializeRange(){
        boxcol.size = new Vector2(3f, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            ivyAttackCallBack();
        }
    }

    private void OnTriggerStay2D(Collider2D collision){
        if(collision.tag == "Player"){
            ivyAttackCallBack();
        }
    }
}
