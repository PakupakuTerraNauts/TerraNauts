using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class changeToTitle : MonoBehaviour
{

    public PlayerFoodManager _playerFoodManager;

    void OnTriggerStay2D(Collider2D other)
    {
        bool wkey = Input.GetKey("w");
        if (Input.GetKey(KeyCode.UpArrow) || wkey){
            SceneManager.LoadScene("TitleScean");
            _playerFoodManager.ItemReset();
        }
    }
}
