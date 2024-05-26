using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class houshi : MonoBehaviour // クラス名はローマ字
{
    #region // variables
    private float distanceCovered = 0.0f;
    private float hoshiInitialPosX = 0.0f;

    private bool isLeft = false;

    private Animator anim = null;
    private Rigidbody2D rb = null;
    private Transform hoshiPosition;
    #endregion

    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        hoshiPosition = transform;
        hoshiInitialPosX = transform.position.x;
    }

    // houshiで乱数生成するとシードが同じになってしまうので、kinokohosushiから引数で受け取る 
    public void Hoshi(){
        distanceCovered = 0.0f;
        float maxDistance = UnityEngine.Random.Range(3, 9);
        float tf = UnityEngine.Random.Range(0, 2);
        float speed = UnityEngine.Random.value;
        anim.Play("kinoko_hoshigrowing");

        if(tf == 1.0f){
            isLeft = true;
            hoshiPosition.position = new Vector3(hoshiInitialPosX - 1.5f, hoshiPosition.position.y, 0.0f);
        }
        else{
            isLeft = false;
            hoshiPosition.position = new Vector3(hoshiInitialPosX + 1.5f, hoshiPosition.position.y, 0.0f);
        }

        while(distanceCovered < maxDistance){
            if(isLeft){
                rb.velocity = new Vector2(-speed, 0.0f);
            }
            else{
                rb.velocity = new Vector2(speed, 0.0f);
            }
            distanceCovered += Mathf.Abs(speed);
        }
    }

    public void HoshiDelete(){
        anim.Play("kinoko_hoshi_default");
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Sword"){
            HoshiDelete();
        }
    }
}
