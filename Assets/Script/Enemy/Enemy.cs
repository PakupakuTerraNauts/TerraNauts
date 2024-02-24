using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour // �G�X�N���v�g�@�X�[�p�[�N���X
{
    #region // variables
    [SerializeField] protected float gravity;
    protected float hp = 0.0f;
    protected float ATK_player = 0.0f;
    protected float ninzin_explosion = 10.0f;

    protected bool isDead = false;

    protected HPBar HP;
    protected Animator anim = null;
    protected Rigidbody2D rb = null;
    protected SpriteRenderer sr = null;
    #endregion

    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        HP = GetComponent<HPBar>();

        ATK_player = Player.ATK;
        hp = HP.maxHealth;
        Initialize();
    }

    protected void Update(){
        if(sr.isVisible && !isDead){
            rb.WakeUp();
            Moving();
        }
        else{
            rb.Sleep();
            Sleeping();
        }
    }

    protected void FixedUpdate(){
        if(sr.isVisible && !isDead){
            rb.WakeUp();
            MovingF();
        }
        else{
            rb.Sleep();
            SleepingF();
        }
    }

    // TriggerEnter����Ă�
    protected void recievedDamage(Collider2D collision){
        if(collision.tag == "Sword" && !isDead){
            HP.UpdateHP(ATK_player);
            hp = hp - ATK_player;
        }

        // �v���C���[�����˕Ԃ����U�����������Ƃ�����
        if(collision.tag == "FallingTree"){
            HP.UpdateHP(hp);
            hp -= hp;
        }
        if(collision.tag == "NinzinExp"){
            HP.UpdateHP(ninzin_explosion);
            hp = hp - ninzin_explosion;
        }

        if(hp <= 0.0f){
            dieAnimation();
            isDead = true;
            Destroy(gameObject, 3f);
        }
    }

    protected virtual void Initialize(){
        // �R���C�_��GetComponent
    }

    protected virtual void Sleeping(){
        // �����X�^�[����ʂɉf���Ă��Ȃ��Ƃ��ɏ����������
    }

    protected virtual void SleepingF(){
        // FixedUpdate�p
    }

    protected virtual void Moving(){
        // ��{anim.Play("�f�t�H���A�j���[�V����")
        // ��{rb.velocity = new Vector2(x, y)
        // ���̑��A�ŗL�̓��ꏈ��
    }

    protected virtual void MovingF(){
        // FixedUpdate�p
    }

    protected virtual void dieAnimation(){
        // anim.Play("���ꂼ���die�A�j���[�V����")
        // �v���C���[�Ƀ_���[�W������̂�h������tag��DeadEnemy�ɕύX
    }

}