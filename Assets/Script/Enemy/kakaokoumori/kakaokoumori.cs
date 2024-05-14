using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kakaokoumori : Enemy
{
    #region // variables
    private bool isSwoop = false;
    private bool isAttack = false;
    private bool isGround = false;

    [SerializeField] private float speed;

    private BoxCollider2D boxcol = null;

    [SerializeField] private kakaoRange rangeLeft;
    [SerializeField] private kakaoRange rangeRight;
    [SerializeField] private groundCheck ground;
    #endregion

    protected override void Initialize(){
        rangeLeft.InitializeCallBack(onKakaoAttack);
        rangeRight.InitializeCallBack(onKakaoAttack);

        boxcol = GetComponent<BoxCollider2D>();
        boxcol.enabled = true;
    }

    private void onKakaoAttack(){
        if(!isSwoop){
            isSwoop = true;
            anim.Play("kakao_swoop");
        }
    }

    protected override void Moving(){
        if(isSwoop){    // ã}ç~â∫
            isGround = ground.IsGround();
            if(isGround){
                isSwoop = false;
                isAttack = true;
            }
            rb.velocity = new Vector2(0, -Data.gravity);
        }
        else if(isAttack){  // çUåÇ
            anim.Play("kakao_attack");
            rb.velocity = new Vector2(speed, 0);
        }

        if(isAttack && !sr.isVisible){
            isAttack = false;
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
