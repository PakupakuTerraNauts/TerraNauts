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
        rangeLeft.InitializeCallBack(onKakaoAttack, true);
        rangeRight.InitializeCallBack(onKakaoAttack, false);

        boxcol = GetComponent<BoxCollider2D>();
        boxcol.enabled = true;
    }

    private void onKakaoAttack(bool isLeft){
        if(!isSwoop){
            isSwoop = true;
            anim.Play("kakao_swoop");
        }
        if(isLeft)
            leftRange = isLeft;
    }

    protected override void Moving(){
        if(isSwoop){    // ã}ç~â∫
            if(leftRange)
                transform.localScale = new Vector3(-1, 1, 1);
            isGround = ground.IsGround();
            if(isGround){
                isSwoop = false;
                isAttack = true;
            }
            rb.velocity = new Vector2(0, -Data.gravity);
        }
        else if(isAttack){  // çUåÇ
            second += Time.deltaTime;
            anim.Play("kakao_attack");
            if(leftRange)
                rb.velocity = new Vector2(-speed, 0);
            else
                rb.velocity = new Vector2(speed, 0);
        }

        if(1f < second){
            isAttack = false;
            if(leftRange)
                rb.velocity = new Vector2(-speed/4, Data.gravity);
            else
                rb.velocity = new Vector2(speed/4, Data.gravity);
        }
    }

    protected override void Sleeping(){
        if(0 < second && !isDead){
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        anim.Play("kakao_die");
    }
}
