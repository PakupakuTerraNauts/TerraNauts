using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butamogura : MonoBehaviour
{
    #region // variables
    [Header ("重力")] public float gravity;
    [Header ("速度")] public float speed;

    private bool isDead = false;
    private bool isEndAnim = true;

    private float hp = 0.0f;
    private float ATK_player = 0.0f;

    private HPBar HP;
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private string swordTag = "Sword";
    // playerオブジェクト取得　インスペクターで操作
    [Header ("どこに向かって攻撃するか(プレイヤー)")] public GameObject player;
    [Header ("自分")] public GameObject Butamogura;
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
        HP = GetComponent<HPBar>();

        ATK_player = Player.ATK;
        hp = HPBar.instance.currentHealth;
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
        }
        else{
            rb.Sleep();
        }
    }

    private void endAnimation(){
        isEndAnim = true;
        return;
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
        Butamogura.transform.position = Vector3.MoveTowards(Butamogura.transform.position, player.transform.position, speed); // 自分の位置, ターゲットの位置, 速度
        if(Vector3.Distance(Butamogura.transform.position, player.transform.position) < 4.0f){
            ChangeState(State.Attack);
            return;
        }
        // スピード0.1でもコルーチンなしだと速すぎる
        // クールタイムがないと上下にガクガクする
        StartCoroutine("MoveCoolTime");
    }
    /// <summary>
    /// 攻撃→移動(待機)
    /// </summary>
    private void AttackUpdate(){
        anim.Play("buta_attack");
        isEndAnim = false;

        if(sr.isVisible){
            ChangeState(State.Move);
            return;
        }
        else{
            ChangeState(State.inGround);
            return;
        }
    }

    private IEnumerator MoveCoolTime(){
        yield return new WaitForSeconds(10.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == swordTag && !isDead){
            HP.UpdateHP(ATK_player);
            hp = hp - ATK_player;
        }
        
        if(hp <= 0.0f){
            anim.Play("buta_die");
            isDead = true;
            Destroy(gameObject, 3f);
        }
    }
}