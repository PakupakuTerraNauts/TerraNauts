using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kinokohoushi : Enemy
{
    #region // variables
    private CapsuleCollider2D capcol = null;
    [SerializeField] private houshi[] hoshi = new houshi[4];
    #endregion
    
    protected override void Initialize(){
        capcol = GetComponent<CapsuleCollider2D>();
        capcol.enabled = true;
    }

    protected override void Moving(){
        anim.Play("kinoko_hoshi");
        rb.velocity = new Vector2(0, -Data.gravity);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        recievedDamage(collision);
    }

    protected override void dieAnimation(){
        anim.Play("kinoko_die");
    }

/// <summary>
/// �A�j���[�V��������Ă� �E�q���΂�
/// </summary>
    private void release(){
        foreach(var h in hoshi){
            h.Hoshi();
        }
    }

/// <summary>
/// �A�j���[�V�������I�������E�q������
/// </summary>
    private void deletehoshi(){
        foreach(var h in hoshi){
            h.HoshiDelete();
        }
    }
}