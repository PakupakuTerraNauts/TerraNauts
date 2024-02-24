using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turara : MonoBehaviour
{
    #region // variables
    public float gravity;
    private bool isFall = false;
    private bool isBreak = false;

    private Vector3 StartPos;
    public neibysuraimu neiby;
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    #endregion

    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        StartPos = transform.position;
        this.gameObject.SetActive(false);
    }

    void Update(){
        if(isFall && !isBreak){
            anim.Play("neiby_turara_fall");
            rb.velocity = new Vector2(0.0f, -gravity);
        }else{
            rb.velocity = new Vector2(0.0f, 0.0f);
        }
    }

    public void Turara(){
        transform.position = StartPos;
        this.gameObject.SetActive(true);
        StartCoroutine("GenerateTurara");
        rb.WakeUp();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "ground" || collision.tag == "Player"){
            StartCoroutine("BreakTurara");
        }
    }

    private IEnumerator GenerateTurara(){
        anim.Play("neiby_turara_generate");
        yield return new WaitForSeconds(0.5f);
        isFall = true;
    }
    private IEnumerator BreakTurara(){
        isBreak = true;
        anim.Play("neiby_turara_break");
        yield return new WaitForSeconds(2.0f);
        this.gameObject.SetActive(false);
        isFall = false;
        isBreak = false;
    }

    public bool FallTF(){
        return isFall;
    }
}