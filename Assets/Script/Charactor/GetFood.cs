using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�A�C�e�����Q�b�g�������ɏ���
public class GetFood:MonoBehaviour
{
    private float gravity = 1.0f;
    public string _objName;
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;

    //Start
    void Start()
    {
        this.gameObject.name = _objName;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(sr.isVisible){
            // ���V�s��velocity����
            if(this.gameObject.tag == "item"){
                rb.velocity = new Vector2(0.0f, -gravity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�^�O�� "item" �̃A�C�e��
        if(collision.tag == "Player" || collision.tag == "DeadZone")
        {
            Destroy(this.gameObject);
        }
        if(collision.tag == "ground")
        {
            gravity = 0.0f;
        }
    }
}
