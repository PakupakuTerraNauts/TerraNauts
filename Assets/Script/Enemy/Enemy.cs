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
    [SerializeField] protected string Name;

    protected bool isDead = false;

    protected GameObject basicObject;
    protected GameObject uniqueObject;

    protected GameObject criticalEffect;

    protected HPBar HP;
    protected Animator anim = null;
    protected Rigidbody2D rb = null;
    protected SpriteRenderer sr = null;

    private enemyData Data;
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
        Spawn();
        criticalEffect = transform.GetChild(0).gameObject;
    }

    // 初期化 最初とゲームオーバーの後に呼ぶ
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
        if(!gameObject.activeSelf){     // gameObjectがfalseなら一度倒されている
            this.gameObject.SetActive(true);
            isDead = false;
            HP.UpdateHP(-hp);   // HPが0なので設定し直し
        }
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
                float atk = Player.ATK + Player.ATKincrement;
                if(rand.Random((Player.CRITRATE + Player.CRITRATEincrement) / 5.0f)){
                    atk += (Player.CRITDMG + Player.CRITDMGincrement) * 2.0f;
                    StartCoroutine(CriticalHit());
                }
                HP.UpdateHP(atk);
                hp = hp - atk;
            }

            // 以下 プレイヤーが跳ね返した敵の攻撃が効くことがある
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
                StartCoroutine(Death());
            }
        }
    }

    private IEnumerator CriticalHit(){
        criticalEffect.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        criticalEffect.SetActive(false);
    }

    // 倒れた時に非表示にする処理
    private IEnumerator Death(){
        Instantiate<GameObject>(basicObject, transform.position, Quaternion.identity); // Quater...は回転で今回は無回転
        // 固有の食材ドロップは3割
        if(rand.Random(30.0f)){
            Instantiate<GameObject>(uniqueObject, transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(3.0f);
        this.gameObject.SetActive(false);
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
    }

    // 倒された状態でセーブされたとき、シングルトンから削除する
    public bool DeleteDead(){
        if(isDead){
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }
}