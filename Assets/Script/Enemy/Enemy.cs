using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour // 敵スクリプト　スーパークラス
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

    // TriggerEnterから呼ぶ
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

            // 以下 プレイヤーが跳ね返した攻撃が効くことがある
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
                Instantiate<GameObject>(basicObject, transform.position, Quaternion.identity); // Quater...は回転で今回は無回転
                // 固有の食材ドロップは3割
                if(rand.Random(30.0f)){
                    Instantiate<GameObject>(uniqueObject, transform.position + Vector3.up, Quaternion.identity);
                }
            }
        }
    }

    protected virtual void Initialize(){
        // コライダをGetComponent
    }

    protected virtual void Sleeping(){
        // モンスターが画面に映っていないときに処理があれば
    }

    protected virtual void SleepingF(){
        // FixedUpdate用
    }

    protected virtual void Moving(){
        // 基本anim.Play("デフォルアニメーション")
        // 基本rb.velocity = new Vector2(x, y)
        // その他、固有の特殊処理
    }

    protected virtual void MovingF(){
        // FixedUpdate用
    }

    protected virtual void dieAnimation(){
        // anim.Play("それぞれのdieアニメーション")
        // プレイヤーにダメージが入るのを防ぐためtagをDeadEnemyに変更

    }

}