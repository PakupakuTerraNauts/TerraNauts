using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class TitleMenu:MonoBehaviour
{
    [SerializeField]
    private GameObject firstSelect;
    Button button;

    public PlayerFoodManager _playerFoodManager;

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
        if(selectObj == null){
            button.Select();
            selectObj = firstSelect;
        }
        if(selectObj.transform.childCount > 0)
            Text = selectObj.transform.GetChild(0).GetComponent<Text>();

        switch(selectObj.name)
        {
            case "StartButton":
                SetTextColor();
                Text.color = Color.white;
                break;
            case "ContinueButton":
                SetTextColor();
                Text.color = Color.white;
                break;
            case "SettingButton":
                SetTextColor();
                Text.color = Color.white;
                break;
            case "EndButton":
                SetTextColor();
                Text.color = Color.white;
                break;
            case "TutorialButton":
                SetTextColor();
                Text.color = Color.white;
                break;
            default:
                break;
        }


    }

    void SetTextColor()
    {
        GameObject StartButton = GameObject.Find("StartButton");
        Text Text1 = StartButton.transform.GetChild(0).GetComponent<Text>();
        Text1.color = Color.black;
        GameObject ContinueButton = GameObject.Find("ContinueButton");
        Text1 = ContinueButton.transform.GetChild(0).GetComponent<Text>();
        Text1.color = Color.black;
        GameObject SettingButton = GameObject.Find("SettingButton");
        Text1 = SettingButton.transform.GetChild(0).GetComponent<Text>();
        Text1.color = Color.black;
        GameObject EndButton = GameObject.Find("EndButton");
        Text1 = EndButton.transform.GetChild(0).GetComponent<Text>();
        Text1.color = Color.black;
        GameObject TutorialButton = GameObject.Find("TutorialButton");
        Text1 = TutorialButton.transform.GetChild(0).GetComponent<Text>();
        Text1.color = Color.black;

    }

    public void OnClickTitleButton(int i)
    {
        switch(i)
        {
            case 0:
                // ������
                Player.InitializePlayerStatus();                        // �v���C���[�̃X�e�[�^�X�����Z�b�g����
                Player.playerStartPos = new Vector2(-0.5f, 0.75f);      // �v���C���[�������ʒu�Ɉړ�
                _playerFoodManager.ItemReset();                         // �`���[�g���A���Ŏ擾�����A�C�e���̓��Z�b�g
                if(SingletonStage.instance != null)
                    Destroy(SingletonStage.instance.gameObject);

                GameManager.instance.nowStage = 1;
                SceneManager.LoadScene("stage1");
                break;
            case 1:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
                    Application.Quit();
                    //�Q�[���v���C�I��
#endif
                break;
            case 2:
                SceneManager.LoadScene("tutorial");
                break;
            case 3:
                MenuChange.LoadMenuScean(3);
                break;
        }
    }
}
