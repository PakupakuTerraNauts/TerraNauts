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
    [Header ("プレイヤー")] public GameObject Player;
    [Header ("自分")] public GameObject Sukaisuraimu;
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
    }

    protected override void Moving(){
        anim.Play("sukai_default");

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
        isFall = Ninzin.fallTF();
        if(!isFall){
            ChangeState(State.Move);
        }
        return;
    }
    /// <summary>
    /// 移動→攻撃
    /// </summary>
    private void MoveUpdate(){
        Ninzin.Reload(Sukaisuraimu.transform.position);
        if(Player.transform.position.x < transform.position.x){
            transform.localScale = new Vector3(1, 1, 1);
            // 人参にもrigidbodyがついているのでrb.velocityで移動できない
            // new 人参をリロードしてから移動させたいので人参に位置を渡して追従させるようにした. それに伴いvelocity移動できるようになった
            rb.velocity = new Vector2(-speed, 0.0f);
        }
        else{
            transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = new Vector2(speed, 0.0f);
        }
        if(Mathf.Abs(Sukaisuraimu.transform.position.x - Player.transform.position.x) < 3.0f){
            ChangeState(State.Attack);
            return;
        }
    }
    /// <summary>
    /// 攻撃→移動
    /// </summary>
    private void AttackUpdate(){
        Ninzin.Falling();
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
}
