using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//アイテムをゲットした時に消す
public class GetFood:MonoBehaviour
{
    private float gravity = 1.0f;
    public string _objName;
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;

    //Start
    void Start()
    {
        this.gameObject.name = _objName;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(sr.isVisible){
            // レシピはvelocity無効
            if(this.gameObject.tag == "item"){
                rb.velocity = new Vector2(0.0f, -gravity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //タグが "item" のアイテム
        if(collision.tag == "Player" || collision.tag == "DeadZone")
        {
            Destroy(this.gameObject);
        }
        if(collision.tag == "ground")
        {
            gravity = 0.0f;
        }
    }
}
