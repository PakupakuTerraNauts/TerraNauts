using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butamogura : Enemy
{
    #region // variables
    public float speed;

    private bool isEndAnim = true;
    private bool isAttack = false;

    private CircleCollider2D circol = null;
    // 状態
    private enum State{
        inGround,
        Move,
        Attack
    }
    private State nowState = State.inGround;
    private State nextState = State.inGround;
    #endregion

    protected override void Initialize(){
        circol = GetComponent<CircleCollider2D>();
        circol.enabled = true;
    }

    protected override void Moving(){
        switch(nowState)
        {
            case State.inGround:
                inGroundUpdate();
                break;
            case State.Move:
                if(isEndAnim){
                    MoveUpdate();
                }
                break;
            case State.Attack:
                AttackUpdate();
                break;
        }

        nowState = nextState;
        rb.velocity = new Vector2(0, -gravity);
    }

    // buta_attack の終了時に呼ばれる
    private void endAnimation(){
        isEndAnim = true;
        //return;
    }

    // buta_attack の途中、ブタが地面に潜ったら呼ぶ
    private void Dived(){
        gameObject.tag = "ground";  // 地面に潜っているときのダメージ判定を防ぐ to player (groundにしてみた)
        isAttack = false;
    }

    ///<summary>
    /// 状態遷移
    ///</summary>
    private void ChangeState(State next){
        nextState = next;
    }


    /// <summary>
    /// 待機→移動
    /// </summary>
    private void inGroundUpdate(){
        if(sr.isVisible){
            ChangeState(State.Move);
            return;
        }
    }
    /// <summary>
    /// 移動→攻撃
    /// </summary>
    private void MoveUpdate(){
        Vector2 butamoguraPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 targetPosition = new Vector2(Player.playerPos.position.x, this.transform.position.y);    // 移動をx軸方向だけにする.
        
        RaycastHit2D hit = Physics2D.Raycast(butamoguraPosition, targetPosition - butamoguraPosition, Mathf.Abs(butamoguraPosition.x - targetPosition.x), LayerMask.GetMask("Ground"));
        if(hit.collider != null){  // ぶたもぐらとプレイヤーの間に障害物があったら 貫通しないように
            if(butamoguraPosition.x < targetPosition.x)
                targetPosition.x = hit.point.x - 2.0f;
            else
                targetPosition.x = hit.point.x + 2.0f;
        }

        // 距離を詰める        
        transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, speed); // 自分, ターゲット, スピード
        if(Vector3.Distance(this.transform.position, Player.playerPos.position) < 4.0f){
            ChangeState(State.Attack);
            return;
        }
    }
    /// <summary>
    /// 攻撃→移動(待機)
    /// </summary>
    private void AttackUpdate(){
        gameObject.tag = "Enemy";       // ブタが地上に出ている時だけダメージ判定 to player
        anim.Play("buta_attack");
        isEndAnim = false;
        isAttack = true;    // 地上に出ているときにのみダメージ from player を受けるため.

        ChangeState(State.inGround);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(isAttack){   // trueのとき = ブタが地上に出ているとき
            recievedDamage(collision);
        }
    }

    protected override void dieAnimation(){
        gameObject.tag = "ground";
        anim.Play("buta_die");
    }
}