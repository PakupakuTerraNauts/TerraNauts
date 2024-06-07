using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ivy : MonoBehaviour
{
    [SerializeField] private ivyRange range;
    [SerializeField] private State currentState;

    [SerializeField, Header("Range�̈ʒu,�T�C�Y��ύX������`�F�b�N")] private bool ChangeRange = false; 

    private Animator anim = null;

    public enum State{
        piercing,
        undulating,
        bending
    }

    void Start(){
        anim = GetComponent<Animator>();
        range.InitializeCallBack(onIvyAttack);

        if(!ChangeRange){   // �n�`�̊֌W��range��ύX������,�A�j���[�V�������Ƃ̃f�t�H�ʒu�ւ̐ݒ�̓X�L�b�v
            switch(currentState){
                case State.piercing : // �f�t�H���g
                    return;
                case State.undulating :
                    range.InitializeRange();
                    return;
                case State.bending :
                    range.InitializeRange();
                    return;
                default :
                    return;
            }
        }
    }

/// <summary>
/// �c�^�U��
/// </summary>
    private void onIvyAttack(){
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("nakabayashi_ivy_default")){  // �t���O�Ǘ����Ƃ��蔲���ăf�b�h���b�N���邱�Ƃ�����̂ŃA�j���[�V�����Ŕ���
            PlayAttackAnimation();
        }
    }

/// <summary>
/// �U���A�j���[�V������I�����čĐ�
/// </summary>
    public void PlayAttackAnimation(){
        switch(currentState){
            case State.piercing :
                anim.Play("nakabayashi_ivy_piercing");
                return;
            case State.undulating :
                anim.Play("nakabayashi_ivy_undulating");
                return;
            case State.bending :
                anim.Play("nakabayashi_ivy_bending");
                return;
            default :
                anim.Play("nakabayashi_ivy_piercing");
                return;
        }
    }

/// <summary>
/// �A�j���[�V�����I��
/// </summary>
    private void EndAnimation(){
        anim.Play("nakabayashi_ivy_default");
    }
}
