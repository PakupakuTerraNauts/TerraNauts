using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingMenu : MonoBehaviour
{

    //ItemCanvas
    GameObject _food;
    GameObject _recipe;

    GameObject _viewFoodText;
    GameObject _viewRecipeText;

    GameObject _textPrefab;
    public GameObject _prefabObject;
    public GameObject _prefabButtonObject;

    PlayerFoodManager _playerFoodManager;
    PlayerRecipeManager _playerRecipeManager;
    GameObject _main;

    public ItemDataBase _itemDataBase;
    FoodSourceData _foodSourceData;


    void Start()
    {
        
        _food = GameObject.Find("FoodCanvas_C");
        _recipe = GameObject.Find("RecipeCanvas_C");

        _viewFoodText = GameObject.Find("FoodContent_C");
        _viewRecipeText = GameObject.Find("RecipeContent_C");

        _main = GameObject.Find("Main");
        _playerFoodManager = _main.GetComponent<PlayerFoodManager>();
        _playerRecipeManager = _main.GetComponent<PlayerRecipeManager>();

        CanvasOff();
        _food.SetActive(true);

        TextDelete(_viewFoodText);
        SetFoodText();
    }

    void Update()
    {
        if(_food.activeSelf == true)
        {
            TextDelete(_viewFoodText);
            SetFoodText();
        }
    }


    public void PushButton(int Data)
    {
        CanvasOff();

        switch (Data)
        {
            case 0:
                _food.SetActive(true);
                TextDelete(_viewFoodText);
                SetFoodText();
                break;

            case 1:
                _recipe.SetActive(true);
                TextDelete(_viewRecipeText);
                SetRecipeText();
                break;
               

            default:
                break;
        }
    }

    public void CanvasOff()
    {
        _food.SetActive(false);
        _recipe.SetActive(false);
    }

    public void TextDelete(GameObject _objectText)
    {
        foreach (Transform child in _objectText.transform)
        {
            //Debug.Log("Child");
            GameObject.Destroy(child.gameObject);
        }
    }

    public void SetFoodText()
    {
        string[] _id = _playerFoodManager.GetItemId();

        for (int i = 0; i < _id.Length; i++)
        {
            _foodSourceData = _itemDataBase.ItemSearch(_id[i]);

            int _count = _playerFoodManager.GetItemCount(_id[i]);
            _textPrefab = (GameObject)Instantiate(_prefabObject, transform.position, Quaternion.identity);
            _textPrefab.transform.SetParent(_viewFoodText.transform, false);

            //ItemText(Clone)
            GameObject _cloneObject = GameObject.Find("ItemText(Clone)");
            Text _cloneText = _cloneObject.GetComponent<Text>();
            Text _cloneText2 = _cloneObject.transform.GetChild(0).GetComponent<Text>();
            _cloneText.text = _foodSourceData.itemName;
            _cloneText2.text = _count.ToString("d");
            _cloneObject.name = _id[i];

        }
    }

    public void SetRecipeText()
    {
        string[] _id = _playerRecipeManager.GetItemId();

        for (int i = 0; i < _id.Length; i++)
        {
            _foodSourceData = _itemDataBase.ItemSearch(_id[i]);

            _textPrefab = (GameObject)Instantiate(_prefabButtonObject, transform.position, Quaternion.identity);
            _textPrefab.transform.SetParent(_viewRecipeText.transform, false);

            GameObject _cloneObject = GameObject.Find("ItemButton(Clone)");

            Text _cloneText2 = _cloneObject.transform.GetChild(0).GetComponent<Text>();
            _cloneText2.text = _foodSourceData.itemName + "のレシピ";
            _cloneObject.name = _id[i];
        }
    }
}
