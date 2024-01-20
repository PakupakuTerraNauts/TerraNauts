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
    // player�Ǝ����̋������擾
    [Header ("�U����(�v���C���[)")] public GameObject player;
    [Header ("����")] public GameObject Butamogura;
    // ���
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

    // buta_attack �̏I�����ɌĂ΂��
    private void endAnimation(){
        isEndAnim = true;
        //return;
    }

    // buta_attack �̓r���A�u�^���n�ʂɐ���������Ă�
    private void Dived(){
        gameObject.tag = "ground";  // �n�ʂɐ����Ă���Ƃ��̃_���[�W���� to player ��h�� (ground�ɂ��Ă݂�)
        isAttack = false;
    }

    ///<summary>
    /// ��ԑJ��
    ///</summary>
    private void ChangeState(State next){
        nextState = next;
    }


    /// <summary>
    /// �ҋ@���ړ�
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
    /// �ړ����U��
    /// </summary>
    private void MoveUpdate(){
        
        // �������l�߂�
        Butamogura.transform.position = Vector3.MoveTowards(Butamogura.transform.position, player.transform.position, speed); // ����, �^�[�Q�b�g, �X�s�[�h
        if(Vector3.Distance(Butamogura.transform.position, player.transform.position) < 4.0f){
            ChangeState(State.Attack);
            return;
        }
    }
    /// <summary>
    /// �U�����ړ�(�ҋ@)
    /// </summary>
    private void AttackUpdate(){
        gameObject.tag = "Enemy";       // �u�^���n��ɏo�Ă��鎞�����_���[�W���� to player
        anim.Play("buta_attack");
        isEndAnim = false;
        isAttack = true;    // �n��ɏo�Ă���Ƃ��ɂ̂݃_���[�W from player ���󂯂邽��

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
        if(isAttack){   // true�̂Ƃ� = �u�^���n��ɏo�Ă���Ƃ�
            recievedDamage(collision);
        }
    }

    protected override void dieAnimation(){
        anim.Play("buta_die");
        circol.tag = "DeadEnemy";
    }
}