using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public string id;   //�A�C�e��id

    public int count;  //������

    //�R���X�g���N�^
    public ItemData(string id, int count = 1)
    {
        this.id = id;
        this.count = count;
    }

    //�������J�E���g�A�b�v
    public void CountUp(int value = 1)
    {
        count += value;
    }

    //�������J�E���g�_�E��
    public void CountDown(int value)
    {
        count -= value;
    }

    public int GetCount()
    {
        return count;
    }
}


//�H�ނ̌����Ǘ�
public class PlayerFoodManager:MonoBehaviour
{
    [SerializeField] static List<ItemData> _itemDataList = new List<ItemData>();   //�v���C���[�̏����A�C�e��

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
                if(_itemDataList[i].GetCount() == 0)
                {
                    _itemDataList.Remove(_itemDataList[i]);
                }
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