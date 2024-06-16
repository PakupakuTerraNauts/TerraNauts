using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeToEnemies : MonoBehaviour
{
    void Update(){
        if(Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.O)){
            GameManager.instance.nowStage = 0;
            SceneManager.LoadScene("enemies");
        }
    }
}
