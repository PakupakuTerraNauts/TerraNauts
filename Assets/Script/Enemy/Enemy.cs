using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour // �G�X�N���v�g�@�X�[�p�[�N���X
{
    #region // variables
    [SerializeField] protected float gravity;
    protected float hp = 0.0f;
    protected float ninzin_explosion = 10.0f;

    protected bool isDead = false;

    [SerializeField] protected GameObject basicObject;
    [SerializeField] protected GameObject uniqueObject;

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
        if(!isDead){
            if(collision.tag == "Sword"){
                float atk = Player.ATK;
                if(rand.Random(Player.CRITRATE / 5.0f)){
                    atk += Player.CRITDMG * 2.0f;
                }
                HP.UpdateHP(atk);
                hp = hp - atk;
            }

            // �ȉ� �v���C���[�����˕Ԃ����U�����������Ƃ�����
            if(collision.tag == "DeadZone"){
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
                Instantiate<GameObject>(basicObject, transform.position, Quaternion.identity); // Quater...�͉�]�ō���͖���]
                // �ŗL�̐H�ރh���b�v��3��
                if(rand.Random(30.0f)){
                    Instantiate<GameObject>(uniqueObject, transform.position + Vector3.up, Quaternion.identity);
                }
            }
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