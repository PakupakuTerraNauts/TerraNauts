using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//レシピのテキストを押した時の処理
public class PushDishButton : MonoBehaviour
{
    private string _objectName;
    private GameObject _main;
    private PlayerFoodManager _playerFoodManager;

    public ItemDataBase _itemDataBase;
    private FoodSourceData _foodSourceData;
    private FoodSourceData _foodSourceData_f;

    private GameObject _viewFoodText;
    private GameObject _viewStatusText;

    private GameObject _textPrefab;
    public GameObject _prefabObject;
    public GameObject _prefabObject2;

    private GameObject _dishNameObject;
    private Text _dishNameText;

    public static string nowPushDish;

    // Start is called before the first frame update
    void Start()
    {
        _main = GameObject.Find("Main");
        _playerFoodManager = _main.GetComponent<PlayerFoodManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //レシピのボタンが押された時
    public void PushButton(Button onButton)
    {
        _main = GameObject.Find("Main");
        _playerFoodManager = _main.GetComponent<PlayerFoodManager>();

        _viewFoodText = GameObject.Find("FoodNeedContent");
        _viewStatusText = GameObject.Find("StatusPlusContent");
        TextDelete(_viewFoodText);
        TextDelete(_viewStatusText);

        _objectName = onButton.name;
        _dishNameObject = GameObject.Find("DishName");
        _dishNameText = _dishNameObject.GetComponent<Text>();

        nowPushDish = _objectName;

        _foodSourceData = _itemDataBase.ItemSearch(_objectName);
        string[] foodTypes = _foodSourceData.GetFoodType(); ;
        string[] statusTypes = _foodSourceData.GetStatusType();
        int statusCount;

        //食材のテキスト更新
        for(int i = 0; i < foodTypes.Length; i++)
        {
            _textPrefab = (GameObject)Instantiate(_prefabObject, transform.position, Quaternion.identity);
            _textPrefab.transform.SetParent(_viewFoodText.transform, false);

            GameObject _cloneObject = GameObject.Find("ItemText1(Clone)");
            Text _cloneText = _cloneObject.GetComponent<Text>();
            Text _cloneText2 = _cloneObject.transform.GetChild(0).GetComponent<Text>();
            _foodSourceData_f = _itemDataBase.ItemSearch(foodTypes[i]);
            _cloneText.text = _foodSourceData_f.itemName;
            _cloneText2.text = _playerFoodManager.GetItemCount(foodTypes[i])+"/1";
            _dishNameText.text = _foodSourceData.itemName;
            _cloneObject.name = foodTypes[i];
        }

        //ステータスのテキスト更新
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
