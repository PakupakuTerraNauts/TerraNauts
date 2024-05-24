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
        else{   // �f�o�b�O �A�j���[�V�����̊֌W�łǂ����Ă�exit������Ȃ����Ƃ�����
                // �󒆂ɂ���̂�isGround=true�ƂȂ�̂��v���I�Ȃ̂Ńf�t�H���g��false�ɂ���
            isGround = false;
        }

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;
        return isGround;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "ground"){
            isGroundEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision){
        if (collision.tag == "ground"){
            isGroundStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.tag == "ground"){
            isGroundExit = true;
        }
    }
}
