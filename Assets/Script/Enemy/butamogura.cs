using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butamogura : MonoBehaviour
{
    #region // variables
    [Header ("重力")] public float gravity;
    [Header ("速度")] public float speed;

    private bool isDead = false;

    private Vector3 butamoguraposition;

    private Vector3 position_player;
    private float ATK_player;
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private string swordTag = "Sword";
    // playerオブジェクト取得　インスペクターで操作
    [Header ("どこに向かって攻撃するか(プレイヤー)")] public GameObject Player;
    // ステートAIに使用
    private enum State{
        inGround,
        Move,
        Attack
    }
    private State nowState = State.inGround;
    private State nextState = State.inGround;
    #endregion

    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update(){
        butamoguraposition = this.gameObject.transform.position;

        if(sr.isVisible){
            if(!isDead){
                rb.WakeUp();
                switch(State)
                {
                    case State.inGround:
                        inGroundUpdate();
                        break;
                    case State.Move:
                        MoveUpdate();
                        break;
                    case State.Attack:
                        AttackUpdate();
                        break;
                }

                nowState = nextState;
                rb.velocity = new Vector2(0, -gravity);
            }
        }
        else{
            rb.Sleep();
        }
    }

    ///<summary>
    /// 遷移するステートの設定
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
        butamoguraposition = Vector3.MoveTowards(butamoguraposition, Player.transform.position, speed*Time.deltaTime); // 自分の位置, ターゲットの位置, 速度

        if(Vector3.Distance(butamoguraposition, Player.transform.position) < 2.0f){
            ChangeState(State.Attack);
            return;
        }
    }
    /// <summary>
    /// 攻撃→移動(待機)
    /// </summary>
    private void AttackUpdate(){
        anim.Play("buta_attack");

        if(sr.isVisible){
            ChangeState(State.Move);
            return;
        }
        else{
            ChangeState(State.inGround);
            return;
        }
    }
}