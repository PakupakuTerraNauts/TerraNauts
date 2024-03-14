using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(menuName = "EnemySourceData")]
public class EnemySourceData : ScriptableObject
{
    // 敵ID
    [SerializeField]
    private string _id;

    public string id{
        get { return _id; }
    }

    // アイテムの名前
    [SerializeField]
    private string _enemyName;

    public string enemyName{
        get { return _enemyName; }
    }

    // 出現する階層
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

    // 重力
    [SerializeField]
    private float _gravity;

    public float gravity{
        get { return _gravity; }
    }

    // 通常食材
    [SerializeField]
    private GameObject _basicObject;
    
    public GameObject basicObject{
        get { return _basicObject; }
    }

    // レア食材
    [SerializeField]
    private GameObject _uniqueObject;

    public GameObject uniqueObject{
        get { return _uniqueObject; }
    }
}
