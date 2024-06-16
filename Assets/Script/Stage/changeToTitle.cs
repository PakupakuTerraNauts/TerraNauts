using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class changeToTitle : MonoBehaviour
{
    public GameObject EnterInfo;

    void OnTriggerStay2D(Collider2D other)
    {
        bool ekey = Input.GetKey(KeyCode.E);
        if (ekey){
            SceneManager.LoadScene("TitleScean");
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
