using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tama : MonoBehaviour
{
    #region // variables
    public float speed ;            // 難度調節 可変
    private float distanceCovered = 0.0f;
    private float maxDistance = 10.0f;

    private Animator anim = null;
    private Rigidbody2D rb = null;
    private Vector3 InitialPosition;
    #endregion

    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        InitialPosition = transform.position;
    }

/// <summary>
/// 投げられた球の移動
/// </summary>
/// <param name="isLeft">左右フラグ</param>
    public void TamaPitch(bool isLeft){
        transform.position = new Vector3(InitialPosition.x, InitialPosition.y, InitialPosition.z);      // 位置の初期化
        distanceCovered = 0.0f;
        anim.Play("tori_tama_pitch");

        // 距離で制御
        while(distanceCovered < maxDistance){
            if(isLeft){
                rb.velocity = new Vector2(-speed, 0.0f);
            }
            else{
                rb.velocity = new Vector2(speed, 0.0f);
            }
            distanceCovered += speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Sword" || collision.tag == "ground"){  // 切られる 壁(ground)に当たる で消える
            this.gameObject.SetActive(false);
        }
    }
}
