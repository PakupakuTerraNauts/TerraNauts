using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//レシピテキスト選択で食材、ステータスが表示
public class PushRecipeButton:MonoBehaviour
{
    //Script
    GameObject _main;
    PlayerFoodManager _playerFoodManager;
    public ItemDataBase _itemDataBase;
    FoodSourceData _foodSourceData;
    //
    private GameObject _viewFoodText;
    private GameObject _viewStatusText;
    private string _objectName;
    private GameObject _textRecipePrefab;
    public GameObject _prefabObject;
    public GameObject _prefabObject2;
    private FoodSourceData _foodSourceData_f;

    void Start()
    {
        _viewFoodText = GameObject.Find("MenuFoodContent");
        _viewStatusText = GameObject.Find("MenuStatusContent");
        TextDelete(_viewFoodText);
        TextDelete(_viewStatusText);
    }

    public void RecipeButton(Button onButton)
    {
        _main = GameObject.Find("Main");
        _playerFoodManager = _main.GetComponent<PlayerFoodManager>();

        _viewFoodText = GameObject.Find("MenuFoodContent");
        _viewStatusText = GameObject.Find("MenuStatusContent");
        TextDelete(_viewFoodText);
        TextDelete(_viewStatusText);

        _objectName = onButton.name;

        //Debug.Log("Button: " + _objectName);

        _foodSourceData = _itemDataBase.ItemSearch(_objectName);
        string[] foodTypes = _foodSourceData.GetFoodType(); ;
        string[] statusTypes = _foodSourceData.GetStatusType();
        int statusCount;

        for(int i = 0; i < foodTypes.Length; i++)
        {
            _textRecipePrefab = (GameObject)Instantiate(_prefabObject, transform.position, Quaternion.identity);
            _textRecipePrefab.transform.SetParent(_viewFoodText.transform, false);

            GameObject _cloneObject = GameObject.Find("ItemText3(Clone)");
            Text _cloneText = _cloneObject.GetComponent<Text>();
            Text _cloneText2 = _cloneObject.transform.GetChild(0).GetComponent<Text>();
            _foodSourceData_f = _itemDataBase.ItemSearch(foodTypes[i]);
            _cloneText.text = _foodSourceData_f.itemName;
            _cloneText2.text = _playerFoodManager.GetItemCount(foodTypes[i]) + "/1";
            _cloneObject.name = foodTypes[i];
        }

        for(int i = 0; i < statusTypes.Length; i++)
        {
            _textRecipePrefab = (GameObject)Instantiate(_prefabObject2, transform.position, Quaternion.identity);
            _textRecipePrefab.transform.SetParent(_viewStatusText.transform, false);

            GameObject _cloneObject = GameObject.Find("ItemText4(Clone)");
            Text _cloneText = _cloneObject.GetComponent<Text>();
            Text _cloneText2 = _cloneObject.transform.GetChild(0).GetComponent<Text>();
            Text _cloneText3 = _cloneObject.transform.GetChild(1).GetComponent<Text>();
            _cloneText.text = statusTypes[i];
            statusCount = _foodSourceData.GetStatusValue(statusTypes[i]);
            switch(statusTypes[i])
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

    public void TextDelete(GameObject _objectText)
    {
        foreach(Transform child in _objectText.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
