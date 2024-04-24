using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOverMenu:MonoBehaviour
{
    [SerializeField]
    private GameObject firstSelect;
    Button button;

    GameObject selectObj;
    Text Text;

    // Start is called before the first frame update
    void Start()
    {
        button = firstSelect.GetComponent<Button>();
        button.Select();
    }

    // Update is called once per frame
    void Update()
    {
        selectObj = EventSystem.current.currentSelectedGameObject;
        Text = selectObj.transform.GetChild(0).GetComponent<Text>();


        switch(selectObj.name)
        {
            case "ContinueButton":
                SetTextColor();
                Text.color = Color.white;
                break;
            case "EndButton":
                SetTextColor();
                Text.color = Color.white;
                break;
            default:
                break;
        }


    }

    void SetTextColor()
    {
        GameObject StartButton = GameObject.Find("ContinueButton");
        Text Text1 = StartButton.transform.GetChild(0).GetComponent<Text>();
        Text1.color = Color.black;
        GameObject ContinueButton = GameObject.Find("EndButton");
        Text1 = ContinueButton.transform.GetChild(0).GetComponent<Text>();
        Text1.color = Color.black;

    }

    public void OnClickTitleButton(int i)
    {
        switch(i)
        {
            case 0:
                // �Z�[�u���Ă��Ȃ��X�e�[�^�X�㏸���̓��Z�b�g
                PlayerStatusReset();
                SceneManager.LoadScene("stage1");
                // �|���ꂽ��Ԃ��Z�[�u����Ă��Ȃ��G�͕�������
                SingletonStage1.instance.RespawnDeadEnemy();
                break;
            case 1:
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
                Application.Quit();
                //�Q�[���v���C�I��
                #endif
                break;
        }
    }

    private void PlayerStatusReset(){
        Status.HP -= Player.HPincrement;
        Status.ATK -= Player.ATKincrement;
        Status.DEF -= Player.DEFincrement;
        Status.SPD -= Player.SPDincrement;
        Status.CRITRATE -= Player.CRITRATEincrement;
        Status.CRITDMG -= Player.CRITDMGincrement;
        Player.HPincrement = 0;
        Player.ATKincrement = 0;
        Player.DEFincrement = 0;
        Player.SPDincrement = 0;
        Player.CRITRATEincrement = 0;
        Player.CRITDMGincrement = 0;
    }
}
