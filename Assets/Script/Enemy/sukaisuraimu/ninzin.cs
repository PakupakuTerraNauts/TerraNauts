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
        exp = transform.GetChild(0).gameObject; // 爆発の方
    }

    void Update(){
        if(isFall && !isHit){
            rb.velocity = new Vector2(0.0f, -gravity);
        }
    }

/// <summary>
/// onNinzinFallenCheck コールバックをセット
/// </summary>
/// <param name="onNinzinFallenCheck">人参の爆発を取得</param>
    public void InitializeCallBack(ninzinFallenCheck onNinzinFallenCheck){
        ninzinFallenCheckCallBack = onNinzinFallenCheck;
    }

/// <summary>
/// スライムに追随する
/// </summary>
/// <param name="nowPosition">スカイスライムの位置</param>
    public void Reload(Vector3 nowPosition){
        if(!isFall){
            transform.position = new Vector3(nowPosition.x, nowPosition.y, nowPosition.z);
        }
    }

/// <summary>
/// 落下スタート
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

    // 爆発エフェクトを表示
    private IEnumerator NinzinExplosion(){
        exp.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        exp.SetActive(false);
        this.gameObject.SetActive(false);

        isFall = false;
        ninzinFallenCheckCallBack();
    }
}
