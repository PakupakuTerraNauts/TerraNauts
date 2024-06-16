using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnteredBossRoom : MonoBehaviour
{
    public UnityEvent onEnteredBossRoom = new UnityEvent();
    public EntranceDoor entrance;

    private BoxCollider2D boxcol;
    private bool isEntered = false;

    void Start(){
        boxcol = GetComponent<BoxCollider2D>();
    }

    // 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!isEntered && collision.tag == "Player"){
            isEntered = true;
            Debug.Log("������");

            // BossCamera1�̃t�F�[�Y�Q�J�ڃt���O
            entrance.CloseDoor();

            // �P�w�{�X��HP�o�[����
            onEnteredBossRoom.Invoke(); // �R�[���o�b�N
            entrance.JudgeDestory();
            boxcol.enabled = false;
            
        }
    }
}
