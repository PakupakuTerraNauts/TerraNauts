using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class Player : MonoBehaviour
{
    #region // variables
    public groundCheck ground;
    public groundCheck head;
    public float gravity;
    private float jumpSpeed = 6.0f;
    public float jumpHeight;
    private float jumpLimitTime = 1.5f;
    public static bool isRestrained = false;
    public static Transform playerPos;
    public static Vector2 playerStartPos;

    public AudioClip NormalAttackSE;
    
        #region // status
        public static int HP = 100;
        public static int nowHP = 100;
        public static int HPincrement = 0;

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
        #endregion

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
    public static bool viewLock = false;
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private CapsuleCollider2D capcol = null;
    private SpriteRenderer sr = null;
    private GameObject cooltimemaker;

    public AnimationCurve JumpupCurve;

    public PlayerFoodManager _playerFoodManager;
    public ParallaxBackground backGround;

        #region // skills
        // ジャンプ回数
        public int maxJumpCount;    // 増やせば何段でも可
        private int jumpCounter = 0;
        // 視野
        public float maxVision;
        [SerializeField] private ZoomCamera vision;
        #endregion
    #endregion

    void Start()
    {
        string SceneName = SceneManager.GetActiveScene().name;
        if(Regex.IsMatch(SceneName, @"^Stage\d+$", RegexOptions.IgnoreCase))    // ステージのみ
            gameObject.transform.position = playerStartPos;                     // 最後に取ったチェックポイントに移動する

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capcol = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        cooltimemaker = transform.Find("cooltime").gameObject;

        if(viewLock){
            if(Camera.main.orthographicSize != maxVision && vision != null)
                vision.JumpZoomOut(0.5f, maxVision);
        }
    }

/// <summary>
/// ステータスをリセット
/// </summary>
    public static void InitializePlayerStatus(){
        HP = 100;
        nowHP = 100;
        HPincrement = 0;

        ATK = 100;
        ATKincrement = 0;
        DEF = 0;
        DEFincrement = 0;
        SPD = 100;
        SPDincrement = 0;
        CRITRATE = 50;
        CRITRATEincrement = 0;
        CRITDMG = 50;
        CRITDMGincrement = 0;
    }

    private void Update(){

        if(!isDown){
            
            // プレイヤーの方向を向く敵 等が参照する
            playerPos = gameObject.transform;

            GetInputTwoJump();
            isAttack = PlayerAttack();

            // 攻撃アニメーション→コルーチンへ
            if(isAttack && !isAttackCool){
                StartCoroutine(AttackCool());
            }

            bool vKey = Input.GetKeyDown("v");
            if(vKey)
                SwitchViewLock();

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
        if(!isDown && !isRestrained){
            isGround = ground.IsGround();
            isHead = head.IsGround();

            float xSpeed = GetXSpeed();
            float ySpeed = GetYSpeed();
            
            rb.velocity = new Vector2(xSpeed, ySpeed);
        }
        else{
            rb.velocity = new Vector2(0, -gravity);
        }

        if(isRestrained){
            ResetDefaultAnimation();
        }
 
        SetAnimation();
    }

    private void SwitchViewLock(){
        viewLock = !viewLock;   // 視界を固定するかどうか
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

            if(verticalKey > 0 || wKey || upKey){
                isJump = true;
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y;
                jumpTime = 0.0f;
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
                if(Camera.main.orthographicSize != maxVision && !viewLock && vision != null){
                    vision.JumpZoomOut(0.5f, maxVision);    // ズームのスピード, 視野の大きさ
                }
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
                ySpeed *= JumpupCurve.Evaluate(jumpTime);
            }
            else{
                isJump = false;
                jumpTime = 0.0f;
                // 落下でズームイン
                if(Camera.main.orthographicSize != 5f && !viewLock && vision != null){
                    vision.JumpZoomIn(0.3f);    // ズームインは少し速い
                }
            }
        }

        return ySpeed;
    }

///<summary>
/// get input of 2ndJump
/// </summary>
    private void GetInputTwoJump(){
        if(!isGround){
            if((Input.GetKeyDown("up") || Input.GetKeyDown("w")) && jumpCounter < maxJumpCount){   // カウンターが現在のジャンプ回数
                anim.Play("neko_jump_2dan");
                jumpCounter++;
                isJump = true;
                jumpPos2 = transform.position.y;    // canHightを更新するため 空中に高さの基準を取り直す
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
            if(backGround != null){  // backGroundがあるのはStageのみ
                backGround.StartScroll(transform.position); // 背景のスクロール
            }
        }
        else if(horizontalKey < 0 || leftKey || aKey){
            transform.localScale = new Vector3(-2, 2, 2);
            isWalk = true;
            xSpeed = -speed;
            if(backGround != null){
                backGround.StartScroll(transform.position);
            }
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
        ResetDefaultAnimation();
        //isContinue = true;
    }

    public void ResetDefaultAnimation(){
        isJump = false;
        isWalk = false;
    }
    
    private void OnCollisionEnter2D(Collision2D collision){

        if(!isDamaged){
            if(collision.collider.tag == "TutorialDamage")
                isDamaged = true;
            if(collision.collider.tag == "Enemy")
                DecrementHP(10);
            if(collision.collider.tag == "Saboten")
                DecrementHP(80);
            
            checkPlayerDie();
        }
    }

    // 敵と接触しているときに継続ダメージ
    private void OnCollisionStay2D(Collision2D collision){
        if(!isDamaged){
            if(collision.collider.tag == "Enemy")
                DecrementHP(10);
            checkPlayerDie();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(!isDamaged){
            if(collision.tag == "TutorialDamage")
                isDamaged = true;
            if(collision.tag == "Enemy")
                DecrementHP(10);
            if(collision.tag == "Sakebigoe" || collision.tag == "tama")
                DecrementHP(20);
            if(collision.tag == "Hoshi")
                DecrementHP(40);
            if(collision.tag == "Sumi")
                DecrementHP(50);
            if(collision.tag == "Tyubi" || collision.tag == "Kabotya")
                DecrementHP(60);
            if(collision.tag == "Ninzin")
                DecrementHP(70);
            if(collision.tag == "NinzinExp")
                DecrementHP(GameManager.instance.ninzinEXP);
            if(collision.tag == "Turara" || collision.tag == "Debidora")
               DecrementHP(130);
            if(collision.tag == "Ivy")
                DecrementHP(150);
            if(collision.tag == "DebidoraFire")
                DecrementHP(180);
            if(collision.tag == "DeadZone")
                DecrementHP(nowHP);

            checkPlayerDie();
        }
    }

    // 当たっている間 継続してダメージを受ける攻撃
    private void OnTriggerStay2D(Collider2D collision){
        if(!isDamaged){
            if(collision.tag == "Sakebigoe")
                DecrementHP(20);
            if(collision.tag == "Tyubi")
                DecrementHP(60);
            if(collision.tag == "DebidoraFire")
                DecrementHP(80);
            
            checkPlayerDie();
        }
    }

/// <summary>
/// ダメージを受けたとき，プレイヤーが倒れるかチェック
/// </summary>
    private void checkPlayerDie(){
            if(nowHP <= 0){
                nowHP = 0;  // マイナスにしない
                anim.Play("neko_die");
                isDown = true;
                StartCoroutine(PlayerDie());
            }
    }

///<summary>
/// decremant HP
///</summary>
    private void DecrementHP(float damage){
        if(damage - (DEF + DEFincrement) <= 0){
            nowHP--;        // 敵の攻撃力 < 防御力 のとき1ダメージ
        }
        else{
            nowHP = nowHP - ((int)damage - (DEF + DEFincrement));
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

        if(isGround){    // 着地していたらNormalAttack
            anim.SetTrigger("nAttack_neko");
        }
        else if (!isGround){ // 空中ならAerialAttack
            anim.SetTrigger("aAttack_neko");
        }
            GameManager.instance.PlaySE(NormalAttackSE);
        
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
///<summary>
/// player rastrained
///</summary>
///<remarks> ボスのHPカウントアップ等 イベント時に移動を制限したいときに呼ぶ </remarks>
    public static void RestrainedByEvent(){
        isRestrained = true;
    }
    public static void UnRestrainedByEvent(){
        isRestrained = false;
    }

///<summary>
/// status level up
///</summary>
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