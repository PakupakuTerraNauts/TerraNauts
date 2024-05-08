using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kabotya : MonoBehaviour
{
    #region // variables
    public float speed;
    public float waitingTime;

    [HideInInspector] public bool Ready = true;

    //private CircleCollider2D circol = null;
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
        //circol = GetComponent<CircleCollider2D>();
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
        StartCoroutine(inStay());
    }

    private IEnumerator inStay(){
        yield return new WaitForSeconds(waitingTime);
        ChangeState(State.Go);
    }

    ///<summary>
    /// 追随
    ///</summary>
    private void Go(){
        if(!sr.isVisible){
            ChangeState(State.Invalid);
            return;
        }

        StartCoroutine(inGo());
    }

    private IEnumerator inGo(){
        Ready = false;
        obake.ThrowKabotya();

        yield return new WaitForSeconds(0.5f);  // おばけのアニメーションが入ってからかぼちゃを動かすため.

        transform.position = Vector3.MoveTowards(transform.position, Player.playerPos.position, speed); // Player.playerPosX --- static Playerの位置
        if(Vector3.Distance(transform.position, Player.playerPos.position) < 2.0f){
            ChangeState(State.Invalid);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player" || collision.tag == "Sword"){
            ChangeState(State.Invalid);
        }
    }

    private void Invalid(){
        if(sr.isVisible){
            rb.WakeUp();
            transform.localPosition = Vector3.zero;
            Ready = true;
            ChangeState(State.Stay);
            gameObject.SetActive(false);
        }
        else{
            rb.Sleep();
        }
    }
}
