using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アイテムをまとめて管理
[SerializeField]
[CreateAssetMenu(fileName = "ItemDataBase", menuName = "CreateItemDataBase")]
public class ItemDataBase : ScriptableObject
{
    public List<FoodSourceData> foodSourceDatas = new List<FoodSourceData>();

    public int Length { get; internal set; }

    public FoodSourceData ItemSearch(string id)
    {
        for(int i = 0; i < foodSourceDatas.Count; i++)
        {
            if(foodSourceDatas[i].id == id)
            {
                //Debug.Log("GetFOOdSourceDatas" + foodSourceDatas[i].name);
                return foodSourceDatas[i];
            }
        }
        return null;
    }
}
