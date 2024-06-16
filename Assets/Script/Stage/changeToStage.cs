using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class changeToStage : MonoBehaviour
{
    public int i;
    public GameObject EnterInfo;

    void getNum(int num) {
        i = num;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        bool ekey = Input.GetKey(KeyCode.E);
        if (ekey){
            // ���݂̃X�e�[�W�����擾���Arestroom��stage �̈ړ��Ɏg�p
            string stage = "stage" + GameManager.instance.nowStage;
            SceneManager.LoadScene(stage);
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
