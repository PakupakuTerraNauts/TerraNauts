using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{

    private bool isGround = false;
    private bool isGroundEnter, isGroundStay, isGroundExit;

    public bool IsGround(){    
        if (isGroundEnter || isGroundStay){
            isGround = true;
        }
        else if (isGroundExit){
            isGround = false;
        }
        else{
            isGround = false;
        }

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;
        return isGround;
    }

    // ’n–Ê‚É’…‚¢‚½‚Æ‚«
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "ground"){
            isGroundEnter = true;
        }
    }

    // ’n–Ê‚É‚¢‚é‚Æ‚«
    private void OnTriggerStay2D(Collider2D collision){
        if (collision.tag == "ground"){
            isGroundStay = true;
        }
    }

    // ’n–Ê‚©‚ç—£‚ê‚½‚Æ‚«
    private void OnTriggerExit2D(Collider2D collision){
        if (collision.tag == "ground"){
            isGroundExit = true;
        }
    }
}
