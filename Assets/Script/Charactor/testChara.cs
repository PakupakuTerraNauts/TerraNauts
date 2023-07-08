using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//テスト用キャラ操作
public class testChara: MonoBehaviour
{
    private float speed = 0.05f;
    

    void Start()
    {
    }

    void Update()
    {
        Vector2 position = transform.position;

        //テスト用のキャラ操作
        if (Input.GetKey("a"))
        {
            position.x -= speed;
        }
        else if (Input.GetKey("d"))
        {
            position.x += speed;
        }
        else if (Input.GetKey("space"))
        {
            position.y += speed;
        }

        transform.position = position;
    }

    
}