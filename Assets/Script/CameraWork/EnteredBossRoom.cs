using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteredBossRoom : MonoBehaviour
{
    public Debidora debidora;
    public EntranceDoor entrance;

    private BoxCollider2D boxcol;

    void Start(){
        boxcol = GetComponent<BoxCollider2D>();
    }

    // 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!debidora.isEntered && collision.tag == "Player"){
            Debug.Log("������");

            // BossCamera1�̃t�F�[�Y�Q�J�ڃt���O
            debidora.isEntered = true;
            entrance.CloseDoor();

            // �P�w�{�X��HP�o�[����
            debidora.BossHPCountUp();
            entrance.JudgeDestory();
            boxcol.enabled = false;
            
        }
    }
}
