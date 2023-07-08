using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//レシピの個数を管理
public class PlayerRecipeManager : MonoBehaviour
{
    [SerializeField] private List<ItemData> _itemDataList = new List<ItemData>();   //プレイヤーの所持アイテム

    //アイテムを取得
    public void CountItem(string itemId, int count)
    {
        //List内を検索
        for (int i = 0; i < _itemDataList.Count; i++)
        {
            //IDが一致していたらカウント
            if (_itemDataList[i].id == itemId)
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
        for (int i = 0; i < _itemDataList.Count; i++)
        {
            //IDが一致していたらカウント
            if (_itemDataList[i].id == itemId)
            {
                //アイテムをカウントダウン
                _itemDataList[i].CountDown(count);
                break;
            }
        }
    }

    //アイテムの持ってる個数を取得
    public int GetItemCount(string itemId)
    {
        for (int i = 0; i < _itemDataList.Count; i++)
        {
            if (_itemDataList[i].id == itemId)
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
        for (int i = 0; i < _itemDataList.Count; i++)
        {
            tmp[i] = _itemDataList[i].id;
        }
        return tmp;
    }
}