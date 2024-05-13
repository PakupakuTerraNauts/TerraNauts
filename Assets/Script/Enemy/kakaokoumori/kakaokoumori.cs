using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kakaokoumori : Enemy
{
    #region // variables
    private bool isAttack = false;

    private BoxCollider2D boxcol = null;

    [SerializeField] private kakaoRange range;
    [SerializeField] private groundCheck ground;
    #endregion

    protected override void Initialize(){
        range.InitializeCallBack(onKakaoAttack);
        boxcol = GetComponent<BoxCollider2D>();
        boxcol.enabled = true;
    }

    private void onKakaoAttack(){
        
    }

    protected override void Moving(){
        rb.velocity = new Vector2(0, gravity);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        anim.Play("kakao_die");
    }
}
