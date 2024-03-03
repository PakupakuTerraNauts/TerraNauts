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
    Menu _menu;

    void Start()
    {
        _menu = GameObject.Find("MenuCanvas").GetComponent<Menu>();

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
