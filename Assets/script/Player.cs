using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private string enemyTag = "Enemy";
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;
        bool dKey = Input.GetKey("d");
        bool rightKey = Input.GetKey("right");
        bool aKey = Input.GetKey("a");
        bool leftKey = Input.GetKey("left");

        if(dKey || rightKey || horizontalKey > 0){
            transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("run_player", true);
            xSpeed = speed;
        }
        else if(aKey || leftKey || horizontalKey < 0){
            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("run_player", true);
            xSpeed = -speed;
        }
        else{
            anim.SetBool("run_player",false);
            xSpeed = 0.0f;
        }
        rb.velocity = new Vector2(xSpeed, rb.velocity.y);
    }
    
        private void OnCollisionEnter2D(Collision2D collision){
            if(collision.collider.tag == enemyTag){
                
            }
        }
}
