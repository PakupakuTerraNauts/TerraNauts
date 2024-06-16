using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kabotya : MonoBehaviour
{
    #region // variables
    public float speed;
    public float waitingTime;

    [HideInInspector] public bool Ready = true;
    private bool isDead = false;

    private SpriteRenderer sr = null;
    private Rigidbody2D rb = null;

    private Vector3 defaultPos;
    public delegate void ThrowKabotya();
    private ThrowKabotya ThrowKabotyaCallBack;

    private enum State{
        Stay,
        Go,
        Invalid
    }
    private State nowState = State.Invalid;
    private State nextState = State.Invalid;
    #endregion

    void Awake(){
        defaultPos = transform.position;
    }

    void Start(){
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        gameObject.SetActive(false);
    }

/// <summary>
/// onkaboReady コールバックをセット
/// </summary>
/// <param name="onkaboReady">おばけの攻撃アニメーションと向き</param>
    public void InitializeCallBack(ThrowKabotya onThrowKabotya){
        ThrowKabotyaCallBack = onThrowKabotya;
    }

    void Update(){
        nowState = nextState;
        
        switch(nowState){
            case State.Stay:
                Stay();
                break;
            case State.Go:
                Go();
                break;
            case State.Invalid:
                Invalid();
                break;

            default:
                Stay();
                break;
        }
    }

///<summary>
/// 状態遷移する
///</summary>
    private void ChangeState(State next){
        nextState = next;
    }

///<summary>
/// 状態 : 待機
///</summary>
    private void Stay(){
        StartCoroutine(inStay());
    }
    // 待機コルーチン
    private IEnumerator inStay(){
        Ready = false;
        yield return new WaitForSeconds(waitingTime);
        ChangeState(State.Go);
    }

///<summary>
/// 状態 : 追随
///</summary>
    private void Go(){
        if(!sr.isVisible|| isDead){
            ChangeState(State.Invalid);
            return;
        }

        StartCoroutine(inGo());
    }
    // 追随コルーチン
    private IEnumerator inGo(){
        ThrowKabotyaCallBack();

        yield return new WaitForSeconds(0.5f);  // おばけのアニメーションが入ってからかぼちゃを動かすため.

        transform.position = Vector3.MoveTowards(transform.position, Player.playerPos.position, speed); // Player.playerPosX → static なPlayerの位置
        if(Vector3.Distance(transform.position, Player.playerPos.position) < 1.0f){
            ChangeState(State.Invalid);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player" || collision.tag == "Sword"){
            ChangeState(State.Invalid);
        }
    }

/// <summary>
/// 状態 : 消滅中
/// </summary>
/// <remarks> 追随→待機の移動中の状態 </remarks>
    private void Invalid(){
        Ready = true;
        transform.position = defaultPos;
        ChangeState(State.Stay);
        gameObject.SetActive(false);
    }

/// <summary>
/// 本体が倒れたらカボチャも無効化する
/// </summary>
    public void ObakeDead(){
        isDead = true;
    }
}
