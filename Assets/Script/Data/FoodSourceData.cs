using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum ItemType
{
    FOOD,
    DISH
}

public enum DishType
{
    meat,
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


//�H�ށA���V�s�̃f�[�^��o�^����A�Z�b�g�𐶐��ł���
[Serializable]
[CreateAssetMenu(menuName = "ItemSourceData")]
public class FoodSourceData:ScriptableObject
{
    //�ŗLID
    [SerializeField]
    private string _id;

    public string id
    {
        get { return _id; }
    }


    //�A�C�e���̖��O
    [SerializeField]
    private string _itemName;

    public string itemName
    {
        get { return _itemName; }
    }


    //�A�C�e���̎��
    [SerializeField]
    private ItemType _itemType;

    public ItemType itemType
    {
        get { return _itemType; }
    }

    public ItemType GetItemType()
    {
        return _itemType;
    }


    //�A�C�R��
    [SerializeField]
    private Sprite _icon;

    public Sprite icon
    {
        get { return _icon; }
    }


    //��ɓ���K�w
    [SerializeField]
    private int _floor;

    public int floor
    {
        get { return _floor; }
    }


    //�H�ނ̎�ނƐ�
    [Serializable]
    class DishData
    {
        public DishType _dishType = default;
        public int _dishValue = 1;
    }
    [SerializeField]
    DishData[] foods = default;

    public string[] GetFoodType()
    {
        string[] foodString = new string[foods.Length];
        DishType[] foodType = new DishType[foods.Length];
        for(int i = 0; i < foods.Length; i++)
        {
            foodType[i] = foods[i]._dishType;
            foodString[i] = foodType[i].ToString();
        }
        return foodString;
    }

    public int GetFoodValue(string _foodType)
    {
        for(int i = 0; i < foods.Length; i++)
        {
            string dishTypeString = foods[i]._dishType.ToString();
            if(dishTypeString == _foodType)
            {
                return foods[i]._dishValue;
            }
        }
        return 0;
    }




    //�X�e�[�^�X�̎�ނƐ�
    [Serializable]
    class StatusData
    {
        public StatusType _statusType = default;
        public int _statusValue = 1;
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
        }
        return statusString;
    }

    public int GetStatusValue(string _statusType)
    {
        for(int i = 0; i < status.Length; i++)
        {
            string statusTypeString = status[i]._statusType.ToString();
            if(statusTypeString == _statusType)
            {
                return status[i]._statusValue;
            }
        }
        return 0;
    }
}

