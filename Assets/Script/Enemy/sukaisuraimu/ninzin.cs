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
    #endregion

    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        exp = transform.Find("sukai_ninzin_exp").gameObject;
    }

    void Update(){
        if(isFall && !isHit){
            rb.velocity = new Vector2(0.0f, -gravity);
        }
    }

    public void Reload(Vector3 nowPosition){
        if(!isFall){
            transform.position = new Vector3(nowPosition.x, nowPosition.y, nowPosition.z);
        }
    }

    public void Falling(){
        isFall = true;
        isHit = false;
        rb.WakeUp();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "ground" || collision.tag == "Sword"){
            isHit = true;
            StartCoroutine("NinzinExplosion");
            rb.Sleep();
        }
    }

    public bool fallTF(){
        return isFall;
    }

    private IEnumerator NinzinExplosion(){
        exp.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        exp.SetActive(false);
        isFall = false;
    }
}
