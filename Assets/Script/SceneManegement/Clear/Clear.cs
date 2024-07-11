using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Clear:MonoBehaviour
{
    [SerializeField]
    private GameObject firstSelect;
    Button button;

    GameObject selectObj;
    Text _Text;

    [SerializeField]
    private Text _StageNum;
    [SerializeField]
    private Text _Clear;
    [SerializeField]
    private Canvas _buttonCanvas;

    private bool isShown = false;

    // Start is called before the first frame update
    void Start()
    {
        button = firstSelect.GetComponent<Button>();
        button.Select();

        _StageNum.text = "STAGE " + GameManager.instance.nowStage;

        StartCoroutine(showClear());
    }

    // Update is called once per frame
    void Update()
    {
        if(!isShown)
            return;

        selectObj = EventSystem.current.currentSelectedGameObject;
        _Text = selectObj.transform.GetChild(0).GetComponent<Text>();


        switch(selectObj.name)
        {
            case "ContinueButton":
                SetTextColor();
                _Text.color = Color.white;
                break;
            case "EndButton":
                SetTextColor();
                _Text.color = Color.white;
                break;
            case "TitleButton":
                SetTextColor();
                _Text.color = Color.white;
                break;
            default:
                break;
        }

    }

    void SetTextColor()
    {
        GameObject ContinueButton = GameObject.Find("ContinueButton");
        Text Text1 = ContinueButton.transform.GetChild(0).GetComponent<Text>();
        Text1.color = Color.black;
        GameObject EndButton = GameObject.Find("EndButton");
        Text Text2 = EndButton.transform.GetChild(0).GetComponent<Text>();
        Text2.color = Color.black;
        GameObject TitleButton = GameObject.Find("TitleButton");
        Text Text3 = TitleButton.transform.GetChild(0).GetComponent<Text>();
        Text3.color = Color.black;
    }

    public void OnClickTitleButton(int i)
    {
        switch(i)
        {
            case 0:
                StartCoroutine(TimeRestart());
                break;
            case 1:
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
                Application.Quit();
                //ゲームプレイ終了
                #endif
                break;
            case 2:
                StartCoroutine(TimeRestart());
                SceneManager.LoadScene("TitleScean");
                break;
        }
    }

    IEnumerator TimeRestart(){
        Time.timeScale = 1;
        yield return null;
        Player.UnRestrainedByEvent();
        
        SceneManager.UnloadSceneAsync("Clear");
    }

    IEnumerator showClear(){
        _StageNum.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);

        _Clear.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1.5f);

        _buttonCanvas.gameObject.SetActive(true);
        isShown = true;
    }
}
