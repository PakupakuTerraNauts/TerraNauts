using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//アイテムをゲットした時に消す
public class GetFood:MonoBehaviour
{
    public string _objName;
    private float second = 0.0f;
    private bool blinked = false;
    private SpriteRenderer sr = null;

    void Start()
    {
        this.gameObject.name = _objName;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update(){
        if(sr.isVisible){
            // タグがitemのものだけ レシピは自然消滅しない
            // コルーチンは1度しか呼ばない
            if(10.0f < second && !blinked && gameObject.tag == "item"){
                blinked = true;
                StartCoroutine(ItemBlink());
            }
            second += Time.deltaTime;
        }
    }

    private IEnumerator ItemBlink(){
        int i = 5;

        while(0 < i){
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(0.2f);
            i -= 1;
            Debug.Log("item blinking " + i);
        }
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
