using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ivyRange : MonoBehaviour
{
    #region // variables
    private BoxCollider2D boxcol = null;

    public delegate void ivyAttack();
    private ivyAttack ivyAttackCallBack;
    #endregion

    void Awake(){
        boxcol = GetComponent<BoxCollider2D>(); //ivy.Start ���� InitializeRange ���ĂԂ��� Awake
    }

/// <summary>
/// onIvyAttack �R�[���o�b�N���Z�b�g
/// </summary>
/// <param name="onIvyAttack">�c�^�U��</param>
    public void InitializeCallBack(ivyAttack onIvyAttack){
        ivyAttackCallBack = onIvyAttack;
    }

/// <summary>
/// undulating��bending�̂Ƃ���range��傫������
/// </summary>
    public void InitializeRange(){
        boxcol.size = new Vector2(3f, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            ivyAttackCallBack();
        }
    }

    private void OnTriggerStay2D(Collider2D collision){
        if(collision.tag == "Player"){
            ivyAttackCallBack();
        }
    }
}
