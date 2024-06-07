using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour // �G�X�N���v�g�@�X�[�p�[�N���X
{
    #region // variables
    protected float hp = 0.0f;
    [SerializeField] protected string Name;

    protected bool isDead = false;

    protected GameObject basicObject;
    protected GameObject uniqueObject;
    protected GameObject HPObject;

    protected HPBar HP;
    protected Animator anim = null;
    protected Rigidbody2D rb = null;
    protected SpriteRenderer sr = null;
    protected SpriteRenderer criticalSr = null;

    protected enemyData Data;
    #endregion

    void Awake(){
        var enemyData = Resources.Load<EnemyData>("EnemyData");
        foreach(var data in enemyData.EnemyDataList){
            if(data.Name == Name){
                Data = data;
            }
        }
    }

    void Start(){
        HPObject = transform.GetChild(1).gameObject;
        Spawn();
        GameObject criticalEffect = transform.GetChild(0).gameObject;
        criticalSr = criticalEffect.GetComponent<SpriteRenderer>();
    }

/// <summary>
/// ������ �ŏ��ƃQ�[���I�[�o�[�̌�ɌĂ�
/// </summary>
    public void Spawn(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        HP = GetComponent<HPBar>();

        basicObject = Data.basicObject;
        uniqueObject = Data.uniqueObject;

        hp = Data.maxHP;
        HP.SetHP(hp);
        Initialize();
        if(!gameObject.activeSelf){     // gameObject��false�Ȃ��x�|����Ă���
            this.gameObject.SetActive(true);
            isDead = false;
            HP.UpdateHP(-hp);   // HP��0�Ȃ̂Őݒ肵����
        }
        HPObject.SetActive(false);
    }

    protected void Update(){
        if(sr.isVisible && !isDead){
            rb.WakeUp();
            Moving();
        }
        else{
            rb.Sleep();
            Sleeping();
            if(HPObject.activeSelf && !isDead)  //��ʊO�ɏo�����\���ɂ�����
                HPObject.SetActive(false);      // �ł����񂾂Ƃ��ɂ͏����\�����Ă�������
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
            if(HPObject.activeSelf && !isDead)
                HPObject.SetActive(false);
        }
    }

    // TriggerEnter����Ă�
    protected void recievedDamage(Collider2D collision){
        if(!isDead){
            if(collision.tag == "Sword"){
                float atk = Player.ATK + Player.ATKincrement;
                if(RandomTF((Player.CRITRATE + Player.CRITRATEincrement) / 5.0f)){
                    atk += (Player.CRITDMG + Player.CRITDMGincrement) * 2.0f;
                    StartCoroutine(CriticalHit());
                }
                DecrementHP(atk);
                hp = hp - atk;
            }

            // �ȉ� �v���C���[�����˕Ԃ����G�̍U�����������Ƃ�����
            if(collision.tag == "DeadZone"){
                DecrementHP(hp);
                hp -= hp;
            }
            if(collision.tag == "NinzinExp"){
                DecrementHP(ninzin.EXP);
                hp = hp - ninzin.EXP;
            }

            if(hp <= 0.0f){
                dieAnimation();
                isDead = true;
                StartCoroutine(Death());
            }
        }
    }

/// <summary>
/// HP�o�[��HP�����炷
/// </summary>
/// <param name="damage"></param>
    protected void DecrementHP(float damage){
        HPObject.SetActive(true);
        HP.UpdateHP(damage);
    }

/// <summary>
/// �N���e�B�J���G�t�F�N�g��\������
/// </summary>
/// <returns></returns>
    protected IEnumerator CriticalHit(){
        criticalSr.enabled = true;
        yield return new WaitForSeconds(1.0f);
        criticalSr.enabled = false;
    }

/// <summary>
/// �|�ꂽ�Ƃ� �A�C�e���h���b�v�Ɣ�\������
/// </summary>
/// <returns></returns>
    private IEnumerator Death(){
        Instantiate<GameObject>(basicObject, transform.position, Quaternion.identity); // Quater...�͉�]�ō���͖���]
        // �ŗL�̐H�ރh���b�v��3��
        if(RandomTF(30.0f)){
            Instantiate<GameObject>(uniqueObject, transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(3.0f);
        this.gameObject.SetActive(false);
        //HPObject.SetActive(false);
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
        // ���̑��A�ŗL�̏���
    }

    protected virtual void MovingF(){
        // FixedUpdate�p
    }

    protected virtual void dieAnimation(){
        // anim.Play("���ꂼ���die�A�j���[�V����")
    }

/// <summary>
/// �|���ꂽ��ԂŃZ�[�u���ꂽ�Ƃ��A�V���O���g������폜����
/// </summary>
/// <returns></returns>
    public bool DeleteDead(){
        if(isDead){
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }

/// <summary>
/// �󂯎�����m���Ŏ��ۂ��N���������ǂ��������肷��
/// </summary>
/// <param name="Persent">�m��</param>
/// <returns>���ۂ��N���邩 ����</returns>
    protected bool RandomTF(float Persent){
        float Rate = UnityEngine.Random.value * 100.0f;

        if(Rate <= Persent){
            return true;
        }
        else{
            return false;
        }
    }
}