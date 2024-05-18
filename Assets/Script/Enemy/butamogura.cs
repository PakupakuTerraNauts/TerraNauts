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
        circol.enabled = true;
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

    // buta_attack �̓r���A�u�^���n�ʂɐ�������Ă�
    private void Dived(){
        gameObject.tag = "ground";  // �n�ʂɐ����Ă���Ƃ��̃_���[�W�����h�� to player (ground�ɂ��Ă݂�)
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
            ChangeState(State.Move);
            return;
        }
    }
    /// <summary>
    /// �ړ����U��
    /// </summary>
    private void MoveUpdate(){
        Vector2 butamoguraPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 targetPosition = new Vector2(Player.playerPos.position.x, this.transform.position.y);    // �ړ���x�����������ɂ���.
        
        RaycastHit2D hit = Physics2D.Raycast(butamoguraPosition, targetPosition - butamoguraPosition, Mathf.Abs(butamoguraPosition.x - targetPosition.x), LayerMask.GetMask("Ground"));
        if(hit.collider != null){  // �Ԃ�������ƃv���C���[�̊Ԃɏ�Q������������ �ђʂ��Ȃ��悤��
            if(butamoguraPosition.x < targetPosition.x)
                targetPosition.x = hit.point.x - 2.0f;
            else
                targetPosition.x = hit.point.x + 2.0f;
        }

        // �������l�߂�        
        transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, speed); // ����, �^�[�Q�b�g, �X�s�[�h
        if(Vector3.Distance(this.transform.position, Player.playerPos.position) < 4.0f){
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
        isAttack = true;    // �n��ɏo�Ă���Ƃ��ɂ̂݃_���[�W from player ���󂯂邽��.

        ChangeState(State.inGround);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(isAttack){   // true�̂Ƃ� = �u�^���n��ɏo�Ă���Ƃ�
            recievedDamage(collision);
        }
    }

    protected override void dieAnimation(){
        gameObject.tag = "ground";
        anim.Play("buta_die");
    }
}