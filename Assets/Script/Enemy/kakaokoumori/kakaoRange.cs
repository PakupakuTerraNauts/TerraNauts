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

    public void InvalidColliderAfterAttack(){
        boxcol.enabled = false;
    }

    public void InitializeCallBack(kakaoAttack onKakaoAttack, bool isLeft){
        kakaoAttackCallBack = onKakaoAttack;
        if(isLeft)      // ç∂âEÇ«ÇøÇÁÇ…îÚÇ◊ÇŒÇÊÇ¢Ç© TF Ç≈ï‘Ç∑
            leftRange = true;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            kakaoAttackCallBack(leftRange);
        }
    }
}
