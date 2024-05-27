using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class changeToRestroom : MonoBehaviour
{
 
    [SerializeField] GameObject player;
    static public Vector2 PlayerLocation = new Vector2(-0.5f, 0.75f);
    public PlayerFoodManager _playerFoodManager;
    public GameObject EnterInfo;

    private void Start() {
        player.transform.position = PlayerLocation;
        EnterInfo.SetActive(false);  
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player"){

            bool ekey = Input.GetKey("e");
            if (ekey){
                PlayerLocation = player.transform.position;
                PlayerStatusSave();
                // �|�����G���������Ȃ��Ȃ�
                SingletonStage.instance.SaveDeadEnemy();
                // ��������UI�\��������
                if(EnterInfo.activeSelf)
                    EnterInfo.SetActive(true);
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

    private void PlayerStatusSave(){
        // �����Ă���A�C�e�����Z�[�u
        _playerFoodManager.UpdateSavedItemList();

        // �X�e�[�^�X�̏㏸���Z�[�u
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
