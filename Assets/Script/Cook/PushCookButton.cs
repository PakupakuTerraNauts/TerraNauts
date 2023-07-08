using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//調理ボタンを押した時の処理
public class PushCookButton : MonoBehaviour
{
    //Script
    public ItemDataBase _itemDataBase;
    PlayerFoodManager _playerFoodManager;
    FoodSourceData _foodSourceData;
    FoodSourceData _foodSourceData_f;
    //
    string _objectName;
    GameObject _textPrefab;

    public GameObject _prefabObject;
    public GameObject _prefabObject2;

    GameObject _viewFoodText;
    GameObject _viewStatusText;

    GameObject _dishNameObject;
    Text _dishNameText;

    void Start()
    {
        GameObject _main = GameObject.Find("Main");
        _playerFoodManager = _main.GetComponent<PlayerFoodManager>();

        _viewFoodText = GameObject.Find("FoodNeedContent");
        _viewStatusText = GameObject.Find("StatusPlusContent");
    }

    //調理ボタンが押された時
    public void PushCook()
    {
        _foodSourceData = _itemDataBase.ItemSearch(PushDishButton.nowPushDish);

        ChangeText();

        string[] foodTypes = _foodSourceData.GetFoodType(); ;
        int foodCount;
        string[] statusTypes = _foodSourceData.GetStatusType();
        int statusCount;


        bool cookOK = true;

        //食材が足りるか判定
        for(int i = 0; i < foodTypes.Length; i++)
        {
            if(_playerFoodManager.GetItemCount(foodTypes[i]) < _foodSourceData.GetFoodValue(foodTypes[i])){
                cookOK = false;
                break;
            }
        }

        //食材が足りた場合、食材を減らし、ステータスを上昇
        if (cookOK)
        {
            //食材を減らす
            for (int i = 0; i < foodTypes.Length; i++)
            {
                foodCount = _foodSourceData.GetFoodValue(foodTypes[i]);
                _playerFoodManager.UseItem(foodTypes[i], foodCount);
            }

            //ステータス上昇
            for (int i = 0; i < statusTypes.Length; i++)
            {
                statusCount = _foodSourceData.GetStatusValue(statusTypes[i]);
                switch (statusTypes[i])
                {
                    case "HP":
                        Status.HP += statusCount;
                        break;
                    case "ATK":
                        Status.ATK += statusCount;
                        break;
                    case "DEF":
                        Status.DEF += statusCount;
                        break;
                    case "SPD":
                        Status.SPD += statusCount;
                        break;
                    case "CRITRATE":
                        Status.CRITRATE += statusCount;
                        break;
                    case "CRITDMG":
                        Status.CRITDMG += statusCount;
                        break;
                }
            }
        }
    }

    //食材とステータスのテキストを更新
    public void ChangeText()
    {
        TextDelete(GameObject.Find("FoodNeedContent"));
        TextDelete(GameObject.Find("StatusPlusContent"));

        _objectName = PushDishButton.nowPushDish;
        _dishNameObject = GameObject.Find("DishName");
        _dishNameText = _dishNameObject.GetComponent<Text>();

        _foodSourceData = _itemDataBase.ItemSearch(_objectName);
        string[] foodTypes = _foodSourceData.GetFoodType(); ;
        string[] statusTypes = _foodSourceData.GetStatusType();
        int statusCount;

        for (int i = 0; i < foodTypes.Length; i++)
        {
            _textPrefab = (GameObject)Instantiate(_prefabObject, transform.position, Quaternion.identity);
            _textPrefab.transform.SetParent(_viewFoodText.transform, false);

            GameObject _cloneObject = GameObject.Find("ItemText1(Clone)");
            Text _cloneText = _cloneObject.GetComponent<Text>();
            Text _cloneText2 = _cloneObject.transform.GetChild(0).GetComponent<Text>();
            _foodSourceData_f = _itemDataBase.ItemSearch(foodTypes[i]);
            _cloneText.text = _foodSourceData_f.itemName;
            _cloneText2.text = _playerFoodManager.GetItemCount(foodTypes[i]) + "/1";
            _dishNameText.text = _foodSourceData.itemName;
            _cloneObject.name = foodTypes[i];
        }

        for (int i = 0; i < statusTypes.Length; i++)
        {
            _textPrefab = (GameObject)Instantiate(_prefabObject2, transform.position, Quaternion.identity);
            _textPrefab.transform.SetParent(_viewStatusText.transform, false);

            GameObject _cloneObject = GameObject.Find("ItemText2(Clone)");
            Text _cloneText = _cloneObject.GetComponent<Text>();
            Text _cloneText2 = _cloneObject.transform.GetChild(0).GetComponent<Text>();
            Text _cloneText3 = _cloneObject.transform.GetChild(1).GetComponent<Text>();
            _cloneText.text = statusTypes[i];
            statusCount = _foodSourceData.GetStatusValue(statusTypes[i]);
            switch (statusTypes[i])
            {
                case "HP":
                    _cloneText2.text = Status.HP.ToString("d");
                    _cloneText3.text = "+" + statusCount.ToString("d");
                    break;
                case "ATK":
                    _cloneText2.text = Status.ATK.ToString("d");
                    _cloneText3.text = "+" + statusCount.ToString("d");
                    break;
                case "DEF":
                    _cloneText2.text = Status.DEF.ToString("d");
                    _cloneText3.text = "+" + statusCount.ToString("d");
                    break;
                case "SPD":
                    _cloneText2.text = Status.SPD.ToString("d");
                    _cloneText3.text = "+" + statusCount.ToString("d");
                    break;
                case "CRITRATE":
                    _cloneText2.text = Status.CRITRATE.ToString("d");
                    _cloneText3.text = "+" + statusCount.ToString("d");
                    break;
                case "CRITDMG":
                    _cloneText2.text = Status.CRITDMG.ToString("d");
                    _cloneText3.text = "+" + statusCount.ToString("d");
                    break;
            }
            _cloneObject.name = statusTypes[i];
        }
    }

    //テキスト削除
    public void TextDelete(GameObject _objectText)
    {
        foreach (Transform child in _objectText.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
