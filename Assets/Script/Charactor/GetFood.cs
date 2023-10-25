using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//アイテムをゲットした時に消す
public class GetFood:MonoBehaviour
{
    public string _objName;

    //Start
    void Start()
    {
        this.gameObject.name = _objName;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //タグが "item" のアイテム
        if(collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
