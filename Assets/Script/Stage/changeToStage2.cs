using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class changeToStage2 : MonoBehaviour
{
    private SingletonStage singleton = SingletonStage.instance;
    public GameObject EnterInfo;

    void OnTriggerStay2D(Collider2D other)
    {
        bool okey = Input.GetKeyDown("o");
        if (okey && !MenuChange.isMenuOpen){
            MenuChange.LoadMenuScean(5);
            //SceneManager.LoadScene("stage2");
            if(singleton != null){
                Destroy(singleton.gameObject);
                singleton = null;
            }
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
