using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "EnemyData", menuName = "ScriptableOjbects/CreateEnemyData")]
public class EnemyData : ScriptableObject
{
    public List<enemyData> EnemyDataList = new List<enemyData>();
}

[System.Serializable]
public class enemyData
{
    // 名前
    public string Name;
    // 出現する階層
    public int flor;
    // HP
    public float maxHP;
    // 重力 基本的には5で良いが,空中にいる敵は0にする
    public float gravity;
    
    // 基本アイテム（水 草 肉）
    public GameObject basicObject;
    // 固有アイテム
    public GameObject uniqueObject;
}
