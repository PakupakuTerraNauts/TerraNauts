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
/// �U���Ɉڂ�����range�͏���
/// </summary>
    public void InvalidColliderAfterAttack(){
        boxcol.enabled = false;
    }

/// <summary>
/// onKakaoAttack �R�[���o�b�N���Z�b�g
/// </summary>
/// <param name="onKakaoAttack">�U��</param>
/// <param name="isLeft">���E�ǂ����ɍU�����邩TF�ŕԂ�</param>
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
