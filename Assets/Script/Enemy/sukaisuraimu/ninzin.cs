using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ninzin : MonoBehaviour
{
    #region // variables
    public float gravity;
    private bool isFall = false;
    private bool isHit = false;

    public sukaisuraimu sukai;
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;

    private GameObject exp;
    public static float EXP = 80f;

    public delegate void ninzinFallenCheck();
    private ninzinFallenCheck ninzinFallenCheckCallBack;
    #endregion

    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        exp = transform.GetChild(0).gameObject; // �����̕�
    }

    void Update(){
        if(isFall && !isHit){
            rb.velocity = new Vector2(0.0f, -gravity);
        }
    }

/// <summary>
/// onNinzinFallenCheck �R�[���o�b�N���Z�b�g
/// </summary>
/// <param name="onNinzinFallenCheck">�l�Q�̔������擾</param>
    public void InitializeCallBack(ninzinFallenCheck onNinzinFallenCheck){
        ninzinFallenCheckCallBack = onNinzinFallenCheck;
    }

/// <summary>
/// �X���C���ɒǐ�����
/// </summary>
/// <param name="nowPosition">�X�J�C�X���C���̈ʒu</param>
    public void Reload(Vector3 nowPosition){
        if(!isFall){
            transform.position = new Vector3(nowPosition.x, nowPosition.y, nowPosition.z);
        }
    }

/// <summary>
/// �����X�^�[�g
/// </summary>
    public void Falling(){
        isFall = true;
        isHit = false;
        rb.WakeUp();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "ground" || collision.tag == "Sword" || collision.tag == "Player"){
            isHit = true;
            StartCoroutine(NinzinExplosion());
            rb.Sleep();
        }
    }

    // �����G�t�F�N�g��\��
    private IEnumerator NinzinExplosion(){
        exp.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        exp.SetActive(false);
        this.gameObject.SetActive(false);

        isFall = false;
        ninzinFallenCheckCallBack();
    }
}
