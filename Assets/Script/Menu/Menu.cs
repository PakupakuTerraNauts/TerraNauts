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

    public Sprite _statusSprite;
    public Sprite _itemSprite;
    public Sprite _skillSprite;
    public Sprite _settingSprite;
    public Sprite _loadSprite;
    public Sprite _exitSprite;
    public Sprite _push_statusSprite;
    public Sprite _push_itemSprite;
    public Sprite _push_skillSprite;
    public Sprite _push_settingSprite;
    public Sprite _push_loadSprite;
    public Sprite _push_exitSprite;

    public GameObject _exit_button;
    public GameObject _volume_slider;

    void Start() {
        _status = GameObject.Find("StatusCanvas");
        _item = GameObject.Find("ItemCanvas");
        _skill = GameObject.Find("SkillCanvas");
        _setting = GameObject.Find("SettingCanvas");
        _load = GameObject.Find("LoadCanvas");
        _exit = GameObject.Find("ExitCanvas");

        _viewFoodText = GameObject.Find("TileFoodContent");
        _viewRecipeText = GameObject.Find("TileDishContent");

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
        _statusButton.GetComponent<Image>().sprite = _push_statusSprite;

    }


    public void PushButton(int Data)
    {
        CanvasOff();

        switch(Data)
        {
            case 0:
                _status.SetActive(true);
                _statusButton.GetComponent<Image>().sprite = _push_statusSprite;
                break;

            case 1:
                _item.SetActive(true);
                _itemButton.GetComponent<Image>().sprite = _push_itemSprite;

                TextDelete(_viewFoodText);
                SetFoodText();
                TextDelete(_viewRecipeText);
                SetRecipeText();

                //_viewFoodText.transform.GetChild(0).GetComponent<Button>().Select();

                break;

            case 2:
                _skill.SetActive(true);
                _skillButton.GetComponent<Image>().sprite = _push_skillSprite;
                break;

            case 3:
                _setting.SetActive(true);
                _settingButton.GetComponent<Image>().sprite = _push_settingSprite;
                _volume_slider.GetComponent<Slider>().Select();
                break;

            case 4:
                _load.SetActive(true);
                _loadButton.GetComponent<Image>().sprite = _push_loadSprite;
                break;

            case 5:
                _exit.SetActive(true);
                _exitButton.GetComponent<Image>().sprite = _push_exitSprite;
                _exit_button.GetComponent<Button>().Select();
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

        _statusButton.GetComponent<Image>().sprite = _statusSprite;
        _itemButton.GetComponent<Image>().sprite = _itemSprite;
        _skillButton.GetComponent<Image>().sprite = _skillSprite;
        _settingButton.GetComponent<Image>().sprite = _settingSprite;
        _loadButton.GetComponent<Image>().sprite = _loadSprite;
        _exitButton.GetComponent<Image>().sprite = _exitSprite;
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

            GameObject _cloneObject = GameObject.Find("ItemButton(Clone)");
            Image _cloneImage = _cloneObject.transform.GetChild(0).GetComponent<Image>();
            Text _cloneText = _cloneObject.transform.GetChild(1).GetComponent<Text>();
            _cloneImage.sprite = _foodSourceData.icon;
            _cloneText.text = "✖︎"+ _count.ToString("d");
            _cloneObject.name = _id[i];

        }
    }

    public void SetRecipeText()
    {
        string[] _id = _playerRecipeManager.GetItemId();

        for (int i = 0; i < _id.Length; i++)
        {
            _foodSourceData = _itemDataBase.ItemSearch(_id[i]);

            _textPrefab = (GameObject)Instantiate(_prefab3Object, transform.position, Quaternion.identity);
            _textPrefab.transform.SetParent(_viewRecipeText.transform, false);

            GameObject _cloneObject = GameObject.Find("ItemButton(Clone)");
            Image _cloneImage = _cloneObject.transform.GetChild(0).GetComponent<Image>();
            Text _cloneText = _cloneObject.transform.GetChild(1).GetComponent<Text>();

            _cloneImage.sprite = _foodSourceData.icon;
            _cloneText.text = "";
            _cloneObject.name = _id[i];

        }
    }


}
