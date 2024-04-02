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
            // �|�����G���������Ȃ��Ȃ�
            SingletonStage1.instance.SaveDeadEnemy();
            SceneManager.LoadScene("restroom");
        }
    }

    private void PlayerStatusReset(){
        // �l�������A�C�e�����Z�[�u
        _playerFoodManager.UpdateSavedItemList();

        // �X�e�[�^�X�̏㏸�����Z�[�u
        Player.ATK += Player.ATKincrement;
        Player.DEF += Player.DEFincrement;
        Player.SPD += Player.SPDincrement;
        Player.CRITRATE += Player.CRITRATEincrement;
        Player.CRITDMG += Player.CRITDMGincrement;
    }
}
