using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
<<<<<<< HEAD
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
=======
    public float speed;
    
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private string enemyTag = "Enemy";
>>>>>>> 75a25fae69896b08d2dadff28967600e838859a5
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
<<<<<<< HEAD
        capcol = GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate()
=======
    }

    void Update()
>>>>>>> 75a25fae69896b08d2dadff28967600e838859a5
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
/// Y成分の計算、ジャンプを司る。速度を返す。
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
        // 2段ジャンプはこのelseifに書く。
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
/// X成分の計算、速度を返す。
///</summary>
    private float GetXSpeed(){
        float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;
        bool dKey = Input.GetKey("d");
        bool rightKey = Input.GetKey("right");
        bool aKey = Input.GetKey("a");
        bool leftKey = Input.GetKey("left");
        float xSpeed = 0.0f;


        if(horizontalKey > 0 || rightKey || dKey){
            transform.localScale = new Vector3(1, 1, 1);
<<<<<<< HEAD
            isRun = true;
=======
            anim.SetBool("run_player", true);
>>>>>>> 75a25fae69896b08d2dadff28967600e838859a5
            xSpeed = speed;
        }
        else if(horizontalKey < 0 || leftKey || aKey){
            transform.localScale = new Vector3(-1, 1, 1);
<<<<<<< HEAD
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
/// アニメーションを設定
    private void SetAnimation(){
        anim.SetBool("jump_player", isJump);
        anim.SetBool("ground_player", isGround);
        anim.SetBool("run_player", isRun);
    }
    
    
=======
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
>>>>>>> 75a25fae69896b08d2dadff28967600e838859a5
}
