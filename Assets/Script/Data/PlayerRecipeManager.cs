using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�v���C���[�̏����A�C�e��
public class PlayerRecipeManager:MonoBehaviour
{
    [SerializeField] static List<ItemData> _itemDataList = new List<ItemData>();   //プレイヤーの所持アイ�?�?

    //�A�C�e�����擾
    public void CountItem(string itemId, int count)
    {
        //List��������
        for(int i = 0; i < _itemDataList.Count; i++)
        {
            //ID����v���Ă�����J�E���g
            if(_itemDataList[i].id == itemId)
            {
                //�A�C�e�����J�E���g
                _itemDataList[i].CountUp(count);
                return;
            }
        }

        //ID����v���Ȃ���΃A�C�e����ǉ�
        ItemData itemData = new ItemData(itemId, count);
        _itemDataList.Add(itemData);
    }

    //�A�C�e�����g�p
    public void UseItem(string itemId, int count)
    {
        //List��������
        for(int i = 0; i < _itemDataList.Count; i++)
        {
            //ID����v���Ă�����J�E���g
            if(_itemDataList[i].id == itemId)
            {
                //�A�C�e�����J�E���g�_�E��
                _itemDataList[i].CountDown(count);
                break;
            }
        }
    }

    //�A�C�e���̎����Ă�����擾
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

    //�����Ă���A�C�e����z��Ŏ擾
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