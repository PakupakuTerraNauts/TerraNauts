using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class niwakokepittya : Enemy
{
    #region //variables
    public tama Tama;

    private float toriPosition_x = 0.0f;

    public bool isLeft = true;     // ������� ������
    private CapsuleCollider2D capcol = null;
    #endregion

    protected override void Initialize(){
        if(!isLeft)
            transform.localScale = new Vector3(-1, 1, 1);
        capcol = GetComponent<CapsuleCollider2D>();
        capcol.enabled = true;
    }

    protected override void Moving(){
        toriPosition_x = transform.position.x;
        anim.Play("tori_pitch");
        rb.velocity = new Vector2(0, -Data.gravity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        Tama.gameObject.SetActive(false);
        anim.Play("tori_die");
    }

/// <summary>
/// player�̕����𔻒肵�� ������������ �A�j���[�V�����I�����ɌĂ�
/// </summary>
    public void DirectJudge(){
        if(Player.playerPos.position.x > toriPosition_x && isLeft){
            transform.localScale = new Vector3(-1, 1, 1);
            isLeft = false;
        }
        else if(Player.playerPos.position.x < toriPosition_x && !isLeft){
            transform.localScale = new Vector3(1, 1, 1);
            isLeft = true;
        }
        // �U��������Ƃ��ɋ��̓����蔻�肪�c���Ă��邱�Ƃ�����̂Ŕ�A�N�e�B�u�ɕύX����
        Tama.gameObject.SetActive(false);
    }

/// <summary>
/// ���𓊂���
/// </summary>
    private void release(){
        Tama.gameObject.SetActive(true);
        Tama.TamaPitch(isLeft);
    }
}

