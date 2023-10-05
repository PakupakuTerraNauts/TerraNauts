using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatusPlus_pushRecipe:MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        HP_Text = HP_plus.GetComponent<Text>();
        ATK_Text = ATK_plus.GetComponent<Text>();
        DEF_Text = DEF_plus.GetComponent<Text>();
        SPD_Text = SPD_plus.GetComponent<Text>();
        CRATE_Text = CRATE_plus.GetComponent<Text>();
        CDMG_Text = CDMG_plus.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.currentSelectedGameObject)
        {
            ReSetText();

            _selectedObject = EventSystem.current.currentSelectedGameObject;
            _foodSourceData = _itemDataBase.ItemSearch(_selectedObject.name);

            string[] statusTypes = _foodSourceData.GetStatusType();
            int statusCount;

            for(int i = 0; i < statusTypes.Length; i++)
            {
                statusCount = _foodSourceData.GetStatusValue(statusTypes[i]);

                switch(statusTypes[i])
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

        }

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
}
