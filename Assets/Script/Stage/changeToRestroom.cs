using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class changeToRestroom : MonoBehaviour
{
 
    [SerializeField] GameObject Player;
    static public Vector2 PlayerLocation = new Vector2(-0.5f, 0.75f);

    private void Start() {
        Player.transform.position = PlayerLocation;    
    }

    void OnTriggerStay2D(Collider2D other)
    {
        bool wkey = Input.GetKey("w");
        if (Input.GetKey(KeyCode.UpArrow) || wkey){
            PlayerLocation = Player.transform.position;
            SceneManager.LoadScene("restroom");
        }
    }
}
