using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public groundCheck ground;
    public groundCheck head;
    public float speed = 2;
    private float gravity = 3;
    private float jumpSpeed = 9;
    private float jumpHeight = 2;
    private float jumpLimitTime = 3;

    private float jumpPos = 0.0f;
    private float jumpTime = 0.0f;
    private bool isGround = false;
    private bool isJump = false;
    private bool isRun = false;
    private bool isHead = false;
    //private bool isDown = false;
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private CapsuleCollider2D capcol = null;
    //private string enemyTag = "enemy";
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capcol = GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate()
    {
        if(/*!isDown*/false){
            isGround = ground.IsGround();
            isHead = ground.IsGround();

            float xSpeed = GetXSpeed();
            float ySpeed = GetYSpeed();

            SetAnimation();

            rb.velocity = new Vector2(xSpeed, ySpeed);
        }
        else{
            rb.velocity = new Vector2(0, -gravity);
        }
    }

///<summary>
/// Y???????v?Z?A?W?????v???i???B???x???????B
///</summary>

    private float GetYSpeed(){
        float verticalKey = Input.GetAxis("Vertical");
        bool wKey = Input.GetKey("w");
        bool upKey = Input.GetKey("up");
        float ySpeed = -gravity;

        if(isGround){
            if(verticalKey > 0 || wKey || upKey){
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y;
                isJump = true;
                jumpTime = 0.0f;
            }
            else{
                isJump = false;
            }
        }
        // 2?i?W?????v??????elseif???????B
        else if(isJump){
            bool pushUpKey = false;
            if(verticalKey > 0 || wKey || upKey){
                pushUpKey = true;
            }
            bool canHeight = jumpPos + jumpHeight > transform.position.y;
            bool canTime = jumpLimitTime > jumpTime;

            if(pushUpKey && canHeight && canTime && !isHead){
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else{
                isJump = false;
                jumpTime = 0.0f;
            }
        }

        return ySpeed;
    }


///<summary>
/// X???????v?Z?A???x???????B
///</summary>
    private float GetXSpeed(){
        float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;
        bool dKey = Input.GetKey("d");
        bool rightKey = Input.GetKey("right");
        bool aKey = Input.GetKey("a");
        bool leftKey = Input.GetKey("left");
        

        if(horizontalKey > 0 || rightKey || dKey){
            transform.localScale = new Vector3(1, 1, 1);
            isRun = true;
            xSpeed = speed;
        }
        else if(horizontalKey < 0 || leftKey || aKey){
            transform.localScale = new Vector3(-1, 1, 1);
            isRun = true;
            xSpeed = -speed;
        }
        else{
            isRun = false;
            xSpeed = 0.0f;
        }

        return xSpeed;
    }
    
    
    
///<summary>
/// ?A?j???[?V??????????
    private void SetAnimation(){
        anim.SetBool("jump_player", isJump);
        anim.SetBool("ground_player", isGround);
        anim.SetBool("run_player", isRun);
    }
    
    
}
