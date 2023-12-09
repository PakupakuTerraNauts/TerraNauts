using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class niwakokepittya : MonoBehaviour
{
    #region //variables
    public float gravity;
    private HPBar HP;
    public GameObject PlayerObject;

    private float ATK_player = 0.0f;
    private float hp = 0.0f;
    private float toriPosition_x = 0.0f;
    private float playerPosition_x = 0.0f;
    private bool isDead = false;
    private bool isLeft = true;
    private Animator anim = null;
    private CapsuleCollider2D capcol = null;
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private string swordTag = "Sword";
    #endregion

    void Start(){
        anim = GetComponent<Animator>();
        capcol = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        HP = GetComponent<HPBar>();

        ATK_player = Player.ATK;
        hp = HPBar.instance.currentHealth;
    }

    void Update(){
        if(sr.isVisible){

            Vector3 toriPosition = this.transform.position;
            Vector3 playerPosition = PlayerObject.transform.position;
            toriPosition_x = toriPosition.x;
            playerPosition_x = playerPosition.x;

            if(!isDead){
                rb.WakeUp();
                anim.Play("tori_pitch");
                rb.velocity = new Vector2(0, -gravity);
            }
        }
        else{
            rb.Sleep();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == swordTag && !isDead){
            HP.UpdateHP(ATK_player);
            hp = hp - ATK_player;
        }
        
        if(hp <= 0.0f){
            anim.Play("tori_die");
            isDead = true;
            capcol.enabled = false;
            Destroy(gameObject, 3f);
        }
    }

    public void DirectJudge(){
        if(playerPosition_x > toriPosition_x && isLeft){
                    transform.localScale = new Vector3(-1, 1, 1);
                    isLeft = false;
                }
                else if(playerPosition_x < toriPosition_x && !isLeft){
                    transform.localScale = new Vector3(1, 1, 1);
                    isLeft = true;
                }
    }
}

