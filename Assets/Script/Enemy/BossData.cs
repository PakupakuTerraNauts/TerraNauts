using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "BossData", menuName = "ScriptableOjbects/CreateBossData")]
public class BossData : ScriptableObject
{
    public List<bossData> BossDataList = new List<bossData>();
}

[System.Serializable]
public class bossData
{
    public string Name;

    public float maxHP;
    
    public GameObject basicObject;
}

