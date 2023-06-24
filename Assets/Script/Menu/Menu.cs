using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    GameObject _status;
    GameObject _item;
    GameObject _skill;
    GameObject _setting;
    GameObject _load;
    GameObject _exit;

    //ItemCanvas
    GameObject _food;
    GameObject _recipe;

    GameObject _viewFoodText;
    GameObject _viewRecipeText;

    GameObject _textPrefab;
    public GameObject _prefabObject;

    PlayerFoodManager _playerFoodManager;
    PlayerRecipeManager _playerRecipeManager;
    GameObject _main;

    public ItemDataBase _itemDataBase;
    FoodSourceData _foodSourceData;


    void Start() {
        _status = GameObject.Find("StatusCanvas");
        _item = GameObject.Find("ItemCanvas");
        _skill = GameObject.Find("SkillCanvas");
        _setting = GameObject.Find("SettingCanvas");
        _load = GameObject.Find("LoadCanvas");
        _exit = GameObject.Find("ExitCanvas");

        _food = GameObject.Find("FoodCanvas");
        _recipe = GameObject.Find("RecipeCanvas");

        _viewFoodText = GameObject.Find("FoodContent");
        _viewRecipeText = GameObject.Find("RecipeContent");

        _main = GameObject.Find("Main");
        _playerFoodManager = _main.GetComponent<PlayerFoodManager>();
        _playerRecipeManager = _main.GetComponent<PlayerRecipeManager>();

        CanvasOff();
        _status.SetActive(true);

    }

    void Update()
    {
    }


    public void PushButton(int Data)
    {
        CanvasOff();

        switch(Data)
        {
            case 0:
                _status.SetActive(true);
                break;

            case 1:
                _item.SetActive(true);
                _food.SetActive(true);

                TextDelete(_viewFoodText);
                SetFoodText();
                
                break;

            case 2:
                _skill.SetActive(true);
                break;

            case 3:
                _setting.SetActive(true);
                break;

            case 4:
                _load.SetActive(true);
                break;

            case 5:
                _exit.SetActive(true);
                break;

            case 6:
                _item.SetActive(true);
                _food.SetActive(true);
                break;

            case 7:
                _item.SetActive(true);
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
        _status.SetActive(false);
        _item.SetActive(false);
        _skill.SetActive(false);
        _setting.SetActive(false);
        _load.SetActive(false);
        _exit.SetActive(false);

        _food.SetActive(false);
        _recipe.SetActive(false);
    }

    public void PushExit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
        #else
            Application.Quit();
            //ゲームプレイ終了
        #endif
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

            int _count = _playerRecipeManager.GetItemCount(_id[i]);
            _textPrefab = (GameObject)Instantiate(_prefabObject, transform.position, Quaternion.identity);
            _textPrefab.transform.SetParent(_viewRecipeText.transform, false);

            //ItemText(Clone)
            GameObject _cloneObject = GameObject.Find("ItemText(Clone)");
            Text _cloneText = _cloneObject.GetComponent<Text>();
            Text _cloneText2 = _cloneObject.transform.GetChild(0).GetComponent<Text>();
            _cloneText.text = _foodSourceData.itemName + "のレシピ";
            _cloneText2.text = _count.ToString("d");
            _cloneObject.name = _id[i];

        }
    }
}
