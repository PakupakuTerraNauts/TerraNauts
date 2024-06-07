using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sisaRange : MonoBehaviour
{
    #region // variables
    private BoxCollider2D boxcol = null;

    public delegate void sisaAttack();
    private sisaAttack sisaAttackCallBack;
    #endregion

    void Start(){
        boxcol = GetComponent<BoxCollider2D>();
    }

/// <summary>
/// onSisaAttack �R�[���o�b�N���Z�b�g
/// </summary>
/// <param name="onSisaAttack">�p�C���V�[�T�[ �U��</param>
    public void InitializeCallBack(sisaAttack onSisaAttack){
        sisaAttackCallBack = onSisaAttack;
    }

    // �������Ƃ� ��������Ƃ��ɍU��������
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            sisaAttackCallBack();
        }
    }
    private void OnTriggerStay2D(Collider2D collision){
        if(collision.tag == "Player"){
            sisaAttackCallBack();
        }
    }
}
