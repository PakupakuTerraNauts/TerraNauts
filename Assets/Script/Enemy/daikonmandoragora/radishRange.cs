using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radishRange : MonoBehaviour
{
    #region // variables
    private BoxCollider2D boxcol = null;

    public delegate void radishStepped();
    private radishStepped radishStappedCallBack;
    #endregion

    void Start(){
        boxcol = GetComponent<BoxCollider2D>();
    }

/// <summary>
/// onRadisshStapped コールバックをセット
/// </summary>
/// <param name="onRadishStapped">プレイヤーが近づいたとき 大根が地面から出現</param>
    public void InitializeCallBack(radishStepped onRadishStapped){
        radishStappedCallBack = onRadishStapped;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player")   // プレイヤーと接触したらコールバックする
            radishStappedCallBack();
    }
}
