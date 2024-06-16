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

    // �n�ʂɒ������Ƃ�
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "ground"){
            isGroundEnter = true;
        }
    }

    // �n�ʂɂ���Ƃ�
    private void OnTriggerStay2D(Collider2D collision){
        if (collision.tag == "ground"){
            isGroundStay = true;
        }
    }

    // �n�ʂ��痣�ꂽ�Ƃ�
    private void OnTriggerExit2D(Collider2D collision){
        if (collision.tag == "ground"){
            isGroundExit = true;
        }
    }
}
