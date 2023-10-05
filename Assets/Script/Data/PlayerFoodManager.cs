using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public string id;   //アイテムid

    public int count;  //所持数

    //コンストラクタ
    public ItemData(string id, int count = 1)
    {
        this.id = id;
        this.count = count;
    }

    //所持数カウントアップ
    public void CountUp(int value = 1)
    {
        count += value;
    }

    //所持数カウントダウン
    public void CountDown(int value)
    {
        count -= value;
    }

    public int GetCount()
    {
        return count;
    }
}


//食材の個数を管理
public class PlayerFoodManager:MonoBehaviour
{
    [SerializeField] static List<ItemData> _itemDataList = new List<ItemData>();   //プレイヤーの所持アイテム

    //アイテムを取得
    public void CountItem(string itemId, int count)
    {
        //List内を検索
        for(int i = 0; i < _itemDataList.Count; i++)
        {
            //IDが一致していたらカウント
            if(_itemDataList[i].id == itemId)
            {
                //アイテムをカウント
                _itemDataList[i].CountUp(count);
                return;
            }
        }

        //IDが一致しなければアイテムを追加
        ItemData itemData = new ItemData(itemId, count);
        _itemDataList.Add(itemData);
    }

    //アイテムを使用
    public void UseItem(string itemId, int count)
    {
        //List内を検索
        for(int i = 0; i < _itemDataList.Count; i++)
        {
            //IDが一致していたらカウント
            if(_itemDataList[i].id == itemId)
            {
                //アイテムをカウントダウン
                _itemDataList[i].CountDown(count);
                if(_itemDataList[i].GetCount() == 0)
                {
                    _itemDataList.Remove(_itemDataList[i]);
                }
                break;
            }
        }
    }

    //アイテムの持ってる個数を取得
    public int GetItemCount(string itemId)
    {
        for(int i = 0; i < _itemDataList.Count; i++)
        {
            if(_itemDataList[i].id == itemId)
            {
                return _itemDataList[i].GetCount();
            }
        }
        return 0;
    }

    //持っているアイテムを配列で取得
    public string[] GetItemId()
    {
        string[] tmp = new string[_itemDataList.Count];
        for(int i = 0; i < _itemDataList.Count; i++)
        {
            tmp[i] = _itemDataList[i].id;
        }
        return tmp;
    }
}