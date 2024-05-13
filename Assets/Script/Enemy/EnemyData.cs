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
    public string Name;
    public int flor;
    public float maxHP;
    public float gravity;
    
    public GameObject basicObject;
    public GameObject uniqueObject;
}
