using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//最初にフォーカスするゲームをオブジェクトを設定、セレクトでボタンが押される
public class Interactable:MonoBehaviour
{
    [SerializeField]
    private GameObject firstSelect;
    Button button;

    GameObject selectObj;
    [SerializeField]
    Menu _menu;

    void Start()
    {
        //_menu = GameObject.Find("MenuCanvas").GetComponent<Menu>();

        button = firstSelect.GetComponent<Button>();
        button.Select();
    }

/// <summary>
/// メニューで最初に開く画面をデフォのステータスから上書きする
/// </summary>
/// <param name="buttonNum">上書きしたいメニューボタンの名前</param>
    public void SetFirstSelect(int buttonNum){
        switch(buttonNum){
            case 0:
                firstSelect = _menu.transform.GetChild(0).gameObject;
                break;
            case 1:
                firstSelect = _menu.transform.GetChild(1).gameObject;
                break;
            case 2:
                firstSelect = _menu.transform.GetChild(2).gameObject;
                break;
            case 3:
                firstSelect = _menu.transform.GetChild(3).gameObject;
                break;
            case 4:
                firstSelect = _menu.transform.GetChild(4).gameObject;
                break;
            case 5:
                firstSelect = _menu.transform.GetChild(5).gameObject;
                break;
            default:
                break;
        }

        button = firstSelect.GetComponent<Button>();
        button.Select();
    }

    void Update()
    {
        selectObj = EventSystem.current.currentSelectedGameObject;

        switch(selectObj.name)
        {
            case "StatusButton":
                _menu.PushButton(0);
                break;
            case "ItemButton":
                _menu.PushButton(1);
                break;
            case "SkillButton":
                _menu.PushButton(2);
                break;
            case "SettingButton":
                _menu.PushButton(3);
                break;
            case "LoadButton":
                _menu.PushButton(4);
                break;
            case "ExitButton":
                _menu.PushButton(5);
                break;
            default:
                break;
        }
    }
}
