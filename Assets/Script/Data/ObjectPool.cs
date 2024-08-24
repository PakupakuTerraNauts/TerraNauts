using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public static ObjectPool instance = null;
    //private Queue<GameObject> poolQueue = new Queue<GameObject>();

    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

/// <summary>
/// プールからオブジェクトを取得
/// </summary>
/// <param name="parent">クリティカルプール(生成用)</param>
/// <param name="prefab">クリティカルエフェクトのプレハブ(生成用)</param>
/// <returns>今使えるクリティカルエフェクト</returns>
    public GameObject GetObject(GameObject parent, GameObject prefab){
        foreach(Transform child in parent.transform){
            if(!child.gameObject.activeSelf){
                child.gameObject.SetActive(true);
                return child.gameObject;
            }
        }
        return Instantiate(prefab, Vector3.zero, Quaternion.identity, parent.transform);
    }

    // オブジェクトをプールに返却
    public void ReturnObject(GameObject obj) {
        obj.SetActive(false); // プールに戻すオブジェクトを無効化
        //poolQueue.Enqueue(obj); // プールに追加
    }
}
