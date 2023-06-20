using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim = null;
    //private Rigidbody2D rb = null;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalKey = Input.GetAxis("Horizontal");
        bool dKey = Input.GetKey("d");
        bool rightKey = Input.GetKey("right");
        bool aKey = Input.GetKey("a");
        bool leftKey = Input.GetKey("left");

        if(dKey || rightKey || horizontalKey > 0){
            transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("run_player", true);
        }
        else if(aKey || leftKey || horizontalKey < 0){
            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("run_player", true);
        }
        else{
            anim.SetBool("run_player",false);
        }
    }
}
