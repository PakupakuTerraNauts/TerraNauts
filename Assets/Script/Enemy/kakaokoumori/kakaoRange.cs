using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kakaoRange : MonoBehaviour
{
    #region // variables
    private BoxCollider2D boxcol = null;

    public delegate void kakaoAttack();
    private kakaoAttack kakaoAttackCallBack;
    #endregion

    void Start(){
        boxcol = GetComponent<BoxCollider2D>();
    }

    public void InitializeCallBack(kakaoAttack onKakaoAttack){
        kakaoAttackCallBack = onKakaoAttack;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            kakaoAttackCallBack();
        }
    }
}
