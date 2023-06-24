using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PushCookButton : MonoBehaviour
{
    public ItemDataBase _itemDataBase;
    PlayerFoodManager _playerFoodManager;
    private FoodSourceData _foodSourceData;

    // Start is called before the first frame update
    void Start()
    {
        GameObject _main = GameObject.Find("Main");
        _playerFoodManager = _main.GetComponent<PlayerFoodManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushCook()
    {
        _foodSourceData = _itemDataBase.ItemSearch(PushDishButton.nowPushDish);

        string[] foodTypes = _foodSourceData.GetFoodType(); ;
        int foodCount;
        string[] statusTypes = _foodSourceData.GetStatusType();
        int statusCount;

        bool cookOK = true;

        for(int i = 0; i < foodTypes.Length; i++)
        {
            if(_playerFoodManager.GetItemCount(foodTypes[i]) < _foodSourceData.GetFoodValue(foodTypes[i])){
                cookOK = false;
                break;
            }
        }

        if (cookOK)
        {
            for (int i = 0; i < foodTypes.Length; i++)
            {
                foodCount = _foodSourceData.GetFoodValue(foodTypes[i]);
                _playerFoodManager.UseItem(foodTypes[i], foodCount);
            }

            for (int i = 0; i < statusTypes.Length; i++)
            {
                statusCount = _foodSourceData.GetStatusValue(statusTypes[i]);
                switch (statusTypes[i])
                {
                    case "HP":
                        Status.HP += statusCount;
                        break;
                    case "ATK":
                        Status.ATK += statusCount;
                        break;
                    case "DEF":
                        Status.DEF += statusCount;
                        break;
                    case "SPD":
                        Status.SPD += statusCount;
                        break;
                    case "CRITRITE":
                        Status.CRITRATE += statusCount;
                        break;
                    case "CRITDMG":
                        Status.CRITDMG += statusCount;
                        break;
                }
        }
        
                

        }
    }
}
