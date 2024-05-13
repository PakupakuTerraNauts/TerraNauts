using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class changeToRestroom : MonoBehaviour
{
 
    [SerializeField] GameObject player;
    static public Vector2 PlayerLocation = new Vector2(-0.5f, 0.75f);
    public PlayerFoodManager _playerFoodManager;

    private void Start() {
        player.transform.position = PlayerLocation;    
    }

    void OnTriggerStay2D(Collider2D other)
    {
        bool wkey = Input.GetKey("w");
        if (Input.GetKey(KeyCode.UpArrow) || wkey){
            PlayerLocation = player.transform.position;
            PlayerStatusSave();
            // 倒した敵が復活しなくなる
            SingletonStage1.instance.SaveDeadEnemy();
            SceneManager.LoadScene("restroom");
        }
    }

    private void PlayerStatusSave(){
        // 持っているアイテムをセーブ
        _playerFoodManager.UpdateSavedItemList();

        // ステータスの上昇をセーブ
        Player.HP += Player.HPincrement;
        Player.HPincrement = 0;
        Player.ATK += Player.ATKincrement;
        Player.ATKincrement = 0;
        Player.DEF += Player.DEFincrement;
        Player.DEFincrement = 0;
        Player.SPD += Player.SPDincrement;
        Player.SPDincrement = 0;
        Player.CRITRATE += Player.CRITDMGincrement;
        Player.CRITRATEincrement = 0;
        Player.CRITDMG += Player.CRITDMGincrement;
        Player.CRITDMGincrement = 0;
    }
}
