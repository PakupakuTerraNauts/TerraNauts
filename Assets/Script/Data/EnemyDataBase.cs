using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// “G‚Ìƒf[ƒ^
[SerializeField]
[CreateAssetMenu(fileName = "EnemyDataBase", menuName = "CreateEnemyDataBase")]

public class EnemyDataBase : ScriptableObject
{
    public List<EnemySourceData> enemeySourceDatas = new List<EnemySourceData>();
    
    public int length { get; internal set; }

    public EnemySourceData EnemyDataSearch(string id){
        for(int i = 0; i < enemeySourceDatas.Count; i++){
            if(enemeySourceDatas[i].id == id){
                return enemeySourceDatas[i];
            }
        }

        return null;
    }
}
