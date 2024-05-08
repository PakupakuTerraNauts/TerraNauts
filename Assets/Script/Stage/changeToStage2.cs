using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class changeToStage2 : MonoBehaviour
{
    private SingletonStage1 singleton = SingletonStage1.instance;

    void OnTriggerStay2D(Collider2D other)
    {
        bool wkey = Input.GetKey("w");
        if (Input.GetKey(KeyCode.UpArrow) || wkey){
            SceneManager.LoadScene("stage2");
            if(singleton != null){
                Destroy(singleton.gameObject);
                singleton = null;
            }
        }
    }
}
