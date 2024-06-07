using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class painsisa : Enemy
{
    #region // variables
    private bool instantDeath = false;
    [HideInInspector] public bool isAttack = false;
    [Header("�`�F�b�N�Ō������]")] public bool isLeft = true;
    [SerializeField] private sisaRange range;

    private CircleCollider2D circol = null;
    #endregion

    protected override void Initialize(){
        range.InitializeCallBack(onSisaAttack);
        circol = GetComponent<CircleCollider2D>();
        circol.enabled = true;
        if(!isLeft){
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

/// <summary>
/// �U��
/// </summary>
    private void onSisaAttack(){
        // �t���O�Ǘ����Ƃ��蔲����̂ŃA�j���[�V�������Ŕ���
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("pinesisa_stand")){
            anim.Play("pinesisa_attack");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "FallingTree"){ // ���G�ƈႢ �؂ő����̐�p�A�j���[�V����������̂Ńt���O�I���ɂ��Ă���
            instantDeath = true;
        }
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        if(instantDeath){
            anim.Play("pinesisa_treedie");
        }
        else{
            anim.Play("pinesisa_defdie");
        }
    }

/// <summary>
/// �A�j���[�V�����C�x���g �U���̏I�������m
/// </summary>
    private void endAnimation(){
        isAttack = false;
    }
}
