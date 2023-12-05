using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butamogura : MonoBehaviour
{
    
    #region // variables
    [Header ("重力")] public float gravity;
    [Header ("速度")] public float speed;
    [Header ("HP")] public float nowHP;

    public float ATK_player = Player.ATK;

    private bool isDead = false;

    public BoxCollider2D boxcol = null;
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private string swordTag = "Sword";
    // playerオブジェクト取得　インスペクターで操作 
    [Header ("どこに向かって攻撃するか(プレイヤー)")] public Player player;
    
    // ステートAIに使用 
    private enum State{
        inGround,
        Move,
        Attack
    }
    private State nowState = State.inGround;
    private State nextState = State.inGround;
    private Vector3 playerPosition = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 butamoguraPosition;
    #endregion

    private void Start(){
        boxcol = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update(){
        if(sr.isVisible){
            if(!isDead){
                rb.WakeUp();
                switch(nowState)
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
    void ChangeState(State next){
        nextState = next;
    }


    /// <summary>
    /// 待機→移動 
    /// </summary>
    void inGroundUpdate(){
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
    void MoveUpdate(){
        // プレイヤーとぶたの位置を更新 
        playerPosition = player.transform.position;
        butamoguraPosition = this.transform.position;
        // 距離を詰める 
        butamoguraPosition = Vector3.MoveTowards(butamoguraPosition, playerPosition, speed); // 自分の位置, ターゲットの位置, 速度 

        if(Vector3.Distance(butamoguraPosition, playerPosition) < 2.0f){
            ChangeState(State.Attack);
            return;
        }
    }
    /// <summary>
    /// 攻撃→移動(待機) 
    void AttackUpdate(){
        anim.Play("buta_attack");

        if(sr.isVisible){
            ChangeState(State.Move);
            return;
        }
        else{
            anim.Play("buta_default");
            ChangeState(State.inGround);
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.tag == swordTag && !isDead){
            nowHP = nowHP - ATK_player;
        }
        if(nowHP <= 0){
            anim.Play("buta_die");
            isDead = true;
            Destroy(gameObject, 3f);
        }
    }
}