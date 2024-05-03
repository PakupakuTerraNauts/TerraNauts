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
    public static int nowHP = 100;
    public static int HPincrement = 0;
    public int maxJumpCount;    // 増やせば何段でも可
    private int jumpCounter = 0;
    public static Transform playerPos;

    public static int ATK = 100;
    public static int ATKincrement = 0;
    public static int DEF = 0;
    public static int DEFincrement = 0;
    public static int SPD = 100;
    public static int SPDincrement = 0;
    // ↓ Enemy.cs内で使用している
    public static int CRITRATE = 50;
    public static int CRITRATEincrement = 0;
    public static int CRITDMG = 50;
    public static int CRITDMGincrement = 0;

    public float attackCooltime;
    private float jumpPos = 0.0f;
    private float jumpPos2 = 0.0f;
    private float jumpTime = 0.0f;
    private float continueTime = 0.0f;
    private float blinkTime = 0.0f;
    //private float invincibleTime = 0.0f;
    private bool isGround = false;
    private bool isJump = false;
    private bool isWalk = false;
    private bool isHead = false;
    private bool isDown = false;
    private bool isAttack = false;
    private bool isAttackCool = false;
    //private bool isContinue = false;
    private bool isDamaged = false;
    private bool readytojump = false;
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private CapsuleCollider2D capcol = null;
    private SpriteRenderer sr = null;
    private GameObject cooltimemaker;

    public AnimationCurve JumpupCurve;

    public PlayerFoodManager _playerFoodManager;
    #endregion

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capcol = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        cooltimemaker = transform.Find("cooltime").gameObject;
    }


    private void Update(){

        playerPos = gameObject.transform;
        if(!isDown){
            
            GetInputTwoJump();
            isAttack = PlayerAttack();

            // 攻撃アニメーション→コルーチンへ
            if(isAttack && !isAttackCool){
                StartCoroutine(AttackCool());
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

        if(isGround){   // 地面にいるとき
            jumpCounter = 0;

        // 着地したらズームイン
        //if(Camera.main.orthographicSize != 5f && StageCamera.Instance != null){
          //  StageCamera.JumpZoomIn(0.3f);
        //}あると酔うかも
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
        // ジャンプ中
        else if(isJump){

            bool pushUpKey = false;
            if(verticalKey > 0 || wKey || upKey){
                pushUpKey = true;
                if(jumpCounter < 1)
                    jumpCounter++;
            }

            bool canHeight = jumpPos + jumpHeight > transform.position.y;
            bool canTime = jumpLimitTime > jumpTime;
            if(jumpCounter > 1){
                canHeight = jumpPos2 + jumpHeight > transform.position.y;
            }
            
            if(pushUpKey && canHeight && canTime && !isHead){
                // ジャンプでズームアウト
                if(Camera.main.orthographicSize != 8f && StageCamera.Instance != null){
                    StageCamera.JumpZoomOut(0.5f);
                }
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
                ySpeed *= JumpupCurve.Evaluate(jumpTime);
            }
            else{
                isJump = false;
                jumpTime = 0.0f;
                // 落下でズームイン
                if(Camera.main.orthographicSize != 5f && StageCamera.Instance != null){
                    StageCamera.JumpZoomIn(0.3f);
                }
            }
        }

        return ySpeed;
    }

    private void GetInputTwoJump(){
        // 2段ジャンプの押下を取得
        if(!isGround){
            if((Input.GetKeyDown("up") || Input.GetKeyDown("w")) && jumpCounter < maxJumpCount){   // カウンターが現在のジャンプ回数
                anim.Play("neko_jump_2dan");
                jumpCounter++;
                isJump = true;
                jumpPos2 = transform.position.y;
                jumpTime = 0.0f;
            }
        }
    }


///<summary>
/// calculate X conponent, return speed.
///</summary>
    private float GetXSpeed(){
        float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;
        float speed = 5.0f + (float)((SPD + SPDincrement) / 50);
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
        //isContinue = true;
    }
    
    private void OnCollisionEnter2D(Collision2D collision){

        if(!isDamaged){
            if(collision.collider.tag == "TutorialDamage"){
                isDamaged = true;
            }
            if(collision.collider.tag == "Enemy"){
                DecrementHP(10);
            }
            if(collision.collider.tag == "Saboten"){
                DecrementHP(80);
            }
            
            if(nowHP <= 0){
                nowHP = 0;
                anim.Play("neko_die");
                isDown = true;
                StartCoroutine(PlayerDie());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(!isDamaged){
            if(collision.tag == "TutorialDamage"){
                isDamaged = true;
            }
            if(collision.tag == "Enemy"){
                DecrementHP(10);
            }
            if(collision.tag == "Sakebigoe" || collision.tag == "tama"){
                DecrementHP(20);   // ダメージを喰らった時無敵時間にするためのフラグ
            }
            if(collision.tag == "Hoshi"){
                DecrementHP(40);
            }
            if(collision.tag == "Sumi"){
                DecrementHP(50);
            }
            if(collision.tag == "Tyubi" || collision.tag == "Kabotya"){
                DecrementHP(60);
            }
            if(collision.tag == "Ninzin"){
                DecrementHP(70);
            }
            if(collision.tag == "NinzinExp"){
                DecrementHP(90);
            }
            if(collision.tag == "Turara" || collision.tag == "Debidora"){
                DecrementHP(130);
            }
            if(collision.tag == "DebidoraFire"){
                DecrementHP(180);
            }
            if(collision.tag == "DeadZone"){
                DecrementHP(nowHP);
            }

            // 無敵時間の間は死んでも動けるかも.
            if(nowHP <= 0){
                nowHP = 0;
                anim.Play("neko_die");
                isDown = true;
                StartCoroutine(PlayerDie());
            }
        }
    }

    // 当たっている間 継続してダメージを受ける攻撃
    private void OnTriggerStay2D(Collider2D collision){
        if(!isDamaged){
            if(collision.tag == "Sakebigoe"){
                DecrementHP(20);
            }
            if(collision.tag == "Tyubi"){
                DecrementHP(60);
            }
            if(collision.tag == "DebidoraFire"){
                DecrementHP(80);
            }
            
            if(nowHP <= 0){
                nowHP = 0;
                anim.Play("neko_die");
                isDown = true;
                StartCoroutine(PlayerDie());
            }
        }
    }

    private void DecrementHP(int damage){
        if(damage - (DEF + DEFincrement) < 0){
            nowHP--;        // 敵の攻撃力 < 防御力 のとき1ダメージ
        }
        else{
            nowHP = nowHP - (damage - (DEF + DEFincrement));
        }
        isDamaged = true;
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
        cooltimemaker.SetActive(false);
        isAttackCool = true;
        isAttack = false;

        if(isGround)    // 着地していたらNormalAttack
            anim.SetTrigger("nAttack_neko");
        else if (!isGround) // 空中ならAerialAttack
            anim.SetTrigger("aAttack_neko");
        
        yield return new WaitForSeconds(attackCooltime);  //クールタイム
        Debug.Log("cooltime " + attackCooltime + "s");
        cooltimemaker.SetActive(true);
        isAttackCool = false;
    }

    IEnumerator PlayerDie()
    {
        _playerFoodManager.ApplySavedItemList();
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
        HPincrement += HPplus;
        Debug.Log("HP level up!! + " + HPplus);
    }
    public static void ATKincrease(int ATKplus){
        ATKincrement += ATKplus;
        Debug.Log("Attack level up!! + " + ATKplus);
    }
    public static void DEFincrease(int DEFplus){
        DEFincrement += DEFplus;
        Debug.Log("Defence level up!! + " + DEFplus);
    }
    public static void SPDincrease(int SPDplus){
        SPDincrement += SPDplus;
        Debug.Log("Speed level up!! + " + SPDplus);
    }
    public static void CRITRATEincrease(int CRplus){
        CRITRATEincrement += CRplus;
        Debug.Log("CriticalRate level up!! + " + CRplus);
    }
    public static void CRITDMGincrease(int CDplus){
        CRITDMGincrement += CDplus;
        Debug.Log("CriticalDamage level up!! + " + CDplus);
    }
}