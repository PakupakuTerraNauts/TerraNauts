using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radisshRange : MonoBehaviour
{
    #region // variables
    private BoxCollider2D boxcol = null;

    public delegate void radisshStepped();
    private radisshStepped radisshStappedCallBack;
    #endregion

    void Start(){
        boxcol = GetComponent<BoxCollider2D>();
    }

/// <summary>
/// onRadisshStapped コールバックをセット
/// </summary>
/// <param name="onRadisshStapped">プレイヤーが近づいたとき 大根が地面から出現</param>
    public void InitializeCallBack(radisshStepped onRadisshStapped){
        radisshStappedCallBack = onRadisshStapped;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player")   // プレイヤーと接触したらコールバックする
            radisshStappedCallBack();
    }
}
