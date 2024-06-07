using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radisshRange : MonoBehaviour
{
    #region // variables
    private BoxCollider2D boxcol = null;

    public delegate void radisshStepped();
    private radisshStepped radisshStappedCallBack;
    #endregion

    void Start(){
        boxcol = GetComponent<BoxCollider2D>();
    }

/// <summary>
/// onRadisshStapped �R�[���o�b�N���Z�b�g
/// </summary>
/// <param name="onRadisshStapped">�v���C���[���߂Â����Ƃ� �卪���n�ʂ���o��</param>
    public void InitializeCallBack(radisshStepped onRadisshStapped){
        radisshStappedCallBack = onRadisshStapped;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player")   // �v���C���[�ƐڐG������R�[���o�b�N����
            radisshStappedCallBack();
    }
}
