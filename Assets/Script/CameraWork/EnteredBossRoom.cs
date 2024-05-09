using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteredBossRoom : MonoBehaviour
{
    public bool isEnter = false;
    public Debidora debidora;
    public EntranceDoor entrance;

    private BoxCollider2D boxcol;

    void Start(){
        boxcol = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!isEnter){
            if(collision.tag == "Player"){
                Debug.Log("������");
                // BossCamera1�̃t�F�[�Y�Q�̃t���O
                isEnter = true;
                entrance.CloseDoor();
                // �P�w�{�X��HP�o�[����
                debidora.BossHPCountUp();
                entrance.JudgeDestory();
                boxcol.enabled = false;
            }
        }
    }
}
