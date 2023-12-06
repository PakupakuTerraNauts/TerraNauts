using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butamogura : MonoBehaviour
{
    #region // variables
    [Header ("�d��")] public float gravity;
    [Header ("���x")] public float speed;

    private bool isDead = false;

    private Vector3 butamoguraposition;

    private Vector3 position_player;
    private float ATK_player;
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private string swordTag = "Sword";
    // player�I�u�W�F�N�g�擾�@�C���X�y�N�^�[�ő���
    [Header ("�ǂ��Ɍ������čU�����邩(�v���C���[)")] public GameObject Player;
    // �X�e�[�gAI�Ɏg�p
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
    /// �J�ڂ���X�e�[�g�̐ݒ�
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
        butamoguraposition = Vector3.MoveTowards(butamoguraposition, Player.transform.position, speed*Time.deltaTime); // �����̈ʒu, �^�[�Q�b�g�̈ʒu, ���x

        if(Vector3.Distance(butamoguraposition, Player.transform.position) < 2.0f){
            ChangeState(State.Attack);
            return;
        }
    }
    /// <summary>
    /// �U�����ړ�(�ҋ@)
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