using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//ボタンセレクトとプッシュ
public class SelectScript : MonoBehaviour
{

    GameObject _gameObject;
    GameObject _firstObj;
    Button _firstButton;
    GameObject _selectedObject;
    Button button;

    PushDishButton _pushDishButton;

    void Start()
    {
        //一番上のボタンをセレクト
        _gameObject = GameObject.Find("RecipeContent_C");
        _firstObj = _gameObject.transform.GetChild(0).gameObject;
        _firstButton = _firstObj.GetComponent<Button>();
        _firstButton.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject)
        {
            _pushDishButton = GameObject.Find("PrefabCookButton").GetComponent<PushDishButton>();
            _selectedObject = EventSystem.current.currentSelectedGameObject;
            if(_selectedObject != null)
            {
                button = _selectedObject.GetComponent<Button>();
                if(_selectedObject.name != "CookButton")
                {
                    _pushDishButton.PushButton(button);
                }
            }
        }
        
    }
}
