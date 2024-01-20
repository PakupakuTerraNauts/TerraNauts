using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour // 敵スクリプト　スーパークラス
{
    #region // variables
    [SerializeField] protected float gravity;
    protected float hp = 0.0f;
    protected float ATK_player = 0.0f;

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

    // TriggerEnterから呼ぶ
    protected void recievedDamage(Collider2D collision){
        if(collision.tag == "Sword" && !isDead){
            HP.UpdateHP(ATK_player);
            hp = hp - ATK_player;
        }

        if(hp <= 0.0f){
            dieAnimation();
            isDead = true;
            Destroy(gameObject, 1f);
        }
    }

    protected virtual void Initialize(){
        // コライダをGetComponent
    }

    protected virtual void Sleeping(){
        // モンスターが画面に映っていないときに処理があれば
    }

    protected virtual void Moving(){
        // 基本anim.Play("デフォルアニメーション")
        // 基本rb.velocity = new Vector2(x, y)
        // その他、固有の特殊処理
    }

    protected virtual void dieAnimation(){
        // anim.Play("それぞれのdieアニメーション")
        // プレイヤーにダメージが入るのを防ぐためtagをDeadEnemyに変更
    }

}