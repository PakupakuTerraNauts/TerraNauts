using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnteredBossRoom : MonoBehaviour
{
    public UnityEvent onEnteredBossRoom = new UnityEvent();
    public EntranceDoor entrance;

    private BoxCollider2D boxcol;
    private bool isEntered = false;

    void Start(){
        boxcol = GetComponent<BoxCollider2D>();
    }

    // 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!isEntered && collision.tag == "Player"){
            isEntered = true;
            Debug.Log("入った");

            // BossCamera1のフェーズ２遷移フラグ
            entrance.CloseDoor();

            // １層ボスのHPバー操作
            onEnteredBossRoom.Invoke(); // コールバック
            entrance.JudgeDestory();
            boxcol.enabled = false;
            
        }
    }
}
