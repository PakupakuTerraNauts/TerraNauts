using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.SceneManagement;
 
public class Test : MonoBehaviour
{
 
    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKey(KeyCode.UpArrow)){
            SceneManager.LoadScene("restroom");
        }
    }
}
