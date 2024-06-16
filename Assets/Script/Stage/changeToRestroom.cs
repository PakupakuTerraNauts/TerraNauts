using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class changeToRestroom : MonoBehaviour
{
    //static public Vector2 PlayerLocation = new Vector2(-0.5f, 0.75f);
    public GameObject EnterInfo;
    public PlayerFoodManager _playerFoodManager;

    // private void Awake() {
    //     Player.playerStartPos = PlayerLocation;
    // }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player"){

            bool ekey = Input.GetKey(KeyCode.E);
            if (ekey){
                //PlayerLocation = Player.playerPos.position;
                Player.playerStartPos = this.gameObject.transform.position;
                // セーブ処理
                _playerFoodManager.UpdateSavedItemList();   // アイテム数
                StatusManager.PlayerStatusSave();           // プレイヤーのステータス
                // 倒した敵が復活しなくなる
                SingletonStage.instance.SaveDeadEnemy();
                SceneManager.LoadScene("restroom");
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
