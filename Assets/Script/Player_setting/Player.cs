using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region // variables
    public groundCheck ground;
    public groundCheck head;
    public float gravity;
    private float jumpSpeed = 6.0f;
    public float jumpHeight;
    private float jumpLimitTime = 1.5f;
    public static int HP = 100;
    public static float nowHP = 100.0f;

    public static int ATK = 100;
    public static int DEF = 0;
    public static int SPD = 100;
    // ↓ Enemy.cs内で使用している
    public static int CRITRATE = 50;
    public static int CRITDMG = 50;

    private float jumpPos = 0.0f;
    private float jumpTime = 0.0f;
    private float continueTime = 0.0f;
    private float blinkTime = 0.0f;
    private float invincibleTime = 0.0f;
    private bool isGround = false;
    private bool isJump = false;
    private bool isWalk = false;
    private bool isHead = false;
    private bool isDown = false;
    private bool isAttack = false;
    private bool isAttackCool = false;
    private bool isContinue = false;
    private bool isDamaged = false;
    private bool coolTime = false;
    private bool readytojump = false;
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private CapsuleCollider2D capcol = null;
    private SpriteRenderer sr = null;

<<<<<<< HEAD
    public AnimationCurve jumpCurve;
=======
    public AnimationCurve JumpCurve;
>>>>>>> main
    #endregion

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capcol = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }


    private void Update(){
        if(!isDown){
            isAttack = PlayerAttack();

            // 攻撃アニメーション→コルーチンへ
            if(isAttack && !isAttackCool){
                StartCoroutine("AttackCool");
            }

            // ダメージを受けた直後は無敵時間
            if (isDamaged){
                if(blinkTime > 0.2f){
                    sr.enabled = true;
                    blinkTime = 0.0f;
                }
                else if (blinkTime > 0.1f){
                    sr.enabled = false;
                }
                else{
                    sr.enabled = true;
                }

                if(continueTime > 1.0f){
                    isDamaged = false;
                    blinkTime = 0f;
                    continueTime = 0f;
                    sr.enabled = true;
                }
                else{
                    blinkTime += Time.deltaTime;
                    continueTime += Time.deltaTime;
                }
            }
        }
        
    }


    void FixedUpdate()
    {
        if(!isDown){
            isGround = ground.IsGround();
            isHead = head.IsGround();

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
/// player's attack both Normal Aerial
///</summary>
    private bool PlayerAttack(){
        if(Input.GetKeyDown("return") && !isAttack){
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
                isJump = true;
                if(readytojump){
                    ySpeed = jumpSpeed;
                    jumpPos = transform.position.y;
                    jumpTime = 0.0f;
                }
            }
            else{
                isJump = false;
            }
        }

        // 2段ジャンプ実装は以下
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
<<<<<<< HEAD
                ySpeed += jumpCurve.Evaluate(jumpTime);
=======
                ySpeed *= JumpCurve.Evaluate(jumpTime);
>>>>>>> main
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
        float speed = 5.0f + (float)(SPD / 50);
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

        if(collision.collider.tag == "Enemy"){
            nowHP = nowHP - 10;
        }
        
        if(nowHP <= 0){
            anim.Play("neko_die");
            isDown = true;
            StartCoroutine(PlayerDie());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Sakebigoe"){
            nowHP = nowHP - 20;
            isDamaged = true;   // ダメージを喰らった時無敵時間にするためのフラグ
        }
        if(collision.tag == "tama"){
            nowHP = nowHP - 20;
            isDamaged = true;
        }
        if(collision.tag == "DebidoraFire"){
            nowHP = nowHP - 80;
            isDamaged = true;
        }
        if(collision.tag == "Hoshi"){
            nowHP = nowHP - 40;
            isDamaged = true;
        }
        if(collision.tag == "Tyubi"){
            nowHP = nowHP - 60;
            isDamaged = true;
        }
        if(collision.tag == "Sumi"){
            nowHP = nowHP - 50;
            isDamaged = true;
        }
        if(collision.tag == "DeadZone"){
            nowHP -= nowHP;
        }
        if(collision.tag == "Ninzin"){
            nowHP = nowHP - 70;
            isDamaged = true;
        }
        if(collision.tag == "NinzinExp"){
            nowHP = nowHP - 90;
            isDamaged = true;
        }
        if(collision.tag == "Turara"){
            nowHP = nowHP - 130;
            isDamaged = true;
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

    private IEnumerator AttackCool(){
        isAttackCool = true;
        isAttack = false;

        if(isGround)    // 着地していたらNormalAttack
            anim.SetTrigger("nAttack_neko");
        else if (!isGround) // 空中ならAerialAttack
            anim.SetTrigger("aAttack_neko");
        
        yield return new WaitForSeconds(3.5f);
        Debug.Log("cooltime 3.5s");
        isAttackCool = false;
    }

    IEnumerator PlayerDie()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameOver");
        nowHP = HP;
        yield break;
    }

    // ジャンプのタメを作る
    public void ReadytoJumpT(){
        readytojump = true;
    }
    public void ReadytoJumpF(){
        readytojump = false;
    }


///<summary>
/// status level up
/// </summary>
    public static void HPincrease(int HPplus){
        HP += HPplus;
        nowHP += HPplus;
        Debug.Log("HP level up!! + " + HPplus);
    }
    public static void ATKincrease(int ATKplus){
        ATK += ATKplus;
        Debug.Log("Attack level up!! + " + ATKplus);
    }
    public static void DEFincrease(int DEFplus){
        DEF += DEFplus;
        Debug.Log("Defence level up!! + " + DEFplus);
    }
    public static void SPDincrease(int SPDplus){
        SPD += SPDplus;
        Debug.Log("Speed level up!! + " + SPDplus);
    }
    public static void CRITRATEincrease(int CRplus){
        CRITRATE += CRplus;
        Debug.Log("CriticalRate level up!! + " + CRplus);
    }
    public static void CRITDMGincrease(int CDplus){
        CRITDMG += CDplus;
        Debug.Log("CriticalDamage level up!! + " + CDplus);
    }
}