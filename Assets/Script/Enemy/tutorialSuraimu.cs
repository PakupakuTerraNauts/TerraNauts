using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialSuraimu : Enemy
{
    private CircleCollider2D circol = null;

    protected override void Initialize(){
        circol = GetComponent<CircleCollider2D>();
        circol.enabled = true;
    }

    protected override void Moving(){
        rb.velocity = new Vector2(0, -Data.gravity);
    }


    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Sword"){
            float atk = StatusManager.ATK + StatusManager.ATKincrement;
            if(RandomTF((StatusManager.CRITRATE + StatusManager.CRITRATEincrement) / 5.0f)){
                atk += (StatusManager.CRITDMG + StatusManager.CRITDMGincrement) * 2.0f;
                StartCoroutine(CriticalHit());
            }
            DecrementHP(atk);
            hp = hp - atk;
        
            if(hp <= 0.0f){
                dieAnimation();
                isDead = true;
                StartCoroutine(tutorialDeath());
            }
        }
    }

    protected override void dieAnimation(){
        anim.Play("suraimu_die");
    }

/// <summary>
/// �`���[�g���A���p �|��Ă�������������
/// </summary>
/// <returns></returns>
    private IEnumerator tutorialDeath(){
        Instantiate<GameObject>(basicObject, transform.position, Quaternion.identity); // Quater...�͉�]�ō���͖���]
        // �ŗL�̐H�ރh���b�v��3��
        if(RandomTF(30.0f)){
            Instantiate<GameObject>(uniqueObject, transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(3.0f);
        sr.enabled = false;     // �X�v���C�g���\���ɂ��邾��
        HPObject.SetActive(false);
        
        yield return new WaitForSeconds(1.0f);
        // �h������
        anim.Play("suraimu_furueru");
        sr.enabled = true;
        circol.enabled = true;
        isDead = false;
        hp = Data.maxHP;
        HP.UpdateHP(-hp);
    }
}
