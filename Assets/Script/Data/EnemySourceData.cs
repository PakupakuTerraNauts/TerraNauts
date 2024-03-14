using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(menuName = "EnemySourceData")]
public class EnemySourceData : ScriptableObject
{
    // �GID
    [SerializeField]
    private string _id;

    public string id{
        get { return _id; }
    }

    // �A�C�e���̖��O
    [SerializeField]
    private string _enemyName;

    public string enemyName{
        get { return _enemyName; }
    }

    // �o������K�w
    [SerializeField]
    private int _floor;

    public int floor{
        get { return _floor; }
    }

    // HP
    [SerializeField]
    private int _hp;

    public int hp{
        get { return _hp; }
    }

    // �d��
    [SerializeField]
    private float _gravity;

    public float gravity{
        get { return _gravity; }
    }

    // �ʏ�H��
    [SerializeField]
    private GameObject _basicObject;
    
    public GameObject basicObject{
        get { return _basicObject; }
    }

    // ���A�H��
    [SerializeField]
    private GameObject _uniqueObject;

    public GameObject uniqueObject{
        get { return _uniqueObject; }
    }
}
