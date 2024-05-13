using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�����{�^�������������̏���
public class PushCookButton:MonoBehaviour
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

    GameObject _viewFoodText;

    public static bool cookOK;

    public GameObject _HP_plus;
    public GameObject _ATK_plus;
    public GameObject _DEF_plus;
    public GameObject _SPD_plus;
    public GameObject _CRATE_plus;
    public GameObject _CDMG_plus;

    public GameObject _HP_plusF;
    public GameObject _ATK_plusF;
    public GameObject _DEF_plusF;
    public GameObject _SPD_plusF;
    public GameObject _CRATE_plusF;
    public GameObject _CDMG_plusF;

    Text _HP_plus_text;
    Text _ATK_plus_text;
    Text _DEF_plus_text;
    Text _SPD_plus_text;
    Text _CRATE_plus_text;
    Text _CDMG_plus_text;

    Text _HP_plusF_text;
    Text _ATK_plusF_text;
    Text _DEF_plusF_text;
    Text _SPD_plusF_text;
    Text _CRATE_plusF_text;
    Text _CDMG_plusF_text;

    void Start()
    {
        GameObject _main = GameObject.Find("Main");
        _playerFoodManager = _main.GetComponent<PlayerFoodManager>();

        _viewFoodText = GameObject.Find("TileFoodNeedContent");
        //_viewStatusText = GameObject.Find("StatusPlusContent");

        _HP_plus_text = _HP_plus.GetComponent<Text>();
        _ATK_plus_text = _ATK_plus.GetComponent<Text>();
        _DEF_plus_text = _DEF_plus.GetComponent<Text>();
        _SPD_plus_text = _SPD_plus.GetComponent<Text>();
        _CRATE_plus_text = _CRATE_plus.GetComponent<Text>();
        _CDMG_plus_text = _CDMG_plus.GetComponent<Text>();

        _HP_plusF_text = _HP_plusF.GetComponent<Text>();
        _ATK_plusF_text = _ATK_plusF.GetComponent<Text>();
        _DEF_plusF_text = _DEF_plusF.GetComponent<Text>();
        _SPD_plusF_text = _SPD_plusF.GetComponent<Text>();
        _CRATE_plusF_text = _CRATE_plusF.GetComponent<Text>();
        _CDMG_plusF_text = _CDMG_plusF.GetComponent<Text>();
    }


    void Update()
    {
        //�����{�^��(C�L�[)�������ꂽ��
        if(Input.GetKeyDown(KeyCode.C))
        {
            _foodSourceData = _itemDataBase.ItemSearch(PushDishButton.nowPushDish);

            ChangeText();

            string[] foodTypes = _foodSourceData.GetFoodType(); ;
            int foodCount;
            string[] statusTypes = _foodSourceData.GetStatusType();
            int statusCount;


            cookOK = false;

            //�H�ނ�����邩����
            for(int i = 0; i < foodTypes.Length; i++)
            {
                if(_playerFoodManager.GetItemCount(foodTypes[i]) < _foodSourceData.GetFoodValue(foodTypes[i]))
                {
                    cookOK = false;
                    break;
                }
                else
                {
                    cookOK = true;
                }
            }

            //�H�ނ����肽�ꍇ�A�H�ނ����炵�A�X�e�[�^�X���㏸
            if(cookOK)
            {
                //�H�ނ����炷
                for(int i = 0; i < foodTypes.Length; i++)
                {
                    foodCount = _foodSourceData.GetFoodValue(foodTypes[i]);
                    _playerFoodManager.UseItem(foodTypes[i], foodCount);
                }

                //�X�e�[�^�X�㏸
                for(int i = 0; i < statusTypes.Length; i++)
                {
                    statusCount = _foodSourceData.GetStatusValue(statusTypes[i]);
                    switch(statusTypes[i])
                    {
                        case "HP":
                            Status.HP += statusCount;
                            Player.HPincrease(statusCount);
                            break;
                        case "ATK":
                            Status.ATK += statusCount;
                            Player.ATKincrease(statusCount);
                            break;
                        case "DEF":
                            Status.DEF += statusCount;
                            Player.DEFincrease(statusCount);
                            break;
                        case "SPD":
                            Status.SPD += statusCount;
                            Player.SPDincrease(statusCount);
                            break;
                        case "CRITRATE":
                            Status.CRITRATE += statusCount;
                            Player.CRITRATEincrease(statusCount);
                            break;
                        case "CRITDMG":
                            Status.CRITDMG += statusCount;
                            Player.CRITDMGincrease(statusCount);
                            break;
                    }
                }

                Debug.Log("��������");
                Player.nowHP = Player.HP + Player.HPincrement;
            }

            ChangeText();
        }
    }


    //�H�ނƃX�e�[�^�X�̃e�L�X�g���X�V
    public void ChangeText()
    {
        TextDelete(_viewFoodText);

        _objectName = PushDishButton.nowPushDish;

        _foodSourceData = _itemDataBase.ItemSearch(_objectName);
        string[] foodTypes = _foodSourceData.GetFoodType(); ;
        string[] statusTypes = _foodSourceData.GetStatusType();
        int statusCount;

        for(int i = 0; i < foodTypes.Length; i++)
        {
            _textPrefab = (GameObject)Instantiate(_prefabObject, transform.position, Quaternion.identity);
            _textPrefab.transform.SetParent(_viewFoodText.transform, false);

            GameObject _cloneObject = GameObject.Find("FoodNeedImage(Clone)");
            Image _cloneImage = _cloneObject.transform.GetChild(0).GetComponent<Image>();
            Text _cloneText2 = _cloneObject.transform.GetChild(1).GetComponent<Text>();
            _foodSourceData_f = _itemDataBase.ItemSearch(foodTypes[i]);
            _cloneImage.sprite = _foodSourceData_f.icon;
            _cloneText2.text = _playerFoodManager.GetItemCount(foodTypes[i]) + "/1";
            _cloneObject.name = foodTypes[i];
        }

        StatusRe();

        for(int i = 0; i < statusTypes.Length; i++)
        {
            statusCount = _foodSourceData.GetStatusValue(statusTypes[i]);
            switch(statusTypes[i])
            {
                case "HP":
                    _HP_plus_text.text = "+" + statusCount.ToString("d");
                    break;
                case "ATK":
                    _ATK_plus_text.text = "+" + statusCount.ToString("d");
                    break;
                case "DEF":
                    _DEF_plus_text.text = "+" + statusCount.ToString("d");
                    break;
                case "SPD":
                    _SPD_plus_text.text = "+" + statusCount.ToString("d");
                    break;
                case "CRITRATE":
                    _CRATE_plus_text.text = "+" + statusCount.ToString("d");
                    break;
                case "CRITDMG":
                    _CDMG_plus_text.text = "+" + statusCount.ToString("d");
                    break;
            }
        }
    }

    //�e�L�X�g�폜
    public void TextDelete(GameObject _objectText)
    {
        foreach(Transform child in _objectText.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void StatusRe()
    {
        _HP_plusF_text.text = Status.HP.ToString("d");
        _HP_plus_text.text = "";
        _ATK_plusF_text.text = Status.ATK.ToString("d");
        _ATK_plus_text.text = "";
        _DEF_plusF_text.text = Status.DEF.ToString("d");
        _DEF_plus_text.text = "";
        _SPD_plusF_text.text = Status.SPD.ToString("d");
        _SPD_plus_text.text = "";
        _CRATE_plusF_text.text = Status.CRITRATE.ToString("d");
        _CRATE_plus_text.text = "";
        _CDMG_plusF_text.text = Status.CRITDMG.ToString("d");
        _CDMG_plus_text.text = "";
    }
}
