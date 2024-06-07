using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class changeToTitle : MonoBehaviour
{
    public PlayerFoodManager _playerFoodManager;
    public GameObject EnterInfo;

    void OnTriggerStay2D(Collider2D other)
    {
        bool ekey = Input.GetKey("e");
        if (ekey){
            SceneManager.LoadScene("TitleScean");
            _playerFoodManager.ItemReset();
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
