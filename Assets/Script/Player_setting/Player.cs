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
    private static Transform _playerNowPosition;
    private static Vector2 _playerStartPosition;

    public AudioClip NormalAttackSE;

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

    private PlayerStatusData _playerStatusData;
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

    void Awake()
    {
        _playerStatusData = Resources.Load<PlayerStatusData>("PlayerStatusData");
    }

    void Start()
    {
        string SceneName = SceneManager.GetActiveScene().name;
        if(Regex.IsMatch(SceneName, @"^Stage\d+$", RegexOptions.IgnoreCase))    // ステージのみ
            gameObject.transform.position = _playerStartPosition;                     // 最後に取ったチェックポイントに移動する

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

    private void Update(){
        
        if(!isDown){
            
            // プレイヤーの方向を向く敵 等が参照する
            _playerNowPosition = gameObject.transform;

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
        if(Input.GetKeyDown("return") && !isAttack && !isRestrained){
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
        float speed = 5.0f + (float)(_playerStatusData.SPD / 50);
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
    }

    public void ResetDefaultAnimation(){
        isJump = false;
        isWalk = false;
    }
    
    private void OnCollisionEnter2D(Collision2D collision){
        if(isDamaged) return;

        switch(collision.collider.tag){
            case "TutorialDamage":
                isDamaged = true;
                break;
            case "Enemy":
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                DecrementHP(enemy.EnemyContactDamage());
                break;
            case "Saboten":
                DecrementHP(80);
                break;
        }
            
        checkPlayerDie();
    }

    // 敵と接触しているときに継続ダメージ
    private void OnCollisionStay2D(Collision2D collision){
        if(isDamaged) return;

        if(collision.collider.tag == "Enemy"){
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            DecrementHP(enemy.EnemyContactDamage());
        }
        checkPlayerDie();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(isDamaged) return;
            
        switch(collision.tag){
            case "TutorialDamage":
                isDamaged = true;
                break;
            case "Enemy":
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                DecrementHP(enemy.EnemyContactDamage());
                break;
            case "Sakebigoe":
            case "tama":
                DecrementHP(20);
                break;
            case "Hoshi":
                DecrementHP(40);
                break;
            case "Sumi":
                DecrementHP(50);
                break;
            case "Tyubi":
            case "Kabotya":
                DecrementHP(60);
                break;
            case "Ninzin":
                DecrementHP(70);
                break;
            case "NinzinExp":
                DecrementHP(GameManager.instance.ninzinEXP);
                break;
            case "Turara":
            case "Debidora":
                DecrementHP(130);
                break;
            case "Ivy":
                DecrementHP(150);
                break;
            case "DebidoraFire":
                DecrementHP(180);
                break;
            case "DeadZone":
                DecrementHP(_playerStatusData.nowHP);
                break;
        }

        checkPlayerDie();
    }

    // 当たっている間 継続してダメージを受ける攻撃
    private void OnTriggerStay2D(Collider2D collision){
        if(isDamaged) return;

        switch(collision.tag){
            case "Sakebigoe":
                DecrementHP(20);
                break;
            case "Tyubi":
                DecrementHP(60);
                break;
            case "DebidoraFire":
                DecrementHP(80);
                break;
        }

        checkPlayerDie();
    }

/// <summary>
/// ダメージを受けたとき，プレイヤーが倒れるかチェック
/// </summary>
    private void checkPlayerDie(){
            if(_playerStatusData.nowHP <= 0){
                _playerStatusData.nowHP = 0;  // 表示をマイナスにしないように
                anim.Play("neko_die");
                isDown = true;
                StartCoroutine(PlayerDie());
            }
    }

///<summary>
/// decremant HP
///</summary>
    private void DecrementHP(int damage){
        if(damage - _playerStatusData.DEF <= 0){
            _playerStatusData.nowHP--;        // 敵の攻撃力 < 防御力 のとき1ダメージ
        }
        else{
            _playerStatusData.nowHP = _playerStatusData.nowHP - (damage - _playerStatusData.DEF);
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
        _playerStatusData.nowHP = _playerStatusData.HP;
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

    public Transform PlayerNowPosition{
        get { return _playerNowPosition; }
        set { _playerNowPosition = value; }
    }

    public Vector2 PlayerStartPosition{
        get { return _playerStartPosition; }
        set { _playerStartPosition = value; }
    }

}