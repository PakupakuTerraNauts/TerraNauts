using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kabotya : MonoBehaviour
{
    #region // variables
    public float speed;
    public float waitingTime;

    [HideInInspector] public bool Ready = true;
    private bool isDead = false;

    private SpriteRenderer sr = null;
    private Rigidbody2D rb = null;

    private Vector3 defaultPos;
    public delegate void ThrowKabotya();
    private ThrowKabotya ThrowKabotyaCallBack;

    private enum State{
        Stay,
        Go,
        Invalid
    }
    private State nowState = State.Invalid;
    private State nextState = State.Invalid;
    #endregion

    void Awake(){
        defaultPos = transform.position;
    }

    void Start(){
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        gameObject.SetActive(false);
    }

/// <summary>
/// onkaboReady �R�[���o�b�N���Z�b�g
/// </summary>
/// <param name="onkaboReady">���΂��̍U���A�j���[�V�����ƌ���</param>
    public void InitializeCallBack(ThrowKabotya onThrowKabotya){
        ThrowKabotyaCallBack = onThrowKabotya;
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
/// ��ԑJ�ڂ���
///</summary>
    private void ChangeState(State next){
        nextState = next;
    }

///<summary>
/// ��� : �ҋ@
///</summary>
    private void Stay(){
        StartCoroutine(inStay());
    }
    // �ҋ@�R���[�`��
    private IEnumerator inStay(){
        Ready = false;
        yield return new WaitForSeconds(waitingTime);
        ChangeState(State.Go);
    }

///<summary>
/// ��� : �ǐ�
///</summary>
    private void Go(){
        if(!sr.isVisible|| isDead){
            ChangeState(State.Invalid);
            return;
        }

        StartCoroutine(inGo());
    }
    // �ǐ��R���[�`��
    private IEnumerator inGo(){
        ThrowKabotyaCallBack();

        yield return new WaitForSeconds(0.5f);  // ���΂��̃A�j���[�V�����������Ă��炩�ڂ���𓮂�������.

        transform.position = Vector3.MoveTowards(transform.position, Player.playerPos.position, speed); // Player.playerPosX �� static ��Player�̈ʒu
        if(Vector3.Distance(transform.position, Player.playerPos.position) < 1.0f){
            ChangeState(State.Invalid);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player" || collision.tag == "Sword"){
            ChangeState(State.Invalid);
        }
    }

/// <summary>
/// ��� : ���Œ�
/// </summary>
/// <remarks> �ǐ����ҋ@�̈ړ����̏�� </remarks>
    private void Invalid(){
        Ready = true;
        transform.position = defaultPos;
        ChangeState(State.Stay);
        gameObject.SetActive(false);
    }

/// <summary>
/// �{�̂��|�ꂽ��J�{�`��������������
/// </summary>
    public void ObakeDead(){
        isDead = true;
    }
}
