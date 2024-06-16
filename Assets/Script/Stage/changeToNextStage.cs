using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class changeToNextStage : MonoBehaviour
{
    bool okey = false;
    public GameObject EnterInfo;

    void Update(){
        if(Input.GetKeyDown(KeyCode.O)) // メニューシーンでE=左なのでEでなくてOを使用
            okey = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (okey){
            okey = false;
            MenuChange.LoadMenuScean(5);    // 2ステージを作り終えるまでは終了ボタンに飛ばす
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
