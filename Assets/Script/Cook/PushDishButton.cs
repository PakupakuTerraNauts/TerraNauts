using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private GameObject _dishNameObject;
    private Text _dishNameText;

    public static string nowPushDish;

    // Start is called before the first frame update
    void Start()
    {
        _main = GameObject.Find("Main");
        _playerFoodManager = _main.GetComponent<PlayerFoodManager>();

        _viewFoodText = GameObject.Find("FoodNeedContent");
        _viewStatusText = GameObject.Find("StatusPlusContent");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushButton(Button onButton)
    {
        TextDelete(_viewFoodText);
        TextDelete(_viewStatusText);

        _objectName = onButton.name;
        _dishNameObject = GameObject.Find("DishName");
        _dishNameText = _dishNameObject.GetComponent<Text>();

        nowPushDish = _objectName;
        Debug.Log("Button: "+ _objectName);

        _foodSourceData = _itemDataBase.ItemSearch(_objectName);
        string[] foodTypes = _foodSourceData.GetFoodType(); ;
        int foodCount;
        string[] statusTypes = _foodSourceData.GetStatusType();
        int statusCount;

        for(int i = 0; i < foodTypes.Length; i++)
        {
            _textPrefab = (GameObject)Instantiate(_prefabObject, transform.position, Quaternion.identity);
            _textPrefab.transform.SetParent(_viewFoodText.transform, false);

            GameObject _cloneObject = GameObject.Find("ItemText1(Clone)");
            Text _cloneText = _cloneObject.GetComponent<Text>();
            Text _cloneText2 = _cloneObject.transform.GetChild(0).GetComponent<Text>();
            _foodSourceData_f = _itemDataBase.ItemSearch(foodTypes[i]);
            _cloneText.text = _foodSourceData_f.itemName;
            foodCount = _foodSourceData.GetFoodValue(foodTypes[i]);
            _cloneText2.text = "";
            _dishNameText.text = _foodSourceData.itemName;
            _cloneObject.name = _objectName;
        }

        for (int i = 0; i < statusTypes.Length; i++)
        {
            _textPrefab = (GameObject)Instantiate(_prefabObject, transform.position, Quaternion.identity);
            _textPrefab.transform.SetParent(_viewStatusText.transform, false);

            GameObject _cloneObject = GameObject.Find("ItemText1(Clone)");
            Text _cloneText = _cloneObject.GetComponent<Text>();
            Text _cloneText2 = _cloneObject.transform.GetChild(0).GetComponent<Text>();
            _cloneText.text = statusTypes[i];
            statusCount = _foodSourceData.GetStatusValue(statusTypes[i]);
            _cloneText2.text = "+" + statusCount.ToString("d");
            _cloneObject.name = _objectName;
        }
    }

    public void TextDelete(GameObject _objectText)
    {
        foreach (Transform child in _objectText.transform)
        {
            //Debug.Log("Child");
            GameObject.Destroy(child.gameObject);
        }
    }
}
