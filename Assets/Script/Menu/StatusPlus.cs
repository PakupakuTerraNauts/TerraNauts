using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatusPlus : MonoBehaviour
{
    GameObject _selectedObject;
    FoodSourceData _foodSourceData;
    public ItemDataBase _itemDataBase;

    public GameObject HP_plus;
    public GameObject ATK_plus;
    public GameObject DEF_plus;
    public GameObject SPD_plus;
    public GameObject CRATE_plus;
    public GameObject CDMG_plus;

    Text HP_Text;
    Text ATK_Text;
    Text DEF_Text;
    Text SPD_Text;
    Text CRATE_Text;
    Text CDMG_Text;

    public GameObject FoodPrefab;
    GameObject _textRecipePrefab;
    FoodSourceData _foodSourceData_f;

    string[] statusTypes;
    string[] foodTypes;

    public GameObject FoodName;
    public GameObject DishName;

    Text _foodNameText;
    Text _dishNameText;

    GameObject beforObj;

    public GameObject _TileFoodContent;
    public GameObject _TileDishContent;

    GameObject _firstObj;
    Button _firstButton;

    // Start is called before the first frame update
    void Start()
    {
        HP_Text = HP_plus.GetComponent<Text>();
        ATK_Text = ATK_plus.GetComponent<Text>();
        DEF_Text = DEF_plus.GetComponent<Text>();
        SPD_Text = SPD_plus.GetComponent<Text>();
        CRATE_Text = CRATE_plus.GetComponent<Text>();
        CDMG_Text = CDMG_plus.GetComponent<Text>();

        _foodNameText = FoodName.GetComponent<Text>();
        _dishNameText = DishName.GetComponent<Text>();

        ReSetText();
        TextDelete();


        if(HasChild(_TileFoodContent))
        {
            _firstObj = _TileFoodContent.transform.GetChild(0).gameObject;
            _firstButton = _firstObj.GetComponent<Button>();
            _firstButton.Select();
        }
        else
        {
            _firstObj = _TileDishContent.transform.GetChild(0).gameObject;
            _firstButton = _firstObj.GetComponent<Button>();
            _firstButton.Select();
        }

        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject && EventSystem.current.currentSelectedGameObject.tag == "item")
        {
            _selectedObject = EventSystem.current.currentSelectedGameObject;
            SelectRecipe(_selectedObject);
        }
        else if (EventSystem.current.currentSelectedGameObject && EventSystem.current.currentSelectedGameObject.tag != "item")
        {
            beforObj.GetComponent<Button>().Select();
            SelectRecipe(beforObj);
        }
        beforObj = _selectedObject;



    }
    void ReSetText()
    {
        HP_Text.text = "+0";
        ATK_Text.text = "+0";
        DEF_Text.text = "+0";
        SPD_Text.text = "+0";
        CRATE_Text.text = "+0";
        CDMG_Text.text = "+0";
    }

    public void TextDelete()
    {
        GameObject _objectText = GameObject.Find("MenuFoodContent");
        foreach (Transform child in _objectText.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void SelectRecipe(GameObject _selectedObject)
    {
        _foodSourceData = _itemDataBase.ItemSearch(_selectedObject.name);

        ReSetText();

        if (_foodSourceData.itemType == ItemType.FOOD)
        {
            _foodNameText.text = _foodSourceData.itemName;
            _dishNameText.text = "";
        }
        else if (_foodSourceData.itemType == ItemType.DISH)
        {
            _dishNameText.text = _foodSourceData.itemName;
            _foodNameText.text = "";
        }



        statusTypes = _foodSourceData.GetStatusType();
        int statusCount;

        for (int i = 0; i < statusTypes.Length; i++)
        {
            statusCount = _foodSourceData.GetStatusValue(statusTypes[i]);

            switch (statusTypes[i])
            {
                case "HP":
                    HP_Text.text = "+" + statusCount.ToString("d");
                    break;
                case "ATK":
                    ATK_Text.text = "+" + statusCount.ToString("d");
                    break;
                case "DEF":
                    DEF_Text.text = "+" + statusCount.ToString("d");
                    break;
                case "SPD":
                    SPD_Text.text = "+" + statusCount.ToString("d");
                    break;
                case "CRITRATE":
                    CRATE_Text.text = "+" + statusCount.ToString("d");
                    break;
                case "CRITDMG":
                    CDMG_Text.text = "+" + statusCount.ToString("d");
                    break;
            }
        }

        //Need Foods

        TextDelete();

        foodTypes = _foodSourceData.GetFoodType();
        GameObject _main = GameObject.Find("Main");
        PlayerFoodManager _playerFoodManager = _main.GetComponent<PlayerFoodManager>();

        for (int k = 0; k < foodTypes.Length; k++)
        {
            _textRecipePrefab = (GameObject)Instantiate(FoodPrefab, transform.position, Quaternion.identity);
            _textRecipePrefab.transform.SetParent(GameObject.Find("MenuFoodContent").transform, false);



            GameObject _cloneObject = GameObject.Find("FoodImage(Clone)");
            Image _cloneText = _cloneObject.GetComponent<Image>();
            Text _cloneText2 = _cloneObject.transform.GetChild(0).GetComponent<Text>();
            _foodSourceData_f = _itemDataBase.ItemSearch(foodTypes[k]);
            _cloneText.sprite = _foodSourceData_f.icon;
            _cloneText2.text = "✖︎" + _foodSourceData.GetFoodValue(foodTypes[k]);
            _cloneObject.name = foodTypes[k];
        }
    }

    public bool HasChild(GameObject gameObject)
    {
        return 0 < gameObject.transform.childCount;  
    }
}