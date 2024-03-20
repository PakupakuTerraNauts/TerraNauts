using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kabotya : MonoBehaviour
{
    #region // variables
    public float speed;

    private CircleCollider2D circol = null;
    private SpriteRenderer sr = null;

    [Header ("ñ{ëÃ")] public obakekabotya obake;
    [Header ("çUåÇêÊ")] public GameObject Player;
    private enum State{
        Stay,
        Go
    }
    private State nowState = State.Stay;
    private State nextState = State.Stay;
    #endregion

    void Start(){
        circol = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update(){
        switch(nowState){
            case State.Stay:
                Stay();
                break;
            case State.Go:
                Go();
                break;
        }

        nowState = nextState;
    }

    ///<summary>
    /// èÛë‘ëJà⁄
    ///</summary>
    private void ChangeState(State next){
        nextState = next;
    }

    ///<summary>
    /// ë“ã@
    ///</summary>
    private void Stay(){
        StartCoroutine("inStay");
    }

    private IEnumerator inStay(){
        //gameObject.SetActive(false);
        yield return new WaitForSeconds(5.0f);
        ChangeState(State.Go);
    }

    ///<summary>
    /// í«êè
    ///</summary>
    private void Go(){
        if(sr.isVisible){
            //gameObject.SetActive(true);
            obake.ThrowKabotya();
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed);
            if(Vector3.Distance(transform.position, Player.transform.position) < 2.0f){
                ToStay();
                return;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player" || collision.tag == "Sword"){
            ToStay();
        }
    }

    private void ToStay(){
        ChangeState(State.Stay);
        transform.localPosition = Vector3.zero;
    }
}
