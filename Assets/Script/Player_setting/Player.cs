using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public groundCheck ground;
    public groundCheck head;
    public float speed = 3;
    private float gravity = 4;
    private float jumpSpeed = 6;
    public float jumpHeight = 2;
    private float jumpLimitTime = 3;
    public static int HP = 100;
    public static int nowHP = 100;

    public static int ATK = 100;
    public static int DEF = 0;
    public static int SPD = 100;
    public static int CRITRATE = 50;
    public static int CRITDMG = 50;

    private float jumpPos = 0.0f;
    private float jumpTime = 0.0f;
    private float continueTime = 0.0f;
    private float blinkTime = 0.0f;
    private bool isGround = false;
    private bool isJump = false;
    private bool isWalk = false;
    private bool isHead = false;
    private bool isDown = false;
    private bool isNAttack = false;
    private bool isContinue = false;
    private bool nonDownAnim = false;
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private CapsuleCollider2D capcol = null;
    private SpriteRenderer sr = null; 
    private string enemyTag = "Enemy";
    private string sakebigoe = "Sakebigoe";
    private string hitAreaTag = "HitArea";

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capcol = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update(){
        if(isContinue){
            if(blinkTime > 0.2f){
                sr.enabled = true;
                blinkTime = 0.0f;
            }
            else if(blinkTime > 0.1f){
                sr.enabled = false;
            }
            else{
                sr.enabled = true;
            }

            if(continueTime > 1.0f){
                isContinue = false;
                blinkTime = 0.0f;
                continueTime = 0.0f;
                sr.enabled = true;
            }
            else{
                blinkTime += Time.deltaTime;
                continueTime += Time.deltaTime;
            }
        }
    }

    void FixedUpdate()
    {
        if(!isDown){
            capcol = GetComponent<CapsuleCollider2D>();
        }

        if(/*!isDown*/true){
            isNAttack = false;
            isGround = ground.IsGround();
            isHead = ground.IsGround();

            if(isNAttack = PlayerNormalAttack()){
                anim.SetTrigger("nAttack_neko");
            }

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
/// player's normal attack
///</summary>
    private bool PlayerNormalAttack(){

        if(Input.GetKey("return")){
            return true;

        }
        return false;
    }

///<summary>
/// calculate Y conponent, return speed.
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

        // 2dan-jump koko
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
/// calculate X conponent, return speed.
///</summary>
    private float GetXSpeed(){
        float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;
        bool dKey = Input.GetKey("d");
        bool rightKey = Input.GetKey("right");
        bool aKey = Input.GetKey("a");
        bool leftKey = Input.GetKey("left");

        

        if(horizontalKey > 0 || rightKey || dKey){
            transform.localScale = new Vector3(2, 2, 2);
            isWalk = true;
            xSpeed = speed;
        }
        else if(horizontalKey < 0 || leftKey || aKey){
            transform.localScale = new Vector3(-2, 2, 2);
            isWalk = true;
            xSpeed = -speed;
        }
        else{
            isWalk = false;
            xSpeed = 0.0f;
        }
        return xSpeed;
    }


    private bool IsContinueWaitin(){
        return IsDownAnimEnd();
    }

    private bool IsDownAnimEnd(){
        if(isDown && anim != null){
            AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
            if(currentState.IsName("neko_die")){
                if(currentState.normalizedTime >= 1){
                    return true;
                }
            }
        }

        return false;
    }


    public void ContinuePlayer(){
        isDown = false;
        anim.Play("neko_die");
        isJump = false;
        isWalk = false;
        isContinue = true;
    }
    
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.tag == enemyTag){
            nowHP = nowHP - 10;
        }
        if(collision.collider.tag == sakebigoe){
            nowHP = nowHP - 20;
        }
        
        if(nowHP <= 0){
            anim.Play("neko_die");
            isDown = true;
            StartCoroutine(PlayerDie());
        }
    }
    
    
///<summary>
/// animation
///</summary>
    private void SetAnimation(){
        anim.SetBool("jump_neko", isJump);
        anim.SetBool("ground_neko", isGround);
        anim.SetBool("walk_neko", isWalk);
    }

    IEnumerator PlayerDie()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOver");
        nowHP = HP;
        yield break;
    }


}

