using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//ボタンセレクトとプッシュ
public class SelectScript : MonoBehaviour
{

    public GameObject _gameObject;
    GameObject _firstObj;
    Button _firstButton;

    public PushDishButton _pushDishButton;
    GameObject selectObj;

    GameObject beforObj;

    void Start()
    {

        //一番上のボタンをセレクト

        if (_gameObject.transform.GetChild(1).gameObject != null)
        {
            _firstObj = _gameObject.transform.GetChild(1).gameObject;
            _firstButton = _firstObj.GetComponent<Button>();
            _firstButton.Select();
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
        if (EventSystem.current.currentSelectedGameObject && EventSystem.current.currentSelectedGameObject.tag == "item")
        {
            selectObj = EventSystem.current.currentSelectedGameObject;
            _pushDishButton.PushButton(selectObj.GetComponent<Button>());
        }
        else if(EventSystem.current.currentSelectedGameObject && EventSystem.current.currentSelectedGameObject.tag != "item")
        {
            beforObj.GetComponent<Button>().Select();
            _pushDishButton.PushButton(beforObj.GetComponent<Button>());
        }
        beforObj = selectObj;
        


    }
}
