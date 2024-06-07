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
    // ���O
    public string Name;
    // �o������K�w
    public int flor;
    // HP
    public float maxHP;
    // �d�� ��{�I�ɂ�5�ŗǂ���,�󒆂ɂ���G��0�ɂ���
    public float gravity;
    
    // ��{�A�C�e���i�� �� ���j
    public GameObject basicObject;
    // �ŗL�A�C�e��
    public GameObject uniqueObject;
}
