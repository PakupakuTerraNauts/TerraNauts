using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kakaokoumori : Enemy
{
    #region // variables
    private bool isSwoop = false;
    private bool isAttack = false;
    private bool isGround = false;
    private bool leftRange = false;

    private float second = 0f;
    [SerializeField] private float speed;

    private BoxCollider2D boxcol = null;

    [SerializeField] private kakaoRange rangeLeft;
    [SerializeField] private kakaoRange rangeRight;
    [SerializeField] private groundCheck ground;
    #endregion

    protected override void Initialize(){
        rangeLeft.InitializeCallBack(onKakaoAttack, true); // コールバック 左
        rangeRight.InitializeCallBack(onKakaoAttack, false); // 右

        boxcol = GetComponent<BoxCollider2D>();
        boxcol.enabled = true;
    }

/// <summary>
/// 攻撃
/// </summary>
/// <param name="isLeft">左右どっちから呼ばれたか</param>
    private void onKakaoAttack(bool isLeft){
        if(!isSwoop){
            isSwoop = true;
            anim.Play("kakao_swoop");
            rangeLeft.InvalidColliderAfterAttack();
            rangeRight.InvalidColliderAfterAttack();
        }
        if(isLeft)
            leftRange = isLeft;
    }

    protected override void Moving(){
        if(isSwoop){    // 急降下
            if(leftRange)
                transform.localScale = new Vector3(-1, 1, 1);
            isGround = ground.IsGround();
            if(isGround){
                isSwoop = false;
                isAttack = true;
            }
            rb.velocity = new Vector2(0, -Data.gravity);
        }
        else if(isAttack){  // 攻撃
            second += Time.deltaTime;
            anim.Play("kakao_attack");
            if(leftRange)
                rb.velocity = new Vector2(-speed, 0);
            else
                rb.velocity = new Vector2(speed, 0);
        }

        if(1f < second){    // 攻撃終わり 
            isAttack = false;
            if(leftRange)
                rb.velocity = new Vector2(-speed/4, Data.gravity);
            else
                rb.velocity = new Vector2(speed/4, Data.gravity);
        }
    }

    protected override void Sleeping(){
        if(0 < second && !isDead){
            gameObject.SetActive(false);    // 画面外に出たとき既に攻撃が始まっていたら second<1 でも強制終了
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        if(isGround)
            anim.Play("kakao_flydie");
        else
            anim.Play("kakao_die");
    }
}
