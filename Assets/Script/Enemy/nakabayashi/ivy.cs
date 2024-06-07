using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ivy : MonoBehaviour
{
    [SerializeField] private ivyRange range;
    [SerializeField] private State currentState;

    [SerializeField, Header("Rangeの位置,サイズを変更したらチェック")] private bool ChangeRange = false; 

    private Animator anim = null;

    public enum State{
        piercing,
        undulating,
        bending
    }

    void Start(){
        anim = GetComponent<Animator>();
        range.InitializeCallBack(onIvyAttack);

        if(!ChangeRange){   // 地形の関係でrangeを変更したら,アニメーションごとのデフォ位置への設定はスキップ
            switch(currentState){
                case State.piercing : // デフォルト
                    return;
                case State.undulating :
                    range.InitializeRange();
                    return;
                case State.bending :
                    range.InitializeRange();
                    return;
                default :
                    return;
            }
        }
    }

/// <summary>
/// ツタ攻撃
/// </summary>
    private void onIvyAttack(){
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("nakabayashi_ivy_default")){  // フラグ管理だとすり抜けてデッドロックすることがあるのでアニメーションで判定
            PlayAttackAnimation();
        }
    }

/// <summary>
/// 攻撃アニメーションを選択して再生
/// </summary>
    public void PlayAttackAnimation(){
        switch(currentState){
            case State.piercing :
                anim.Play("nakabayashi_ivy_piercing");
                return;
            case State.undulating :
                anim.Play("nakabayashi_ivy_undulating");
                return;
            case State.bending :
                anim.Play("nakabayashi_ivy_bending");
                return;
            default :
                anim.Play("nakabayashi_ivy_piercing");
                return;
        }
    }

/// <summary>
/// アニメーション終了
/// </summary>
    private void EndAnimation(){
        anim.Play("nakabayashi_ivy_default");
    }
}
