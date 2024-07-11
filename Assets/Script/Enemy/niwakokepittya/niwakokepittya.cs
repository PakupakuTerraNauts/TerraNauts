using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class niwakokepittya : Enemy
{
    #region //variables
    public tama Tama;

    private float toriPosition_x = 0.0f;

    [SerializeField]
    private Player _player;

    public bool isLeft = true;     // 初期状態 左向き
    private CapsuleCollider2D capcol = null;
    #endregion

    protected override void Initialize(){
        toriPosition_x = transform.position.x;
        if(!isLeft)
            transform.localScale = new Vector3(-1, 1, 1);
        capcol = GetComponent<CapsuleCollider2D>();
        capcol.enabled = true;
    }

    protected override void Moving(){
        anim.Play("tori_pitch");
        rb.velocity = new Vector2(0, -Data.gravity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        Tama.gameObject.SetActive(false);
        anim.Play("tori_die");
    }

/// <summary>
/// playerの方向を判定して そっちを向く アニメーション終了時に呼ぶ
/// </summary>
    public void DirectJudge(){
        Debug.Log(_player.PlayerNowPosition);
        float playerPosition_x = _player.PlayerNowPosition.position.x;
        if(playerPosition_x > toriPosition_x && isLeft){
            transform.localScale = new Vector3(-1, 1, 1);
            isLeft = false;
        }
        else if(playerPosition_x < toriPosition_x && !isLeft){
            transform.localScale = new Vector3(1, 1, 1);
            isLeft = true;
        }
        // 振り向いたときに球の当たり判定が残っていることがあるので非アクティブに変更した
        Tama.gameObject.SetActive(false);
    }

/// <summary>
/// 球を投げる
/// </summary>
    private void release(){
        Tama.gameObject.SetActive(true);
        Tama.TamaPitch(isLeft);
    }
}

