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
        // �� Enemy.cs���Ŏg�p���Ă���
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
        // �W�����v��
        public int maxJumpCount;    // ���₹�Ή��i�ł���
        private int jumpCounter = 0;
        // ����
        public float maxVision;
        [SerializeField] private ZoomCamera vision;
        #endregion
    #endregion

    void Start()
    {
        string SceneName = SceneManager.GetActiveScene().name;
        if(Regex.IsMatch(SceneName, @"^Stage\d+$", RegexOptions.IgnoreCase))    // �X�e�[�W�̂�
            gameObject.transform.position = playerStartPos;                     // �Ō�Ɏ�����`�F�b�N�|�C���g�Ɉړ�����

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
/// �X�e�[�^�X�����Z�b�g
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
            
            // �v���C���[�̕����������G �����Q�Ƃ���
            playerPos = gameObject.transform;

            GetInputTwoJump();
            isAttack = PlayerAttack();

            // �U���A�j���[�V�������R���[�`����
            if(isAttack && !isAttackCool){
                StartCoroutine(AttackCool());
            }

            bool vKey = Input.GetKeyDown("v");
            if(vKey)
                SwitchViewLock();

            // �_���[�W���󂯂�����͖��G����
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
        viewLock = !viewLock;   // ���E���Œ肷�邩�ǂ���
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

        if(isGround){   // �n�ʂɂ���Ƃ�
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
        // �W�����v��
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
                // �W�����v�ŃY�[���A�E�g
                if(Camera.main.orthographicSize != maxVision && !viewLock && vision != null){
                    vision.JumpZoomOut(0.5f, maxVision);    // �Y�[���̃X�s�[�h, ����̑傫��
                }
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
                ySpeed *= JumpupCurve.Evaluate(jumpTime);
            }
            else{
                isJump = false;
                jumpTime = 0.0f;
                // �����ŃY�[���C��
                if(Camera.main.orthographicSize != 5f && !viewLock && vision != null){
                    vision.JumpZoomIn(0.3f);    // �Y�[���C���͏�������
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
            if((Input.GetKeyDown("up") || Input.GetKeyDown("w")) && jumpCounter < maxJumpCount){   // �J�E���^�[�����݂̃W�����v��
                anim.Play("neko_jump_2dan");
                jumpCounter++;
                isJump = true;
                jumpPos2 = transform.position.y;    // canHight���X�V���邽�� �󒆂ɍ����̊����蒼��
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
            if(backGround != null){  // backGround������̂�Stage�̂�
                backGround.StartScroll(transform.position); // �w�i�̃X�N���[��
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

    // �G�ƐڐG���Ă���Ƃ��Ɍp���_���[�W
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

    // �������Ă���� �p�����ă_���[�W���󂯂�U��
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
/// �_���[�W���󂯂��Ƃ��C�v���C���[���|��邩�`�F�b�N
/// </summary>
    private void checkPlayerDie(){
            if(nowHP <= 0){
                nowHP = 0;  // �}�C�i�X�ɂ��Ȃ�
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
            nowHP--;        // �G�̍U���� < �h��� �̂Ƃ�1�_���[�W
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

        if(isGround){    // ���n���Ă�����NormalAttack
            anim.SetTrigger("nAttack_neko");
        }
        else if (!isGround){ // �󒆂Ȃ�AerialAttack
            anim.SetTrigger("aAttack_neko");
        }
            GameManager.instance.PlaySE(NormalAttackSE);
        
        yield return new WaitForSeconds(attackCooltime);  //�N�[���^�C��
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
///<remarks> �{�X��HP�J�E���g�A�b�v�� �C�x���g���Ɉړ��𐧌��������Ƃ��ɌĂ� </remarks>
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