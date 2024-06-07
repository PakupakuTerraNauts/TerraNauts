using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class houshi : MonoBehaviour // �N���X���̓��[�}��
{
    #region // variables
    private float distanceCovered = 0.0f;
    private float hoshiInitialPosX = 0.0f;

    private bool isLeft = false;

    private Animator anim = null;
    private Rigidbody2D rb = null;
    #endregion

    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        hoshiInitialPosX = transform.position.x;
    }

/// <summary>
/// �E�q�����V����
/// </summary>
    public void Hoshi(){
        distanceCovered = 0.0f;
        float maxDistance = UnityEngine.Random.Range(3, 9); // ���� : �򋗗�
        bool moveLeft = UnityEngine.Random.value <= 0.5;    // ���� : ���E
        float speed = UnityEngine.Random.value;             // ���� : ���x
        anim.Play("kinoko_hoshigrowing");

        if(moveLeft){
            isLeft = true;
            transform.position = new Vector3(hoshiInitialPosX - 1.5f, transform.position.y, 0.0f);
        }
        else{
            isLeft = false;
            transform.position = new Vector3(hoshiInitialPosX + 1.5f, transform.position.y, 0.0f);
        }

        // �򋗗� �܂� ���x �ŕ��V����
        while(distanceCovered < maxDistance){
            if(isLeft){
                rb.velocity = new Vector2(-speed, 0.0f);
            }
            else{
                rb.velocity = new Vector2(speed, 0.0f);
            }
            distanceCovered += Mathf.Abs(speed);
        }
    }

/// <summary>
/// �E�q������
/// </summary>
    public void HoshiDelete(){
        anim.Play("kinoko_hoshi_default");
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Sword"){   // �؂�ꂽ��E�q������
            HoshiDelete();
        }
    }
}
