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
        bool ekey = Input.GetKey("e");
        if (ekey){
            SceneManager.LoadScene("stage1");
        }
    }
}
