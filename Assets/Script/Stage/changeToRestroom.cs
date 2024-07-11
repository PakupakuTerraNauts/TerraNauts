using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class changeToRestroom : MonoBehaviour
{
    //static public Vector2 PlayerLocation = new Vector2(-0.5f, 0.75f);
    public GameObject EnterInfo;
    [SerializeField]
    private Player _player;
    [SerializeField]
    PlayerFoodManager _playerFoodManager;
    [SerializeField]
    PlayerStatusChange _statusChange;

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player"){

            bool ekey = Input.GetKey(KeyCode.E);
            if (ekey){
                //PlayerLocation = Player.playerPos.position;
                _player.PlayerStartPosition = this.gameObject.transform.position;
                // セーブ処理
                _playerFoodManager.UpdateSavedItemList();   // アイテム数
                _statusChange.PlayerStatusSave();          // プレイヤーのステータス
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
