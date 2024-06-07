using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kakaoRange : MonoBehaviour
{
    #region // variables
    private bool leftRange = false;
    private BoxCollider2D boxcol = null;

    public delegate void kakaoAttack(bool isLeft);
    private kakaoAttack kakaoAttackCallBack;
    #endregion

    void Start(){
        boxcol = GetComponent<BoxCollider2D>();
    }

/// <summary>
/// 攻撃に移ったらrangeは消す
/// </summary>
    public void InvalidColliderAfterAttack(){
        boxcol.enabled = false;
    }

/// <summary>
/// onKakaoAttack コールバックをセット
/// </summary>
/// <param name="onKakaoAttack">攻撃</param>
/// <param name="isLeft">左右どっちに攻撃するかTFで返す</param>
    public void InitializeCallBack(kakaoAttack onKakaoAttack, bool isLeft){
        kakaoAttackCallBack = onKakaoAttack;
        if(isLeft)
            leftRange = true;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            kakaoAttackCallBack(leftRange);
        }
    }
}
