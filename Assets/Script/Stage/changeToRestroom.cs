using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class changeToRestroom : MonoBehaviour
{
 
    [SerializeField] GameObject Player;
    static public Vector2 PlayerLocation = new Vector2(113.1f, -0.6f);

    private void Start() {
        Player.transform.position = PlayerLocation;    
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKey(KeyCode.UpArrow)){
            PlayerLocation = Player.transform.position;
            SceneManager.LoadScene("restroom");
        }
    }
}
