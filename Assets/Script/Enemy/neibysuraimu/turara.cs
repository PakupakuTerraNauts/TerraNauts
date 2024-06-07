using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turara : MonoBehaviour
{
    #region // variables
    public float gravity;
    private bool isBreak = false;
    [HideInInspector] public bool isFall = false;

    private Vector3 StartPos;
    public neibysuraimu neiby;
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;

    public delegate void turaraFallenCheck();
    private turaraFallenCheck turaraFallenCheckCallBack;
    #endregion

    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        StartPos = transform.position;
        this.gameObject.SetActive(false);
    }

    void Update(){
        if(isFall && !isBreak){
            anim.Play("neiby_turara_fall");
            rb.velocity = new Vector2(0.0f, -gravity);
        }else{
            rb.velocity = new Vector2(0.0f, 0.0f);
        }

        if(20f < StartPos.y - transform.position.y)
            StartCoroutine(BreakTurara());
    }

/// <summary>
/// onTuraraFallenCheck �R�[���o�b�N���Z�b�g
/// </summary>
/// <param name="onTuraraFallenCheck">�S�Ă̍U�����I�����Ă��邩�`�F�b�N</param>
    public void InitializeCallBack(turaraFallenCheck onTuraraFallenCheck){
        turaraFallenCheckCallBack = onTuraraFallenCheck;
    }

/// <summary>
/// �X�����~�点��
/// </summary>
    public void Turara(){
        transform.position = StartPos;
        this.gameObject.SetActive(true);

        StartCoroutine(GenerateTurara());
        rb.WakeUp();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        Debug.Log(collision.tag);
        if(collision.tag == "ground" || collision.tag == "Player" || collision.tag == "Sword"){
            StartCoroutine(BreakTurara());
        }
    }

/// <summary>
/// �X���𐶐�����R���[�`��
/// </summary>
/// <returns></returns>
    private IEnumerator GenerateTurara(){
        anim.Play("neiby_turara_generate");
        yield return new WaitForSeconds(0.5f);
        isFall = true;
    }

/// <summary>
/// �X�����󂳂ꂽ�Ƃ��̃R���[�`��
/// </summary>
/// <returns></returns>
    private IEnumerator BreakTurara(){
        isBreak = true;
        anim.Play("neiby_turara_break");
        yield return new WaitForSeconds(2.0f);
        this.gameObject.SetActive(false);
        
        isFall = false;
        turaraFallenCheckCallBack();
        isBreak = false;
    }
}