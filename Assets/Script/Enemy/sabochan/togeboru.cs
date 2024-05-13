using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class togeboru : MonoBehaviour
{
    #region // variables
    public float gravity;
    public float speed;

    private bool isLeft;
    public sabochan Saboten;

    private Rigidbody2D rb = null;
    private Animator anim = null;
    private Vector3 InitialPosition;
    #endregion

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        InitialPosition = transform.localPosition;
        isLeft = Saboten.isLeft;
        if(!isLeft)
            InitialPosition.x = -InitialPosition.x;
        gameObject.SetActive(false);
    }

    public bool ThrowBall(){
        transform.rotation = Quaternion.identity;
        if(!isLeft){
            InitialPosition.x = -InitialPosition.x;
        }
        transform.localPosition = InitialPosition;

        if(isLeft){
            rb.velocity = new Vector2(-speed, -gravity);
        }
        else{
            rb.velocity = new Vector2(speed, -gravity);
        }

        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Sword"){
            anim.Play("saboten_togeboru_attacked");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.tag == "Player"){
            gameObject.SetActive(false);
        }
    }
}
