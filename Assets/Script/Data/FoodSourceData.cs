using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;


public enum ItemType
{
    FOOD,
    DISH
}

public enum DishType
{
    meet,
    grass,
    water,
    egg,
    radish,
    essence,
    pork,
    mashroom,
    crab,
    fire,
    pumpkin,
    octopus,
    pineapple,
    juice,
    carrot,
    chocolate,
    apple,
    ice
}

public enum StatusType
{
    HP,
    ATK,
    DEF,
    SPD,
    CRITRATE,
    CRITDMG
}

[Serializable]
[CreateAssetMenu(menuName = "BlogAssets/ItemSourceData")]
public class FoodSourceData : ScriptableObject
{
    //固有ID
    [SerializeField]
    private string _id;

    public string id
    {
        get { return _id; }
    }


    //アイテムの名前
    [SerializeField]
    private string _itemName;

    public string itemName
    {
        get { return _itemName; }
    }


    //アイテムの種類
    [SerializeField]
    private ItemType _itemType;

    public ItemType itemType
    {
        get { return _itemType; }
    }


    //アイコン
    [SerializeField]
    private Sprite _icon;

    public Sprite icon
    {
        get { return _icon; }
    }


    //手に入る階層
    [SerializeField]
    private int _floor;

    public int floor
    {
        get { return _floor; }
    }


    //食材の種類と数
    [Serializable]
    class DishData
    {
        public DishType _dishType = default;
        public int _dishValue = default;
    }
    [SerializeField]
    DishData[] foods = default;

    public string[] GetFoodType()
    {
         string[] foodString = new string[foods.Length];
         DishType[] foodType = new DishType[foods.Length];
         for (int i = 0; i < foods.Length; i++)
         {
             foodType[i] = foods[i]._dishType;
             foodString[i] = foodType[i].ToString();
             Debug.Log(i + ":" + foodString[i]);
        }
         return foodString;
    }

    public int GetFoodValue(string _foodType)
    {
        for (int i = 0; i < foods.Length; i++)
        {
            string dishTypeString = foods[i]._dishType.ToString();
            if (dishTypeString == _foodType)
            {
                return foods[i]._dishValue;
            }
        }
        return 0;
    }

    


    //ステータスの種類と数
    [Serializable]
    class StatusData
    {
        public StatusType _statusType = default;
        public int _statusValue = default;
    }
    [SerializeField]
    StatusData[] status = default;

    public string[] GetStatusType()
    {
        string[] statusString = new string[status.Length];
        StatusType[] statusType = new StatusType[status.Length];
        for(int i = 0; i < status.Length; i++)
        {
            statusType[i] = status[i]._statusType;
            statusString[i] = statusType[i].ToString();
            Debug.Log(i + ":" + statusString[i]);
        }
        return statusString;
    }

    public int GetStatusValue(string _statusType)
    {
        for (int i = 0; i < status.Length; i++)
        {
            string statusTypeString = status[i]._statusType.ToString();
            if (statusTypeString == _statusType)
            {
                return status[i]._statusValue;
            }
        }
        return 0;
    }
}

