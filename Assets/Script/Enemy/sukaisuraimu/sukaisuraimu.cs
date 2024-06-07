using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sukaisuraimu : Enemy
{
    #region // variables
    private BoxCollider2D boxcol = null;
    public float speed;

    private bool isFall = false;

    [Header ("にんじん")] public ninzin Ninzin;
    private enum State{
        Wait,
        Move,
        Attack
    }
    private State nowState = State.Wait;
    private State nextState = State.Wait;
    #endregion

    protected override void Initialize(){
        boxcol = GetComponent<BoxCollider2D>();
        Ninzin.InitializeCallBack(onNinzinFallenCheck); // コールバック
    }

    protected override void Moving(){
        //anim.Play("sukai_default");

        switch(nowState)
        {
            case State.Wait:
                WaitUpdate();
                break;
            case State.Move:
                MoveUpdate();
                break;
            case State.Attack:
                AttackUpdate();
                break;
        }

        nowState = nextState;
    }

/// <summary>
/// 状態遷移
/// </summary>
    private void ChangeState(State next){
        nextState = next;
    }

/// <summary>
/// 待機→移動
/// </summary>
    private void WaitUpdate(){
        if(!isFall){
            ChangeState(State.Move);
        }
        return;
    }

/// <summary>
/// 移動→攻撃
/// </summary>
    private void MoveUpdate(){
        if(!Ninzin.gameObject.activeSelf)
            Ninzin.gameObject.SetActive(true);  // 爆発→手元に戻る の間,無効化しておく

        // 人参に位置を渡して追従させる
        Ninzin.Reload(this.transform.position);

        if(Player.playerPos.position.x < transform.position.x){
            transform.localScale = new Vector3(1, 1, 1);
            rb.velocity = new Vector2(-speed, 0.0f);
        }
        else{
            transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = new Vector2(speed, 0.0f);
        }
        if(Mathf.Abs(this.transform.position.x - Player.playerPos.position.x) < 3.0f){
            ChangeState(State.Attack);
            return;
        }
    }

/// <summary>
/// 攻撃→移動
/// </summary>
    private void AttackUpdate(){
        Ninzin.Falling();
        isFall = true;
        
        ChangeState(State.Wait);
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        anim.Play("sukai_die");
        boxcol.tag = "DeadEnemy";
    }

/// <summary>
/// 人参の爆発を取得
/// </summary>
    private void onNinzinFallenCheck(){
        isFall = false;
    }
}
