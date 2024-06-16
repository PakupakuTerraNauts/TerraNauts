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
            case "TitleButton":
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
        GameObject TitleButton = GameObject.Find("TitleButton");
        Text Text3 = TitleButton.transform.GetChild(0).GetComponent<Text>();
        Text3.color = Color.black;
    }

    public void OnClickTitleButton(int i)
    {
        switch(i)
        {
            case 0:
                if(GameManager.instance.nowStage == 0)
                    SceneManager.LoadScene("enemies");
                else{
                    // セーブしていないステータス上昇分はリセット
                    StatusManager.PlayerStatusReset();
                    string Stage = "stage" + GameManager.instance.nowStage;
                    SceneManager.LoadScene(Stage);
                    // 倒された状態をセーブされていない敵は復活する
                    SingletonStage.instance.RespawnDeadEnemy();
                }
                break;
            case 1:
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
                Application.Quit();
                //ゲームプレイ終了
                #endif
                break;
            case 2:
                SceneManager.LoadScene("TitleScean");
                break;
        }
    }
}
