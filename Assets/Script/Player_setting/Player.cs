using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public groundCheck ground;
    public groundCheck head;
    public HPBar HP;
    public float speed = 3;
    public float gravity = 2;
    private float jumpSpeed = 6;
    private float jumpHeight = 2;
    private float jumpLimitTime = 3;

    private float jumpPos = 0.0f;
    private float jumpTime = 0.0f;
    private float continueTime = 0.0f;
    private float blinkTime = 0.0f;
    private bool isGround = false;
    private bool isJump = false;
    private bool isWalk = false;
    private bool isHead = false;
    private bool isDown = false;
    //private bool isNAttack = false;
    //private bool isAAttack = false;
    private bool isAttack = false;
    private bool isContinue = false;
    //private bool nonDownAnim = false;
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private CapsuleCollider2D capcol = null;
    private SpriteRenderer sr = null; 
    private string enemyTag = "Enemy";
    private string sakebigoe = "Sakebigoe";
    
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

            if(isAttack){
                if(isGround)
                    anim.SetTrigger("nAttack_neko");
                else if (!isGround)
                    anim.SetTrigger("aAttack_neko");
            }
            isAttack = false;
            }
        

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

    #region//damage
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.tag == enemyTag){
            HP.UpdateHP(10.0f);         //contact damage
        }

        if(collision.collider.tag == sakebigoe){
            HP.UpdateHP(20.0f);         //daikonmandoragora's voice damage
        }
    }
    #endregion

///<summary>
/// player's normal attack
///</summary>
    private bool PlayerAttack(){
        if(Input.GetKey("return") && !anim.IsInTransition(0)){
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

///<summary>
/// animation
///</summary>
    private void SetAnimation(){
        anim.SetBool("jump_neko", isJump);
        anim.SetBool("ground_neko", isGround);
        anim.SetBool("walk_neko", isWalk);
    }
}
/*
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
            HP = HP - 10;
        }
        if(collision.collider.tag == sakebigoe){
            HP = HP - 20;
        }
        
        if(HP <= 0){
            anim.Play("neko_die");
            isDown = true;
        }
    }

}
*/





























/*



/// <summary>
/// コンティニュー待機状態か
/// </summary>
/// <returns></returns>
public bool IsContinueWaiting()
{
    if (GM.instance.isGameOver)
    {
        return false;
    }
    else
    {
        return IsDownAnimEnd() || nonDownAnim;
    }
}

//ダウンアニメーションが完了しているかどうか
private bool IsDownAnimEnd()
{
    if (isDown && anim != null)
    {
        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
        if (currentState.IsName("player_die"))
        {
            if (currentState.normalizedTime >= 1)
            {
                return true;
            }
        }
    }
    return false;
}

/// <summary>
/// コンティニューする
/// </summary>
public void ContinuePlayer()
{
    GM.instance.PlaySE(continueSE);
    isDown = false;
    anim.Play("player_default");
    isJump = false;
    isOtherJump = false;
    isRun = false;
    isContinue = true;
    nonDownAnim = false;
}

//やられた時の処理
private void ReceiveDamage(bool downAnim)
{
    if (isDown || GM.instance.isStageClear)
    {
        return;
    }
    else
    {
        if (downAnim)
        {
            anim.Play("player_die");
        }
        else
        {
            nonDownAnim = true;
        }
        isDown = true;
        GM.instance.PlaySE(downSE);
        GM.instance.SubHeartNum();
    }
}

#region//接触判定
private void OnCollisionEnter2D(Collision2D collision)
{
    bool enemy = (collision.collider.tag == enemyTag);
    bool moveFloor = (collision.collider.tag == moveFloorTag);
    bool fallFloor = (collision.collider.tag == fallFloorTag);

    if (enemy || moveFloor || fallFloor)
    {
        //踏みつけ判定になる高さ
        float stepOnHeight = (capcol.size.y * (stepOnRate / 100f));

        //踏みつけ判定のワールド座標
        float judgePos = transform.position.y - (capcol.size.y / 2f) + stepOnHeight;

        foreach (ContactPoint2D p in collision.contacts)
        {
            if (p.point.y < judgePos)
            {
                if (enemy || fallFloor)
                {
                    ObjectCollision o = collision.gameObject.GetComponent<ObjectCollision>();
                    if (o != null)
                    {
                        if (enemy)
                        {
                            otherJumpHeight = o.boundHeight;    //踏んづけたものから跳ねる高さを取得する
                            o.playerStepOn = true;        //踏んづけたものに対して踏んづけた事を通知する
                            jumpPos = transform.position.y; //ジャンプした位置を記録する
                            isOtherJump = true;
                            isJump = false;
                            jumpTime = 0.0f;
            }
            else if(fallFloor)
            {
                            o.playerStepOn = true;
            }
                    }
                    else
                    {
                        Debug.Log("ObjectCollisionが付いてないよ!");
                    }
        }
        else if(moveFloor)
        {
                    moveObj = collision.gameObject.GetComponent<MoveObject>();
                }
            }
            else
            {
                if (enemy)
                {
                    ReceiveDamage(true);
                    break;
                }
            }
        }
    }
}

private void OnCollisionExit2D(Collision2D collision)
{
    if (collision.collider.tag == moveFloorTag)
    {
        //動く床から離れた
        moveObj = null;
    }
}

private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.tag == deadAreaTag)
    {
        ReceiveDamage(false);
    }
    else if (collision.tag == hitAreaTag)
    {
        ReceiveDamage(true);
    }
}
#endregion
}
*/