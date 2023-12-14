using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butamogura : MonoBehaviour
{
    #region // variables
    [Header ("?d??")] public float gravity;
    [Header ("???x")] public float speed;

    private bool isDead = false;
    private bool isEndAnim = true;

    private float hp = 0.0f;
    private float ATK_player = 0.0f;

    private HPBar HP;
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private string swordTag = "Sword";
    // player?I?u?W?F?N?g?��?@?C???X?y?N?^?[?????
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

    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        HP = GetComponent<HPBar>();

        ATK_player = Player.ATK;
        hp = HP.maxHealth;
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