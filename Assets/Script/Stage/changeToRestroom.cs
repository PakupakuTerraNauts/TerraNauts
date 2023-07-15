using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.SceneManagement;
 
public class changeToBoss : MonoBehaviour
{
 
    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKey(KeyCode.UpArrow)){
            SceneManager.LoadScene("Shrine");
        }
    }
}
