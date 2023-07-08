using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//メニュー画面の制御
public class Menu : MonoBehaviour
{
    GameObject _status;
    GameObject _item;
    GameObject _skill;
    GameObject _setting;
    GameObject _load;
    GameObject _exit;

    Button _statusButton;
    Button _itemButton;
    Button _skillButton;
    Button _settingButton;
    Button _loadButton;
    Button _exitButton;

    GameObject _viewFoodText;
    GameObject _viewRecipeText;

    GameObject _textPrefab;
    public GameObject _prefab3Object;

    PlayerFoodManager _playerFoodManager;
    PlayerRecipeManager _playerRecipeManager;
    GameObject _main;

    public ItemDataBase _itemDataBase;
    FoodSourceData _foodSourceData;

    public Sprite _menuSprite1;
    public Sprite _menuSprite2;


    void Start() {
        _status = GameObject.Find("StatusCanvas");
        _item = GameObject.Find("ItemCanvas");
        _skill = GameObject.Find("SkillCanvas");
        _setting = GameObject.Find("SettingCanvas");
        _load = GameObject.Find("LoadCanvas");
        _exit = GameObject.Find("ExitCanvas");

        _viewFoodText = GameObject.Find("FoodContent");
        _viewRecipeText = GameObject.Find("RecipeContent");

        _main = GameObject.Find("Main");
        _playerFoodManager = _main.GetComponent<PlayerFoodManager>();
        _playerRecipeManager = _main.GetComponent<PlayerRecipeManager>();

        _statusButton = GameObject.Find("StatusButton").GetComponent<Button>();
        _itemButton = GameObject.Find("ItemButton").GetComponent<Button>();
        _skillButton = GameObject.Find("SkillButton").GetComponent<Button>();
        _settingButton = GameObject.Find("SettingButton").GetComponent<Button>();
        _loadButton = GameObject.Find("LoadButton").GetComponent<Button>();
        _exitButton = GameObject.Find("ExitButton").GetComponent<Button>();


        CanvasOff();
        _status.SetActive(true);
         _statusButton.GetComponent<Image>().sprite = _menuSprite2;

    }


    public void PushButton(int Data)
    {
        CanvasOff();

        switch(Data)
        {
            case 0:
                _status.SetActive(true);
                _statusButton.GetComponent<Image>().sprite = _menuSprite2;
                break;

            case 1:
                _item.SetActive(true);
                _itemButton.GetComponent<Image>().sprite = _menuSprite2;

                TextDelete(_viewFoodText);
                SetFoodText();
                TextDelete(_viewRecipeText);
                SetRecipeText();
                break;

            case 2:
                _skill.SetActive(true);
                _skillButton.GetComponent<Image>().sprite = _menuSprite2;
                break;

            case 3:
                _setting.SetActive(true);
                _settingButton.GetComponent<Image>().sprite = _menuSprite2;
                break;

            case 4:
                _load.SetActive(true);
                _loadButton.GetComponent<Image>().sprite = _menuSprite2;
                break;

            case 5:
                _exit.SetActive(true);
                _exitButton.GetComponent<Image>().sprite = _menuSprite2;
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

        _statusButton.GetComponent<Image>().sprite = _menuSprite1;
        _itemButton.GetComponent<Image>().sprite = _menuSprite1;
        _skillButton.GetComponent<Image>().sprite = _menuSprite1;
        _settingButton.GetComponent<Image>().sprite = _menuSprite1;
        _loadButton.GetComponent<Image>().sprite = _menuSprite1;
        _exitButton.GetComponent<Image>().sprite = _menuSprite1;
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
            _textPrefab = (GameObject)Instantiate(_prefab3Object, transform.position, Quaternion.identity);
            _textPrefab.transform.SetParent(_viewFoodText.transform, false);

            GameObject _cloneObject = GameObject.Find("Button(Clone)");
            Text _cloneText = _cloneObject.transform.GetChild(0).GetComponent<Text>();
            Text _cloneText2 = _cloneObject.transform.GetChild(2).GetComponent<Text>();
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
            _textPrefab = (GameObject)Instantiate(_prefab3Object, transform.position, Quaternion.identity);
            _textPrefab.transform.SetParent(_viewRecipeText.transform, false);

            GameObject _cloneObject = GameObject.Find("Button(Clone)");
            Text _cloneText = _cloneObject.transform.GetChild(0).GetComponent<Text>();
            Text _cloneText2 = _cloneObject.transform.GetChild(2).GetComponent<Text>();
            _cloneText.text = _foodSourceData.itemName + "のレシピ";
            _cloneText2.text = _count.ToString("d");
            _cloneObject.name = _id[i];

        }
    }

}