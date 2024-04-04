using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class changeToRestroom : MonoBehaviour
{
 
    [SerializeField] GameObject Player;
    static public Vector2 PlayerLocation = new Vector2(-0.5f, 0.75f);
    public PlayerFoodManager _playerFoodManager;

    private void Start() {
        Player.transform.position = PlayerLocation;    
    }

    void OnTriggerStay2D(Collider2D other)
    {
        bool wkey = Input.GetKey("w");
        if (Input.GetKey(KeyCode.UpArrow) || wkey){
            PlayerLocation = Player.transform.position;
            _playerFoodManager.UpdateSavedItemList();
            // “|‚µ‚½“G‚ª•œŠˆ‚µ‚È‚­‚È‚é
            SingletonStage1.instance.SaveDeadEnemy();
            SceneManager.LoadScene("restroom");
        }
    }
}
