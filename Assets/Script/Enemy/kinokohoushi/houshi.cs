using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class houshi : MonoBehaviour // クラス名はローマ字
{
    #region // variables
    //private Random direction = new Random();
    //private Random distance = new Random();

    private float maxDistance = 0.0f;
    private float speed = 0.0f;
    private float distanceCovered = 0.0f;
    private float tf = 0.0f;
    private float hoshiInitialPosX = 0.0f;

    private bool isLeft = false;
    private bool isFloat = false;

    private Animator anim = null;
    private Rigidbody2D rb = null;
    private CircleCollider2D circol = null;
    private Transform hoshiPosition;
    #endregion

    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        circol = GetComponent<CircleCollider2D>();
        hoshiPosition = transform;
        hoshiInitialPosX = transform.position.x;
    }

    public void Hoshi(System.Random rand){
        distanceCovered = 0.0f;
        maxDistance = rand.Next(3, 9);
        tf = rand.Next(0, 2);
        speed = (float)rand.NextDouble();
        Debug.Log(tf);
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
                rb.velocity = new Vector2(-speed, 0);
            }
            else{
                rb.velocity = new Vector2(speed, 0);
            }
            distanceCovered += Mathf.Abs(speed);
        }
    }

    public void HoshiDelete(){
        anim.Play("kinoko_hoshi_default");
    }
}
