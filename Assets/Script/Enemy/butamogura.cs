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
    // playerと自分の距離を取得
    [Header ("攻撃先(プレイヤー)")] public GameObject player;
    [Header ("自分")] public GameObject Butamogura;
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

    // buta_attack の途中、ブタが地面に潜っったら呼ぶ
    private void Dived(){
        gameObject.tag = "ground";  // 地面に潜っているときのダメージ判定 to player を防ぐ (groundにしてみた)
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
            rb.WakeUp();
            ChangeState(State.Move);
            return;
        }
        else{
            rb.Sleep();
        }
    }
    /// <summary>
    /// 移動→攻撃
    /// </summary>
    private void MoveUpdate(){
        
        // 距離を詰める
        Butamogura.transform.position = Vector3.MoveTowards(Butamogura.transform.position, player.transform.position, speed); // 自分, ターゲット, スピード
        if(Vector3.Distance(Butamogura.transform.position, player.transform.position) < 4.0f){
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
        isAttack = true;    // 地上に出ているときにのみダメージ from player を受けるため

        if(sr.isVisible){
            ChangeState(State.Move);
            return;
        }
        else{
            ChangeState(State.inGround);
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(isAttack){   // trueのとき = ブタが地上に出ているとき
            recievedDamage(collision);
        }
    }

    protected override void dieAnimation(){
        anim.Play("buta_die");
        circol.tag = "DeadEnemy";
    }
}