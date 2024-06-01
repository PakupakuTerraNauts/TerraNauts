using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�A�C�e�����Q�b�g�������ɏ���
public class GetFood:MonoBehaviour
{
    public string _objName;
    private float second = 0.0f;
    private bool blinked = false;
    private bool isGround = false;

    private SpriteRenderer sr = null;
    private Rigidbody2D rb = null;
    public ItemDataBase _itemDataBase;
    private FoodSourceData _foodSourceData;
    public groundCheck ground;

    void Start()
    {
        this.gameObject.name = _objName;
        _foodSourceData = _itemDataBase.ItemSearch(_objName);
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if(_foodSourceData.itemType == ItemType.FOOD)
            sr.sprite = _foodSourceData.icon;
        else
            sr.sprite = Resources.Load<Sprite>("UI/ItemUI/recipe");
    }

    void Update(){
        if(sr.isVisible){
            // �d�͂��|����
            if(!isGround && ground != null){
                isGround = ground.IsGround();
                rb.velocity = new Vector2(0f, -1.0f);
            }
            else{
                rb.velocity = Vector2.zero;
            }
        }

        // �H�ނ��� ���V�s�͎��R���ł��Ȃ�
        // �R���[�`����1�x�����Ă΂Ȃ�
        if(10.0f < second && !blinked && _foodSourceData.itemType == ItemType.FOOD){
            blinked = true;
            Debug.Log(_objName+" is destroied");
            StartCoroutine(ItemBlink());
        }
        second += Time.deltaTime;
    }

    // ��莞�Ԃœ_�ł������㎩�R����
    private IEnumerator ItemBlink(){
        second = 0.0f;

        while(second < 2.5f){
            sr.enabled = !sr.enabled;
            float waitTime = Mathf.Lerp(0.3f, 0.1f, second/2.5f);
            yield return new WaitForSeconds(waitTime);
        }

        yield return null;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "DeadZone")
        {
            Destroy(this.gameObject);
        }
    }
}
