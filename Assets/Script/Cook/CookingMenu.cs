using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Cookingメニューにレシピを表示
public class CookingMenu : MonoBehaviour
{
    //Script
    public ItemDataBase _itemDataBase;
    PlayerRecipeManager _playerRecipeManager;
    FoodSourceData _foodSourceData;


    public GameObject _prefabObject;
    GameObject _viewRecipeText;
    GameObject _textPrefab;
    GameObject _main;

    
    


    void Start()
    {
        _viewRecipeText = GameObject.Find("RecipeContent_C");

        _main = GameObject.Find("Main");
        _playerRecipeManager = _main.GetComponent<PlayerRecipeManager>();

        //レシピ表示
        TextDelete(_viewRecipeText);
        SetRecipeText();

    }

    //テキストを削除
    public void TextDelete(GameObject _objectText)
    {
        foreach (Transform child in _objectText.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    //レシピのテキスト生成
    public void SetRecipeText()
    {
        string[] _id = _playerRecipeManager.GetItemId();

        for (int i = 0; i < _id.Length; i++)
        {
            int _count = _playerRecipeManager.GetItemCount(_id[i]);
            _foodSourceData = _itemDataBase.ItemSearch(_id[i]);

            _textPrefab = (GameObject)Instantiate(_prefabObject, transform.position, Quaternion.identity);
            _textPrefab.transform.SetParent(_viewRecipeText.transform, false);

            GameObject _cloneObject = GameObject.Find("Button1(Clone)");

            Text _cloneText = _cloneObject.transform.GetChild(0).GetComponent<Text>();
            Text _cloneText2 = _cloneObject.transform.GetChild(2).GetComponent<Text>();

            _cloneText.text = _foodSourceData.itemName + "のレシピ";
            _cloneText2.text = _count.ToString("d");
            _cloneObject.name = _id[i];
        }
    }
}
