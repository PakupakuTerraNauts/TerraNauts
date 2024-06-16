using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radishRange : MonoBehaviour
{
    #region // variables
    private BoxCollider2D boxcol = null;

    public delegate void radishStepped();
    private radishStepped radishStappedCallBack;
    #endregion

    void Start(){
        boxcol = GetComponent<BoxCollider2D>();
    }

/// <summary>
/// onRadisshStapped �R�[���o�b�N���Z�b�g
/// </summary>
/// <param name="onRadishStapped">�v���C���[���߂Â����Ƃ� �卪���n�ʂ���o��</param>
    public void InitializeCallBack(radishStepped onRadishStapped){
        radishStappedCallBack = onRadishStapped;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player")   // �v���C���[�ƐڐG������R�[���o�b�N����
            radishStappedCallBack();
    }
}
