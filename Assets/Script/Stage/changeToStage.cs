using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class changeToStage : MonoBehaviour
{
    public int i;

    void getNum(int num) {
        i = num;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKey(KeyCode.UpArrow)){
            SceneManager.LoadScene("stage1");
        }
    }
}
