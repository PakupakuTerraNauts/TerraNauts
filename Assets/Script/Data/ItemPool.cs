using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    public static ItemPool instance = null;

    private ObjectPool pool;

    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    void Start(){
        pool = ObjectPool.instance;
    }

    public void GenerateItem(GameObject item, Vector2 position){
        GameObject objMove = null;  // オブジェクトをドロップ位置まで移動するための箱

        foreach(Transform obj in this.gameObject.transform){
            if(item.name == obj.gameObject.name){
                objMove = ObjectPool.instance.GetObject(obj.gameObject, item);
                objMove.transform.position = new Vector2(position.x, position.y);
                return;
            }
        }

        var newObj = new GameObject(item.name);
        newObj.transform.parent = this.transform;
        objMove = ObjectPool.instance.GetObject(newObj, item);
        objMove.transform.position = new Vector2(position.x, position.y);
    }
}