using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class changeToNextStage : MonoBehaviour
{
    bool okey = false;
    public GameObject EnterInfo;

    void Update(){
        if(Input.GetKeyDown(KeyCode.O)) // ���j���[�V�[����E=���Ȃ̂�E�łȂ���O���g�p
            okey = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (okey){
            okey = false;
            MenuChange.LoadMenuScean(5);    // 2�X�e�[�W�����I����܂ł͏I���{�^���ɔ�΂�
        }
    }
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player")
            EnterInfo.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player")
            EnterInfo.SetActive(false);
    }
}
