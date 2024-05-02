using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kabotya : MonoBehaviour
{
    #region // variables
    public float speed;
    public float waitingTime;

    private CircleCollider2D circol = null;
    private SpriteRenderer sr = null;
    private Rigidbody2D rb = null;

    [Header ("本体")] public obakekabotya obake;
    private enum State{
        Stay,
        Go,
        Invalid
    }
    private State nowState = State.Invalid;
    private State nextState = State.Invalid;
    #endregion

    void Start(){
        circol = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
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
    /// 状態遷移
    ///</summary>
    private void ChangeState(State next){
        nextState = next;
    }

    ///<summary>
    /// 待機
    ///</summary>
    private void Stay(){
        StartCoroutine("inStay");
    }

    private IEnumerator inStay(){
        transform.localPosition = Vector3.zero;
        yield return new WaitForSeconds(waitingTime);   // かぼちゃの待機時間
        if(sr.isVisible)
            ChangeState(State.Go);
        else
            ChangeState(State.Invalid);
    }

    ///<summary>
    /// 追随
    ///</summary>
    private void Go(){
        StartCoroutine("inGo");
    }

    private IEnumerator inGo(){
        obake.ThrowKabotya();

        yield return new WaitForSeconds(1.0f);  // おばけのアニメーションが入ってからかぼちゃを動かすため.

        transform.position = Vector3.MoveTowards(transform.position, Player.playerPos.position, speed); // Player.playerPosX --- static Playerの位置
        if(Vector3.Distance(transform.position, Player.playerPos.position) < 2.0f || !sr.isVisible){
            ChangeState(State.Stay);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player" || collision.tag == "Sword"){
            ChangeState(State.Stay);
        }
    }

    private void Invalid(){
        if(sr.isVisible){
            rb.WakeUp();
            ChangeState(State.Go);
            return;
        }
        ChangeState(State.Invalid);
    }
}
